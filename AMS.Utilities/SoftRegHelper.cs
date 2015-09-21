using System;

namespace DotNet.Utilities
{
    /// <summary>
    /// 软件注册校验
    /// </summary>
    public class SoftRegHelper
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public SoftRegHelper()
        {
            Registration();
        }
        /// <summary>
        /// 软件注册码验证
        /// </summary>
        /// <returns></returns>
        private void Registration()
        {
            try
            {
                ComputerHelper Computer = new ComputerHelper();
                RegistryHelper registry = new RegistryHelper();
                string IsRegistration = ConfigHelper.GetValue("IsRegistration");//软件是否试用
                if (IsRegistration == "false")
                {
                    DateTime SetupTime = new DateTime();
                    if (!registry.IsRegeditExit("SetupTime"))
                    {
                        registry.WTRegedit("SetupTime", DESEncrypt.Encrypt(DateTime.Now.AddDays(+30).Date.ToString()));
                    }
                    SetupTime = DateTimeHelper.ToDate(DESEncrypt.Decrypt(registry.GetRegistData("SetupTime").ToString()));
                    if (DateTime.Now > SetupTime)
                    {
                        throw new Exception("您的试用期已过，请购买正版！");
                    }
                }
                else
                {
                    if (!registry.IsRegeditExit("IsAuthorization"))
                    {
                        string RegistrationCode = ConfigHelper.GetValue("RegistrationCode");//获取软件注册码
                        string Licence = DESEncrypt.Encrypt(Computer.GetMNum());
                        if (RegistrationCode == Licence)
                        {
                            registry.WTRegedit("IsAuthorization", DESEncrypt.Encrypt("true"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
