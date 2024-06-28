using UnityEngine;

class OpeningTradeWindow : MonoBehaviour//카운터에 말을 걸었을 때, 상점 주인이 있는 경우 상점창 출력.
{   //카운터는 본인의 아이템 품목을 플레이어가 지닌 tradewindow에 넘긴다.
    //카운터는 본인이 저장한 이미지를 tradewindow에 넘겨 portrait를 설정한다.
    [SerializeField] Sprite myOwner;
    [SerializeField] GameObject myTradeWindow;
    [SerializeField] bool CasherOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CasherOn && collision.tag == "RightClick" && collision.transform.parent.tag == "Player" 
            && GetComponent<PrintMessageBox>() == null) //메세지 박스가 없는 단순 상점일 경우만.
        {
            OpenTradeWindow(collision.transform.parent.gameObject);
        }
    }

    public void OpenTradeWindow(GameObject playerObject)
    {
        TradeManage tradeManage = playerObject.GetComponent<TradeManage>();

        tradeManage.ActivateTrade(); // activatetrade 메서드에 요소를 추가하여 원하는 거래창을 띄울 수 있다.
                                     //tradeManage.ActivateTrade(sellDB, portrait, 등등).; 

        GameObject summonedWindow = Instantiate(myTradeWindow, Vector3.zero, Quaternion.identity, this.transform);
        summonedWindow.GetComponent<TradeWindow>().portrait = myOwner;
        summonedWindow.GetComponent<TradeWindow>().pCon = playerObject.GetComponent<PlayerController>();
        summonedWindow.GetComponent<TradeWindow>().pInven = playerObject.GetComponent<PlayerInventroy>();
        // nowTradingWindow. 카운터에서 판매 물품을 거래창에 전달.
        // 즉 카운터에서 DB를 가지고 있다가 조건에 맞는 DB만 추출하여 거래창에 전달해야함.
    }
}