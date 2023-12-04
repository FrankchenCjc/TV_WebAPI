using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using DDTVWebAPI;

namespace DDTVWebAPI
{
    public partial class DDTVServer
    {
        public async Task<Pack<T?>> PostAsync<T>(string ApiCmd, Dictionary<string, string>? Selfval)
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
                    { "cmd", ApiCmd},
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
            Selfval?.ToList().ForEach((i) => valuePairs.Add(i.Key, i.Value));
            FormUrlEncodedContent from = new(valuePairs);
            //请求并逆序列化
            var pack = JsonSerializer.Deserialize<Pack<T?>>(
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
        public DDTVServer(string serverurl, string aid, string asecret)
        {
            ChangeServer(serverurl, aid, asecret);
        }
    }
}