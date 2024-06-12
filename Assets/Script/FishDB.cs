using System;

class FishDB
{
    public int fishID { get; private set; }
    public string fishName { get; private set; }
    public int fishDifficulty { get; private set; }
    public int fishPercentage { get; private set; }


    public bool[] season { get; private set; } = new bool[4]; //0:봄, 1:여름, 2:가을, 3:겨울
    public bool[] weather { get; private set; } = new bool[3]; //0:맑음, 1:비, 2:폭풍
    public int[] time { get; private set; } = { 6 , 26 }; //기본 06 ~ 26;
    public bool[] place { get; private set; } = new bool[3]; //bool[0]:강 = true || false, 1=바다 2=호수

    public FishDB(int i)
    {
        FishSetting(i);
    }

    void FishSetting(int i)
    {
        
        switch (i)
        {
            case 0:
                return;
            case 1:
                fishName = "SpringEasyFish";

                fishID = 401;

                fishDifficulty = 10;
                fishPercentage = 90;

                season[0] = true;
                season[1] = false;
                season[2] = false;
                season[3] = false;

                weather[0] = true;
                weather[1] = true;
                weather[2] = true;

                place[0] = true;
                place[1] = true;
                place[2] = true;
                return;
            case 2:
                fishName = "SpringHardFish";

                fishID = 402;

                fishDifficulty = 80;
                fishPercentage = 10;
                season[0] = true;
                season[1] = false;
                season[2] = false;
                season[3] = false;

                weather[0] = true;
                weather[1] = true;
                weather[2] = true;

                place[0] = true;
                place[1] = true;
                place[2] = true;
                return;
            case 3:
                fishName = "SummerEasyFish";

                fishID = 403;

                fishDifficulty = 10;
                fishPercentage = 90;
                season[0] = false;
                season[1] = true;
                season[2] = false;
                season[3] = false;

                weather[0] = true;
                weather[1] = true;
                weather[2] = true;

                place[0] = true;
                place[1] = true;
                place[2] = true;
                return;
            case 4:
                fishName = "SummerHardFish";

                fishID = 404;

                fishDifficulty = 80;
                fishPercentage = 10;
                season[0] = false;
                season[1] = true;
                season[2] = false;
                season[3] = false;

                weather[0] = true;
                weather[1] = true;
                weather[2] = true;

                place[0] = true;
                place[1] = true;
                place[2] = true;
                return;
            case 5:
                fishName = "FallEasyFish";

                fishID = 405;

                fishDifficulty = 10;
                fishPercentage = 90;
                season[0] = false;
                season[1] = false;
                season[2] = true;
                season[3] = false;

                weather[0] = true;
                weather[1] = true;
                weather[2] = true;

                place[0] = true;
                place[1] = true;
                place[2] = true;
                return;
            case 6:
                fishName = "FallHardFish";

                fishID = 406;

                fishDifficulty = 80;
                fishPercentage = 10;
                season[0] = false;
                season[1] = false;
                season[2] = true;
                season[3] = false;

                weather[0] = true;
                weather[1] = true;
                weather[2] = true;

                place[0] = true;
                place[1] = true;
                place[2] = true;
                return;
            case 7:
                fishName = "WinterEasyFish";

                fishID = 407;

                fishDifficulty = 10;
                fishPercentage = 90;
                season[0] = false;
                season[1] = false;
                season[2] = false;
                season[3] = true;

                weather[0] = true;
                weather[1] = true;
                weather[2] = true;

                place[0] = true;
                place[1] = true;
                place[2] = true;
                return;
            case 8:
                fishName = "WinterHardFish";

                fishID = 408;

                fishDifficulty = 80;
                fishPercentage = 10;
                season[0] = false;
                season[1] = false;
                season[2] = false;
                season[3] = true;

                weather[0] = true;
                weather[1] = true;
                weather[2] = true;

                place[0] = true;
                place[1] = true;
                place[2] = true;
                return;
            case 9:
                fishName = "DayFish";

                fishID = 409;

                fishDifficulty = 10;
                fishPercentage = 50;
                season[0] = true;
                season[1] = true;
                season[2] = true;
                season[3] = true;

                weather[0] = true;
                weather[1] = true;
                weather[2] = true;

                time[0] = 6;
                time[1] = 16;

                place[0] = true;
                place[1] = true;
                place[2] = true;
                return;
            case 10:
                fishName = "NightFish";

                fishID = 410;

                fishDifficulty = 10;
                fishPercentage = 50;
                season[0] = true;
                season[1] = true;
                season[2] = true;
                season[3] = true;

                weather[0] = true;
                weather[1] = true;
                weather[2] = true;

                time[0] = 16;
                time[1] = 26;

                place[0] = true;
                place[1] = true;
                place[2] = true;
                return;
        }
    }
}