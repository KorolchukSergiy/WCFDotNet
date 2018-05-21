using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFDotNet.DataContracts
{
    [DataContract]
    public class DCCpuFromShop: DCItemFromShop
    {
        [DataMember]
        public string CpuSocket { get; set; }
        [DataMember]
        public int Frequency { get; set; }
        [DataMember]
        public int Core { get; set; }
        [DataMember]
        public int Threads { get; set; }
        [DataMember]
        public string Cash { get; set; }
        [DataMember]
        public string Video { get; set; }
    }
}