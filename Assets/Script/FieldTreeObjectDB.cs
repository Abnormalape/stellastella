class FieldTreeObjectDb
{
    int iD;
    public int toolType;
    public int toolLevel;
    public int hp;
    public int items;
    public int[] itemID; // ����ϴ� �������� ���̵�
    public int[] dropnumber; // ����ϴ� �������� ��
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
				this.treeName = "������";
				this.items = 2;
				itemID = new int[items];
				itemID[0] = 1; //����
				itemID[1] = 3; //����
				dropnumber = new int[items];
				dropnumber[0] = 10;
				dropnumber[1] = 5;
				return;
			case 2:
                this.toolType = 1;
                this.toolLevel = 1;
                this.hp = 10;
                this.treeName = "��ǳ����";
                this.items = 2;
                itemID = new int[items];
                itemID[0] = 1; //����
                itemID[1] = 3; //����
                dropnumber = new int[items];
                dropnumber[0] = 10;
                dropnumber[1] = 5;
                return;
			case 3:
                this.toolType = 1;
                this.toolLevel = 1;
                this.hp = 10;
                this.treeName = "�ҳ���";
                this.items = 2;
                itemID = new int[items];
                itemID[0] = 1; //����
                itemID[1] = 3; //����
                dropnumber = new int[items];
                dropnumber[0] = 10;
                dropnumber[1] = 5;
                return;
            case 4:
                this.toolType = 1;
                this.toolLevel = 2;
                this.hp = 20;
                this.treeName = "�ܴ��ѳ���";
                this.items = 1;
                itemID = new int[items];
                itemID[0] = 2; //�ܴ��� ����
                dropnumber = new int[items];
                dropnumber[0] = 5;
                return;
        }
	}
}