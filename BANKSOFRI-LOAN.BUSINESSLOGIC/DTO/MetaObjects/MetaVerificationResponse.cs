using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO.MetaObjects
{
   public class MetaVerificationResponse
    {
        public List<object> documents { get; set; }
        public bool expired { get; set; }
        public Flow flow { get; set; }
        public string identity { get; set; }
        public List<Input> inputs { get; set; }
        public Metadata metadata { get; set; }
        public List<Step> steps { get; set; }
        public string id { get; set; }
        public DeviceFingerprint deviceFingerprint { get; set; }
        public bool hasProblem { get; set; }
    }
    public class DeviceFingerprint
    {
        public string ua { get; set; }
        public string ip { get; set; }
        public bool vpnDetectionEnabled { get; set; }
    }

    public class Flow
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Input
    {
        public string id { get; set; }
        public int status { get; set; }
        public bool optional { get; set; }
        public int? group { get; set; }
    }
    public class Metadata
    {
        public string user { get; set; }
        public string id { get; set; }
    }

    public class Step
    {
        public int status { get; set; }
        public string id { get; set; }
    }
}
