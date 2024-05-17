using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventroy : MonoBehaviour // �÷��̾�� �����ȴ�
{
    Inventory[] pInventory = new Inventory[36]; // 36ĭ�� �κ��丮
    int[] pInventoryCount = new int[36];
    ItemDB currentItemDB; // ������ ���� ȣ���
    int currentInventory; // �����κ�
    public int currentInventoryItem; // �����κ��� ������ID
    public int itemCount; // ���� �κ��� ������ ��

    PlayerLeftClick PLClick;


    void MakePlayerInventory() // �����Ҷ� �ִ� ���� = 1ȸ��
    {
        for (int i = 8; i < 36; i++)
        {
            pInventory[i] = new Inventory(0);
        }

        pInventory[0] = new Inventory(4); // ������ID 4 = ����
        pInventory[1] = new Inventory(5); // ������ID 5 = ����
        pInventory[2] = new Inventory(6); // ������ID 6 = ���Ѹ���
        pInventory[3] = new Inventory(7); // ������ID 7 = ���
        pInventory[4] = new Inventory(8); // ������ID 8 = ��
        pInventory[5] = new Inventory(9); // ������ID 9 = ��ȭ����
        pInventory[6] = new Inventory(10); // ������ID 10 = ��ȭ���
        pInventory[7] = new Inventory(20); // ������ID 20 = ���˴�, �� ID=9
        for (int i = 0; i < 8; i++) { pInventory[i].itemCount = 1; }
    }

    private void Awake()
    {
        PLClick = this.GetComponent<PlayerLeftClick>();
        MakePlayerInventory(); // �⺻�������� �����Ѵ�
    }
    void Start()
    {
        currentInventory = 0;
        //���� ������ = ���� �κ��丮�� ������ID
        currentInventoryItem = pInventory[currentInventory].itemID;
    }
    void Update()
    {
        Debug.Log(currentInventoryItem);
        ChangeInventory(); // �̰��� �������� �÷��̾�� ������Ʈ�� ��ȣ�ۿ�
    }
    void ChangeInventory() // �κ��丮�� �ٲٰ� �������� ����, ���� ������϶��� ����
    {
        if (!PLClick.toolUsed)
        {
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

            currentInventoryItem = pInventory[currentInventory].itemID; // ������ID ȣ��
            currentItemDB = new ItemDB(currentInventoryItem); // ID�� ������� ���� ȣ��
            itemCount = pInventory[currentInventory].itemCount;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("FieldDropItem")) // �浹ü�� ������ �̶��
        {
            for (int i = 0; i < 36; i++) // �κ��丮�� �Ⱦ
            {
                if (collision.gameObject.GetComponent<FieldItem>().itemID == pInventory[i].itemID) // ���� ID�� �������� �κ��丮��
                {
                    pInventory[i].itemCount += 1; // ī��Ʈ�� �ø���
                    Destroy(collision.gameObject); // �׳��� �����ϰ�
                    return; // �޼��� ����
                }
            }
            for (int i = 0; i < 36; i++) // ��ġ�� �ϳ��� �ȵȴٸ�
            {
                if (pInventory[i].itemID == 0) // �κ��丮�� ������ ���̵� ����ִ°��� ã�� (���������� null�����ͱ��ѵ�)
                {
                    pInventory[i].itemID = collision.gameObject.GetComponent<FieldItem>().itemID; // �� �κ��丮�� ������ID�� �浹ü�� ID�� �ٲٰ�
                    pInventory[i].itemCount += 1; // ī��Ʈ�� �ø��� (0�̾����ϱ�)
                    Destroy(collision.gameObject);
                    return; // �޼��� ����
                }
            }
            return; // �׳� ����
        }
    }

    void Bags()
    {
        
    }

    void addItem(int itemID)  // �ʵ忡�� ������ �������� �κ��丮�� �ִ¹�
                    // �������� �ڽ��� ������ ������Ʈ(player gathering)�� �θ������Ʈ(player) �� ���� �ٰ�����, �� �Ÿ��� ���� ������ �Ǿ����� �Ҹ��ϸ� Playerinventory�� addItem�� �۵���Ų��.
    {
        // for���� ������ pInventory[]�� �ִ� itemID�� �񱳸� �Ѵ�
        // �׷��� pInventory[]�� ������ �ִ� itemID�� ��ġ�� �Ѵٸ�, �� pInventory�� itemCount�� 1���� ��Ų��.
        // �׸��� Ż���Ѵ�

        // ���� for���� �����ϴ� ���� ��𿡼��� ã������ �ʴ´ٸ�
        // for���� ���� ���� null�� �κ��丮�� itemID�� �ְ� itemCount�� 1������Ų��.
    }
    void addItem(int itemID, int itemGrade)  // �������� �κ��丮�� �ִ¹�
                              // �������� �ڽ��� ������ ������Ʈ(player gathering)�� �θ������Ʈ(player) �� ���� �ٰ�����, �� �Ÿ��� ���� ������ �Ǿ����� �Ҹ��ϸ� Playerinventory�� addItem�� �۵���Ų��.
    {
        // ���� ������, itemID �� itemGrade�� ���� �ֵ��� ã�´�.
    }
}