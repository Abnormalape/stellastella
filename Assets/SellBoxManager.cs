using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

class SellBoxManager : MonoBehaviour
{
    //=======DataManage========//
    public List<int> ListItemID; //ID
    public List<int> ListItemCount; //Count
    public List<int> ListItemGrade; //Grade
    int lastListNum = 0;
    //=======DataManage========//

    //=======Slot========//
    GameObject[] sellBoxUISlot = new GameObject[36];
    int[] SlotItemID = new int[36];
    int[] SlotItemCount = new int[36];
    int[] SlotItemGrade = new int[36];
    //=======Slot========//

    //=======LastPut========//
    GameObject LastSellSlot;
    int LastPutID = 0;
    private int tLastPutCount = 0;
    int LastPutCount
    {
        get { return tLastPutCount; }
        set
        {
            if (value == 0)
            {
                OffLastSellSlot();
                return;
            }
            tLastPutCount = value;
            UpdateLastSellSlot();
        }
    }
    string LastPutName = "";
    int LastPutGrade = 0;
    //=======LastPut========//


    [SerializeField] bool asdf;

    private void CheckList(bool value)
    {
        if (!value)
        {
            return;
        }
        else if (value)
        {
            int i = ListItemID.Count;

            for (int j = 0; j < i; j++)
            {
                Debug.Log($"{j} ID : {ListItemID[j]} / Count : {ListItemCount[j]}");
            }
            asdf = false;
        }
    }

    void Update()
    {
        CheckList(asdf);

        if (playerObject != null)
        {
            CloseSellBox();
        }
    }

    void CloseSellBox()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            sellBoxCanvas.gameObject.SetActive(false);
            playerObject.GetComponent<PlayerController>().Conversation(false);

            playerObject = null;
        }
    }

    SpriteManager spriteManager;
    GameObject sellBoxCanvas;
    private void Awake()
    {
        AwakeReset();
    }

    void AwakeReset()
    {
        ResetList();
        spriteManager = FindAnyObjectByType<SpriteManager>();
        sellBoxCanvas = transform.GetChild(0).gameObject;

        LastSellSlot = sellBoxCanvas.GetComponentInChildren<SellBoxLastSellSlot>().gameObject;
        LastSellSlot.SetActive(false);

        sellBoxCanvas.SetActive(false);
    }
    void ResetList()
    {
        ListItemID = new List<int>();
        ListItemCount = new List<int>();
        ListItemGrade = new List<int>();
    }

    PlayerInventroy pInven;
    GameObject playerObject;

    public void OpenSellBox(GameObject playerObject)
    {   //OpenSellBox를 sellboxobject가 여는데, 그녀석이 playerobejct를 건네준다.
        this.playerObject = playerObject;
        pInven = playerObject.GetComponent<PlayerInventroy>();
        playerObject.GetComponent<PlayerController>().Conversation(true);
        sellBoxCanvas.SetActive(true);
        OpenPlayerInventory(pInven);
    }

    private void OpenPlayerInventory(PlayerInventroy pInven)
    {   //플레이어 인벤토리의 정보를 슬롯에 배치하고 보여주는 메소드.

        //temp code
        int temp;
        temp = transform.GetChild(0).GetComponentsInChildren<SellBoxSlot>().Length;

        for (int i = 0; i < pInven.inventSlots; i++) // 추후에 36개로
        {   //getchild = canvas , sellboxslot = canvas.child;
            if (i >= temp)
            {
                return;
            }
            sellBoxUISlot[i] = transform.GetChild(0).GetComponentsInChildren<SellBoxSlot>()[i].gameObject;

            ItemDB itemDB = new ItemDB(pInven.pInventoryItemID[i]);
            string gradename = gradeToString(pInven.pInventoryItemGrade[i]);

            UIData(i, itemDB, gradename, pInven);
        }
    }

    private void UIData(int i, ItemDB itemDB, string gradename, PlayerInventroy pInven)
    {
        if (pInven.pInventoryItemCount[i] > 0)
        {
            //pInven의 UI.
            sellBoxUISlot[i].transform.GetChild(0).GetComponent<Image>().sprite = spriteManager.GetSprite(itemDB.name);   //slot의 이미지.

            if (gradename != "")//slot의 등급 이미지.
            {
                sellBoxUISlot[i].transform.GetChild(1).GetComponent<Image>().sprite = spriteManager.GetSprite(gradename);
            }
            else if (gradename == "")
            {
                sellBoxUISlot[i].transform.GetChild(1).GetComponent<Image>().sprite = null;
            }

            sellBoxUISlot[i].GetComponentInChildren<Text>().text = pInven.pInventoryItemCount[i].ToString();  //slot의 텍스트(아이템 수).

            //pInven의 Data.
            sellBoxUISlot[i].GetComponent<SellBoxSlot>().thisSlotNumber = i;
            SlotItemID[i] = pInven.pInventoryItemID[i];
            SlotItemCount[i] = pInven.pInventoryItemCount[i];
            SlotItemGrade[i] = pInven.pInventoryItemGrade[i];
        }
        else
        {
            sellBoxUISlot[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
            sellBoxUISlot[i].transform.GetChild(1).GetComponent<Image>().sprite = null;
            sellBoxUISlot[i].GetComponentInChildren<Text>().text = "";

            sellBoxUISlot[i].GetComponent<SellBoxSlot>().thisSlotNumber = i;
            SlotItemID[i] = 0;
            SlotItemCount[i] = 0;
            SlotItemGrade[i] = 0;
        }
    }

    private void UpdateLastSellSlot()
    {
        if (LastPutCount > 0)
        {
            LastSellSlot.SetActive(true);
            LastSellSlot.transform.GetChild(0).GetComponent<Image>().sprite = spriteManager.GetSprite(LastPutName);
            LastSellSlot.transform.GetChild(1).GetComponent<Image>().sprite = spriteManager.GetSprite(gradeToString(LastPutGrade));
            LastSellSlot.GetComponentInChildren<Text>().text = LastPutCount.ToString();
        }
        else
        {
            LastSellSlot.SetActive(false);
        }
    }

    private string gradeToString(int grade)
    {
        if (grade == 1)
        {
            return "SilverGrade";
        }
        else if (grade == 2)
        {
            return "GoldGrade";
        }
        else if (grade == 3)
        {
            return "IridiumGrade";
        }
        else { return "NoneGrade"; }
    }

    public void AddAllItemFromSlot(int SlotNumber)
    {
        if (SlotItemCount[SlotNumber] == 0)
        {
            Debug.Log("슬롯이 비어있습니다.");
            return;
        }

        //else if : lastinput과 add된 아이템의ID, Grade를 비교해서 같다면
        if (LastPutID == SlotItemID[SlotNumber] && LastPutGrade == SlotItemGrade[SlotNumber])
        {
            Debug.Log("Same Item All");

            //lastinput에 count만큼 add.
            LastPutCount += SlotItemCount[SlotNumber];
            //마지막에 넣은 항목에 count만큼 더한값을 대치.
            ListItemCount[lastListNum] = LastPutCount;

            //slot의 count감소를 pinven에 전달.
            pInven.ChangeCount(SlotNumber, -SlotItemCount[SlotNumber]);

            //UI data 업데이트.
            UIData(SlotNumber, new ItemDB(SlotItemID[SlotNumber]), gradeToString(SlotItemGrade[SlotNumber]), pInven);
        }
        else //if : lastinput과 add된 아이템의ID, Grade를 비교해서 같지 않다면 아래 실행
        {
            Debug.Log("Different Item All");

            //List에 데이터 추가.
            ListItemID.Add(SlotItemID[SlotNumber]);
            ListItemCount.Add(SlotItemCount[SlotNumber]);
            ListItemGrade.Add(SlotItemGrade[SlotNumber]);

            //Last Input에 아이템 등록.
            LastPutID = SlotItemID[SlotNumber];
            LastPutName = new ItemDB(SlotItemID[SlotNumber]).name;
            LastPutGrade = SlotItemGrade[SlotNumber];
            LastPutCount = SlotItemCount[SlotNumber];

            lastListNum = ListItemID.Count - 1;

            //Slot의 모든 데이터 삭제 및 UI와 데이터 업데이트.
            pInven.ChangeCount(SlotNumber, -SlotItemCount[SlotNumber]); //inventory에 count를 0으로. = 데이터 삭제.
            UIData(SlotNumber, new ItemDB(SlotItemID[SlotNumber]), gradeToString(SlotItemGrade[SlotNumber]), pInven); //이후 업데이트.
        }
    }

    public void AddSigleItemFromSlot(int SlotNumber)
    {
        if (SlotItemCount[SlotNumber] == 0)
        {
            Debug.Log("슬롯이 비어있습니다.");
            return;
        }
        //else if : lastinput과 add된 아이템의ID, Grade를 비교해서 같다면
        if (LastPutID == SlotItemID[SlotNumber] && LastPutGrade == SlotItemGrade[SlotNumber])
        {
            Debug.Log("Same Item Single");
            //lastinput에 1만큼 add.
            LastPutCount += 1;
            //마지막에 넣은 항목에 count만큼 더한값을 대치.
            ListItemCount[lastListNum] = LastPutCount;
            //slot의 count감소를 pinven에 전달.
            pInven.ChangeCount(SlotNumber, -1);
            //UI data 업데이트.
            UIData(SlotNumber, new ItemDB(SlotItemID[SlotNumber]), gradeToString(SlotItemGrade[SlotNumber]), pInven);
        }
        else //if : lastinput과 add된 아이템의ID, Grade를 비교해서 같지 않다면 아래 실행
        {
            Debug.Log("Different Item Single");
            //List에 데이터 추가.
            ListItemID.Add(SlotItemID[SlotNumber]);
            ListItemCount.Add(1);
            ListItemGrade.Add(SlotItemGrade[SlotNumber]);

            //Last Input에 아이템 등록.
            LastPutID = SlotItemID[SlotNumber];
            LastPutName = new ItemDB(SlotItemID[SlotNumber]).name;
            LastPutGrade = SlotItemGrade[SlotNumber];
            LastPutCount = 1;

            lastListNum = ListItemID.Count - 1;

            //Slot의 Count 감소 및 UI와 데이터 업데이트.
            pInven.ChangeCount(SlotNumber, -1);
            UIData(SlotNumber, new ItemDB(SlotItemID[SlotNumber]), gradeToString(SlotItemGrade[SlotNumber]), pInven); //이후 업데이트;
        }
    }

    public void RestoreLastSell()
    {
        pInven.AddDirectItem(LastPutID, LastPutGrade, LastPutCount);
        LastPutCount = 0;

        ListItemCount[lastListNum] = 0;
        ListItemID[lastListNum] = 0;
        ListItemGrade[lastListNum] = 0;

        int a = pInven.AddSlotNumber;

        Debug.Log(pInven.pInventoryItemCount[a]);


        ItemDB itemDB = new ItemDB(pInven.pInventoryItemID[a]);
        string gradename = gradeToString(pInven.pInventoryItemGrade[a]);

        UIData(a, itemDB, gradename, pInven);
    }

    public void OffLastSellSlot()
    {
        LastPutGrade = 0;
        LastPutID = 0;
        LastPutName = "";
        LastSellSlot.SetActive(false);
    }

    public void ResetAll()
    {
        ListItemID = new List<int>(); //ID
        ListItemCount = new List<int>(); //Count
        ListItemGrade = new List<int>(); //Grade
        lastListNum = 0;

        SlotItemID = new int[36];
        SlotItemCount = new int[36];
        SlotItemGrade = new int[36];

        LastPutID = 0;
        LastPutCount = 0;
        LastPutName = "";
        LastPutGrade = 0;
    }
}