//using System;
//using System.Diagnostics;

//class BigInteger : IDisposable
//{
//    public void Dispose()
//    {
//        GC.SuppressFinalize(this);
//    }

//    public static string[] displayLabel =
//    {
//        "a","b","z"
//    };

//    private const int maxLength = 70;

//    public static readonly int[] primesBelow2000 =
//    {   //소수
//        0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31
//    };

//    private unit[] data = null;

//    public int dataLength;
//    private float minority = 0;

//    public BigInteger()
//    {
//        data = new unit[maxLength];
//        dataLength = 1;
//    }

//    public BigInteger(long value)
//    {
//        data = new unit[maxLength];
//        long tempVal = value;

//        dataLength = 0;

//        while (value != 0 && dataLength < maxLength)
//        {
//            data[dataLength] = (unit)(value & 0xFFFFFFFF);
//            value >>= 32;
//            dataLength++;
//        }

//        if (tempVal > 0)
//        {
//            if (value != 0 || (data[maxLength - 1] & 0x80000000 != 0))
//                // Debug.LogError("양수오버플로우");
//        }
//        else if (tempVal < 0)
//        {
//            if (value != -1 || (data[maxLength - 1] &))
//        }
//    }

//    //#region static 상수
//    //public static readonly BigInteger MinusOne = new BigInteger(-1);
//    //public static readonly BigInteger Zero = new BigInteger(0);
//    //public static readonly BigInteger One = new BigInteger(1);
//    //public static readonly BigInteger Two = new BigInteger(2);
//    //public static readonly BigInteger Ten = new BigInteger(10);
//}
