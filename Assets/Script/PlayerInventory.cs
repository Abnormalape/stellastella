using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventroy : MonoBehaviour
{
    Inventory[] pInventory;
    public int currentInventoryItem; //null이 가능해야 한다.
    int currentInventory; // 현재인벤 번호
    void MakePlayerInventory()
    {
        pInventory = new Inventory[12];// 인벤토리는 12칸
        pInventory[0] = new Inventory(4); // 아이템ID 4 = 도끼
        pInventory[1] = new Inventory(5); // 아이템ID 5 = 괭이
        pInventory[2] = new Inventory(6); // 아이템ID 6 = 물뿌리개
        pInventory[3] = new Inventory(7); // 아이템ID 7 = 곡괭이
        pInventory[4] = new Inventory(8); // 아이템ID 8 = 낫
        pInventory[5] = new Inventory(9); // 아이템ID 9 = 강화도끼
        pInventory[6] = new Inventory(10); // 아이템ID 10 = 강화곡괭이
    }
    
    void Start()
    {
        MakePlayerInventory(); // 플레이어 인벤토리를 생성한다
        currentInventory = 0;
        currentInventoryItem = pInventory[currentInventory].itemID; // 현재인벤토리가 선택한 물건은 currentInventory번인벤토리가 가진 아이템ID이다.
    }
    void Update()
    {
        currentInventoryItem = pInventory[currentInventory].itemID;
        if (Input.GetKeyDown(KeyCode.Alpha1)) { currentInventory = 0; } // 만약 1번키를 누른다면 플레이어인벤[0]의 아이템ID가 현재 아이템이다. 그걸 1234567890-=로 반복한다.
        if (Input.GetKeyDown(KeyCode.Alpha2)) { currentInventory = 1; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { currentInventory = 2; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { currentInventory = 3; }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { currentInventory = 4; }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { currentInventory = 5; }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { currentInventory = 6; }
        if (Input.GetKeyDown(KeyCode.Alpha8)) { currentInventory = 7; }
        if (Input.GetKeyDown(KeyCode.Alpha9)) { currentInventory = 8; }
        if (Input.GetKeyDown(KeyCode.Alpha0)) { currentInventory = 9; }
        if (Input.GetKeyDown(KeyCode.Minus)) { currentInventory = 10; }
        if (Input.GetKeyDown(KeyCode.Equals)) { currentInventory = 11; }

        float wheelscroll = Input.GetAxis("Mouse ScrollWheel");
        if (wheelscroll > 0)
        {
            currentInventory = currentInventory - 1;
            if (currentInventory == -1) { currentInventory = 11; }
        } //밀때
        else if (wheelscroll < 0)
        {
            currentInventory = currentInventory + 1;
            if (currentInventory == 12) { currentInventory = 0; }
        } //당길때

    }
}