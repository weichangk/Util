using System;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;

namespace Util
{
    /// <summary>
    /// 计算机相关信息的帮助类
    /// </summary>
    public static class ComputeHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly SystemInfo _systemInfo = new SystemInfo();

        /// <summary>
        /// 获取本地IP
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIp()
        {
            string strIpV4 = string.Empty;

            try
            {
                string hostName = System.Net.Dns.GetHostName();
                string strValue;

                //IP地址,注意目前只取获取到的首个IPV4地址.
                System.Net.IPAddress[] adr = System.Net.Dns.GetHostAddresses(hostName);

                for (int i = 0; i < adr.Length; i++)
                {
                    strValue = adr[i].ToString();
                    if (strValue.Length > 0 && strValue.Length <= 15)
                    {
                        strIpV4 = strValue;
                        break;
                    }
                }
            }
            catch
            {
            }

            return strIpV4;
        }

        /// <summary>
        /// 获取本地Mac true 全部 false 第一个Mac
        /// </summary>
        /// <param name="isAll">是否全部Mac</param>
        /// <returns></returns>
        public static string GetLocalMac(bool isAll)
        {
            string mac = string.Empty;

            try
            {
                ManagementClass MC = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection MOC = MC.GetInstances();

                foreach (ManagementObject MO in MOC)
                {
                    if ((bool)MO["IPEnabled"] == true)
                    {
                        if (isAll)
                        {
                            mac += MO["MACAddress"].ToString();
                        }
                        else
                        {
                            mac = MO["MACAddress"].ToString();
                            break;
                        }
                    }
                }
            }
            catch
            {
                mac = string.Empty;
            }

            if (Object.Equals(mac, DBNull.Value) || mac == null)
                mac = string.Empty;
            else
                mac =  mac.ToString().Trim();


            return mac.Trim().Replace(":", "").Replace("-", "");
        }

        public static string GetOSVersion()
        {
            try
            {
                return WindowsVersionDetector.GetVersion() + " " + WindowsVersionDetector.GetServicePack();
            }
            catch { }

            return "操作系统获取失败";
        }

        /// <summary>
        /// 获取系统内存大小
        /// </summary>
        /// <returns>内存大小（单位M）</returns>
        public static long GetPhisicalMemory()
        {
            try
            {
                return _systemInfo.PhysicalMemory;
            }
            catch { }

            return 0;
        }

        /// <summary>
        /// 获取可用内存
        /// </summary>
        /// <returns></returns>
        public static long GetAvailableMemory()
        {
            try
            {
                return _systemInfo.MemoryAvailable;
            }
            catch { }

            return 0;
        }

        /// <summary>
        /// 获取CPU名称
        /// </summary>
        /// <returns></returns>
        public static string GetCpuName()
        {
            try
            {
                return _systemInfo.CpuName;
            }
            catch { }

            return "";
        }

        /// <summary>
        /// 获取CPU占用率
        /// </summary>
        /// <returns></returns>
        public static float GetCpuUtilizationPercentage()
        {
            try
            {
                return _systemInfo.CpuLoad;
            }
            catch { }

            return 0;
        }


        /// <summary>
        /// <para>获取系统信息</para>
        /// <para>https://msdn.microsoft.com/en-us/library/aa394084(VS.85).aspx</para>
        /// </summary>
        /// <returns></returns>
        private static string GetPhicnalInfo()
        {
            ManagementClass osClass = new ManagementClass("Win32_Processor");//后面几种可以试一下，会有意外的收获//Win32_PhysicalMemory/Win32_Keyboard/Win32_ComputerSystem/Win32_OperatingSystem
            StringBuilder builder = new StringBuilder();
            foreach (ManagementObject obj in osClass.GetInstances())
            {
                PropertyDataCollection pdc = obj.Properties;
                foreach (PropertyData pd in pdc)
                {
                    builder.AppendFormat("{0}： {1}{2}", pd.Name, pd.Value, "\r\n");
                }
            }
            return builder.ToString();
        }

        static string _hardWareSerialNumber = null;
        /// <summary>
        /// 获取硬件标识
        /// </summary>
        /// <returns></returns>
        public static string GetHardWareString()
        {
            if (_hardWareSerialNumber == null)
            {
                _hardWareSerialNumber = "";
                try
                {
                    var diskSerialNumber = GetDiskSerialNumber();
                    var cpuSerialNumber = GetCpuSerialNumber();
                    var macAddress = GetMacByNetworkInterface();

                    _hardWareSerialNumber = $"{diskSerialNumber}<SX1>{cpuSerialNumber}<SX2>{macAddress}";
                }
                catch { }
            }

            return _hardWareSerialNumber;
        }

        /// <summary>
        /// 获取硬件标识（这里取硬盘序列号 + CPU序列号 + MAC地址）
        /// </summary>
        /// <returns></returns>
        public static string GetHardWareString(out string diskSerialNumber
            , out string cpuSerialNumber
            , out string baseboardSerialNumber
            , out string macAddress
            , out long costMilliseconds)
        {
            diskSerialNumber = "";
            cpuSerialNumber = "";
            baseboardSerialNumber = "";
            macAddress = "";
            costMilliseconds = 0L;
            try
            {
                var timeStart = DateTime.Now;
                diskSerialNumber = GetDiskSerialNumber();
                cpuSerialNumber = GetCpuSerialNumber();
                baseboardSerialNumber = GetBaseboardSerialNumber();
                macAddress = GetMacByNetworkInterface();
                costMilliseconds = (long)((DateTime.Now - timeStart).TotalMilliseconds);

                return $"{diskSerialNumber}<SX1>{cpuSerialNumber}<SX2>{macAddress}";
            }
            catch { }

            return "";
        }

        /// <summary>
        /// 获取硬件标识
        /// </summary>
        /// <returns></returns>
        public static string GetOldHardWareString()
        {
            try
            {
                return $"{GetDiskSerialNumber()}SIXUNTD{GetCpuSerialNumber()}";
            }
            catch { }
            return "";
        }

        #region 获取硬件信息
        /// <summary>
        /// 硬盘序列号
        /// </summary>
        /// <returns></returns>
        private static string GetDiskSerialNumber()
        {
            try
            {
                ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc1 = cimobject1.GetInstances();
                foreach (ManagementObject mo in moc1)
                {
                    return (string)mo.Properties["Model"].Value;
                }
            }
            catch { }

            return "";

        }

        /// <summary>
        /// 主板序列号（注意：部分主板获取不到序列号，返回的是“Default String”）
        /// </summary>
        /// <returns></returns>
        private static string GetBaseboardSerialNumber()
        {
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_baseboard");
                foreach (ManagementObject mo in mos.Get())
                {
                    return mo["SerialNumber"].ToString();
                }
            }
            catch { }

            return "";
        }

        /// <summary>
        /// CPU序列号
        /// </summary>
        /// <returns></returns>
        private static string GetCpuSerialNumber()
        {
            try
            {
                string cpuInfo = "";//cpu序列号
                ManagementClass cimobject = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = cimobject.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                    return cpuInfo.ToString();
                }
            }
            catch { }

            return "";
        }

        /// <summary>
        /// 通过NetworkInterface获取MAC地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacByNetworkInterface()
        {
            try
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in interfaces)
                {
                    return BitConverter.ToString(ni.GetPhysicalAddress().GetAddressBytes());
                }
            }
            catch { }

            return "00-00-00-00-00-00";
        }
        #endregion

    }
}
