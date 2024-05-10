using UnityEngine;

public class FarmLandControl : MonoBehaviour
{
    PlayerInventroy playerInventroy;
    ItemDB onHandItem;
    float toolType = 2;
    protected bool digged=false;

    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInventroy = collision.GetComponentInParent<PlayerInventroy>();
        onHandItem = new ItemDB(playerInventroy.currentInventoryItem);
        onHandItem.itemSetting();

        if (onHandItem.toolType == this.toolType)
        {
                Debug.Log("¶¥À» ÆÍ´Ù.");   
            digged = true;
        }
    }
}
