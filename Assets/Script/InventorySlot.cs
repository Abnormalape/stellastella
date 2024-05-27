using System;
using Unity;
using UnityEngine;
using UnityEngine.UI;


class InventorySlot : MonoBehaviour
{
    ItemDB inventoryitemDB;
    public int inventoryitemID;

    GameObject mouseCursor;

    private void Awake()
    {
        mouseCursor = GameObject.FindGameObjectWithTag("Cursor");
    }
    private void Update()
    {
        inventoryitemDB = new ItemDB(inventoryitemID);
        inventoryitemDB.itemSetting();
    }
    public void ClickSlot()
    {
        if(mouseCursor.GetComponent<MyPlayerCursor>().itemOnHand == false) 
        {
            GetItemUp();
        }
        else
        {
            GetItemDown();
        }
    }

    void GetItemUp()
    {   // 마우스가 비어있는 상태로 아이템이 있는 슬롯을 클릭했을때
        if (this.inventoryitemID != 0) 
        {
            mouseCursor.GetComponent<MyPlayerCursor>().itemID = inventoryitemID;
            mouseCursor.AddComponent<Text>().text = inventoryitemDB.name;
            mouseCursor.GetComponent <MyPlayerCursor>().itemOnHand = true;
            this.inventoryitemID = 0;
            this.inventoryitemDB.name = "";
        }
    }

    void GetItemDown()
    {   // 마우스가 비어있지 않은 상태로 아이템이있는||없는 슬롯을 클릭했을때
        if (this.inventoryitemID == 0)
        {   //내 아이템 인벤토리가 비어있다면.
            //마우스의 아이템을 인벤토리에 놓는다.
            inventoryitemID = mouseCursor.GetComponent<MyPlayerCursor>().itemID;    //마우스의 아이템 아이디를 인벤토리의 아이템 아이디로 만든다.
            this.GetComponent<Text>().text = mouseCursor.GetComponent<Text>().text; //마우스의 텍스트를 나의 텍스트로 만든다.
            mouseCursor.GetComponent<MyPlayerCursor>().itemID = 0;                  //마우스의 아이템 아이디를 0으로 만든다.
            Destroy(mouseCursor.GetComponent<Text>());                              //마우스의 텍스트를 파괴한다.
            mouseCursor.GetComponent<MyPlayerCursor>().itemOnHand = false;          //마우스에 아이템이 더 이상 없다.
        }
        else
        {   //내 아이템 인벤토리가 무언가로 차있다면.
            //마우스의 아이템과 인벤토리의 아이템을 교환한다.
            int myItemID = inventoryitemID;
            string myItemText = this.GetComponent<Text>().text;

            inventoryitemID = mouseCursor.GetComponent<MyPlayerCursor>().itemID;      //마우스의 아이템 아이디를 인벤토리의 아이템 아이디로 만든다.
            this.GetComponent<Text>().text = mouseCursor.GetComponent<Text>().text; //마우스의 텍스트를 나의 텍스트로 만든다.
            mouseCursor.GetComponent<MyPlayerCursor>().itemID = myItemID;             //마우스의 아이템 아이디를 저장된 나의 아이템 아이디로 만든다.
            mouseCursor.GetComponent<Text>().text = myItemText;                     //마우스의 텍스트를 저장된 나의 텍스트로 만든다.
        }
    }
}

