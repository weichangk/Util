using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;

namespace Util
{
    ///  
    /// 系统信息类 - 获取CPU、内存、磁盘、进程信息 
    ///  
    internal class SystemInfo
    {
        private int m_ProcessorCount = 0;   //CPU个数 
        private PerformanceCounter pcCpuLoad;   //CPU计数器 
        private long m_PhysicalMemory = 0;   //物理内存 

        private const int GW_HWNDFIRST = 0;
        private const int GW_HWNDNEXT = 2;
        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 268435456;
        private const int WS_BORDER = 8388608;
        Dictionary<string, ManagementObjectCollection> WmiDict = new Dictionary<string, ManagementObjectCollection>();

        #region AIP声明 
        [DllImport("IpHlpApi.dll")]
        extern static public uint GetIfTable(byte[] pIfTable, ref uint pdwSize, bool bOrder);

        [DllImport("User32")]
        private extern static int GetWindow(int hWnd, int wCmd);

        [DllImport("User32")]
        private extern static int GetWindowLongA(int hWnd, int wIndx);

        [DllImport("user32.dll")]
        private static extern bool GetWindowText(int hWnd, StringBuilder title, int maxBufSize);

        [DllImport("user32", CharSet = CharSet.Auto)]
        private extern static int GetWindowTextLength(IntPtr hWnd);
        #endregion

        #region 构造函数 
        ///  
        /// 构造函数，初始化计数器等 
        ///  
        public SystemInfo()
        {
            try
            {
                //初始化CPU计数器 
                pcCpuLoad = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                pcCpuLoad.MachineName = ".";
                pcCpuLoad.NextValue();

                //CPU个数 
                m_ProcessorCount = Environment.ProcessorCount;

                //获得物理内存 
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if (mo["TotalPhysicalMemory"] != null)
                    {
                        m_PhysicalMemory = (long.Parse(mo["TotalPhysicalMemory"].ToString()) / 1024) / 1024;
                    }
                }

                var names = Enum.GetNames(typeof(WmiType));
                foreach (string name in names)
                {
                    WmiDict.Add(name, new ManagementObjectSearcher("SELECT * FROM " + name).Get());
                }
            }
            catch { }
        }
        #endregion

        #region CPU个数 
        ///  
        /// 获取CPU个数 
        ///  
        public int ProcessorCount
        {
            get
            {
                return m_ProcessorCount;
            }
        }
        #endregion

        #region CPU占用率 
        ///  
        /// 获取CPU占用率 
        ///  
        public float CpuLoad
        {
            get
            {
                return pcCpuLoad.NextValue();
            }
        }
        #endregion

        #region CPU名称
        public string CpuName
        {
            get
            {
                var query = WmiDict[WmiType.Win32_Processor.ToString()];
                string name = "";
                foreach (var obj in query)
                {
                    name = (obj["Name"] ?? "").ToString();
                }

                return name;
            }
        }
        #endregion

        #region 可用内存 
        ///  
        /// 获取可用内存 
        ///  
        public long MemoryAvailable
        {
            get
            {
                long availablebytes = 0;
                //ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_PerfRawData_PerfOS_Memory"); 
                //foreach (ManagementObject mo in mos.Get()) 
                //{ 
                //    availablebytes = long.Parse(mo["Availablebytes"].ToString()); 
                //} 
                ManagementClass mos = new ManagementClass("Win32_OperatingSystem");
                foreach (ManagementObject mo in mos.GetInstances())
                {
                    if (mo["FreePhysicalMemory"] != null)
                    {
                        availablebytes = (long.Parse(mo["FreePhysicalMemory"].ToString()) / 1024);
                    }
                }
                return availablebytes;
            }
        }
        #endregion

        #region 物理内存 
        ///  
        /// 获取物理内存 
        ///  
        public long PhysicalMemory
        {
            get
            {
                return m_PhysicalMemory;
            }
        }
        #endregion

        #region 获得分区信息 
        ///  
        /// 获取分区信息 
        ///  
        public List<DiskInfo> GetLogicalDrives()
        {
            List<DiskInfo> drives = new List<DiskInfo>();
            ManagementClass diskClass = new ManagementClass("Win32_LogicalDisk");
            ManagementObjectCollection disks = diskClass.GetInstances();
            foreach (ManagementObject disk in disks)
            {
                // DriveType.Fixed 为固定磁盘(硬盘) 
                if (int.Parse(disk["DriveType"].ToString()) == (int)DriveType.Fixed)
                {
                    drives.Add(new DiskInfo(disk["Name"].ToString(), long.Parse(disk["Size"].ToString()), long.Parse(disk["FreeSpace"].ToString())));
                }
            }
            return drives;
        }
        ///  
        /// 获取特定分区信息 
        ///  
        /// 盘符 
        public List<DiskInfo> GetLogicalDrives(char DriverID)
        {
            List<DiskInfo> drives = new List<DiskInfo>();
            WqlObjectQuery wmiquery = new WqlObjectQuery("SELECT * FROM Win32_LogicalDisk WHERE DeviceID = ’" + DriverID + ":’");
            ManagementObjectSearcher wmifind = new ManagementObjectSearcher(wmiquery);
            foreach (ManagementObject disk in wmifind.Get())
            {
                if (int.Parse(disk["DriveType"].ToString()) == (int)DriveType.Fixed)
                {
                    drives.Add(new DiskInfo(disk["Name"].ToString(), long.Parse(disk["Size"].ToString()), long.Parse(disk["FreeSpace"].ToString())));
                }
            }
            return drives;
        }
        #endregion

        #region 获得进程列表 
        ///  
        /// 获得进程列表 
        ///  
        public List<ProcessInfo> GetProcessInfo()
        {
            List<ProcessInfo> pInfo = new List<ProcessInfo>();
            Process[] processes = Process.GetProcesses();
            foreach (Process instance in processes)
            {
                try
                {
                    pInfo.Add(new ProcessInfo(instance.Id,
                        instance.ProcessName,
                        (long)instance.TotalProcessorTime.TotalMilliseconds,
                        instance.WorkingSet64,
                        instance.MainModule.FileName));
                }
                catch { }
            }
            return pInfo;
        }
        ///  
        /// 获得特定进程信息 
        ///  
        /// 进程名称 
        public List<ProcessInfo> GetProcessInfo(string ProcessName)
        {
            List<ProcessInfo> pInfo = new List<ProcessInfo>();
            Process[] processes = Process.GetProcessesByName(ProcessName);
            foreach (Process instance in processes)
            {
                try
                {
                    pInfo.Add(new ProcessInfo(instance.Id,
                        instance.ProcessName,
                        (long)instance.TotalProcessorTime.TotalMilliseconds,
                        instance.WorkingSet64,
                        instance.MainModule.FileName));
                }
                catch { }
            }
            return pInfo;
        }
        #endregion

        #region 结束指定进程 
        ///  
        /// 结束指定进程 
        ///  
        /// 进程的 Process ID 
        public static void EndProcess(int pid)
        {
            try
            {
                Process process = Process.GetProcessById(pid);
                process.Kill();
            }
            catch { }
        }
        #endregion


        #region 查找所有应用程序标题 
        ///  
        /// 查找所有应用程序标题 
        ///  
        /// 应用程序标题范型 
        public static List<string> FindAllApps(int Handle)
        {
            List<string> apps = new List<string>();

            int hwCurr;
            hwCurr = GetWindow(Handle, GW_HWNDFIRST);

            while (hwCurr > 0)
            {
                int IsTask = (WS_VISIBLE | WS_BORDER);
                int lngStyle = GetWindowLongA(hwCurr, GWL_STYLE);
                bool TaskWindow = ((lngStyle & IsTask) == IsTask);
                if (TaskWindow)
                {
                    int length = GetWindowTextLength(new IntPtr(hwCurr));
                    StringBuilder sb = new StringBuilder(2 * length + 1);
                    GetWindowText(hwCurr, sb, sb.Capacity);
                    string strTitle = sb.ToString();
                    if (!string.IsNullOrEmpty(strTitle))
                    {
                        apps.Add(strTitle);
                    }
                }
                hwCurr = GetWindow(hwCurr, GW_HWNDNEXT);
            }

            return apps;
        }
        #endregion
    }

    /// <summary>
    /// 进程信息
    /// </summary>
    internal class ProcessInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">进程Id</param>
        /// <param name="name">进程名称</param>
        /// <param name="totalProcessorMilliseconds">总的处理器时间（毫秒）</param>
        /// <param name="workingSet">分配的物理内存</param>
        /// <param name="mainModuleFileName">关联模块主进程名</param>
        public ProcessInfo(int id, string name, long totalProcessorMilliseconds, long workingSet, string mainModuleFileName)
        {
            Id = id;
            Name = name;
            TotalProcessorMilliseconds = totalProcessorMilliseconds;
            WorkingSet64 = workingSet;
            MainModuleFileName = mainModuleFileName;
        }

        /// <summary>
        /// 进程Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 进程名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 总的处理器时间（毫秒）
        /// </summary>
        public long TotalProcessorMilliseconds { get; set; }

        /// <summary>
        /// 分配的物理内存
        /// </summary>
        public long WorkingSet64 { get; set; }

        /// <summary>
        /// 关联模块主进程名
        /// </summary>
        public string MainModuleFileName { get; set; }
    }

    internal class DiskInfo
    {
        public DiskInfo(string name, long size, long freeSpace)
        {
            Name = name;
            Size = size;
            FreeSpace = freeSpace;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 总空间
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// 剩余空间
        /// </summary>
        public long FreeSpace { get; set; }
    }

    internal enum WmiType
    {
        Win32_Processor,
        Win32_PerfFormattedData_PerfOS_Memory,
        Win32_PhysicalMemory,
        Win32_NetworkAdapterConfiguration,
        Win32_LogicalDisk
    }
}
