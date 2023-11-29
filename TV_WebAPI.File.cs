using TV_WebAPI.ApiClass;
namespace TV_WebAPI.ApiClass.File
{
    /// <summary>
    /// 获取已录制的文件列表
    /// ApiData List<string>
    /// </summary>
    public class File_GetAllFileList : PostAPI { }

    /// <summary>
    /// 下载对应的文件
    /// GETAPI
    /// </summary>
    public class File_GetFile
    {
        public new Dictionary<string, string> Selfval = new Dictionary<string, string>
        {{"FilName",""}};
    }

    /// <summary>
    /// 根据文件树结构返回已录制的文件总列表
    /// ApiData List<FileNames> 
    /// </summary>
    public class File_GetFilePathList : PostAPI
    {
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
    /// ApiData List<File_GetTypeFileList.FileList>
    /// </summary>
    public class File_GetTypeFileList : PostAPI
    {
        public class FileList
        {
            public string Type { set; get; } = string.Empty;
            public List<string> files = new List<string>();
        }
    }
}
