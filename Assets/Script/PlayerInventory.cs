using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventroy : MonoBehaviour
{
    Inventory[] pInventory;
    public int currentInventoryItem; //null�� �����ؾ� �Ѵ�.
    int currentInventory; // �����κ� ��ȣ
    void MakePlayerInventory()
    {
        pInventory = new Inventory[12];// �κ��丮�� 12ĭ
        pInventory[0] = new Inventory(4); // ������ID 4 = ����
        pInventory[1] = new Inventory(5); // ������ID 5 = ����
        pInventory[2] = new Inventory(6); // ������ID 6 = ���Ѹ���
        pInventory[3] = new Inventory(7); // ������ID 7 = ���
        pInventory[4] = new Inventory(8); // ������ID 8 = ��
        pInventory[5] = new Inventory(9); // ������ID 9 = ��ȭ����
        pInventory[6] = new Inventory(10); // ������ID 10 = ��ȭ���
    }
    
    void Start()
    {
        MakePlayerInventory(); // �÷��̾� �κ��丮�� �����Ѵ�
        currentInventory = 0;
        currentInventoryItem = pInventory[currentInventory].itemID; // �����κ��丮�� ������ ������ currentInventory���κ��丮�� ���� ������ID�̴�.
    }
    void Update()
    {
        currentInventoryItem = pInventory[currentInventory].itemID;
        if (Input.GetKeyDown(KeyCode.Alpha1)) { currentInventory = 0; } // ���� 1��Ű�� �����ٸ� �÷��̾��κ�[0]�� ������ID�� ���� �������̴�. �װ� 1234567890-=�� �ݺ��Ѵ�.
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
        } //�ж�
        else if (wheelscroll < 0)
        {
            currentInventory = currentInventory + 1;
            if (currentInventory == 12) { currentInventory = 0; }
        } //��涧

    }
}