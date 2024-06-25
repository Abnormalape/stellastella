[System.Serializable]
class ItemDB
{
    protected int itemID;
    public int hpRestore=0; // ���� �� �������� ��� �������� ���
    public int staminaRestor=0; // �������� ��� ���׹̳� �Ҹ�� ���
    public int sellPrice = 0;
    public int buyPrice = 0;
    public int grade = 0;
    public int toolType = 0;
    public int season;
    public string name = "";
    public string type = "";
    public bool eatable = false;

    // ���ѷ� �������� �ڶ�µ� �ɸ��� �ð�, ����ܰ� ���� �߰��� DB�� ������ �Ѵ�

    // ������ �Ӽ��� ����

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
                name = "Stone"; // ��
                type = "Material";
                return;
            case 12:
                sellPrice = 10;
                buyPrice = 20;
                name = "Coal"; // ��ź
                type = "Material";
                return;
            case 13:
                sellPrice = 10;
                buyPrice = 20;
                name = "CopperOre"; // ����
                type = "Material";
                return;
            case 14:
                sellPrice = 10;
                buyPrice = 20;
                name = "SpringCrop1Seed"; // ���۹� 1
                type = "Seed";
                season = 0;
                return;
            case 15:
                sellPrice = 20;
                buyPrice = 40;
                name = "SpringCrop2Seed"; // ���۹�2
                type = "Seed";
                season = 0;
                return;
            case 16:
                sellPrice = 11;
                buyPrice = 22;
                name = "SummerCrop1Seed"; // �����۹�1
                type = "Seed";
                season = 1;
                return;
            case 17:
                sellPrice = 22;
                buyPrice = 44;
                name = "SummerCrop2Seed"; // �����۹�2
                type = "Seed";
                season = 1;
                return;
            case 18:
                sellPrice = 13;
                buyPrice = 26;
                name = "FallCrop1Seed"; // �����۹�1
                type = "Seed";
                season = 2;
                return;
            case 19:
                sellPrice = 26;
                buyPrice = 52;
                name = "FallCrop2Seed"; // �����۹�2
                type = "Seed";
                season = 2;
                return;
            case 20:
                sellPrice = 100;
                buyPrice = 200;
                name = "FishingRod"; // ���˴�
                type = "Tool";
                grade = 1; // 1���� ���˴�
                toolType = 9; // 9�� �� ���˴�
                staminaRestor = 10; //���׼Ҹ�
                return;


            case 50:
                sellPrice = 10;
                buyPrice = 0;
                name = "GreenSlimeDrop"; // �ʷϽ����� ���
                type = "Material";
                return;



            case 101://���۹�1 ����
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
            case 106://�����۹�2 ����
                name = "FallCrop2";
                sellPrice = 10;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;

            case 151://��ä�� 1
                name = "SpringGathering1";
                sellPrice = 10;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 152://��ä�� 2
                name = "SpringGathering2";
                sellPrice = 11;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 153://��ä�� 3
                name = "SpringGathering3";
                sellPrice = 12;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 154://��ä�� 4
                name = "SpringGathering4";
                sellPrice = 13;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 155://���� ä�� 1
                name = "SummerGathering1";
                sellPrice = 20;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 156://���� ä�� 2
                name = "SummerGathering2";
                sellPrice = 22;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 157://���� ä�� 3
                name = "SummerGathering3";
                sellPrice = 23;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 158://���� ä�� 1
                name = "FallGathering1";
                sellPrice = 31;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 159://���� ä�� 2
                name = "FallGathering2";
                sellPrice = 32;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 160://���� ä�� 3
                name = "FallGathering3";
                sellPrice = 33;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 161://�ܿ� ä�� 1
                name = "WinterGathering1";
                sellPrice = 41;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;
            case 162://�ܿ� ä�� 2
                name = "WinterGathering2";
                sellPrice = 42;
                buyPrice = 0;
                grade = 1;
                type = "Fruit";
                eatable = true;
                return;


            case 201: //������ ����
                name = "OakTreeSeed";
                sellPrice = 10;
                buyPrice = 0;
                type = "TreeSeed";
                return;
            case 202: //��ǳ���� ����
                name = "MapleTreeSeed";
                sellPrice = 10;
                buyPrice = 0;
                type = "TreeSeed";
                return;
            case 203: //�ҳ��� ����
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