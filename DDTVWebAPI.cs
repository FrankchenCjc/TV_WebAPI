using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using DDTVWebAPI;
using Microsoft.Extensions.Logging.Abstractions;

namespace DDTVWebAPI
{
    public partial class DDTVServer
    {
        readonly SHA1 _sha = SHA1.Create();
        readonly HttpClient _client = new();
        public async Task<Pack<T>> PostAsync<T>(string ApiCmd, Dictionary<string, string>? Selfval)
        {
            //计算SIG
            var valuePairs = new Dictionary<string, string>{
                    { "accesskeyid", AccessKeyID },
                    { "accesskeysecret", AccessKeySecret },
                    { "cmd", ApiCmd.ToLower()},
                    { "time", (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, 0)).TotalSeconds.ToString()},
                };
            var sig = string.Join(
                string.Empty,
                valuePairs.Select((p) => $"{p.Key}={p.Value};"));
            sig = string.Join(
                string.Empty,
                _sha.ComputeHash(Encoding.UTF8.GetBytes(sig))
                   .Select((b) => string.Format("{0:x2}", b))
                ).ToUpper();
            //构造from表
            valuePairs.Remove("accesskeysecret");
            valuePairs.Add("sig", sig);
            Selfval?.ToList().ForEach((i) => valuePairs.Add(i.Key, i.Value));
            FormUrlEncodedContent from = new(valuePairs);
            //请求并逆序列化
            var jss = await (await _client.PostAsync(valuePairs.GetValueOrDefault("cmd"), from))
                    .Content
                    .ReadAsStringAsync();
            var pack = JsonSerializer.Deserialize<Pack<T>>(jss) ?? throw new Exception("空结果");
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
        public DDTVServer(string serverurl, string aid, string asecret)
        {
            ChangeServer(serverurl, aid, asecret);
            _client.BaseAddress = new Uri(ServerURL);
        }
    }
}