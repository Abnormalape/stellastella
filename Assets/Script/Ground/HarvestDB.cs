using System;
using Unity;
using static UnityEditor.Progress;


class HarvestDB // 수확물의 데이터베이스.
{
    int iD;
    public int items; // 종류수
    public int[] itemID; // 드랍하는 아이템의 아이디
    public int[] dropnumber; // 드랍하는 아이템의 수
    public string cropName;

    public HarvestDB(int iD) // 작물 아이디
    {
        this.iD = iD;
    }

    public void HarvestDBSetting()
    {
        switch (iD)
        {
            case 1:
                this.cropName = "봄작물1";
                this.items = 2;
                itemID = new int[items];
                itemID[0] = 101; // 봄작물1 열매 101번
                itemID[1] = 102; // 봄작물1 씨앗 102번
                dropnumber = new int[items];
                dropnumber[0] = 1; // 1개
                dropnumber[1] = 1; // 1개
                return;
            case 2:
                this.cropName = "봄작물2";
                this.items = 1;
                itemID = new int[items];
                itemID[0] = 103;// 봄작물2 열매 103번
                dropnumber = new int[items];
                dropnumber[0] = 1; // 랜덤개
                return;
            case 3:
                this.cropName = "여름작물1";
                this.items = 2;
                itemID = new int[items];
                itemID[0] = 101; // 봄작물1 열매 101번
                itemID[1] = 102; // 봄작물1 씨앗 102번
                dropnumber = new int[items];
                dropnumber[0] = 1; // 1개
                dropnumber[1] = 1; // 1개
                return;
            case 4:
                this.cropName = "여름작물2";
                this.items = 1;
                itemID = new int[items];
                itemID[0] = 103;// 봄작물2 열매 103번
                dropnumber = new int[items];
                dropnumber[0] = 1; // 랜덤개
                return;
            case 5:
                this.cropName = "가을작물1";
                this.items = 2;
                itemID = new int[items];
                itemID[0] = 101; // 봄작물1 열매 101번
                itemID[1] = 102; // 봄작물1 씨앗 102번
                dropnumber = new int[items];
                dropnumber[0] = 1; // 1개
                dropnumber[1] = 1; // 1개
                return;
            case 6:
                this.cropName = "가을작물2";
                this.items = 1;
                itemID = new int[items];
                itemID[0] = 103;// 봄작물2 열매 103번
                dropnumber = new int[items];
                dropnumber[0] = 1; // 랜덤개
                return;
        }
    }
}

