using UnityEngine;

public class SellBoxControl : MonoBehaviour
{
    Inventory sellBoxInventory;
    int lastItem;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.GetComponentInParent<PlayerInventroy>() != null)  // 플레이어 인벤토리를 가진 녀석은 플레이어 밖에 없으니까
        {
            // 인벤토리를 여는 메서드
            // 가시적인 인벤토리가 필요한 시점이군
        }
    }
}
