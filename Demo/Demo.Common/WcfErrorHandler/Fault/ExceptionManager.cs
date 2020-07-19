using System;
using System.Data.SqlClient;

namespace Demo.Common.WcfErrorHandler.Fault
{
    /// <summary>
    /// Exception manager class
    /// returns objects of service exception class
    /// </summary>
    public static class ExceptionManager
    {
        public static ServiceException HandleException(string message)
        {
            return HandleException(message, false);
        }

        public static ServiceException HandleException(string message, bool isCritical)
        {
            var exception = new ServiceException { Message = message, IsCritical = isCritical };

            // Log exception details if it is critical

            return exception;
        }

        public static ServiceException HandleException(Exception ex)
        {
            var exception = new ServiceException { Message = ex.Message, IsCritical = IsCritical(ex) };

            // Log exception details if it is critical

            return exception;
        }

        /// <summary>
        /// If error is not user defined then IsCritical should be true.
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>bool value</returns>
        private static bool IsCritical(Exception ex)
        {
            if (!(ex is SqlException))
            {
                return true;
            }
            var sqlEx = (SqlException)ex;
            return sqlEx.Number <= 50000;
        }

        public static bool HandleException(Exception ex, string policyName)
        {
            throw new NotImplementedException();
        }
    }

}
