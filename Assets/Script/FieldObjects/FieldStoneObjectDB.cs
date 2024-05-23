public class FieldStoneObjectDB
{
    int iD;
    public int toolType;
    public int toolLevel;
    public int hp;
    public int items;
    public int[] itemID; // 드랍하는 아이템의 아이디
    public int[] dropnumber; // 드랍하는 아이템의 수
    public string stoneName;

    public FieldStoneObjectDB(int iD)
    {
        this.iD = iD;
    }

    public void FieldStoneObjectSetting()
    {
        switch (iD)
        {
            case 1:
                this.toolType = 4;
                this.toolLevel = 1;
                this.hp = 1;
                this.stoneName = "돌";
                this.items = 2; //돌과 확률적 석탄
                itemID = new int[items];
                itemID[0] = 11; //돌 ID
                itemID[1] = 12; //돌 ID
                dropnumber = new int[items];
                dropnumber[0] = 1;
                dropnumber[1] = 1; // 확률적 계산
                return;
            case 2:
                this.toolType = 4;
                this.toolLevel = 2;
                this.hp = 10;
                this.stoneName = "바위";
                this.items = 1; // 돌
                itemID = new int[items];
                itemID[0] = 11; //돌 ID
                dropnumber = new int[items];
                dropnumber[0] = 15; // 돌 갯수
                return;
            case 3:
                this.toolType = 4;
                this.toolLevel = 1;
                this.hp = 5;
                this.stoneName = "구리광맥";
                this.items = 1;
                itemID = new int[items];
                itemID[0] = 13; //구리 ID
                dropnumber = new int[items];
                dropnumber[0] = 1;
                return;
            case 4:
                
                return;
        }
    }
}
