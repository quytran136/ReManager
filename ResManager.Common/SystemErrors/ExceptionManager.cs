using ResManager.Common.Message;
using System;

namespace ResManager.Common.SystemErrors
{
    public class ExceptionManager : Exception
    {
        public ExceptionManager()
        {
        }
        public ExceptionManager(string message) : base(message)
        {
        }

        public static string GetSystemMessage(string ErrCode, params object[] param)
        {
            string value = SystemExceptionMessage.ResourceManager.GetString(ErrCode);
            return String.Format(value, param);
        }

        public static string GetSystemMessage(string ErrCode)
        {
            return SystemExceptionMessage.ResourceManager.GetString(ErrCode);
        }

        public static ExceptionManager GetBusinessMessage()
        {
            return new ExceptionManager();
        }
    }
}
