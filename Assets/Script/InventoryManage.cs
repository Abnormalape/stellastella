using System;
using Unity;
using UnityEngine;
using UnityEngine.UI;

class InventoryManage : MonoBehaviour
{
    GameObject[] inventoryUISlot = new GameObject[36];
    PlayerController pCon;
    PlayerInventroy pInven;
    ItemDB itemDB;

    private void Awake()
    {
        pCon = GetComponent<PlayerController>();
        pInven = GetComponent<PlayerInventroy>(); 
    }

    private void Update()
    {
        if (pCon.inventory)
        {
            LoadInventory();
        }
        else if (pCon.inventory == false)
        {
            LoadOffInventory();
        }
    }

    void LoadInventory()
    {
        for (int i = 0; i < 12; i++) // 추후에 36개로
        {
            int j;
            j = i / 12;

            inventoryUISlot[i] = transform.GetComponentsInChildren<InventorySlot>()[i].gameObject;
            if (inventoryUISlot[i] == null) { return; }; // 반환되는 슬롯이 없으면 실행 종료

            itemDB = new ItemDB(pInven.pInventoryItemID[i]);
            inventoryUISlot[i].GetComponentInChildren<Text>().text = $"{itemDB.name}";
            inventoryUISlot[i].GetComponentInChildren<InventorySlot>().inventoryitemID = pInven.pInventoryItemID[i]; //ui슬롯 0번에는 pinventory0번째 아이템의 아이디가 삽입된다.
            inventoryUISlot[i].GetComponentInChildren<InventorySlot>().inventoryitemcount = pInven.pInventoryItemCount[i]; //ui슬롯 0번에는 pinventory0번째 아이템의 갯수가 삽입된다.
            inventoryUISlot[i].GetComponentInChildren<InventorySlot>().thisInvenToryNumber = i;
        }
    }

    int currentInventory;
    void LoadOffInventory()
    {
        
        for (int i = 0; i < 12; i++)
        {
            int j;
            j = i / 12;

            inventoryUISlot[i] = GameObject.Find("InventoryBarUI").GetComponentsInChildren<OffInventorySlot>()[i].gameObject;
            if (inventoryUISlot[i] == null) { return; }; // 반환되는 슬롯이 없으면 실행 종료

            itemDB = new ItemDB(pInven.pInventoryItemID[i]);
            inventoryUISlot[i].GetComponentInChildren<Text>().text = $"{itemDB.name}";

            if(pInven.currentInventory == i)
            {
                currentInventory = i;
            }
        }
        GameObject.Find("CurrentOffInventoryUI").transform.position = inventoryUISlot[currentInventory].transform.position;

    }
}

