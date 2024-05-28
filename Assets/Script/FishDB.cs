using System;

class FishDB
{
    public int fishID;
    string fishName;
    public int fishDifficulty;
    public int fishPercentage;
    // int appearSeason;
    // int appearWeather;
    // int appearTime;
    public FishDB(int i)
    {
        FishSetting(i);
    }

    void FishSetting(int i)
    {
        fishID = i;
        switch (i)
        {
            case 0:
                return;
            case 1:
                fishName = "SpringEasyFish";
                fishDifficulty = 10;
                fishPercentage = 90;
                return;
            case 2:
                fishName = "SpringHardFish";
                fishDifficulty = 80;
                fishPercentage = 10;
                return;
            case 3:
                fishName = "SummerEasyFish";
                fishDifficulty = 10;
                fishPercentage = 90;
                return;
            case 4:
                fishName = "SummerHardFish";
                fishDifficulty = 80;
                fishPercentage = 10;
                return;
            case 5:
                fishName = "FallEasyFish";
                fishDifficulty = 10;
                fishPercentage = 90;
                return;
            case 6:
                fishName = "FallHardFish";
                fishDifficulty = 80;
                fishPercentage = 10;
                return;
            case 7:
                fishName = "WinterEasyFish";
                fishDifficulty = 10;
                fishPercentage = 90;
                return;
            case 8:
                fishName = "WinterHardFish";
                fishDifficulty = 80;
                fishPercentage = 10;
                return;
            case 9:
                fishName = "DayFish";
                fishDifficulty = 10;
                fishPercentage = 50;
                return;
            case 10:
                fishName = "NightFish";
                fishDifficulty = 10;
                fishPercentage = 50;
                return;
        }
    }
}