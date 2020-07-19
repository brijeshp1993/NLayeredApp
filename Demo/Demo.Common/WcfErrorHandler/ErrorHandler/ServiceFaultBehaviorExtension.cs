using System;
using System.ServiceModel.Configuration;

namespace Demo.Common.WcfErrorHandler.ErrorHandler
{
    public class ServiceFaultBehaviorExtension : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(ServiceFaultBehavior); }
        }
        protected override object CreateBehavior()
        {
            return new ServiceFaultBehavior();
        }
    }
}
