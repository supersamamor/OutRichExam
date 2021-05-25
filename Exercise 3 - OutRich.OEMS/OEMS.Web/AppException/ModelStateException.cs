using System;

namespace OEMS.Web.AppException
{
    public class ModelStateException : Exception 
    {
        public ModelStateException(string message) : base(message)
        { 
        }
    }
}
