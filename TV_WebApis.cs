using TV_WebAPI;
namespace TV_WebAPI.ApiClass
{

    #region public pack

    /// <summary>s
    /// 表征使用Post还是get
    /// </summary>
    public class ApiAct
    {
        public delegate Task<T> POST<T>(Dictionary<string, string> keys, Server server);
        public delegate Task<byte[]> GET(Dictionary<string, string> keys, Server server);
    };

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
    [System.Serializable]
    public class Pack
    {
        public Code code { get; set; } = Code.NotUsed;
        public string cmd { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;
    }

    #endregion

    #region System

    /// <summary>
    /// 获取系统硬件资源使用情况
    /// </summary>
    public class System_Resource
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new()
        {
            {"cmd","system_resource"}
        };

        public class Respon : Pack
        {
            public Data data { get; set; } = new();
        }
        public class Data
        {
            /// <summary>
            /// 平台
            /// </summary>
            public string? Platform { set; get; }
            /// <summary>
            /// CPU使用率
            /// </summary>
            public double CPU_usage { set; get; }
            /// <summary>
            /// 内存
            /// </summary>
            public MemInfo? Memory { set; get; }
            /// <summary>
            /// 硬盘信息
            /// </summary>
            public List<HDDInfo>? HDDInfo { set; get; }
        }
    }

    public class MemInfo
    {
        /// <summary>
        /// 总计内存大小
        /// </summary>
        public long Total { get; set; }
        /// <summary>
        /// 可用内存大小
        /// </summary>
        public long Available { get; set; }
    }
    public class HDDInfo
    {
        /// <summary>
        /// 注册路径
        /// </summary>
        public string FileSystem { set; get; } = string.Empty;
        /// <summary>
        /// 硬盘大小
        /// </summary>
        public string Size { get; set; } = string.Empty;

        /// <summary>
        /// 已使用大小
        /// </summary>
        public string Used { get; set; } = string.Empty;

        /// <summary>
        /// 可用大小
        /// </summary>
        public string Avail { get; set; } = string.Empty;

        /// <summary>
        /// 使用率
        /// </summary>
        public string Usage { get; set; } = string.Empty;
        /// <summary>
        /// 挂载路径
        /// </summary>
        public string MountPath { set; get; } = string.Empty;
    }


    /// <summary>
    /// 获取系统运行情况
    /// </summary>
    public class System_info
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new()
        {
            {"cmd","system_info"}
        };

        public class Respon : Pack
        {
            public Data data { get; set; } = new();
        }

        public class Data
        {
            /// <summary>
            /// 当前DDTV版本号
            /// </summary>
            public string? DDTVCore_Ver { get; set; }
            /// <summary>
            /// 监控房间数量
            /// </summary>
            public int Room_Quantity { get; set; }
            /// <summary>
            /// 设置的服务器名称
            /// </summary>
            public string? ServerName { get; set; }
            /// <summary>
            /// 服务器的唯一资源编号
            /// </summary>
            public string ServerAID { get; set; } = string.Empty;
            /// <summary>
            /// 操作系统相关信息
            /// </summary>
            public OS_Info os_Info { get; set; } = new();
            /// <summary>
            /// 下载任务基础信息
            /// </summary>
            public Download_Info download_Info { get; set; } = new();
        }
    }

    public class OS_Info
    {
        /// <summary>
        /// 系统版本
        /// </summary>
        public string OS_Ver { get; set; } = string.Empty;
        /// <summary>
        /// 系统类型
        /// </summary>
        public string OS_Tpye { get; set; } = string.Empty;
        /// <summary>
        /// 使用内存量，单位bit
        /// </summary>
        public long Memory_Usage { get; set; }
        /// <summary>
        /// 运行时版本
        /// </summary>
        public string Runtime_Ver { get; set; } = string.Empty;
        /// <summary>
        /// 是否在交互模式下
        /// </summary>
        public bool UserInteractive { get; set; }
        /// <summary>
        /// 关联的用户
        /// </summary>
        public string Associated_Users { get; set; } = string.Empty;
        /// <summary>
        /// 工作目录
        /// </summary>
        public string Current_Directory { get; set; } = string.Empty;
        /// <summary>
        /// Core程序核心框架版本
        /// </summary>
        public string AppCore_Ver { set; get; } = string.Empty;
        /// <summary>
        /// Web程序核心框架版本
        /// </summary>
        public string WebCore_Ver { set; get; } = string.Empty;
    }
    public class Download_Info
    {
        /// <summary>
        /// 下载中的任务数
        /// </summary>
        public int Downloading { get; set; }
        /// <summary>
        /// 下载结束的任务数
        /// </summary>
        public int Completed_Downloads { get; set; }
    }


    /// <summary>
    /// 获取系统配置文件信息
    /// </summary>
    public class System_Config
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new()
        {
            {"cmd","system_config"}
        };
        public class Respon : Pack
        {
            public List<Config> data { get; set; } = new();
        }

        public class Config
        {
            /// <summary>
            /// 配置键
            /// </summary>
            public Key Key { set; get; }
            /// <summary>
            /// 配置键名称
            /// </summary>
            public string KeyName { set; get; } = string.Empty;
            /// <summary>
            /// 配置分组
            /// </summary>
            public Group Group { set; get; } = Group.Default;
            /// <summary>
            /// 配置值
            /// </summary>
            public string Value { set; get; } = string.Empty;
            /// <summary>
            /// 是否有效
            /// </summary>
            public bool Enabled { set; get; } = false;

        }
        /// <summary>
        /// 配置分组(每个值对应的组是固定的，请勿随意填写)
        /// </summary>
        public enum Group
        {
            /// <summary>
            /// 缺省配置组(按道理应该给每个配置都设置组，不应该在缺省组里)
            /// </summary>
            Default,
            /// <summary>
            /// DDTV_Core运行相关的配置
            /// </summary>
            Core,
            /// <summary>
            /// 下载系统运行相关的配置
            /// </summary>
            Download,
            /// <summary>
            /// WEBAPI相关的配置
            /// </summary>
            WEB_API,
            /// <summary>
            /// 播放器相关设置
            /// </summary>
            Play,
            /// <summary>
            /// GUI相关设置
            /// </summary>
            GUI,
        }
        /// <summary>
        /// 配置键
        /// </summary>
        public enum Key
        {
            /// <summary>
            /// 房间配置文件路径 (应该是一个绝对\相对路径文件地址)
            /// 组：Core      默认值：./RoomListConfig.json
            /// </summary>
            RoomListConfig,
            /// <summary>
            /// 默认下载总文件夹路径 (应该是一个绝对\相对路径目录)
            /// 组：Download  默认值：./Rec/
            /// </summary>
            DownloadPath,
            /// <summary>
            /// 临时文件存放文件夹路径 (应该是一个绝对\相对路径文件地址)
            /// 组：Download  默认值：./tmp/
            /// </summary>
            TmpPath,
            /// <summary>
            /// 默认下载文件夹名字格式 (应该为关键字组合，如:{KEY}_{KEY})
            /// 组：Download  默认值：{ROOMID}_{NAME}        可选值：ROOMID|NAME|DATE|TIME|TITLE|R
            /// </summary>
            DownloadDirectoryName,
            /// <summary>
            /// 默认下载文件名格式 (应该为关键字组合，如:{KEY}_{KEY})
            /// 组：Download  默认值：{DATE}_{TIME}_{TITLE}  可选值：ROOMID|NAME|DATE|TIME|TITLE|R
            /// </summary>
            DownloadFileName,
            /// <summary>
            /// 转码默认参数 (应该是带{After}{Before}的ffmpeg参数字符串，如:-i {Before} -vcodec copy -acodec copy {After})
            /// 组：Core      默认值：-i {Before} -vcodec copy -acodec copy {After}
            /// </summary>
            TranscodParmetrs,
            /// <summary>
            /// 自动转码 (自动转码的使能配置，为布尔值false或ture)
            /// 组：Core      默认值：false
            /// </summary>
            IsAutoTranscod,
            /// <summary>
            /// 是否启用WEB_API加密证书 (应该为布尔值)
            /// 组：WEB_API   默认值：false
            /// </summary>
            WEB_API_SSL,
            /// <summary>
            /// WEB_API启用HTTPS后调用的pfx证书文件路径 (应该是一个绝对\相对路径文件地址)
            /// 组：WEB_API   默认值：
            /// </summary>
            pfxFileName,
            /// <summary>
            /// WEB_API启用后HTTPS调用的pfx证书秘钥文件路径 (应该是一个绝对\相对路径文件地址)
            /// 组：WEB_API   默认值：
            /// </summary>
            pfxPasswordFileName,
            /// <summary>
            /// 播放器默认音量 (应该是一个uint值)
            /// 组：Play      默认值：50      可选值：0-100
            /// </summary>
            DefaultVolume,
            /// <summary>
            /// GUI首次启动标志位 (应该是一个布尔值第一次启动为真)
            /// 组：Core      默认值：true
            /// </summary>
            GUI_FirstStart,
            /// <summary>
            /// WEB首次启动标志位 (应该是一个布尔值第一次启动为真)
            /// 组：Core      默认值：true
            /// </summary>
            WEB_FirstStart,
            /// <summary>
            /// 录制分辨率 (应该为有限的int值)
            /// 组：Download  默认值：10000  可选值：流畅:80  高清:150  超清:250  蓝光:400  原画:10000
            /// </summary>
            RecQuality,
            /// <summary>
            /// 默认在线观看的分辨率 (应该为有限的int值)
            /// 组：Play      默认值：250    可选值：流畅:80  高清:150  超清:250  蓝光:400  原画:10000
            /// </summary>
            PlayQuality,
            /// <summary>
            /// 全局弹幕录制开关 (布尔值，每个房间自己在房间配置列表单独设置，这个是是否启用弹幕录制功能的总共开关)
            /// 组：Download  默认值：true
            /// </summary>
            IsRecDanmu,
            /// <summary>
            /// 全局礼物录制开关 (布尔值)
            /// 组：Download  默认值：true
            /// </summary>
            IsRecGift,
            /// <summary>
            /// 全局上舰录制开关 (布尔值)
            /// 组：Download  默认值：true
            /// </summary>
            IsRecGuard,
            /// <summary>
            /// 全局SC录制开关 (布尔值)
            /// 组：Download  默认值：true
            /// </summary>
            IsRecSC,
            /// <summary>
            /// 全局FLV文件按大小切分开关 (布尔值)
            /// 组：Download  默认值：false
            /// </summary>
            IsFlvSplit,
            /// <summary>
            /// 当IsFlvSplit为真时使能，FLV文件切分的大小 (应该为long值，切割值应该以byte计算)
            /// 组：Download  默认值：1073741824
            /// </summary>
            FlvSplitSize,
            /// <summary>
            /// WEB登陆使用的用户名 (string)
            /// 组：WEB_API   默认值：ami
            /// </summary>
            WebUserName,
            /// <summary>
            /// WEB登陆使用的密码 (string)
            /// 组：WEB_API   默认值：ddtv
            /// </summary>
            WebPassword,
            /// <summary>
            /// WEBAPI使用的KeyId (string)
            /// 组：WEB_API   默认值：(随机字符串)
            /// </summary>
            AccessKeyId,
            /// <summary>
            /// WEBAPI使用的KeySecret (string)
            /// 组：WEB_API   默认值：(随机字符串)
            /// </summary>
            AccessKeySecret,
            /// <summary>
            /// 用于标记服务器资源ID编号 (string)
            /// 组：WEB_API   默认值：(随机字符串)
            /// </summary>
            ServerAID,
            /// <summary>
            /// 用于标记服务器名称 (string)
            /// 组：WEB_API   默认值：DDTV_Server
            /// </summary>
            ServerName,
            /// <summary>
            /// 客户端唯一标识 (string)
            /// 组：Core      默认值：(随机字符串)
            /// </summary>
            ClientAID,
            /// <summary>
            /// 是否需要初始化
            /// 组：  默认值：
            /// </summary>
            InitializationStatus,
            /// <summary>
            /// DDTVGUI缩放是否隐藏到托盘
            /// 组：GUI       默认值：false
            /// </summary>
            HideIconState,
            /// <summary>
            /// DDTV_WEB跨域设置路径（应为前端网址，必须带协议和端口号，如：http://127.0.0.1:5500）
            /// 组：WEB_API   默认值：*
            /// </summary>
            AccessControlAllowOrigin,
            /// <summary>
            /// DDTV_WEB的Credentials设置 (布尔值)
            /// 组：WEB_API   默认值：true
            /// </summary>
            AccessControlAllowCredentials,

        }
    }

    public class LogClass
    {
        public enum LogType
        {
            /// <summary>
            /// 会造成整个DDTV无法运行的严重错误
            /// </summary>
            Error = 10,
            /// <summary>
            /// 虽然现在还没发生问题，但是不管这个问题之后肯定会导致严重错误
            /// </summary>
            Error_IsAboutToHappen = 11,
            /// <summary>
            /// 会造成错误，但是不影响运行的警告
            /// </summary>
            Warn = 20,
            /// <summary>
            /// 房间巡逻系统错误日志
            /// </summary>
            Warn_RoomPatrol = 23,
            /// <summary>
            /// 系统一般消息
            /// </summary>
            Info = 30,
            /// <summary>
            /// 转码消息
            /// </summary>
            Info_Transcod = 31,
            /// <summary>
            /// API消息
            /// </summary>
            Info_API = 32,
            /// <summary>
            /// IP协议版本消息
            /// </summary>
            Info_IP_Ver = 33,
            /// <summary>
            /// 调试信息
            /// </summary>
            Debug = 40,
            /// <summary>
            /// 调试信息
            /// </summary>
            Debug_Request = 41,
            /// <summary>
            /// DDcenter请求
            /// </summary>
            Debug_DDcenter = 42,
            /// <summary>
            /// 调试信息
            /// </summary>
            Debug_Request_Error = 43,
            /// <summary>
            /// 一些追踪数据
            /// </summary>
            Trace = 50,
            Trace_Web = 51,
            TmpInfo = 99,
            /// <summary>
            /// 打开所有日志
            /// </summary>
            All = int.MaxValue,
        }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string? Message { set; get; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public LogType Type { set; get; }
        /// <summary>
        /// 系统时间
        /// </summary>
        public DateTime Time { set; get; }
        /// <summary>
        /// 软件的运行时间
        /// </summary>
        public long RunningTime { set; get; }
        /// <summary>
        /// 来源
        /// </summary>
        public string? Source { set; get; }
        /// <summary>
        /// 时候是需要写入txt记录的错误
        /// </summary>
        public bool IsError { set; get; }
        /// <summary>
        /// IsError为真时有效，记录错误详细信息
        /// </summary>
        public Exception exception { set; get; } = new();
        /// <summary>
        /// 是否应该打印到终端
        /// </summary>
        public bool IsDisplay { set; get; } = false;
    }

    /// <summary>
    /// 获取历史日志
    /// </summary>
    public class System_Log
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new
        Dictionary<string, string>
        {
            {"cmd","system_log"},
            {"page","0"},
            {"Quantity","0"}
        };
        public class Respon : Pack
        {
            Data data { get; set; } = new();
        }

        public class Data
        {
            /// <summary>
            /// 总日志条数
            /// </summary>
            public long TotalLogs { get; set; }
            /// <summary>
            /// 查询量的日志信息
            /// </summary>
            public List<LogClass> Logs { get; set; } = new List<LogClass>();
        }
    }

    /// <summary>
    /// 获取最新日志
    /// </summary>
    public class System_LatestLog
    {

        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new Dictionary<string, string>
        {
            {"cmd","system_latestlog"},
            {"Quantity","0"}
        };

        public class Respon : Pack
        {
            public List<LogClass> Data
            { get { return data; } set { data = value; } }
            List<LogClass> data = new();
        }
    }

    /// <summary>
    /// 返回一个可以自行设定的初始化状态值(用于前端自行判断)
    /// </summary>
    public class System_QueryWebFirstStart
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new()
        {
            {"cmd","system_querywebfirststart"},
        };
        public class Respon : Pack
        {
            public bool data { get; set; } = false;
        }
    }

    /// <summary>
    /// 设置初始化状态值
    /// </summary>
    public class System_SetWebFirstStart
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new Dictionary<string, string>
        {
            {"cmd","System_SetWebFirstStart"},
            {"state","false"}
        };
        public class Respon : Pack
        {
            public string data = string.Empty;
        }
    }

    /// <summary>
    /// 用于判断用户登陆状态是否有效
    /// </summary>
    public class System_QueryUserState
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new()
        {
            {"cmd","System_QueryUserState"},
        };
        public class Respon : Pack
        {
            public bool data = false;
        }
    }

    #endregion

    #region config

    /// <summary>
    /// 设置自动转码总开关
    /// </summary>
    public class Config_Transcod
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new Dictionary<string, string>
        {
            {"cmd","Config_Transcod"},
            {"state","false"}
        };
        public class Respon : Pack
        {
            public string data { get; set; } = string.Empty;
        }
    }

    /// <summary>
    /// 根据文件大小自动切片
    /// </summary>
    public class Config_FileSplit
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new Dictionary<string, string>
        {
            {"cmd","Config_FileSplit"},
            {"size","10240000"}
        };
        public class Respon : Pack
        {
            public string data { get; set; } = string.Empty;
        }
    }

    /// <summary>
    /// 弹幕录制总共开关(包括礼物、舰队、SC)
    /// </summary>
    public class Config_DanmuRec
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new Dictionary<string, string>
        {
            {"cmd","Config_DanmuRec"},
            {"state","false"}
        };
        public class Respon : Pack
        {
            public string data { get; set; } = string.Empty;
        }
    }

    /// <summary>
    /// 导入关注列表中的V(请确认已经扫码登陆)
    /// </summary>
    public class Config_GetFollow
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new()
        {
            {"cmd","Config_GetFollow"},
        };
        public class Respon : Pack
        {
            public List<follow> data { get; set; } = new();
        }
        public class follow
        {
            public long mid;
            public int roomid;
            public string name = string.Empty;
        }
    }

    #endregion

    #region File 

    /// <summary>
    /// 获取已录制的文件列表
    /// </summary>
    public class File_GetAllFileList
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new()
        {
            {"cmd","File_GetAllFileList"},
        };

        public class Respon : Pack
        {
            public List<string> data { get; set; } = new();
        }
    }

    /// <summary>
    /// 下载对应的文件
    /// </summary>
    public class File_GetFile
    {
        ApiAct.GET Act = new(Server.GetAsync);
        public Task<byte[]> ApiExecAsync(Server server) { return Act(Request, server); }
        //public static ApiAct Type { get; } = ApiAct.Get;
        public Dictionary<string, string> Request = new Dictionary<string, string>
        {
            {"cmd","File_GetFile"},
            {"FilName",""}
        };
    }

    /// <summary>
    /// 根据文件树结构返回已录制的文件总列表
    /// </summary>
    public class File_GetFilePathList
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new()
        {
            {"cmd","File_GetFilePathList"},
        };
        public class Respon : Pack
        {
            public List<string> data { get; set; } = new();
        }

        public class Data : List<FileNames> { }

        public class FileNames
        {
            /// <summary>
            /// 文件名
            /// </summary>
            public string Name { get; set; } = string.Empty;
            /// <summary>
            /// 文件类型
            /// </summary>
            public string FileType { get; set; } = string.Empty;
            /// <summary>
            /// 文件大小(如果类型是文件夹则为0)
            /// </summary>
            public long Size { get; set; }
            /// <summary>
            /// 文件创建时间
            /// </summary>
            public DateTime DateTime { get; set; }
            /// <summary>
            /// 子文件夹
            /// </summary>
            public List<FileNames> children { get; set; } = new();
        }
    }

    /// <summary>
    /// 分类获取已录制的文件总列表
    /// </summary>
    public class File_GetTypeFileList
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new()
        {
            {"cmd","File_GetTypeFileList"},
        };

        public class Respon : Pack
        {
            public List<FileList> data { get; set; } = new();
        }

        public class FileList
        {
            public string Type { set; get; } = string.Empty;
            public List<string> files = new List<string>();
        }
    }

    #endregion

    #region login

    /// <summary>
    /// 在提示登陆的情况下获取用于的登陆二维码
    /// </summary>
    public class loginqr
    {
        ApiAct.GET Act = new(Server.GetAsync);
        public Dictionary<string, string> Request = new();
        public Task<byte[]> ApiExecAsync(Server server) { return Act(Request, server); }
        public class Respon : Pack
        {
            public Byte[]? data { get; set; }
        }
    }

    /// <summary>
    /// 重新登陆哔哩哔哩账号
    /// </summary>
    public class Login_Reset
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new()
        {
            {"cmd","Login_Reset"},
        };
        public class Respon : Pack
        {
            string data = string.Empty;
        }
    }

    /// <summary>
    /// 查询内部登陆状态
    /// </summary>
    public class Login_State
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public Task<Respon> ApiExecAsync(Server server) { return Act(Request, server); }
        public Dictionary<string, string> Request = new Dictionary<string, string>
        {
            {"cmd", "login_state" }
        };

        public class Respon : Pack
        {
            public Data data = new();
            public class Data
            {
                public loginState LoginState { get; set; }
            }
        }

        public enum loginState
        {
            /// <summary>
            /// 未登录
            /// </summary>
            NotLoggedIn = 0,
            /// <summary>
            /// 已登陆
            /// </summary>
            LoggedIn = 1,
            /// <summary>
            /// 登陆失效
            /// </summary>
            LoginFailure = 2,
            /// <summary>
            /// 登陆中
            /// </summary>
            LoggingIn = 3
        }

    }

    #endregion

    #region Rec

    public class Downloads
    {
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// 房间号
        /// </summary>
        public string RoomId { get; set; } = string.Empty;
        /// <summary>
        /// 用户UID
        /// </summary>
        public long Uid { set; get; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// FLV大小限制使能
        /// </summary>
        public bool FlvSplit { get; set; } = false;
        /// <summary>
        /// FLV切割大小单位为byte
        /// </summary>
        public long FlvSplitSize { set; get; }
        /// <summary>
        /// 是否下载中
        /// </summary>
        public bool IsDownloading { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// 下载的完整文件路径
        /// </summary>
        public string FileName { set; get; } = string.Empty;
        /// <summary>
        /// 文件夹路径
        /// </summary>
        public string FilePath { set; get; } = string.Empty;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { set; get; }
        public dynamic flvTimes { set; get; } = 0;
        /// <summary>
        /// FLV文件头
        /// </summary>
        public dynamic FlvHeader { set; get; } = string.Empty;
        /// <summary>
        /// FLV头脚本数据
        /// </summary>
        public dynamic FlvScriptTag { set; get; } = string.Empty;
        /// <summary>
        /// WebRequest类的HTTP的实现
        /// </summary>
        public dynamic HttpWebRequest { get; set; } = string.Empty;
        /// <summary>
        /// 当前已下载字节数
        /// </summary>
        public long DownloadCount { get; set; }
        /// <summary>
        /// 该任务下所有任务的总下载字节数
        /// </summary>
        public long TotalDownloadCount { get; set; }
        /// <summary>
        /// 下载状态
        /// </summary>
        public DownloadStatus Status { get; set; } = DownloadStatus.NewTask;
        public enum DownloadStatus
        {
            /// <summary>
            /// 新任务
            /// </summary>
            NewTask,
            /// <summary>
            /// 已准备
            /// </summary>
            Standby,
            /// <summary>
            /// 下载中
            /// </summary>
            Downloading,
            /// <summary>
            /// 下载结束
            /// </summary>
            DownloadComplete,
            /// <summary>
            /// 取消下载
            /// </summary>
            Cancel,
        }
    }

    public class LiteDownloads
    {
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// 房间号
        /// </summary>
        public string RoomId { get; set; } = string.Empty;
        /// <summary>
        /// 用户UID
        /// </summary>
        public long Uid { set; get; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 文件夹路径
        /// </summary>
        public string FilePath { set; get; } = string.Empty;
        /// <summary>
        /// 开始时间(秒，Unix时间戳)
        /// </summary>
        public long StartTime { set; get; }
        /// <summary>
        /// 结束时间(秒，Unix时间戳)
        /// </summary>
        public long EndTime { set; get; }
        /// <summary>
        /// 该任务下所有子任务的总下载字节数
        /// </summary>
        public long TotalDownloadCount { get; set; }
    }

    /// <summary>
    /// 获取下载中的任务情况详细情况
    /// </summary>
    public class Rec_RecordingInfo
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new()
        {
            {"cmd","Rec_RecordingInfo"},
        };

        public class Respon : Pack
        {
            public List<Downloads> data = new();
        }
    }

    /// <summary>
    /// 获取下载中的任务情况简略情况
    /// </summary> 
    public class Rec_RecordingInfo_Lite
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new()
        {
            {"cmd","Rec_RecordingInfo_Lite"},
        };
        public class Respon : Pack
        {
            public List<LiteDownloads> data = new();
        }
    }

    /// <summary>
    /// 获取已经完成的任务详细情况
    /// </summary>
    public class Rec_RecordCompleteInfon
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new()
        {
            {"cmd","Rec_RecordingInfo_Lite"},
        };

        public class Respon : Pack
        {
            public List<Downloads> data = new();
        }
    }

    /// <summary>
    /// 获取已经完成的任务简略情况
    /// </summary>
    public class Rec_RecordCompleteInfon_Lite
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new()
        {
            {"cmd","Rec_RecordCompleteInfon_Lite"},
        };
        public class Respon : Pack
        {
            public List<LiteDownloads> data = new();
        }
    }

    /// <summary>
    /// 取消某个下载任务
    /// </summary>
    public class Rec_CancelDownload
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new Dictionary<string, string>
        {
            {"cmd","Rec_RecordCompleteInfon_Lite"},
            {"UID","0"}
        };
        public class Respon : Pack
        {
            public string data = string.Empty;
        }
    }

    #endregion

    #region  Room

    [Serializable]
    public class RoomInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; } = string.Empty;
        /// <summary>
        /// 主播简介
        /// </summary>
        public string? description { get; set; }
        /// <summary>
        /// 关注数
        /// </summary>
        public int attention { get; set; }
        /// <summary>
        /// 直播间房间号(直播间实际房间号)
        /// </summary>
        public int room_id { get; set; }
        /// <summary>
        /// 主播mid
        /// </summary>
        public long uid { get; set; }
        /// <summary>
        /// 直播间在线人数
        /// </summary>
        public int online { get; set; }
        /// <summary>
        /// 开播时间(未开播时为-62170012800,live_status为1时有效)
        /// </summary>
        public long live_time { get; set; }
        /// <summary>
        /// 直播状态(1为正在直播，2为轮播中)
        /// </summary>
        public int live_status { get; set; }
        /// <summary>
        /// 直播间房间号(直播间短房间号，常见于签约主播)
        /// </summary>
        public int short_id { get; set; }
        /// <summary>
        /// 直播间分区id
        /// </summary>
        public int area { get; set; }
        /// <summary>
        /// 直播间分区名
        /// </summary>
        public string area_name { get; set; } = string.Empty;
        /// <summary>
        /// 直播间新版分区id
        /// </summary>
        public int area_v2_id { get; set; }
        /// <summary>
        /// 直播间新版分区名
        /// </summary>
        public string area_v2_name { get; set; } = string.Empty;
        /// <summary>
        /// 直播间父分区名
        /// </summary>
        public string area_v2_parent_name { get; set; } = string.Empty;
        /// <summary>
        /// 直播间父分区id
        /// </summary>
        public int area_v2_parent_id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string uname { get; set; } = string.Empty;
        /// <summary>
        /// 主播头像url
        /// </summary>
        public string face { get; set; } = string.Empty;
        /// <summary>
        /// 系统tag列表(以逗号分割)
        /// </summary>
        public string tag_name { get; set; } = string.Empty;
        /// <summary>
        /// 用户自定义tag列表(以逗号分割)
        /// </summary>
        public string tags { get; set; } = string.Empty;
        /// <summary>
        /// 直播封面图
        /// </summary>
        public string cover_from_user { get; set; } = string.Empty;
        /// <summary>
        /// 直播关键帧图
        /// </summary>
        public string keyframe { get; set; } = string.Empty;
        /// <summary>
        /// 直播间封禁信息
        /// </summary>
        public string lock_till { get; set; } = string.Empty;
        /// <summary>
        /// 直播间隐藏信息
        /// </summary>
        public string hidden_till { get; set; } = string.Empty;
        /// <summary>
        /// 直播类型(0:普通直播，1：手机直播)
        /// </summary>
        public int broadcast_type { get; set; }
        /// <summary>
        /// 是否p2p
        /// </summary>
        public int need_p2p { set; get; }
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool is_hidden { set; get; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool is_locked { set; get; }
        /// <summary>
        /// 是否竖屏
        /// </summary>
        public bool is_portrait { set; get; }
        /// <summary>
        /// 是否加密
        /// </summary>
        public bool encrypted { set; get; }
        /// <summary>
        /// 加密房间是否通过密码验证(encrypted=true时才有意义)
        /// </summary>
        public bool pwd_verified { set; get; }
        /// <summary>
        /// 未知
        /// </summary>
        public int room_shield { set; get; }
        /// <summary>
        /// 是否为特殊直播间(0：普通直播间 1：付费直播间)
        /// </summary>
        public int is_sp { set; get; }
        /// <summary>
        /// 特殊直播间标志(0：普通直播间 1：付费直播间 2：拜年祭直播间)
        /// </summary>
        public int special_type { set; get; }
        /// <summary>
        /// 是否是临时播放项目
        /// </summary>
        public bool IsTemporaryPlay { set; get; } = false;
        /// <summary>
        /// 直播间状态(0:无房间 1:有房间)
        /// </summary>
        public int roomStatus { set; get; }
        /// <summary>
        /// (废弃：请使用live_status)(该值为getRoomInfoOld接口冗余值)直播状态(1为正在直播，2为轮播中)
        /// </summary>
        internal int liveStatus { set; get; }
        /// <summary>
        /// (废弃：请使用cover_from_user(该值为getRoomInfoOld接口冗余值)直播封面图
        /// </summary>
        internal string user_cover { get; set; } = string.Empty;
        /// <summary>
        /// 轮播状态(0：未轮播 1：轮播)
        /// </summary>
        public int roundStatus { set; get; }
        /// <summary>
        /// 直播间网页url
        /// </summary>
        public string url { set; get; } = string.Empty;
        /// <summary>
        /// 描述(Local值)
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 是否自动录制(Local值)
        /// </summary>
        public bool IsAutoRec { set; get; }
        /// <summary>
        /// 是否开播提醒(Local值)
        /// </summary>
        public bool IsRemind { set; get; }
        /// <summary>
        /// 是否录制弹幕(Local值)
        /// </summary>
        public bool IsRecDanmu { set; get; }
        /// <summary>
        /// 特殊标记(Local值)
        /// </summary>
        public bool Like { set; get; }
        /// <summary>
        /// 用户等级
        /// </summary>
        public int level { set; get; }
        /// <summary>
        /// 主播性别
        /// </summary>
        public string sex { set; get; } = string.Empty;
        /// <summary>
        /// 主播简介
        /// </summary>
        public string sign { set; get; } = string.Empty;
        /// <summary>
        /// 房间的WS连接对象类
        /// </summary>
        public RoomWebSocket roomWebSocket { set; get; } = new();
        /// <summary>
        /// 下载标识符
        /// </summary>
        public bool IsDownload { set; get; } = false;
        /// <summary>
        /// 房间当前下载任务记录
        /// </summary>
        public List<Downloads> DownloadingList { set; get; } = new();
        /// <summary>
        /// 是否被用户取消操作
        /// </summary>
        public bool IsUserCancel { set; get; } = false;
        /// <summary>
        /// 房间历史下载记录
        /// </summary>
        public List<Downloads> DownloadedLog { set; get; } = new();
        /// <summary>
        /// 弹幕录制对象
        /// </summary>
        public dynamic? DanmuFile { set; get; }
        /// <summary>
        /// 是否正在被编辑
        /// </summary>
        public bool IsCliping { set; get; } = false;
        /// <summary>
        /// 该房间当前的任务时间
        /// </summary>
        public DateTime CreationTime { set; get; }
        /// <summary>
        /// 该房间最近一次完成的下载任务的文件信息
        /// </summary>
        public DownloadedFileInfo DownloadedFileInfo { set; get; } = new();
        /// <summary>
        /// 该房间录制完成后会执行的Shell命令
        /// </summary>
        public string Shell { set; get; } = string.Empty;
        /// <summary>
        /// 用于房间监控系统，记录的是监控系统检测到开始直播的时间
        /// </summary>
        public DateTime MonitoringSystem_Airtime = DateTime.Now;
        /// <summary>
        ///  用于房间监控系统，记录开播时的关注数
        /// </summary>
        public int MonitoringSystem_Attention = 0;
        /// <summary>
        /// 当前Host地址
        /// </summary>
        public string Host { set; get; } = string.Empty;
        /// <summary>
        /// 当前模式（0:FLV 1:HLS）
        /// </summary>
        public int CurrentMode { set; get; } = 0;
        /// <summary>
        /// 下载的文件记录
        /// </summary>
        public List<DownloadedFiles> Files { set; get; } = new List<DownloadedFiles>();
        public class DownloadedFiles
        {
            public string FilePath { set; get; } = string.Empty;
            public bool IsTranscod { set; get; } = false;
        }
    }
    [Serializable]
    public class RoomWebSocket
    {
        /// <summary>
        /// 是否已连接
        /// </summary>
        public bool IsConnect { set; get; }
        public long dokiTime { set; get; }
        /// <summary>
        /// WbdScket服务器信息
        /// </summary>
        public dynamic LiveChatListener { set; get; } = string.Empty;
    }
    [Serializable]
    public class DownloadedFileInfo
    {
        /// <summary>
        /// 修复后的文件完整路径List
        /// </summary>
        public List<FileInfo> AfterRepairFiles { set; get; } = new List<FileInfo>();
        /// <summary>
        /// 修复前的文件完整路径List
        /// </summary>
        public List<FileInfo> BeforeRepairFiles { set; get; } = new List<FileInfo>();
        /// <summary>
        /// 录制的弹幕文件
        /// </summary>
        public FileInfo? DanMuFile { set; get; }
        /// <summary>
        /// 录制的SC记录文件
        /// </summary>
        public FileInfo? SCFile { set; get; }
        /// <summary>
        /// 录制的大航海记录文件
        /// </summary>
        public FileInfo? GuardFile { set; get; }
        /// <summary>
        /// 录制的礼物记录文件
        /// </summary>
        public FileInfo? GiftFile { set; get; }
    }
    [Serializable]
    public class BRoomInfo
    {
        /// <summary>
        /// 直播间房间号(直播间实际房间号)
        /// </summary>
        public int room_id { get; set; }
        /// <summary>
        /// 主播mid
        /// </summary>
        public long uid { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string uname { get; set; } = string.Empty;
        /// <summary>
        /// 是否自动录制(Local值)
        /// </summary>
        public bool IsAutoRec { set; get; }
        /// <summary>
        /// 是否开播提醒(Local值)
        /// </summary>
        public bool IsRemind { set; get; }
        /// <summary>
        /// 是否录制弹幕(Local值)
        /// </summary>
        public bool IsRecDanmu { set; get; }
        /// <summary>
        /// 特殊标记(Local值)
        /// </summary>
        public bool Like { set; get; }
        /// <summary>
        /// 下载标识符
        /// </summary>
        public bool IsDownload { set; get; } = false;
    }

    /// <summary>
    /// 获取房间详细配置信息
    /// </summary>
    public class Room_AllInfo
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new Dictionary<string, string>
        {
            {"cmd", "room_allinfo" }
        };
        [Serializable]
        public class Respon : Pack
        {
            public List<RoomInfo> data { get; set; } = new();
        }
    }

    /// <summary>
    /// 获取房间简要配置信息
    /// </summary>
    public class Room_SummaryInfo
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new Dictionary<string, string>
        {
            {"cmd", "room_summaryinfo" }
        };
        [Serializable]
        public class Respon : Pack
        {
            public List<BRoomInfo> data { get; set; } = new();
        }
    }

    /// <summary>
    /// 增一个加房间配置
    /// </summary>
    public class Room_Add
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new Dictionary<string, string>
        {
            {"cmd","Room_Add"},
            {"UID","100000000000"}
        };

        public class Respon : Pack
        {
            public string data { get; set; } = string.Empty;
        }
    }

    /// <summary>
    /// 删除一个房间配置
    /// </summary>
    public class Room_Del
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new Dictionary<string, string>
        {
            {"cmd","Room_Del"},
            {"UID","100000000000"}
        };

        public class Respon : Pack
        {
            public string data { get; set; } = string.Empty;
        }
    }

    /// <summary>
    /// 修改房间自动录制配置信息
    /// </summary>
    public class Room_AutoRec
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new Dictionary<string, string>
        {
            {"cmd","Room_AutoRec"},
            {"UID","100000000000"},
            {"IsAutoRec","false"}
        };

        public class Respon : Pack
        {
            public string data { get; set; } = string.Empty;
        }
    }

    /// <summary>
    /// 修改房间弹幕录制配置信息
    /// </summary>
    public class Room_DanmuRec
    {
        ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
        public Respon ResPon = new();
        public async Task ApiExecAsync(Server server) { ResPon = await Act(Request, server); }
        public Dictionary<string, string> Request = new Dictionary<string, string>
        {
            {"cmd","Room_DanmuRec"},
            {"UID","100000000000"},
            {"IsAutoRec","false"}
        };
        public class Respon : Pack
        {
            public string data { get; set; } = string.Empty;
        }
    }

    // public class User_Search
    // {
    //             ApiAct.POST<Respon> Act = new(Server.PostAsync<Respon>);
    //    public Respon ResPon = new();
    //    public Task<Respon> ApiExecAsync(Server server){return Act(Request, server );}
    //     public Dictionary<string, string> Request = new Dictionary<string, string>
    //     {
    //         {"keyword",""}
    //     };
    //     public class Data
    //     {
    //         string type =""
    //     }

    // }

    #endregion

}
