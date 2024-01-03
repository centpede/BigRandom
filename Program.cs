using System.Numerics;
using System.Security.Cryptography;

// Create new Random

Random r = new Random();

// Generate 100 random numbers

for (int i = 0; i < 100; i++)
{    
    BigInteger myRandom = r.GetBigRandom((BigInteger)1_000_000_000_000, (BigInteger)9_999_999_999_999);
    WriteLine($"{myRandom:N0}");
}


public static class BigRandom
{
    public static BigInteger GetBigRandom(this Random r, BigInteger minValue, BigInteger maxValue)
    {
        if( minValue > maxValue)
        {
            throw new ArgumentException("Error: maxValue must greater than minValue!" );
        }
        else if ( maxValue < 1 || minValue < 1) 
        {
            throw new ArgumentException("Error: maxValue and minValue must greater than zero!");
        }

        // Get size in bytes of maxvalue 

        int sizeInBytes = maxValue.GetByteCount();

        // fill random BigInteger
        BigInteger bigRandom = new(RandomNumberGenerator.GetBytes(sizeInBytes), isUnsigned: true);

        // fill bytes to ratio number with 0xFF
        BigInteger ratio = new((Enumerable.Repeat((byte)0xFF,sizeInBytes).ToArray()), isUnsigned: true);

        // Calculation to ensure upper limit
        ratio /= (maxValue - minValue + 1);

        while (minValue + (bigRandom / ratio) > maxValue) ratio++;

        bigRandom = minValue + (bigRandom / ratio);

        return bigRandom;
    }
}