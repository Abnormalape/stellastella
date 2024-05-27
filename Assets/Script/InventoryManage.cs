using System;
using Unity;
using UnityEngine;
using UnityEngine.UI;

class InventoryManage : MonoBehaviour
{
    GameObject[] inventoryUISlot = new GameObject[36];
    GameManager gameManager;
    PlayerInventroy pInven;
    ItemDB itemDB;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        pInven = GameObject.Find("Player").GetComponent<PlayerInventroy>(); // 추후에 인벤매니지 자체를 플레이어에 이식
    }

    private void Update()
    {
        if (gameManager.isInventoryOn)
        {
            LoadInventory();
        }
        else
        {
            LoadOffInventory();
        }
    }

    bool loadInventroy = false;
    void LoadInventory()
    {

        for (int i = 0; i < 12; i++)
        {
            int j;
            j = i / 12;

            inventoryUISlot[i] = transform.GetComponentsInChildren<InventorySlot>()[i].gameObject;
            if (inventoryUISlot[i] == null) { return; }; // 반환되는 슬롯이 없으면 실행 종료

            itemDB = new ItemDB(pInven.pInventoryCount[i]);
            inventoryUISlot[i].GetComponentInChildren<Text>().text = $"{itemDB.name}";
        }

    }

    int currentInventory;
    void LoadOffInventory()
    {
        
        for (int i = 0; i < 12; i++)
        {
            int j;
            j = i / 12;

            inventoryUISlot[i] = GameObject.Find("InventoryOffBackUI").GetComponentsInChildren<OffInventorySlot>()[i].gameObject;
            if (inventoryUISlot[i] == null) { return; }; // 반환되는 슬롯이 없으면 실행 종료

            itemDB = new ItemDB(pInven.pInventoryCount[i]);
            inventoryUISlot[i].GetComponentInChildren<Text>().text = $"{itemDB.name}";

            if(pInven.currentInventory == i)
            {
                currentInventory = i;
            }
        }
        GameObject.Find("CurrentOffInventoryUI").transform.position = inventoryUISlot[currentInventory].transform.position;

    }
}

