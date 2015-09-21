using System.Management;

namespace DotNet.Utilities
{
    /// <summary>
    /// 获取计算机硬件信息
    /// </summary>
    public class ComputerHelper
    {
        ///<summary>
        /// 获取硬盘卷标号
        ///</summary>
        ///<returns></returns>
        private string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc = new ManagementClass("win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }

        ///<summary>
        /// 获取CPU序列号
        ///</summary>
        ///<returns></returns>
        private string GetCpu()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuCollection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuCollection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
            }
            return strCpu;
        }

        ///<summary>
        /// 软件特征码
        ///</summary>
        ///<returns>软件特征码是获取正式版软件</returns>
        public string GetMNum()
        {
            string strNum = GetCpu() + GetDiskVolumeSerialNumber();
            strNum = strNum.Substring(0, 16);    //截取前24位作为机器码
            return strNum;
        }
    }
}