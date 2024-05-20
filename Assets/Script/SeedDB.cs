using System;
using Unity;
class SeedDB// 하드 코딩
{
    public int maxLevle; //최대 성장단계
    public int maxDays;
    public int reDays;
    public bool reGather;

    public SeedDB(int itemID)
    {
        switch (itemID)
        {
            case 14: //봄1
                maxLevle = 3;
                maxDays = 6;
                reGather = false;
                return;
            case 15: //봄2
                maxLevle = 4;
                maxDays = 8;
                reGather = true;
                return;
            case 16: //여름1
                maxLevle = 3;
                maxDays = 6;
                reGather = false;
                return;
            case 17: //여름2
                maxLevle = 4;
                maxDays = 8;
                reGather = true;
                return;
            case 18: //가을1
                maxLevle = 3;
                maxDays = 6;
                reGather = false;
                return;
            case 19: //가을2
                maxLevle = 4;
                maxDays = 8;
                reGather = true;
                return;

        }
    }

}

