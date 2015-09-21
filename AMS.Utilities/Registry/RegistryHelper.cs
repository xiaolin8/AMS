using Microsoft.Win32;

namespace DotNet.Utilities
{
    /// <summary>
    /// 写入/读取注册表
    /// </summary>
    public class RegistryHelper
    {
        /// <summary>
        /// 读取指定名称的注册表的值 
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public object GetRegistData(string name)
        {
            string registData;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
            RegistryKey aimdir = software.OpenSubKey("HXLSoft", true);
            registData = aimdir.GetValue(name).ToString();
            return registData;
        }
        /// <summary>
        /// 向注册表中写数据 
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="tovalue">值</param>
        public void WTRegedit(string name, object tovalue)
        {
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey software = hklm.OpenSubKey("SOFTWARE", true);
            RegistryKey aimdir = software.CreateSubKey("HXLSoft");
            aimdir.SetValue(name, tovalue);
        }
        /// <summary>
        /// 判断指定注册表项是否存在 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsRegeditExit(string name)
        {
            try
            {
                this.GetRegistData(name);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
