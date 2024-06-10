using UnityEngine;
using UnityEngine.UI;

class TradeManage : MonoBehaviour
{   //플레이어에 삽입하는 클래스.
    GameObject[] tradeUISlot = new GameObject[36];
    PlayerController pCon;
    PlayerInventroy pInven;
    ItemDB itemDB;

    GameObject inventoryBar;
    GameObject tradeWindowShop;
    GameObject tradeWindowPlayer;
    private void Awake()
    {
        pCon = GetComponent<PlayerController>();
        pInven = GetComponent<PlayerInventroy>();
        tradeWindowShop = transform.Find("TradeWindowShop").gameObject;
        tradeWindowPlayer = transform.Find("TradeWindowPlayer").gameObject;
        inventoryBar = transform.Find("InventoryBarUI").gameObject;

        tradeWindowShop.SetActive(false);
        tradeWindowPlayer.SetActive(false);
    }

    private void Update()
    {
        if(pCon.trade == true)
        {
            OpenPlayerTradeWindow();
            
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                DeActivateTrade();
            }
        }

        
    }

    public void OpenPlayerTradeWindow()
    {
        for (int i = 0; i < pInven.inventSlots; i++) // 추후에 36개로
        {
            tradeUISlot[i] = transform.GetComponentsInChildren<TradeWindowPlayerInteract>()[i].gameObject;
            if (tradeUISlot[i] == null) { return; }; // 반환되는 슬롯이 없으면 실행 종료

            itemDB = new ItemDB(pInven.pInventoryItemID[i]);
            tradeUISlot[i].GetComponentInChildren<Text>().text = $"{itemDB.name}";
            tradeUISlot[i].GetComponentInChildren<TradeWindowPlayerInteract>().inventoryitemID = pInven.pInventoryItemID[i]; //ui슬롯 0번에는 pinventory0번째 아이템의 아이디가 삽입된다.
            tradeUISlot[i].GetComponentInChildren<TradeWindowPlayerInteract>().inventoryitemcount = pInven.pInventoryItemCount[i]; //ui슬롯 0번에는 pinventory0번째 아이템의 갯수가 삽입된다.
            tradeUISlot[i].GetComponentInChildren<TradeWindowPlayerInteract>().thisInvenToryNumber = i;
        }
    }

    public void ActivateTrade()
    {
        tradeWindowShop.SetActive(true);
        tradeWindowPlayer.SetActive(true);
        inventoryBar.SetActive(false);
        GetComponent<PlayerController>().Trade(true);
    }
    public void DeActivateTrade()
    {
        tradeWindowShop.SetActive(false);
        tradeWindowPlayer.SetActive(false);
        inventoryBar.SetActive(true);
        GetComponent<PlayerController>().Trade(false);
    }
}
