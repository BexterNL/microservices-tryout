namespace HexMaster.Core.Contracts
{
    public interface IRandomizer
    {
        string GenerateVerificationCode();
        int GetRandomNumber(int max, int min = 0);
    }
}