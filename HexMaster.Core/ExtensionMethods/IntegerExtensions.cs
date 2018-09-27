namespace HexMaster.Keesz.Core.ExtensionMethods
{
    public static class IntegerExtensions
    {

        public static int CalculatePages(this int totalEntries, int pageSize)
        {
            return (int)System.Math.Ceiling((double)totalEntries / pageSize);
        }

        public static int CalculatePages(this long totalEntries, int pageSize)
        {
            return (int)System.Math.Ceiling((double)totalEntries / pageSize);
        }
    }
}