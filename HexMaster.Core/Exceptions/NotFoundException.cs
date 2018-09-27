using System;

namespace HexMaster.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entity, string id, Exception ex =null) : base(
            $"Object of type {entity} with id {id} was not found", ex)
        {
        }

    }
}
