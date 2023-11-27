using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using TV_WebAPI.ApiClass;

namespace TV_WebAPI
{
    /// <summary>
    /// 这是一个DDTV_WEB_SERVER客户端的完成
    /// </summary>
    public class Server
    {


        /// <summary>
        /// 使用向DDTV_WEB_SERVER使用post的异步方法
        /// </summary>
        /// <param name="keys">提供CMD和必要的参数，sig计算和api认证不包含在内</param>
        /// <returns>
        /// 参见API部分
        /// </returns>
        public async Task<Pack<U>>? PostAsync<U, T>(T req)
        where T : PostAPI
        where U : new()
        {
            //JsonSerializerOptions opz = new(JsonSerializerDefaults.Web);
            SHA1 sha = SHA1.Create();
            HttpClient client = new();
            client.BaseAddress = new Uri(ServerURL);

            var valuePairs = new Dictionary<string, string>{
                    { "accesskeyid", AccessKeyID },
                    { "accesskeysecret", AccessKeySecret },
                    { "cmd", req.GetType().ToString().Split('.').Last().ToLower()},
                    { "time", (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, 0)).TotalSeconds.ToString()},
                };
            var Sig = string.Join(
                string.Empty,
                sha.ComputeHash(Encoding.UTF8.GetBytes(
                        string.Join(string.Empty,
                            valuePairs.Select((P) => $"{P.Key}={P.Value};"))))
                   .Select((B) => string.Format("{0:x2}", B))
                ).ToUpper();

            valuePairs.Remove("accesskeysecret");
            valuePairs.Add("sig", Sig);

            req.Selfval.ToList().ForEach((i) => valuePairs.Append(i));
            FormUrlEncodedContent from = new(valuePairs);

            var pack = JsonSerializer.Deserialize<Pack<U>>(
                await (await client.PostAsync(valuePairs.GetValueOrDefault("cmd"), from))
                    .Content
                    .ReadAsStringAsync());

            return pack;
        }

        #region sever
        #region serverconfig
        string accessKeyID = string.Empty;
        string accessKeySecret = string.Empty;
        string serverURL = string.Empty;


        public string AccessKeyID { get { return accessKeyID; } }
        public string AccessKeySecret { get { return accessKeySecret; } }
        /// <summary>
        /// 这是服务器的响应地址
        /// </summary>
        public string ServerURL { get { return serverURL; } }
        #endregion
        public void Renew(string serverurl, string aid, string asecret)
        {
            serverURL = serverurl;
            accessKeyID = aid;
            accessKeySecret = asecret;
        }
        /// <summary>
        /// 使用url，AccessKeyID，和ServerBaseURL注册到一个服务器
        /// </summary>
        /// <param name="serverurl">这个api不包含/api目录，请包含api目录避免问题</param>
        /// <param name="aid">AccessKeyID</param>
        /// <param name="asecret">AccessKeySecret</param>
        public Server(string serverurl, string aid, string asecret)
        {
            Renew(serverurl, aid, asecret);
        }

        [System.Serializable]
        public class TVWebClientException : System.Exception
        {
            public TVWebClientException() { }
            public TVWebClientException(string message) : base(message) { }
            public TVWebClientException(string message, System.Exception inner) : base(message, inner) { }
            protected TVWebClientException(
                System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }

        #endregion


    }
}

#region apibase
namespace TV_WebAPI.ApiClass
{

    /// <summary>
    /// DDTV的传入状态码，参考 https://ddtv.pro/API/
    /// 参考日期2023/4/12
    /// </summary>
    public enum Code
    {
        /// <summary>
        /// 该状态码表示尚未被使用或者赋值，本程序自定
        /// </summary>
        NotUsed = 1,

        Success = 0,
        UIDNotExist = -1,

        WebLoginFail = 6000,
        WebSigFail = 6001,

        ApiSigFail = 6002,

        OpFail = 7000
    }

    /// <summary>
    /// DDTV传入的标准格式
    /// </summary>
    /// 
    [Serializable]
    public class Pack<TData>
    where TData : new()
    {
        public int code { get; set; } = 0;
        public string cmd { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;
        public TData? data { get; set; }
    }

    [Serializable]
    public abstract class PostAPI
    {
        public virtual Dictionary<string, string> Selfval { get; set; } = new();
        public abstract class ApiData { }
    }


}
#endregion
