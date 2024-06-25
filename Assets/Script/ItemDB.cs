[System.Serializable]
class ItemDB
{
    protected int itemID;
    public int hpRestore=0; // 무기 및 도구류일 경우 데미지로 사용
    public int staminaRestor=0; // 도구류일 경우 스테미너 소모로 사용
    public int sellPrice = 0;
    public int buyPrice = 0;
    public int grade = 0;
    public int toolType = 0;
    public int season;
    public string name = "";
    public string type = "";
    public bool eatable = false;

    // 씨앗류 아이템은 자라는데 걸리는 시간, 성장단계 등을 추가로 DB로 가져야 한다

    // 아이템 속성이 들어간다

    public ItemDB(int itemID)
    {
        this.itemID = itemID;
        itemSetting();
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
				type = "Material";
				return;
			case 2:
				sellPrice = 20;
				buyPrice = 40;
				name = "HardWood";
				type = "Material";
				return;
			case 3:
				sellPrice = 1;
				name = "Sap";
				type = "Material";
				return;
			case 4:
				name = "Axe";
				type = "Tool";
				grade = 1;
				toolType = 1;
                staminaRestor = 1;
                hpRestore = 1;
				return;
            case 5:
                name = "Hoe";
                type = "Tool";
                grade = 5;
                toolType = 2;
                staminaRestor = 1;
                return;
            case 6:
                name = "WateringCan";
                type = "Tool";
                grade = 5;
                toolType = 3;
                staminaRestor = 1;
                return;
            case 7:
                name = "PickAxe";
                type = "Tool";
                grade = 1;
                toolType = 4;
                staminaRestor = 1;
                hpRestore = 1;
                return;
            case 8:
                name = "Sickle";
                type = "Tool";
                grade = 1;
                toolType = 5;
                return;
            case 9:
                name = "SteelAxe";
                type = "Tool";
                grade = 3;
                toolType = 1;
                staminaRestor = 1;
                hpRestore = 2;
                return;
            case 10:
                name = "SteelPickAxe";
                type = "Tool";
                grade = 3;
                toolType = 4;
                staminaRestor = 1;
                hpRestore = 2;
                return;
            case 11:
                sellPrice = 5;
                buyPrice = 10;
                name = "Stone"; // 돌
                type = "Material";
                return;
            case 12:
                sellPrice = 10;
                buyPrice = 20;
                name = "Coal"; // 석탄
                type = "Material";
                return;
            case 13:
                sellPrice = 10;
                buyPrice = 20;
                name = "CopperOre"; // 구리
                type = "Material";
                return;
            case 14:
                sellPrice = 10;
                buyPrice = 20;
                name = "SpringCrop1Seed"; // 봄작물 1
                type = "Seed";
                season = 0;
                return;
            case 15:
                sellPrice = 20;
                buyPrice = 40;
                name = "SpringCrop2Seed"; // 봄작물2
                type = "Seed";
                season = 0;
                return;
            case 16:
                sellPrice = 11;
                buyPrice = 22;
                name = "SummerCrop1Seed"; // 여름작물1
                type = "Seed";
                season = 1;
                return;
            case 17:
                sellPrice = 22;
                buyPrice = 44;
                name = "SummerCrop2Seed"; // 여름작물2
                type = "Seed";
                season = 1;
                return;
            case 18:
                sellPrice = 13;
                buyPrice = 26;
                name = "FallCrop1Seed"; // 가을작물1
                type = "Seed";
                season = 2;
                return;
            case 19:
                sellPrice = 26;
                buyPrice = 52;
                name = "FallCrop2Seed"; // 가을작물2
                type = "Seed";
                season = 2;
                return;
            case 20:
                sellPrice = 100;
                buyPrice = 200;
                name = "FishingRod"; // 낚싯대
                type = "Tool";
                grade = 1; // 1레벨 낚싯대
                toolType = 9; // 9번 툴 낚싯대
                staminaRestor = 10; //스테소모
                return;


            case 50:
                sellPrice = 10;
                buyPrice = 0;
                name = "GreenSlimeDrop"; // 초록슬라임 드랍
                type = "Material";
                return;



            case 101://봄작물1 열매
                name = "SpringCrop1";
                sellPrice = 10;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 102:
                name = "SpringCrop2";
                sellPrice = 10;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 103:
                name = "SummerCrop1";
                sellPrice = 10;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 104:
                name = "SummerCrop2";
                sellPrice = 10;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 105:
                name = "FallCrop1";
                sellPrice = 10;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 106://가을작물2 열매
                name = "FallCrop2";
                sellPrice = 10;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;

            case 151://봄채집 1
                name = "SpringGathering1";
                sellPrice = 10;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 152://봄채집 2
                name = "SpringGathering2";
                sellPrice = 11;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 153://봄채집 3
                name = "SpringGathering3";
                sellPrice = 12;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 154://봄채집 4
                name = "SpringGathering4";
                sellPrice = 13;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 155://여름 채집 1
                name = "SummerGathering1";
                sellPrice = 20;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 156://여름 채집 2
                name = "SummerGathering2";
                sellPrice = 22;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 157://여름 채집 3
                name = "SummerGathering3";
                sellPrice = 23;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 158://가을 채집 1
                name = "FallGathering1";
                sellPrice = 31;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 159://가을 채집 2
                name = "FallGathering2";
                sellPrice = 32;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 160://가을 채집 3
                name = "FallGathering3";
                sellPrice = 33;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 161://겨울 채집 1
                name = "WinterGathering1";
                sellPrice = 41;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 162://겨울 채집 2
                name = "WinterGathering2";
                sellPrice = 42;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;


            case 201: //참나무 씨앗
                name = "OakTreeSeed";
                sellPrice = 10;
                buyPrice = 0;
                type = "TreeSeed";
                return;
            case 202: //단풍나무 씨앗
                name = "MapleTreeSeed";
                sellPrice = 10;
                buyPrice = 0;
                type = "TreeSeed";
                return;
            case 203: //소나무 씨앗
                name = "PineTreeSeed";
                sellPrice = 10;
                buyPrice = 0;
                type = "TreeSeed";
                return;

            case 301:
                name = "Weed";
                sellPrice = 10;
                buyPrice = 0;
                type = "Material";
                return;

            case 401:
                name = "SpringEasyFish";
                sellPrice = 100;
                buyPrice = 0;
                hpRestore = 10;
                staminaRestor = 10;
                type = "Fish";
                eatable = true;
                return;
            case 402:
                name = "SpringHardFish";
                sellPrice = 1000;
                buyPrice = 0;
                hpRestore = 100;
                staminaRestor = 100;
                type = "Fish";
                eatable = true;
                return;
            case 403:
                name = "SummerEasyFish";
                sellPrice = 100;
                buyPrice = 0;
                hpRestore = 10;
                staminaRestor = 10;
                type = "Fish";
                eatable = true;
                return;
            case 404:
                name = "SummerHardFish";
                sellPrice = 1000;
                buyPrice = 0;
                hpRestore = 100;
                staminaRestor = 100;
                type = "Fish";
                eatable = true;
                return;
            case 405:
                name = "FallEasyFish";
                sellPrice = 100;
                buyPrice = 0;
                hpRestore = 10;
                staminaRestor = 10;
                type = "Fish";
                eatable = true;
                return;
            case 406:
                name = "FallHardFish";
                sellPrice = 1000;
                buyPrice = 0;
                hpRestore = 100;
                staminaRestor = 100;
                type = "Fish";
                eatable = true;
                return;
            case 407:
                name = "WinterEasyFish";
                sellPrice = 100;
                buyPrice = 0;
                hpRestore = 10;
                staminaRestor = 10;
                type = "Fish";
                eatable = true;
                return;
            case 408:
                name = "WinterHardFish";
                sellPrice = 1000;
                buyPrice = 0;
                hpRestore = 100;
                staminaRestor = 100;
                type = "Fish";
                eatable = true;
                return;
            case 409:
                name = "DayFish";
                sellPrice = 500;
                buyPrice = 0;
                hpRestore = 50;
                staminaRestor = 50;
                type = "Fish";
                eatable = true;
                return;
            case 410:
                name = "NightFish";
                sellPrice = 500;
                buyPrice = 0;
                hpRestore = 50;
                staminaRestor = 50;
                type = "Fish";
                eatable = true;
                return;
        }
	}
}