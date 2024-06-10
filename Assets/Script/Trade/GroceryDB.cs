enum whenSell
{
    spring,
    summer,
    fall,
    winter,
    mon,
    tue,
    wed,
    thu,
    fri,
    sat,
    sun,
    always,
}


class GroceryDB // 잡화점 전용 데이터.
{
    public int sellID;
    public int itemID;
    public whenSell whenSell;

    GroceryDB(int sellID)
    {
        this.sellID = sellID;
        itemSetting();
    }
    public void itemSetting()
    {
        switch (this.sellID)
        {
            case 0:
                return;
            case 1:
                itemID = 14;
                whenSell = whenSell.spring;
                return;
            case 2:
                itemID = 15;
                whenSell = whenSell.spring;
                return;
            case 3:
                itemID = 16;
                whenSell = whenSell.summer;
                return;
            case 4:
                itemID = 17;
                whenSell = whenSell.summer;
                return;
            case 5:
                itemID = 18;
                whenSell = whenSell.fall;
                return;
            case 6:
                itemID = 19;
                whenSell = whenSell.fall;
                return;
        }
    }
}