public class FieldStoneObjectDB
{
    int iD;
    public int toolType;
    public int toolLevel;
    public int hp;
    public int items;
    public int[] itemID; // ����ϴ� �������� ���̵�
    public int[] dropnumber; // ����ϴ� �������� ��
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
                this.stoneName = "��";
                this.items = 2; //���� Ȯ���� ��ź
                itemID = new int[items];
                itemID[0] = 11; //�� ID
                itemID[1] = 12; //�� ID
                dropnumber = new int[items];
                dropnumber[0] = 1;
                dropnumber[1] = 1; // Ȯ���� ���
                return;
            case 2:
                this.toolType = 4;
                this.toolLevel = 2;
                this.hp = 10;
                this.stoneName = "����";
                this.items = 1; // ��
                itemID = new int[items];
                itemID[0] = 11; //�� ID
                dropnumber = new int[items];
                dropnumber[0] = 15; // �� ����
                return;
            case 3:
                this.toolType = 4;
                this.toolLevel = 1;
                this.hp = 5;
                this.stoneName = "��������";
                this.items = 1;
                itemID = new int[items];
                itemID[0] = 13; //���� ID
                dropnumber = new int[items];
                dropnumber[0] = 1;
                return;
            case 4:
                
                return;
        }
    }
}
