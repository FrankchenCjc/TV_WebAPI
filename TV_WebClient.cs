using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text.Json;
using TV_WebAPI.EncryptionModule;
using TV_WebAPI.WebAPI;

namespace TV_WebAPI
{
    /// <summary>
    /// 这是一个DDTV_WEB_SERVER客户端的完成
    /// </summary>
    public class TVWebC
    {
        HttpClient client = new();
        /// <summary>
        /// 使用向DDTV_WEB_SERVER使用post的异步方法
        /// </summary>
        /// <param name="keys">提供CMD和必要的参数，sig计算和api认证不包含在内</param>
        /// <returns>
        /// 参见API部分
        /// </returns>
        public async Task<Respon> PostAsync<Respon>(Dictionary<string, dynamic> keys)
        {
            string Time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0, 0)).TotalSeconds.ToString();
            string Sig = string.Empty;
            var valuePairs = new Dictionary<string, string>{
                    { "accesskeyid", accessKeyID },
                    { "accesskeysecret", accessKeySecret },
                    { "cmd", keys.GetValueOrDefault("cmd")},
                    { "time", Time },
                };
            foreach (var item in valuePairs)
                Sig += $"{item.Key}={item.Value};";
            Sig = Encryption.SHA1_Encrypt(Sig);

            valuePairs.Remove("accesskeysecret");
            valuePairs.Remove("cmd");
            valuePairs.Add("sig", Sig);
            foreach (var i in keys)
                valuePairs.Add(i.Key, i.Value);

            FormUrlEncodedContent from = new(valuePairs);

            JsonSerializerOptions opz = new(JsonSerializerDefaults.Web);

            client.BaseAddress = new Uri(serverURL);

            var js =
                await (
                await client.PostAsync(
                    valuePairs.GetValueOrDefault("cmd"),
                    from)
                ).Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<Respon>(js, opz);
            if (obj == null)
                throw new Exception("");
            return obj;
        }

        /// <summary>
        /// 使用向DDTV_WEB_SERVER使用get的异步方法
        /// </summary>
        /// <param name="keys">提供CMD和必要的参数，sig计算和api认证不包含在内</param>
        /// <returns>
        /// 将使用Dictionary返回相应 注意使用await
        /// </returns>
        //public async Task<byte[]> GetAsync<T>(Dictionary<string, dynamic> keys) { throw new NotImplementedException("方法在该版本中尚未实现\n作者正在掉头发中。。。。"); }

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
        public TVWebC(string serverurl, string aid, string asecret)
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
