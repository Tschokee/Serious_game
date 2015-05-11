using System.Net.NetworkInformation;
using System.Management;

namespace SeriousGameWPF
{
    internal class PirateDetector
    {

        public string MacAddress;
        public string HardDiscId;
        public string CentralProcessingUnitId;

        public PirateDetector()
        {
            this.MacAddress = GetMacAddress();
            this.HardDiscId = HddId();
            this.CentralProcessingUnitId = CpuId();
        }

        private static string GetMacAddress()
        {
            var macAddresses = string.Empty;

            foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += networkInterface.GetPhysicalAddress().ToString();
                    break;
                }
            }

            return macAddresses;
        }

        private string CpuId()
        {
            var cpuInfo = string.Empty;
            var mc = new ManagementClass("win32_processor");
            var moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
            }
            return cpuInfo;
        }

        private string HddId()
        {
            ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + "C" + @":""");
            dsk.Get();
            string volumeSerial = dsk["VolumeSerialNumber"].ToString();
            return volumeSerial;
        }

        private void ReadAuthenticationFile()
        {
            
        }
    }
}
