using TV_WebAPI.ApiClass;
namespace TV_WebAPI.ApiClass.Login
{
    /// <summary>
    /// 在提示登陆的情况下获取用于的登陆二维码
    /// GETAPI
    /// </summary>
    public class loginqr { }

    /// <summary>
    /// 重新登陆哔哩哔哩账号
    /// ApiData String
    /// </summary>
    public class Login_Reset : PostAPI { }

    /// <summary>
    /// 查询内部登陆状态
    /// ApiData Login_State.LoginState
    /// </summary>
    public class Login_State : PostAPI
    {
        public enum LoginState
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
}
