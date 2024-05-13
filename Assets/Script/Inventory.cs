class Inventory // 여기에 들어오는 아이템은 분류되어야 한다
                // 1차적으로 아이템 ID로 분류되어야 한다.
{
    public int itemID;
    public int grade;
    public int itemCount;
    public Inventory(int itemID) // 단순하게 ID만 받는 경우 : 나무, 돌, 수액 같은 것들
    {
        this.itemID = itemID;
    }
    public Inventory(int itemID, int grade) // 농작물, 채집물, 가공품, 생선 등 등급이 존재하는 것들
    {
        this.itemID = itemID;
        this.grade = grade;
    }
}