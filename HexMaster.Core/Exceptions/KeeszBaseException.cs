using System;
using HexMaster.Keesz.Core.Infrastructure.Enums;

namespace HexMaster.Keesz.Core.Exceptions
{
    public class KeeszBaseException : Exception
    {
        public ErrorCode Code { get; }

        public KeeszBaseException(ErrorCode code, string message, Exception innerException = null)
            : base(message, innerException)
        {
            Code = code;
        }

    }
}