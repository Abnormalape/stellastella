using System;
using Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


class OffInventorySlot : MonoBehaviour
{
    PlayerInventroy pInven;
    private void Awake()
    {
        pInven = transform.parent.parent.parent.GetComponent<PlayerInventroy>();
    }

    
    public void ChangeCurrentInventory()
    {   
        string i = this.gameObject.name.Replace("Inventory1_","");
        int ii = Convert.ToInt32(i);
        pInven.currentInventory = (ii-1);
    }

}

