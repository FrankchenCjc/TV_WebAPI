
using TV_WebAPI.ApiClass;
namespace TV_WebAPI.ApiClass.Config
{
    /// <summary>
    /// 设置自动转码总开关
    /// ApiData string
    /// </summary>
    public class Config_Transcod : PostAPI
    {
        public new Dictionary<string, string> Selfval = new Dictionary<string, string>
        {{"state","false"}};
    }

    /// <summary>
    /// 根据文件大小自动切片
    /// ApiData string
    /// </summary>
    public class Config_FileSplit : PostAPI
    {
        public new Dictionary<string, string> Selfval = new Dictionary<string, string>
        {{"size","10240000"}};

    }

    /// <summary>
    /// 弹幕录制总共开关(包括礼物、舰队、SC)
    /// ApiData string
    /// </summary>
    public class Config_DanmuRec : PostAPI
    {
        public new Dictionary<string, string> Selfval = new Dictionary<string, string>
        {{"state","false"}};
    }

    /// <summary>
    /// 导入关注列表中的V(请确认已经扫码登陆)
    /// ApiData List<Config_GetFollow.Follow>
    /// </summary>
    public class Config_GetFollow : PostAPI
    {
        public class Follow
        {
            public long mid;
            public int roomid;
            public string name = string.Empty;
        }
    }
}
