using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

class TradeWindowShopInteract : MonoBehaviour
{   // 오브젝트에 삽입되어 버튼의 이벤트 함수로 호출.
    // 버튼 클릭시(아이템 박스를 클릭시), 아이템을 구매하여 마우스에 든다.
    // 마우스에 카운트를 1 올린다.
    // 클릭할때마다 플레이어의 돈을 가격만큼 줄인다.
    // 데이터는 부모오브젝트에를 통해야한다.

    [SerializeField] int invNum; // 현재 게임오브젝트의 번호 0~3;

    GameObject mouseCursor;
    PlayerController pCon;
    PlayerInventroy playerInventroy;
    TradeWindow tradeWindow;
    ItemDB ItemDB;

    private void Awake()
    {
        pCon = transform.parent.parent.GetComponent<PlayerController>();
        mouseCursor = transform.parent.parent.transform.Find("Cursor").gameObject;
        playerInventroy = transform.parent.parent.GetComponent<PlayerInventroy>();
        tradeWindow = transform.parent.GetComponent<TradeWindow>();
    }

    private void Update()
    {
        ItemDB = new ItemDB(tradeWindow.sellList[invNum]);
    }
    public void BuyItem()
    {   //버튼이 클릭 되었을때 실행할 메서드.
        if (mouseCursor.GetComponent<MyPlayerCursor>().itemOnHand == false)
        {
            if(pCon.currentGold < ItemDB.buyPrice)
            {
                return;
            }

            mouseCursor.GetComponent<MyPlayerCursor>().itemID = tradeWindow.sellList[invNum];
            mouseCursor.GetComponent<MyPlayerCursor>().itemCounts++;
            mouseCursor.GetComponentInChildren<Text>().text = ItemDB.name;

            pCon.currentGold = pCon.currentGold - ItemDB.buyPrice;
            mouseCursor.GetComponent<MyPlayerCursor>().itemOnHand = true;
        }
        else if (mouseCursor.GetComponent<MyPlayerCursor>().itemOnHand == true
            && mouseCursor.GetComponent<MyPlayerCursor>().itemID == tradeWindow.sellList[invNum])
        {
            if (pCon.currentGold < ItemDB.buyPrice)
            {
                return;
            }

            mouseCursor.GetComponent<MyPlayerCursor>().itemCounts++;
            pCon.currentGold = pCon.currentGold - ItemDB.buyPrice;
        }
    }
}
