using System;
using System.Collections.Generic;
using System.Text;
using HexMaster.Core.Contracts;

namespace HexMaster.Core.Services
{
    public class RandomizerService : IRandomizer
    {

        private Random _random;
        private string alphanumericPool = "0123456789abcdefghijklmnopqrstuvwxyz";

        public string GenerateVerificationCode()
        {
            var verificationCode = new StringBuilder();
            while (verificationCode.Length < 8)
            {
                verificationCode.Append(alphanumericPool.Substring(_random.Next(0, alphanumericPool.Length), 1));
            }

            return verificationCode.ToString();
        }
        public int GetRandomNumber(int max, int min = 0)
        {
            return _random.Next(min, max);
        }
        public RandomizerService()
        {
            var ticks = DateTime.UtcNow.Ticks;
            while (ticks > int.MaxValue)
            {
                ticks = ticks / 10;
            }
            _random = new Random((int)ticks);
        }
    }
}
