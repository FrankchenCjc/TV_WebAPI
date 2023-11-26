using System;
using System.Threading.Tasks;
using TV_WebAPI;
using TV_WebAPI.ApiClass.System;
namespace a
{
    class main
    {
        public static void Main()
        {
            Server server = new("https://192.168.2.1/ddtv/serv/api/", "252dbf51743e48b787325dc408d11da4", "d86c5166dbbf466c8d1ce493a9df1f39");
            Console.WriteLine("server build");
            System_info info = new System_info();
            Console.WriteLine("ask build");
            var run = server.PostAsync<System_info.APack, System_info>(info);
            Console.WriteLine("task runing");
            Console.WriteLine("done");
            System_info.APack Pack = new();
            Pack = run.Result;
            Console.WriteLine("info as {0}", Pack.message);
            Console.ReadKey();
        }
    }
}
