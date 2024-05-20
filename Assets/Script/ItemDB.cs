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
    public string name = "";
    public string type = "";
    

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
				type = "���";
				return;
			case 2:
				sellPrice = 20;
				buyPrice = 40;
				name = "HardWood";
				type = "���";
				return;
			case 3:
				sellPrice = 1;
				name = "Sap";
				type = "���";
				return;
			case 4:
				name = "����";
				type = "����";
				grade = 1;
				toolType = 1;
                staminaRestor = 10;
                hpRestore = 1;
				return;
            case 5:
                name = "����";
                type = "����";
                grade = 5;
                toolType = 2;
                staminaRestor = 10;
                return;
            case 6:
                name = "���Ѹ���";
                type = "����";
                grade = 1;
                toolType = 3;
                staminaRestor = 10;
                return;
            case 7:
                name = "���";
                type = "����";
                grade = 1;
                toolType = 4;
                staminaRestor = 10;
                hpRestore = 1;
                return;
            case 8:
                name = "��";
                type = "����";
                grade = 1;
                toolType = 5;
                return;
            case 9:
                name = "��ȭ����";
                type = "����";
                grade = 3;
                toolType = 1;
                staminaRestor = 10;
                hpRestore = 2;
                return;
            case 10:
                name = "��ȭ���";
                type = "����";
                grade = 3;
                toolType = 4;
                staminaRestor = 10;
                hpRestore = 2;
                return;
            case 11:
                sellPrice = 5;
                buyPrice = 10;
                name = "Stone"; // ��
                type = "���";
                return;
            case 12:
                sellPrice = 10;
                buyPrice = 20;
                name = "Coal"; // ��ź
                type = "���";
                return;
            case 13:
                sellPrice = 10;
                buyPrice = 20;
                name = "CopperOre"; // ����
                type = "���";
                return;
            case 14:
                sellPrice = 10;
                buyPrice = 20;
                name = "SpringSeed1"; // ���۹� 1
                type = "����";
                return;
            case 15:
                sellPrice = 20;
                buyPrice = 40;
                name = "SpringSeed2"; // ���۹�2
                type = "����";
                return;
            case 16:
                sellPrice = 11;
                buyPrice = 22;
                name = "SummerSeed1"; // �����۹�1
                type = "����";
                return;
            case 17:
                sellPrice = 22;
                buyPrice = 44;
                name = "SummerSeed2"; // �����۹�2
                type = "����";
                return;
            case 18:
                sellPrice = 13;
                buyPrice = 26;
                name = "FallSeed1"; // �����۹�1
                type = "����";
                return;
            case 19:
                sellPrice = 26;
                buyPrice = 52;
                name = "FallSeed2"; // �����۹�2
                type = "����";
                return;
            case 20:
                sellPrice = 100;
                buyPrice = 200;
                name = "FishingRod"; // ���˴�
                type = "����";
                grade = 1; // 1���� ���˴�
                toolType = 9; // 9�� �� ���˴�
                staminaRestor = 10; //���׼Ҹ�
                return;


            case 50:
                sellPrice = 10;
                buyPrice = 0;
                name = "GreenSlimeDrop"; // �ʷϽ����� ���
                type = "���";
                return;

        }
	}
}