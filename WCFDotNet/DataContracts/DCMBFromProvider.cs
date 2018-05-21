using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFDotNet.DataContracts
{
    [DataContract]
    public class DCMBFromProvider : DCItemFromProvider 
    {
        [DataMember]
        public string MBSocket { get; set; }
        [DataMember]
        public string ChipSet { get; set; }
        [DataMember]
        public string PciE { get; set; }
        [DataMember]
        public string RAM { get; set; }
        [DataMember]
        public string USB { get; set; }
    }
}