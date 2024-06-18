class FieldTreeObjectDb
{
    int iD;
    public int toolType;    //도구 종류
    public int toolLevel;   //도구레벨
    public int maxHp;          //체력
    public int items;       //드랍 아이템 수
    public int maxLevel;    //성장단계
    public int[] itemID;    //드랍하는 아이템의 아이디
    public int[] droprate;  //드랍율 . 200퍼면 2개 이런식.
    public string treeName; //나무이름

    public FieldTreeObjectDb(int iD)
    {
        this.iD = iD;
        FieldTreeObjectSetting();
    }

    public void FieldTreeObjectSetting()
    {
        switch (iD)
        {
            case 1:
                this.toolType = 1;
                this.toolLevel = 1;
                this.maxHp = 15;
                this.treeName = "참나무";
                this.items = 3;
                itemID = new int[items];
                itemID[0] = 1; // Wood
                itemID[1] = 3; // Sap
                itemID[2] = 201; // OakTreeSeed
                droprate = new int[items];
                return;
            case 2:
                this.toolType = 1;
                this.toolLevel = 1;
                this.maxHp = 15;
                this.treeName = "단풍나무";
                this.items = 3;
                itemID = new int[items];
                itemID[0] = 1; // Wood
                itemID[1] = 3; // Sap
                itemID[2] = 202; // MapleTreeSeed
                droprate = new int[items];
                return;
            case 3:
                this.toolType = 1;
                this.toolLevel = 1;
                this.maxHp = 15;
                this.treeName = "소나무";
                this.items = 3;
                itemID = new int[items];
                itemID[0] = 1; // Wood
                itemID[1] = 3; // Sap
                itemID[2] = 203; // PineTreeSeed
                droprate = new int[items];
                return;
                //case 4:
                //    this.toolType = 1;
                //    this.toolLevel = 2;
                //    this.hp = 20;
                //    this.treeName = "단단한나무";
                //    this.items = 1;
                //    itemID = new int[items];
                //    itemID[0] = 2; //단단한 나무
                //    dropnumber = new int[items];
                //    dropnumber[0] = 5;
                //    return;

                //case 5: //나무도막
                //    this.toolType = 1;
                //    this.toolLevel = 1;
                //    this.maxHp = 1;
                //    this.treeName = "나무도막";
                //    this.items = 1;
                //    itemID = new int[items];
                //    itemID[0] = 1; // Wood
                //    droprate = new int[items];
                //    return;
        }
    }
}