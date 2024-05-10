using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FarmLandControl : MonoBehaviour
{
    PlayerInventroy playerInventroy;
    ItemDB onHandItem;
    float toolType = 2;
    protected bool digged = false;
    [SerializeField]
    
    private void Awake()
    {
        
    }
    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInventroy = collision.GetComponentInParent<PlayerInventroy>();
        onHandItem = new ItemDB(playerInventroy.currentInventoryItem);
        onHandItem.itemSetting();
        
        if (onHandItem.toolType == this.toolType && collision.tag == "Tool")
        {
            digged = true;
        }
    }
}
