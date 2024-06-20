using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

class SellBoxManager : MonoBehaviour
{
    //=======DataManage========//
    List<int> ListItemID; //ID
    List<int> ListItemCount; //Count
    List<int> ListItemGrade; //Grade
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
    private int tLastPutCount;
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
    string LastPutName;
    int LastPutGrade = 0;
    //=======LastPut========//



    SpriteManager spriteManager;
    GameObject sellBoxCanvas;
    private void Awake()
    {
        ResetList();
        spriteManager = FindAnyObjectByType<SpriteManager>();
        sellBoxCanvas = transform.GetChild(0).gameObject;

        LastSellSlot = sellBoxCanvas.GetComponentInChildren<SellBoxLastSellSlot>().gameObject;

        sellBoxCanvas.SetActive(false);
    }

    void ResetList()
    {
        ListItemID = new List<int>();
        ListItemCount = new List<int>();
        ListItemGrade = new List<int>();
    }

    PlayerInventroy pInven;
    public void OpenSellBox(GameObject playerObject)
    {   //OpenSellBox를 sellboxobject가 여는데, 그녀석이 playerobejct를 건네준다.
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

            ItemDB itemDB;
            itemDB = new ItemDB(pInven.pInventoryItemID[i]);

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
        LastSellSlot.transform.GetChild(0).GetComponent<Image>().sprite = spriteManager.GetSprite(LastPutName);
        LastSellSlot.transform.GetChild(1).GetComponent<Image>().sprite = spriteManager.GetSprite(gradeToString(LastPutGrade));
        LastSellSlot.GetComponentInChildren<Text>().text = LastPutCount.ToString();
    }

    string gradeToString(int grade)
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
            pInven.ChangeCount(SlotNumber, SlotItemCount[SlotNumber]);
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

            //Slot의 모든 데이터 삭제 및 UI와 데이터 업데이트.
            pInven.ChangeCount(SlotNumber, -SlotItemCount[SlotNumber]); //inventory에 count를 0으로. = 데이터 삭제.
            UIData(SlotNumber, new ItemDB(SlotItemID[SlotNumber]), gradeToString(SlotItemGrade[SlotNumber]), pInven); //이후 업데이트.

            //spriteManager.GetSprite(itemDB.name)

            //Last Input에 아이템 등록.
            LastPutID = SlotItemID[SlotNumber];
            LastPutName = new ItemDB(SlotItemID[SlotNumber]).name;
            LastPutCount = SlotItemCount[SlotNumber];
            LastPutGrade = SlotItemGrade[SlotNumber];

            lastListNum = ListItemID.Count - 1;
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

            Debug.Log(ListItemID[ListItemID.Count - 1]);

            //Slot의 Count 감소 및 UI와 데이터 업데이트.
            pInven.ChangeCount(SlotNumber, -1); //inventory에 count를 0으로. = 데이터 삭제;
            UIData(SlotNumber, new ItemDB(SlotItemID[SlotNumber]), gradeToString(SlotItemGrade[SlotNumber]), pInven); //이후 업데이트;

            //Last Input에 아이템 등록.
            LastPutID = SlotItemID[SlotNumber];
            LastPutName = new ItemDB(SlotItemID[SlotNumber]).name;
            LastPutGrade = SlotItemGrade[SlotNumber];
            LastPutCount = 1;

            lastListNum = ListItemID.Count - 1;
        }
    }

    public void RestoreLastSell()
    {
        Debug.Log("Restore");
    }

    public void OffLastSellSlot()
    {
        LastSellSlot.SetActive(false);
    }
}