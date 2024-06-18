class FieldTreeObjectDb
{
    int iD;
    public int toolType;    //���� ����
    public int toolLevel;   //��������
    public int maxHp;          //ü��
    public int items;       //��� ������ ��
    public int maxLevel;    //����ܰ�
    public int[] itemID;    //����ϴ� �������� ���̵�
    public int[] droprate;  //����� . 200�۸� 2�� �̷���.
    public string treeName; //�����̸�

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
                this.treeName = "������";
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
                this.treeName = "��ǳ����";
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
                this.treeName = "�ҳ���";
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
                //    this.treeName = "�ܴ��ѳ���";
                //    this.items = 1;
                //    itemID = new int[items];
                //    itemID[0] = 2; //�ܴ��� ����
                //    dropnumber = new int[items];
                //    dropnumber[0] = 5;
                //    return;

                //case 5: //��������
                //    this.toolType = 1;
                //    this.toolLevel = 1;
                //    this.maxHp = 1;
                //    this.treeName = "��������";
                //    this.items = 1;
                //    itemID = new int[items];
                //    itemID[0] = 1; // Wood
                //    droprate = new int[items];
                //    return;
        }
    }
}