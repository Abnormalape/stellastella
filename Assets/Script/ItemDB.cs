[System.Serializable]
class ItemDB
{
    int itemID;
    public int hpRestore=0; // 무기 및 도구류일 경우 데미지로 사용
    public int staminaRestor=0; // 도구류일 경우 스테미너 소모로 사용
    public int sellPrice = 0;
    public int buyPrice = 0;
    public int grade = 0;
    public int toolType = 0;
    public string name = "";
    public string type = "";
    

    // 아이템 속성이 들어간다

    public ItemDB(int itemID)
    {
        this.itemID = itemID;
    }

	public void itemSetting (){
		switch (itemID)
		{
            case 0:
                return;
			case 1:
				sellPrice = 5;
				buyPrice = 10;
				name = "Wood";
				type = "재료";
				return;
			case 2:
				sellPrice = 20;
				buyPrice = 40;
				name = "HardWood";
				type = "재료";
				return;
			case 3:
				sellPrice = 1;
				name = "Sap";
				type = "재료";
				return;
			case 4:
				name = "도끼";
				type = "도구";
				grade = 1;
				toolType = 1;
                staminaRestor = 10;
                hpRestore = 1;
				return;
            case 5:
                name = "괭이";
                type = "도구";
                grade = 1;
                toolType = 2;
                staminaRestor = 10;
                return;
            case 6:
                name = "물뿌리개";
                type = "도구";
                grade = 1;
                toolType = 3;
                staminaRestor = 10;
                return;
            case 7:
                name = "곡괭이";
                type = "도구";
                grade = 1;
                toolType = 4;
                staminaRestor = 10;
                hpRestore = 1;
                return;
            case 8:
                name = "낫";
                type = "도구";
                grade = 1;
                toolType = 5;
                return;
            case 9:
                name = "강화도끼";
                type = "도구";
                grade = 3;
                toolType = 1;
                staminaRestor = 10;
                hpRestore = 2;
                return;
            case 10:
                name = "강화곡괭이";
                type = "도구";
                grade = 3;
                toolType = 4;
                staminaRestor = 10;
                hpRestore = 2;
                return;
            case 11:
                sellPrice = 5;
                buyPrice = 10;
                name = "Stone"; // 돌
                type = "재료";
                return;
            case 12:
                sellPrice = 10;
                buyPrice = 20;
                name = "Coal"; // 석탄
                type = "재료";
                return;
            case 13:
                sellPrice = 10;
                buyPrice = 20;
                name = "CopperOre"; // 구리
                type = "재료";
                return;
        }
	}
}