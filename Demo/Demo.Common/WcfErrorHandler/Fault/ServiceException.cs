using System.Runtime.Serialization;

namespace Demo.Common.WcfErrorHandler.Fault
{
    [DataContract]
    public class ServiceException
    {
        /// <summary>
        /// Exception Message
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// If critical, user should redirect to error page 
        /// and exception details should log.
        /// </summary>
        [DataMember]
        public bool IsCritical { get; set; }
    }
}
