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
        else if (pCon.inventory == false && GameObject.Find("InventoryBarUI") != null)
        {
            LoadOffInventory();
        }
    }

    void LoadInventory()
    {
        for (int i = 0; i < pInven.inventSlots; i++) // 추후에 36개로
        {
            inventoryUISlot[i] = transform.GetComponentsInChildren<InventorySlot>()[i].gameObject;
            if (inventoryUISlot[i] == null) { return; }; // 반환되는 슬롯이 없으면 실행 종료

            itemDB = new ItemDB(pInven.pInventoryItemID[i]);

            inventoryUISlot[i].GetComponentInChildren<InventorySlot>().inventoryitemID = pInven.pInventoryItemID[i]; //ui슬롯 0번에는 pinventory0번째 아이템의 아이디가 삽입된다.
            inventoryUISlot[i].GetComponentInChildren<InventorySlot>().inventoryitemcount = pInven.pInventoryItemCount[i]; //ui슬롯 0번에는 pinventory0번째 아이템의 갯수가 삽입된다.
            inventoryUISlot[i].GetComponentInChildren<InventorySlot>().inventoryitemgrade = pInven.pInventoryItemGrade[i]; //ui슬롯 0번에는 pinventory0번째 아이템의 갯수가 삽입된다.
            inventoryUISlot[i].GetComponentInChildren<InventorySlot>().thisInvenToryNumber = i;
        }
    }

    int currentInventory;
    void LoadOffInventory()
    {

        for (int i = 0; i < 12; i++)
        {
            inventoryUISlot[i] = GameObject.Find("InventoryBarUI").GetComponentsInChildren<OffInventorySlot>()[i].gameObject;
            if (inventoryUISlot[i] == null) { return; }; // 반환되는 슬롯이 없으면 실행 종료

            itemDB = new ItemDB(pInven.pInventoryItemID[i]);

            if (pInven.pInventoryItemCount[i] == 1 || pInven.pInventoryItemCount[i] == 0)
            {
                inventoryUISlot[i].GetComponentInChildren<Text>().text = $"";
            }
            else
            {
                inventoryUISlot[i].GetComponentInChildren<Text>().text = $"{pInven.pInventoryItemCount[i]}";
            }

            if (SpriteManager.Instance.GetSprite(itemDB.name) != null)
            {
                inventoryUISlot[i].transform.GetChild(0).GetComponent<Image>().sprite =
                    SpriteManager.Instance.GetSprite(itemDB.name);
                inventoryUISlot[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                inventoryUISlot[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                inventoryUISlot[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }


            if (pInven.pInventoryItemGrade[i] != 0)
            {
                inventoryUISlot[i].transform.Find("Grade").GetComponent<Image>().sprite =
                    SpriteManager.Instance.GetSprite(gradeToString(pInven.pInventoryItemGrade[i]));
                inventoryUISlot[i].transform.Find("Grade").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                inventoryUISlot[i].transform.Find("Grade").GetComponent<Image>().sprite =
                    SpriteManager.Instance.GetSprite(gradeToString(pInven.pInventoryItemGrade[i]));
                inventoryUISlot[i].transform.Find("Grade").GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }

            if (pInven.currentInventory == i)
            {
                currentInventory = i;
            }
        }
        GameObject.Find("CurrentOffInventoryUI").transform.position = inventoryUISlot[currentInventory].transform.position;

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

}

