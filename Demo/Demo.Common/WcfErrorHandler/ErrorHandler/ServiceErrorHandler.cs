using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Demo.Common.WcfErrorHandler.Fault;

namespace Demo.Common.WcfErrorHandler.ErrorHandler
{
    // If you want to suppress the fault message, just implement the HandlerError method and return false.
    // If you want to raise FaultException instead of suppressing, then implement the ProvideFault method to provide the MessageFault value.
    // IErrorHandler approach should be used with care, since it handles all the exceptions inside the service in a generic way.
    // It is always better to handle the exception where it occurred instead of going with the generic exception handler like IErrorHandler.
    /// <summary>
    /// Wcf Error handler
    /// </summary>
    public class WcfErrorHandler : IErrorHandler
    {
        public WcfErrorHandler(ServiceFaultBehavior behaviour)
        {
        }

        /*This method is called automatically when an exception is happened at the service layer.*/
        public void ProvideFault(Exception ex, MessageVersion version, ref Message fault)
        {
            var strMessgae = Convert.ToString(ConfigurationManager.AppSettings["CustomErrorMessage"]);
            var faultException = new FaultException<ServiceException>(new ServiceException
            {
                Message = strMessgae
            },
            new FaultReason(strMessgae));
            var messageFault = faultException.CreateMessageFault();
            fault = Message.CreateMessage(version, messageFault, faultException.Action);
        }

        public bool HandleError(Exception ex)
        {
            //call service to write log to either db or project specific path.
            return true;
        }
    }
}
