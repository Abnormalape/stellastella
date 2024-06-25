using UnityEngine;


public class PlayerInventroy : MonoBehaviour // 플레이어에게 부착된다
{
    Inventory[] pInventory = new Inventory[36]; // 36칸의 인벤토리
    public int[] pInventoryItemID { get; private set; } = new int[36];
    public int[] pInventoryItemCount { get; private set; } = new int[36];
    public int[] pInventoryItemGrade { get; private set; } = new int[36];
    ItemDB currentItemDB; // 아이템 정보 호출용
    [SerializeField] public int currentInventory; // 현재인벤
    [SerializeField] public int currentInventoryItem; // 현재인벤의 아이템ID
    [SerializeField] public int itemCount; // 현재 인벤의 아이템 수

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


    void MakePlayerInventory() // 시작할때 주는 도구 = 1회성
    {
        for (int i = 8; i < 36; i++)
        {
            pInventory[i] = new Inventory(0);
        }

        pInventory[0] = new Inventory(4); // 아이템ID 4 = 도끼
        pInventory[1] = new Inventory(5); // 아이템ID 5 = 괭이
        pInventory[2] = new Inventory(6); // 아이템ID 6 = 물뿌리개
        pInventory[3] = new Inventory(7); // 아이템ID 7 = 곡괭이
        pInventory[4] = new Inventory(8); // 아이템ID 8 = 낫
        pInventory[5] = new Inventory(9); // 아이템ID 9 = 강화도끼
        pInventory[6] = new Inventory(10); // 아이템ID 10 = 강화곡괭이
        pInventory[7] = new Inventory(20); // 아이템ID 20 = 낚싯대, 툴 ID=9
        pInventory[8] = new Inventory(14); // 봄작물1 씨앗 ,10개.
        for (int i = 0; i < 8; i++) { pInventory[i].itemCount = 1; }
        pInventory[8].itemCount = 10;
    }

    private void Awake()
    {
        pCon = GetComponent<PlayerController>();
        PLClick = this.GetComponent<PlayerLeftClick>();
        MakePlayerInventory(); // 기본아이템을 생성한다
    }
    void Start()
    {
        currentInventory = 0;
        //현재 아이템 = 현재 인벤토리의 아이템ID
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
        ChangeInventory(); // 이것을 바탕으로 플레이어와 오브젝트가 상호작용
        InventoryItemData();
        UpdateCounts();
        OuterData();
    }
    void ChangeInventory() // 인벤토리를 바꾸고 아이템을 선택.
    {
        if (pCon.idle || pCon.moving)
        {
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

            currentInventoryItem = pInventory[currentInventory].itemID; // 아이템ID 호출
            currentItemDB = new ItemDB(currentInventoryItem); // ID를 기반으로 정보 호출
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
            outerImportedSlotNumber = -1; // 오류 발생용.
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
    {   //필드의 아이템을 먹는 메소드

        if (collision.gameObject.tag == "FieldItem") // 충돌체가 아이템 이라면
        {
            for (int i = 0; i < inventSlots ; i++) // 인벤토리를 훑어서
            {
                if (collision.GetComponent<FieldItem>().itemID == pInventory[i].itemID && 
                    collision.GetComponent<FieldItem>().grade == pInventory[i].grade) // 같은 ID와 등급을 보유중인 인벤토리에
                {
                    pInventory[i].itemCount += 1; // 카운트를 올리고
                    Destroy(collision.gameObject); // 그놈을 제거하고
                    return; // 메서드 종료
                }
            }
            for (int i = 0; i < inventSlots; i++) // 일치가 하나도 안된다면
            {
                if (pInventory[i].itemID == 0) // 인벤토리의 아이템 아이디가 비어있는곳을 찾아 (개인적으론 null쓰고싶긴한데)
                {
                    pInventory[i].itemID = collision.gameObject.GetComponent<FieldItem>().itemID; // 그 인벤토리의 아이템ID를 충돌체의 ID로 바꾸고
                    pInventory[i].grade = collision.gameObject.GetComponent<FieldItem>().grade; // 그 인벤토리의 아이템 등급을 충돌체의 등급으로 바꾸고
                    pInventory[i].itemCount += 1; // 카운트를 올린다 (0이었으니까)
                    Destroy(collision.gameObject);
                    return; // 메서드 종료   
                }
            }
            //비어있지도 않고, 일치하는 것도 없으면, 인벤토리가 가득 찼다는 알림을 전송
            return; // 그냥 종료
        }
    }

    public int AddSlotNumber { get; private set; }
    public void AddDirectItem(int itemID, int grade, int count)
    {   //바로 인벤토리에 들어오는 메소드. 인벤토리가 꽉 찼을시, 교환창을 연다.
        for (int i = 0; i < inventSlots ; i++)
        {
            if (itemID == pInventory[i].itemID && 
                grade == pInventory[i].grade)
            {
                pInventory[i].itemCount += count;
                pInventoryItemCount[i] = pInventory[i].itemCount;
                AddSlotNumber = i;
                return; // 메서드 종료
            }
        }
        // 검색된 값이 없다면 빈것을 찾아서
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
                return; // 메서드 종료   
            }
        }
        //인벤토리에 검색되는 것도 없고 빈칸도 없다면
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
    {   //현재 아이템 아이디를 출력.
        return currentInventory;
    }
    public int currentItemToolType()
    {   //현재 아이템 도구타입을 출력.
        return currentItemDB.toolType;
    }

    public string currentItemType()
    {   //현재 아이템의 타입을 반환
        return currentItemDB.type;
    }

    GameObject inventroyUI;
    GameObject inventroyBarUI;
    void OpenInventory()
    {
        if (pCon.moving || pCon.idle) // 움직이거나 정지해 있을때만 인벤토리를 열수 있다.
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pCon.Inventroy(true);

                // 인벤토리 창을 만들어야 하겠다
                // 인벤토리오브젝트.SetActive
                // 인벤토리 오브젝트가 켜짐

                inventroyUI.SetActive(true); // 플레이어한테 인벤토리 달아야함
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