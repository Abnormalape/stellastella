class Inventory // ���⿡ ������ �������� �з��Ǿ�� �Ѵ�
                // 1�������� ������ ID�� �з��Ǿ�� �Ѵ�.
{
    public int itemID;
    public int grade;
    public int itemCount;
    public Inventory(int itemID) // �ܼ��ϰ� ID�� �޴� ��� : ����, ��, ���� ���� �͵�
    {
        this.itemID = itemID;
    }
    public Inventory(int itemID, int grade) // ���۹�, ä����, ����ǰ, ���� �� ����� �����ϴ� �͵�
    {
        this.itemID = itemID;
        this.grade = grade;
    }
}