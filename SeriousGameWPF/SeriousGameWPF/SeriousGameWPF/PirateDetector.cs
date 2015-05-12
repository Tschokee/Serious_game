using System.IO;
using System.Net.NetworkInformation;
using System.Management;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SeriousGameWPF
{
    /// <summary>
    /// Helps us to check wether the user tries to YARRRR
    /// </summary>
    internal class PirateDetector 
    {

        public string MacAddress;
        public string HardDiscId;
        public string CentralProcessingUnitId;

        /// <summary>
        /// Get hardware unique keys
        /// </summary>
        public PirateDetector()
        {
            this.MacAddress = GetMacAddress();
            this.HardDiscId = HddId();
            this.CentralProcessingUnitId = CpuId();
        }

        /// <summary>
        /// Gets MAC Address
        /// </summary>
        /// <returns>mac</returns>
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

        /// <summary>
        /// Gets CPU identifier
        /// </summary>
        /// <returns>id</returns>
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

        /// <summary>
        /// Gets HDD identifier
        /// </summary>
        /// <returns>id</returns>
        private string HddId()
        {
            ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + "C" + @":""");
            dsk.Get();
            string volumeSerial = dsk["VolumeSerialNumber"].ToString();
            return volumeSerial;
        }

        /// <summary>
        /// Reads the data.bin file located in root
        /// </summary>
        public void ReadAuthenticationFile()
        {
            IFormatter formatter = new BinaryFormatter();
            StringBuilder sb = new StringBuilder();
            if (!File.Exists("data.bin")) //first run 
                //nah it should be permanent in root, so delete = piracy
            {
                sb.Append(this.CentralProcessingUnitId);
                sb.Append('-');
                sb.Append(this.HardDiscId);
                sb.Append('-');
                sb.Append(this.MacAddress);

                using (var stream = new FileStream("data.bin", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    //should be object, not string
                    formatter.Serialize(stream, sb.ToString());
                }
                return;   
            }

            using (var stream = new FileStream("data.bin", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                formatter = new BinaryFormatter();
                var piratePrint = formatter.Deserialize(stream);
                var separatedPrint = ((string)piratePrint).Split('-');
                CheckPiracy(separatedPrint);
            }
        }

        /// <summary>
        /// Checks wether it is still the same machine 
        /// or slightly different, in case of hardware upgrade
        /// </summary>
        /// <param name="separatedPrint">keys separated with '-'</param>
        /// <returns>user tries to YARRRR</returns>
        private bool CheckPiracy(string[] separatedPrint)
        {
            const int tolerance = 2; //in case of hardware upgrade
            var hardwareDeviation = 0;

            if (separatedPrint[0] != CentralProcessingUnitId)
                hardwareDeviation++;
            if (separatedPrint[1] != HardDiscId)
                hardwareDeviation++;
            if (separatedPrint[2] != MacAddress)
                hardwareDeviation++;

            return hardwareDeviation <= tolerance;
        }
    }
}
