class FieldTreeObjectDb
{
    int iD;
    public int toolType;
    public int toolLevel;
    public int hp;
    public int items;
    public int[] itemID; // 드랍하는 아이템의 아이디
    public int[] dropnumber; // 드랍하는 아이템의 수
    public string treeName;

    public FieldTreeObjectDb(int iD)
    {
        this.iD = iD;
    }

    public void FieldTreeObjectSetting()
    {
		switch (iD) {
			case 1:
				this.toolType = 1;
				this.toolLevel = 1;
				this.hp = 10;
				this.treeName = "참나무";
				this.items = 2;
				itemID = new int[items];
				itemID[0] = 1; //나무
				itemID[1] = 3; //수액
				dropnumber = new int[items];
				dropnumber[0] = 10;
				dropnumber[1] = 5;
				return;
			case 2:
                this.toolType = 1;
                this.toolLevel = 1;
                this.hp = 10;
                this.treeName = "단풍나무";
                this.items = 2;
                itemID = new int[items];
                itemID[0] = 1; //나무
                itemID[1] = 3; //수액
                dropnumber = new int[items];
                dropnumber[0] = 10;
                dropnumber[1] = 5;
                return;
			case 3:
                this.toolType = 1;
                this.toolLevel = 1;
                this.hp = 10;
                this.treeName = "소나무";
                this.items = 2;
                itemID = new int[items];
                itemID[0] = 1; //나무
                itemID[1] = 3; //수액
                dropnumber = new int[items];
                dropnumber[0] = 10;
                dropnumber[1] = 5;
                return;
            case 4:
                this.toolType = 1;
                this.toolLevel = 2;
                this.hp = 20;
                this.treeName = "단단한나무";
                this.items = 1;
                itemID = new int[items];
                itemID[0] = 2; //단단한 나무
                dropnumber = new int[items];
                dropnumber[0] = 5;
                return;
        }
	}
}