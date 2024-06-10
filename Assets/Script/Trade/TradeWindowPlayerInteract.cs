using UnityEngine;
using UnityEngine.UI;

class TradeWindowPlayerInteract : MonoBehaviour
{
    ItemDB inventoryitemDB;
    public int inventoryitemID;
    public int inventoryitemcount;
    public int thisInvenToryNumber;// 이 인벤토리의 번호 (0~35)
    GameObject mouseCursor;
    PlayerInventroy playerInventroy;
    PlayerController pCon;

    private void Awake()
    {
        pCon = transform.parent.parent.parent.GetComponent<PlayerController>();
        mouseCursor = transform.parent.parent.parent.Find("Cursor").gameObject;
        playerInventroy = transform.parent.parent.parent.GetComponent<PlayerInventroy>();
    }

    private void Update()
    {
        inventoryitemDB = new ItemDB(inventoryitemID);
        inventoryitemDB.itemSetting();
        ImageSetting(inventoryitemDB.name);
    }

    public void ClickSlot()
    {   //버튼이 눌렸을때 실행되는 메소드.
        if (mouseCursor.GetComponent<MyPlayerCursor>().itemOnHand == false)
        {
            SellItem();
        }
        else
        {
            PlaceItem();
        }
    }

    public void ImageSetting(string itemName)
    {   //경로에서 주어진 이름과 같은 이름의 이미지를 자식 오브젝트의 이미지에 넣어라.

        if (SpriteManager.Instance.GetSprite(itemName) != null)
        {
            transform.GetChild(1).GetComponent<Image>().sprite = SpriteManager.Instance.GetSprite(itemName);
            transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            transform.GetChild(1).GetComponent<Image>().sprite = null;
            transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }

    void SellItem()
    {   // 마우스가 비어있는 상태로 아이템이 있는 슬롯을 클릭했을때
        pCon.currentGold += inventoryitemcount * inventoryitemDB.sellPrice; //아이템 갯수 * 판매가 만큼 돈을 획득한다.
        
        //이후 슬롯을 비운다.
        playerInventroy.outerImportedSlotNumber = thisInvenToryNumber;
        playerInventroy.outerImportedID = 0;
        playerInventroy.outerImportedCount = 0;
        playerInventroy.outerDataImported = true;
    }

    void PlaceItem()
    {   // 마우스가 비어있지 않은 상태로 아이템이있는||없는 슬롯을 클릭했을때
        if (this.inventoryitemID == 0)
        {   //내 아이템 인벤토리가 비어있다면.
            //마우스의 아이템을 인벤토리에 놓는다.
            playerInventroy.outerImportedSlotNumber = thisInvenToryNumber;
            playerInventroy.outerImportedID = mouseCursor.GetComponent<MyPlayerCursor>().itemID;
            playerInventroy.outerImportedCount = mouseCursor.GetComponent<MyPlayerCursor>().itemCounts;
            playerInventroy.outerDataImported = true;

            mouseCursor.GetComponent<MyPlayerCursor>().itemID = 0;                  //마우스의 아이템 아이디를 0으로 만든다.
            mouseCursor.GetComponent<MyPlayerCursor>().itemCounts = 0; //마우스의 아이템 갯수를 0으로 만든다.
            mouseCursor.GetComponentInChildren<Text>().text = "";                              //마우스의 텍스트를 제거한다.

            mouseCursor.GetComponent<MyPlayerCursor>().itemOnHand = false;          //마우스에 아이템이 더 이상 없다.
        }
        else if (this.inventoryitemID == mouseCursor.GetComponent<MyPlayerCursor>().itemID)
        {
            playerInventroy.outerImportedSlotNumber = thisInvenToryNumber;
            playerInventroy.outerImportedID = mouseCursor.GetComponent<MyPlayerCursor>().itemID;
            playerInventroy.outerImportedCount = mouseCursor.GetComponent<MyPlayerCursor>().itemCounts + this.inventoryitemcount;
            playerInventroy.outerDataImported = true;

            mouseCursor.GetComponent<MyPlayerCursor>().itemID = 0;
            mouseCursor.GetComponent<MyPlayerCursor>().itemCounts = 0;
            mouseCursor.GetComponentInChildren<Text>().text = "";

            mouseCursor.GetComponent<MyPlayerCursor>().itemOnHand = false;
        }
        else
        {   //내 아이템 인벤토리가 무언가로 차있다면.
            //마우스의 아이템과 인벤토리의 아이템을 교환한다.
            int myItemID = inventoryitemID;
            int myItemCount = inventoryitemcount;
            string myItemText = this.GetComponentInChildren<Text>().text;

            playerInventroy.outerImportedSlotNumber = thisInvenToryNumber;
            playerInventroy.outerImportedID = mouseCursor.GetComponent<MyPlayerCursor>().itemID;
            playerInventroy.outerImportedCount = mouseCursor.GetComponent<MyPlayerCursor>().itemCounts;
            playerInventroy.outerDataImported = true;

            mouseCursor.GetComponent<MyPlayerCursor>().itemID = myItemID;             //마우스의 아이템 아이디를 저장된 나의 아이템 아이디로 만든다.
            mouseCursor.GetComponent<MyPlayerCursor>().itemCounts = myItemCount; //마우스의 아이템 갯수를 저장된 나의 아이템 갯수로 만든다.
            mouseCursor.GetComponentInChildren<Text>().text = myItemText;                     //마우스의 텍스트를 저장된 나의 텍스트로 만든다.
        }
    }
}