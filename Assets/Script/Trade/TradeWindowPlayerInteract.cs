﻿using UnityEngine;
using UnityEngine.UI;

class TradeWindowPlayerInteract : MonoBehaviour
{
    ItemDB inventoryitemDB;
    public int inventoryitemID;
    public int inventoryitemcount;
    public int inventoryitemgrade;
    public int thisInvenToryNumber;// 이 인벤토리의 번호 (0~35)
    GameObject mouseCursor;
    PlayerInventroy playerInventroy;
    PlayerController pCon;

    private void Awake()
    {
        pCon = transform.parent.parent.parent.GetComponent<PlayerController>();
        mouseCursor = FindFirstObjectByType<MyPlayerCursor>().gameObject;
        playerInventroy = transform.parent.parent.parent.GetComponent<PlayerInventroy>();
    }

    private void Update()
    {
        SpriteTextSet();
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

    private void SpriteTextSet()
    {
        string itemname;
        inventoryitemDB = new ItemDB(inventoryitemID);

        if (inventoryitemID != 0)
        {
            ItemDB itemDB = new ItemDB(inventoryitemID);
            itemname = itemDB.name;
        }
        else { itemname = ""; }

        if (inventoryitemID == 0 || inventoryitemID == null)
        {   //Sprite & Text => Delete, transparent
            transform.GetChild(0).GetComponent<Image>().sprite = null;
            transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
            transform.GetChild(1).GetComponent<Text>().text = "";
            transform.GetChild(2).GetComponent<Image>().sprite = null;
            transform.GetChild(2).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        else
        {
            if (inventoryitemcount == 1)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = SpriteManager.Instance.GetSprite(itemname);
                transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                transform.GetChild(1).GetComponent<Text>().text = "";
                transform.GetChild(2).GetComponent<Image>().sprite = SpriteManager.Instance.GetSprite(gradeToString(inventoryitemgrade));
                transform.GetChild(2).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().sprite = SpriteManager.Instance.GetSprite(itemname);
                transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                transform.GetChild(1).GetComponent<Text>().text = $"{inventoryitemcount}";
                transform.GetChild(2).GetComponent<Image>().sprite = SpriteManager.Instance.GetSprite(gradeToString(inventoryitemgrade));
                transform.GetChild(2).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
    }


    void SellItem()
    {   // 마우스가 비어있는 상태로 아이템이 있는 슬롯을 클릭했을때
        pCon.currentGold += (int)(inventoryitemcount * inventoryitemDB.sellPrice * (1 + inventoryitemgrade * 0.1f)); //아이템 갯수 * 판매가 만큼 돈을 획득한다. 
        
        //이후 슬롯을 비운다.
        playerInventroy.outerImportedSlotNumber = thisInvenToryNumber;
        playerInventroy.outerImportedID = 0;
        playerInventroy.outerImportedCount = 0;
        playerInventroy.outerImportedGrade = 0;
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
            playerInventroy.outerImportedGrade = mouseCursor.GetComponent<MyPlayerCursor>().itemGrade;
            playerInventroy.outerDataImported = true;

            mouseCursor.GetComponent<MyPlayerCursor>().itemID = 0;                  //마우스의 아이템 아이디를 0으로 만든다.
            mouseCursor.GetComponent<MyPlayerCursor>().itemCounts = 0; //마우스의 아이템 갯수를 0으로 만든다.
            mouseCursor.GetComponent<MyPlayerCursor>().itemGrade = 0;
            mouseCursor.GetComponentInChildren<Text>().text = "";                              //마우스의 텍스트를 제거한다.

            mouseCursor.GetComponent<MyPlayerCursor>().itemOnHand = false;          //마우스에 아이템이 더 이상 없다.
        }
        else if (this.inventoryitemID == mouseCursor.GetComponent<MyPlayerCursor>().itemID && this.inventoryitemgrade == mouseCursor.GetComponent<MyPlayerCursor>().itemGrade)
        {
            playerInventroy.outerImportedSlotNumber = thisInvenToryNumber;
            playerInventroy.outerImportedID = mouseCursor.GetComponent<MyPlayerCursor>().itemID;
            playerInventroy.outerImportedGrade = mouseCursor.GetComponent<MyPlayerCursor>().itemGrade;
            playerInventroy.outerImportedCount = mouseCursor.GetComponent<MyPlayerCursor>().itemCounts + this.inventoryitemcount;
            playerInventroy.outerDataImported = true;

            mouseCursor.GetComponent<MyPlayerCursor>().itemID = 0;
            mouseCursor.GetComponent<MyPlayerCursor>().itemCounts = 0;
            mouseCursor.GetComponent<MyPlayerCursor>().itemGrade = 0;
            mouseCursor.GetComponentInChildren<Text>().text = "";

            mouseCursor.GetComponent<MyPlayerCursor>().itemOnHand = false;
        }
        else
        {   //내 아이템 인벤토리가 무언가로 차있다면.
            //마우스의 아이템과 인벤토리의 아이템을 교환한다.
            int myItemID = inventoryitemID;
            int myItemCount = inventoryitemcount;
            int myItemGrade = inventoryitemgrade;
            string myItemText = this.GetComponentInChildren<Text>().text;

            playerInventroy.outerImportedSlotNumber = thisInvenToryNumber;
            playerInventroy.outerImportedID = mouseCursor.GetComponent<MyPlayerCursor>().itemID;
            playerInventroy.outerImportedGrade = mouseCursor.GetComponent<MyPlayerCursor>().itemGrade;
            playerInventroy.outerImportedCount = mouseCursor.GetComponent<MyPlayerCursor>().itemCounts;
            playerInventroy.outerDataImported = true;

            mouseCursor.GetComponent<MyPlayerCursor>().itemID = myItemID;             //마우스의 아이템 아이디를 저장된 나의 아이템 아이디로 만든다.
            mouseCursor.GetComponent<MyPlayerCursor>().itemCounts = myItemCount; //마우스의 아이템 갯수를 저장된 나의 아이템 갯수로 만든다.
            mouseCursor.GetComponent<MyPlayerCursor>().itemGrade = myItemGrade;
            mouseCursor.GetComponentInChildren<Text>().text = myItemText;                     //마우스의 텍스트를 저장된 나의 텍스트로 만든다.
        }
    }

    private string gradeToString(int grade)
    {
        if (grade == 1)
        {
            return "SilverGrade";
        }
        else if (grade == 2)
        {
            return "GoldGrade";
        }
        else if (grade == 3)
        {
            return "IridiumGrade";
        }
        else { return "NoneGrade"; }
    }
}