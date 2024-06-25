using UnityEngine;


public class PlayerInventroy : MonoBehaviour // �÷��̾�� �����ȴ�
{
    Inventory[] pInventory = new Inventory[36]; // 36ĭ�� �κ��丮
    public int[] pInventoryItemID { get; private set; } = new int[36];
    public int[] pInventoryItemCount { get; private set; } = new int[36];
    public int[] pInventoryItemGrade { get; private set; } = new int[36];
    ItemDB currentItemDB; // ������ ���� ȣ���
    [SerializeField] public int currentInventory; // �����κ�
    [SerializeField] public int currentInventoryItem; // �����κ��� ������ID
    [SerializeField] public int itemCount; // ���� �κ��� ������ ��

    PlayerLeftClick PLClick;
    PlayerController pCon;


    
    public int changeCount 
    {
        set { pInventory[currentInventory].itemCount += value; }
    }
    
    public void ChangeCount(int inventoryNumber, int value)
    {
        pInventory[inventoryNumber].itemCount += value;
        pInventoryItemCount[inventoryNumber] = pInventory[inventoryNumber].itemCount;

        if (pInventoryItemCount[inventoryNumber] == 0) 
        {
            pInventoryItemID[inventoryNumber] = 0;
            pInventoryItemGrade[inventoryNumber] = 0;
            pInventory[inventoryNumber].itemID = 0;
            pInventory[inventoryNumber].grade = 0;
        }
    }


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
        pInventory[8] = new Inventory(14); // ���۹�1 ���� ,10��.
        for (int i = 0; i < 8; i++) { pInventory[i].itemCount = 1; }
        pInventory[8].itemCount = 10;
    }

    private void Awake()
    {
        pCon = GetComponent<PlayerController>();
        PLClick = this.GetComponent<PlayerLeftClick>();
        MakePlayerInventory(); // �⺻�������� �����Ѵ�
    }
    void Start()
    {
        currentInventory = 0;
        //���� ������ = ���� �κ��丮�� ������ID
        currentInventoryItem = pInventory[currentInventory].itemID;
    }

    public bool outerDataImported = false;
    public int outerImportedSlotNumber;
    public int outerImportedID;
    public int outerImportedCount;
    public int outerImportedGrade;
    void Update()
    {
        InvenSlotNumbers();
        ChangeInventory(); // �̰��� �������� �÷��̾�� ������Ʈ�� ��ȣ�ۿ�
        InventoryItemData();
        UpdateCounts();
        OuterData();
    }
    void ChangeInventory() // �κ��丮�� �ٲٰ� �������� ����.
    {
        if (pCon.idle || pCon.moving)
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

    void UpdateCounts()
    {
        for (int i = 0; i < 36; i++)
        {
            pInventoryItemCount[i] = pInventory[i].itemCount;
        }
    }


    void OuterData()
    {
        if (outerDataImported == true)
        {
            pInventory[outerImportedSlotNumber].itemID = outerImportedID;
            pInventory[outerImportedSlotNumber].itemCount = outerImportedCount;
            pInventory[outerImportedSlotNumber].grade = outerImportedGrade;
            outerDataImported = false;
            outerImportedID = 0;
            outerImportedCount = 0;
            outerImportedGrade = 0;
            outerImportedSlotNumber = -1; // ���� �߻���.
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        AddFieldItem(collision);
    }

    public int inventSlots { get; private set; } = 12;
    void InvenSlotNumbers()
    {
        if (!pCon.firstBag && !pCon.secondBag)
        {
            inventSlots = 12;
        }
        else if (pCon.firstBag && !pCon.secondBag)
        {
            inventSlots = 24;
        }
        else if (pCon.firstBag && pCon.secondBag)
        {
            inventSlots = 36;
        }
    }


    public void AddFieldItem(Collider2D collision)
    {   //�ʵ��� �������� �Դ� �޼ҵ�

        if (collision.gameObject.tag == "FieldItem") // �浹ü�� ������ �̶��
        {
            for (int i = 0; i < inventSlots ; i++) // �κ��丮�� �Ⱦ
            {
                if (collision.GetComponent<FieldItem>().itemID == pInventory[i].itemID && 
                    collision.GetComponent<FieldItem>().grade == pInventory[i].grade) // ���� ID�� ����� �������� �κ��丮��
                {
                    pInventory[i].itemCount += 1; // ī��Ʈ�� �ø���
                    Destroy(collision.gameObject); // �׳��� �����ϰ�
                    return; // �޼��� ����
                }
            }
            for (int i = 0; i < inventSlots; i++) // ��ġ�� �ϳ��� �ȵȴٸ�
            {
                if (pInventory[i].itemID == 0) // �κ��丮�� ������ ���̵� ����ִ°��� ã�� (���������� null����ͱ��ѵ�)
                {
                    pInventory[i].itemID = collision.gameObject.GetComponent<FieldItem>().itemID; // �� �κ��丮�� ������ID�� �浹ü�� ID�� �ٲٰ�
                    pInventory[i].grade = collision.gameObject.GetComponent<FieldItem>().grade; // �� �κ��丮�� ������ ����� �浹ü�� ������� �ٲٰ�
                    pInventory[i].itemCount += 1; // ī��Ʈ�� �ø��� (0�̾����ϱ�)
                    Destroy(collision.gameObject);
                    return; // �޼��� ����   
                }
            }
            //��������� �ʰ�, ��ġ�ϴ� �͵� ������, �κ��丮�� ���� á�ٴ� �˸��� ����
            return; // �׳� ����
        }
    }

    public int AddSlotNumber { get; private set; }
    public void AddDirectItem(int itemID, int grade, int count)
    {   //�ٷ� �κ��丮�� ������ �޼ҵ�. �κ��丮�� �� á����, ��ȯâ�� ����.
        for (int i = 0; i < inventSlots ; i++)
        {
            if (itemID == pInventory[i].itemID && 
                grade == pInventory[i].grade)
            {
                pInventory[i].itemCount += count;
                pInventoryItemCount[i] = pInventory[i].itemCount;
                AddSlotNumber = i;
                return; // �޼��� ����
            }
        }
        // �˻��� ���� ���ٸ� ����� ã�Ƽ�
        for (int i = 0; i < 36; i++)
        {
            if (pInventory[i].itemID == 0)
            {
                pInventory[i].itemID = itemID;
                pInventoryItemID[i] = itemID;

                pInventory[i].grade = grade;
                pInventoryItemGrade[i] = grade;

                pInventory[i].itemCount = count;
                pInventoryItemCount[i] = count;

                AddSlotNumber = i;
                return; // �޼��� ����   
            }
        }
        //�κ��丮�� �˻��Ǵ� �͵� ���� ��ĭ�� ���ٸ�
        return;
    }

    void InventoryItemData()
    {
        for (int i = 0; i < 36; i++)
        {
            if (pInventory[i].itemCount <= 0)
            {
                pInventory[i].itemID = 0;
            }

            pInventoryItemID[i] = pInventory[i].itemID;
            pInventoryItemGrade[i] = pInventory[i].grade;
        }
    }

    public int currentItemID()
    {   //���� ������ ���̵� ���.
        return currentInventory;
    }
    public int currentItemToolType()
    {   //���� ������ ����Ÿ���� ���.
        return currentItemDB.toolType;
    }

    public string currentItemType()
    {   //���� �������� Ÿ���� ��ȯ
        return currentItemDB.type;
    }

    GameObject inventroyUI;
    GameObject inventroyBarUI;
    void OpenInventory()
    {
        if (pCon.moving || pCon.idle) // �����̰ų� ������ �������� �κ��丮�� ���� �ִ�.
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pCon.Inventroy(true);

                // �κ��丮 â�� ������ �ϰڴ�
                // �κ��丮������Ʈ.SetActive
                // �κ��丮 ������Ʈ�� ����

                inventroyUI.SetActive(true); // �÷��̾����� �κ��丮 �޾ƾ���
            }
        }
        if (pCon.inventory)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                pCon.Inventroy(false);

                inventroyUI.SetActive(false);
            }
        }
    }
}