using UnityEngine;

class SellBoxObject : MonoBehaviour
{
    SellBoxManager sellBoxManager;
    private void Awake()
    {
        //sellBoxManager = GameObject.FindGameObjectWithTag("SellBoxManager").GetComponent<SellBoxManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OpenSellBoxUI(collision);
    }

    //우클릭시 플레이어 인벤토리가 하단에 오픈.
    private void OpenSellBoxUI(Collider2D collision)
    {
        if (collision.tag == "RightClick" && collision.transform.parent.tag == "Player")
        {
            GameObject playerObject = collision.transform.parent.gameObject;
            SellBoxManager sellBoxManager = GameObject.FindWithTag("SellBoxManager").GetComponent<SellBoxManager>();
            sellBoxManager.OpenSellBox(playerObject);
        }
    }


    private void SlotLeftClick()
    {
        //UI에서 슬롯을 우클릭 하면, 아이템이 통째로 SellBox에 들어감.
        //슬롯에 부착될 메서드이며, SellBoxManager의 AddItem.
    }

    private void SlotRightClick()
    {
        //UI에서 슬롯을 우클릭 하면, 아이템이 한개씩 SellBox에 들어감.
        //슬롯에 부착될 메서드이며, SellBoxManager의 AddItem.
    }

    private void LastSellSlotClick()
    {
        //마지막으로 넣은 아이템은 플레이어 인벤토리 중앙 상단에 표시됨.
        //마지막으로 넣은 아이템은 좌클릭 하면 통째로 빼올 수 있음.
        //아이템을 빼 오면, 중앙 상단에 표시된 아이템은 사라짐.
        //마지막으로 넣은 아이템을 제외하고 빼올 수 없음.
    }
}