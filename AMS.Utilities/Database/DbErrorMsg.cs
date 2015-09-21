namespace DotNet.Utilities
{
    /// <summary>
    /// 提交数据库错误信息
    /// 应用单例模式，保存状态
    /// </summary>
    public class DbErrorMsg
    {
        public static string ReturnMsg { get; set; }
    }
}