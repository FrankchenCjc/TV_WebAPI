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
        /// Post方法
        /// </summary>
        /// <typeparam name="U">ApiData 类型，详见各个API说明</typeparam>
        /// <typeparam name="T">Api类型</typeparam>
        /// <param name="req">带有参数的API申请数据</param>
        /// <returns></returns>
        public async Task<Pack<U>>? PostAsync<U, T>(T req)
        where T : PostAPI
        {
            //创建需要的类
            //JsonSerializerOptions opz = new(JsonSerializerDefaults.Web);
            SHA1 sha = SHA1.Create();
            HttpClient client = new();
            client.BaseAddress = new Uri(ServerURL);
            //计算SIG
            var valuePairs = new Dictionary<string, string>{
                    { "accesskeyid", AccessKeyID },
                    { "accesskeysecret", AccessKeySecret },
                    { "cmd", req.GetType().ToString().Split('.').Last().ToLower()},
                    { "time", (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, 0)).TotalSeconds.ToString()},
                };
            var Sig = string.Join(
                string.Empty,
                valuePairs.Select((P) => $"{P.Key}={P.Value};"));
            Sig = string.Join(
                string.Empty,
                sha.ComputeHash(Encoding.UTF8.GetBytes(Sig))
                   .Select((B) => string.Format("{0:x2}", B))
                ).ToUpper();
            //构造from表
            valuePairs.Remove("accesskeysecret");
            valuePairs.Add("sig", Sig);
            req.Selfval.ToList().ForEach((i) => valuePairs.Append(i));
            FormUrlEncodedContent from = new(valuePairs);
            //请求并逆序列化
            var pack = JsonSerializer.Deserialize<Pack<U>>(
                await (await client.PostAsync(valuePairs.GetValueOrDefault("cmd"), from))
                    .Content
                    .ReadAsStringAsync());
            //回传结果
            return pack;
        }

        public string AccessKeyID { get; private set; } = string.Empty;
        public string AccessKeySecret { get; private set; } = string.Empty;
        /// <summary>
        /// 这是服务器的响应地址
        /// </summary>
        public string ServerURL { get; private set; } = string.Empty;
        public void ChangeServer(string serverurl, string aid, string asecret)
        {
            AccessKeyID = aid;
            AccessKeySecret = asecret;
            ServerURL = serverurl;
        }
        /// <summary>
        /// 使用url，AccessKeyID，和ServerBaseURL注册到一个服务器
        /// </summary>
        /// <param name="serverurl">这个api不包含/api目录，请包含api目录避免问题</param>
        /// <param name="aid">AccessKeyID</param>
        /// <param name="asecret">AccessKeySecret</param>
        public Server(string serverurl, string aid, string asecret)
        {
            ChangeServer(serverurl, aid, asecret);
        }
    }
}