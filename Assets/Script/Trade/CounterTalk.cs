using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

class CounterTalk : MonoBehaviour//카운터에 말을 걸었을 때, 상점 주인이 있는 경우 상점창 출력.
{
    bool ChasherOn = false;

    UnityEvent OpenTradeWindow;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(ChasherOn && collision.tag == "RightClick" && collision.transform.parent.tag == "Player")
        {
            OpenTradeWindow.Invoke();
        }
    }

    //void TradeWindow()
    //{
    //    // 1. 숨겨진 게임 오브젝트를 부르기.
    //    // 2. 상점창을 프리팹으로 만들어 불러오기.
    //    // 상점창은 기록된 다양한 정보들을 반영해 물품을 불러온다.
    //}
}