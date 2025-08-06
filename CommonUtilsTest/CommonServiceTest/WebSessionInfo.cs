using System.Runtime.Serialization;

namespace CommonService
{
    [DataContract]
    public class WebSessionInfo
    {
        [DataMember]
        public string ServiceSession { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string ApplicationName { get; set; }
        [DataMember]
        public string ServiceName { get; set; }

    }
}