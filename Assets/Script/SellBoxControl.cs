using UnityEngine;

public class SellBoxControl : MonoBehaviour
{
    Inventory sellBoxInventory;
    int lastItem;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.GetComponentInParent<PlayerInventroy>() != null)  // �÷��̾� �κ��丮�� ���� �༮�� �÷��̾� �ۿ� �����ϱ�
        {
            // �κ��丮�� ���� �޼���
            // �������� �κ��丮�� �ʿ��� �����̱�
        }
    }
}
