namespace DotNet.Utilities
{
    //当前客户端环境
    public class ClientCultureModel
    {
        LoginUser loginUser = null;

        public ClientCultureModel(LoginUser loginUser)
        {
            this.loginUser = loginUser;
        }

        public LoginUser LoginUser
        {
            get { return loginUser; }
            //set { loginUser = value; }
        }
    }
}