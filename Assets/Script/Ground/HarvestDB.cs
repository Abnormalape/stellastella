using System;
using Unity;
using static UnityEditor.Progress;


class HarvestDB // 수확물의 데이터베이스.
{
    int iD;
    public int items; // 종류수
    public int[] itemID; // 드랍하는 아이템의 아이디
    public int[] dropnumber; // 드랍하는 아이템의 수
    public int[] itemName;
    public string cropName;
    ItemDB itemDB;

    public HarvestDB(int iD) // 작물 아이디
    {
        this.iD = iD;
    }

    public void HarvestDBSetting()
    {
        switch (iD)
        {
            case 14:
                this.cropName = "봄작물1";
                this.items = 2;
                itemID = new int[items];
                itemID[0] = 101; // 봄작물1 열매 101번
                itemID[1] = 14; // 봄작물1 씨앗
                dropnumber = new int[items];
                dropnumber[0] = 1; // 1개
                dropnumber[1] = 1; // 1개
                return;
            case 15:
                this.cropName = "봄작물2";
                this.items = 1;
                itemID = new int[items];
                itemID[0] = 102;
                dropnumber = new int[items];
                dropnumber[0] = 1; // 랜덤개
                return;
            case 16:
                this.cropName = "여름작물1";
                this.items = 2;
                itemID = new int[items];
                itemID[0] = 103; 
                itemID[1] = 16; 
                dropnumber = new int[items];
                dropnumber[0] = 1; // 1개
                dropnumber[1] = 1; // 1개
                return;
            case 17:
                this.cropName = "여름작물2";
                this.items = 1;
                itemID = new int[items];
                itemID[0] = 104;
                dropnumber = new int[items];
                dropnumber[0] = 1; // 랜덤개
                return;
            case 18:
                this.cropName = "가을작물1";
                this.items = 2;
                itemID = new int[items];
                itemID[0] = 105; 
                itemID[1] = 18; 
                dropnumber = new int[items];
                dropnumber[0] = 1; // 1개
                dropnumber[1] = 1; // 1개
                return;
            case 19:
                this.cropName = "가을작물2";
                this.items = 1;
                itemID = new int[items];
                itemID[0] = 106;
                dropnumber = new int[items];
                dropnumber[0] = 1; // 랜덤개
                return;
        }
    }
}

