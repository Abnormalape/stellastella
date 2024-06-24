using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

class EndDayManager : MonoBehaviour
{
    SellBoxManager sellBoxManager;
    SpriteManager spriteManager;

    public List<int> IDs;
    public List<int> Grades;
    public List<int> Counts;

    int[] aIDs;
    int[] aGrades;
    int[] aCounts;

    private void Awake()
    {
        sellBoxManager = FindFirstObjectByType<SellBoxManager>();
        fishText = transform.Find("GoldGain").transform.Find("Fish").GetComponent<Text>();
        fruitText = transform.Find("GoldGain").transform.Find("Fruit").GetComponent<Text>();
        etcText = transform.Find("GoldGain").transform.Find("Etc").GetComponent<Text>();
        oreText = transform.Find("GoldGain").transform.Find("Ore").GetComponent<Text>();
        animalText = transform.Find("GoldGain").transform.Find("Animal").GetComponent<Text>();
        totalText = transform.Find("GoldGain").transform.Find("SellGold").GetComponent<Text>();

        fishDetail = transform.Find("GoldGain").transform.Find("FishDetail").gameObject;
        fruitDetail = transform.Find("GoldGain").transform.Find("FruitDetail").gameObject;
        oreDetail = transform.Find("GoldGain").transform.Find("OreDetail").gameObject;
        animalDetail = transform.Find("GoldGain").transform.Find("AnimalDetail").gameObject;
        etcDetail = transform.Find("GoldGain").transform.Find("EtcDetail").gameObject;

        spriteManager = FindFirstObjectByType<SpriteManager>();

        IDs = new List<int>();
        Grades = new List<int>();
        Counts = new List<int>();

        MakeSum();
        PrintSells();
    }
    private void MakeSum()
    {
        aIDs = new int[sellBoxManager.ListItemID.Count];
        aGrades = new int[sellBoxManager.ListItemID.Count];
        aCounts = new int[sellBoxManager.ListItemID.Count];

        for (int i = 0; i < sellBoxManager.ListItemID.Count; i++)
        {
            aIDs[i] = sellBoxManager.ListItemID.ToArray()[i];
            aGrades[i] = sellBoxManager.ListItemGrade.ToArray()[i];
            aCounts[i] = sellBoxManager.ListItemCount.ToArray()[i];
        }

        int L;
        int K = 0;
        L = aIDs.Length;

        for (int i = 0; i < L; i++)
        {
            if (aIDs[i] == 0 || aCounts[i] == 0)
            {
                continue;
            }

            if (K == 0)
            {
                IDs.Add(aIDs[i]);
                Grades.Add(aGrades[i]);
                Counts.Add(aCounts[i]);
                K++;
                continue;
            }

            bool foundIDGrade = false;
            for (int j = 0; j < K; j++)
            {
                Debug.Log(K);

                if (IDs[j] == aIDs[i]) // 저장하려는 ID가 목록에 있다면.
                {
                    if (Grades[j] == aGrades[i]) // 해당위치의 등급값과 일치하는지 확인하고.
                    {   //만약 그렇다면 해당위치에 count를 더하고.
                        Counts[j] += aCounts[i];
                        foundIDGrade = true;
                        break; //탈출한다.
                    }
                }
            }   //for문을 도는 동안 아무일도 없었다. = ID 등급이 일치하는 경우가 없다.

            if (foundIDGrade == false)
            {
                IDs.Add(aIDs[i]);
                Grades.Add(aGrades[i]);
                Counts.Add(aCounts[i]);
                K++;
            }
        }

        for (int i = 0; i < IDs.Count; i++)
        {
            Debug.Log($"{i}번째 / ID : {IDs[i]} / Grade : {Grades[i]} / Counts : {Counts[i]}");
        }
    }

    //========================Sell=======================//
    Text fishText;
    Text fruitText;
    Text etcText;
    Text oreText;
    Text animalText;
    Text totalText;

    int totalSellPrice = 0;
    int fishSellPrice = 0; string lastfish;
    int fruitSellPrice = 0; string lastfruit;
    int etcSellPrice = 0; string lastetc;
    int oreSellPrice = 0; string lastore;
    int animalSellPrice = 0; string lastanimal;

    
    private void PrintSells()
    {
        for (int i = 0; i < IDs.Count; i++)
        {
            ItemDB findPrice = new ItemDB(IDs[i]);
            totalSellPrice += (int)(findPrice.sellPrice * Counts[i] * (1 + (0.1f * Grades[i])));

            if(findPrice.type == "Fish")
            {
                int sellprice = (int)(findPrice.sellPrice * Counts[i] * (1 + (0.1f * Grades[i])));
                fishSellPrice += sellprice;
                lastfish = findPrice.name;
                fishDetail.GetComponent<SellProductDetail>().thisID.Add(IDs[i]);
                fishDetail.GetComponent<SellProductDetail>().thisCount.Add(Counts[i]);
                fishDetail.GetComponent<SellProductDetail>().thisGrade.Add(Grades[i]);
                fishDetail.GetComponent<SellProductDetail>().sellPrice.Add(sellprice);
            }
            else if (findPrice.type == "Fruit")
            {
                int sellprice = (int)(findPrice.sellPrice * Counts[i] * (1 + (0.1f * Grades[i])));
                fruitSellPrice += sellprice;
                lastfruit = findPrice.name;
                fruitDetail.GetComponent<SellProductDetail>().thisID.Add(IDs[i]);
                fruitDetail.GetComponent<SellProductDetail>().thisCount.Add(Counts[i]);
                fruitDetail.GetComponent<SellProductDetail>().thisGrade.Add(Grades[i]);
                fruitDetail.GetComponent<SellProductDetail>().sellPrice.Add(sellprice);
            }
            else if (findPrice.type == "Ore")
            {
                int sellprice = (int)(findPrice.sellPrice * Counts[i] * (1 + (0.1f * Grades[i])));
                oreSellPrice += sellprice;
                lastore = findPrice.name;
                oreDetail.GetComponent<SellProductDetail>().thisID.Add(IDs[i]);
                oreDetail.GetComponent<SellProductDetail>().thisCount.Add(Counts[i]);
                oreDetail.GetComponent<SellProductDetail>().thisGrade.Add(Grades[i]);
                oreDetail.GetComponent<SellProductDetail>().sellPrice.Add(sellprice);
            }
            else if (findPrice.type == "Animal")
            {
                int sellprice = (int)(findPrice.sellPrice * Counts[i] * (1 + (0.1f * Grades[i])));
                animalSellPrice += sellprice;
                lastanimal = findPrice.name;
                animalDetail.GetComponent<SellProductDetail>().thisID.Add(IDs[i]);
                animalDetail.GetComponent<SellProductDetail>().thisCount.Add(Counts[i]);
                animalDetail.GetComponent<SellProductDetail>().thisGrade.Add(Grades[i]);
                animalDetail.GetComponent<SellProductDetail>().sellPrice.Add(sellprice);
            }
            
            else
            {
                int sellprice = (int)(findPrice.sellPrice * Counts[i] * (1 + (0.1f * Grades[i])));
                etcSellPrice += sellprice;
                lastetc = findPrice.name;
                etcDetail.GetComponent<SellProductDetail>().thisID.Add(IDs[i]);
                etcDetail.GetComponent<SellProductDetail>().thisCount.Add(Counts[i]);
                etcDetail.GetComponent<SellProductDetail>().thisGrade.Add(Grades[i]);
                etcDetail.GetComponent<SellProductDetail>().sellPrice.Add(sellprice);
            }
        }

        fishText.text = fishSellPrice.ToString();
        fruitText.text = fruitSellPrice.ToString();
        oreText.text = oreSellPrice.ToString();
        animalText.text = animalSellPrice.ToString();
        etcText.text = etcSellPrice.ToString();
        totalText.text = totalSellPrice.ToString();

        fishDetail.transform.GetChild(0).GetComponent<Image>().sprite = spriteManager.GetSprite(lastfish);
        fruitDetail.transform.GetChild(0).GetComponent<Image>().sprite = spriteManager.GetSprite(lastfruit);
        oreDetail.transform.GetChild(0).GetComponent<Image>().sprite = spriteManager.GetSprite(lastore);
        animalDetail.transform.GetChild(0).GetComponent<Image>().sprite = spriteManager.GetSprite(lastanimal);
        etcDetail.transform.GetChild(0).GetComponent<Image>().sprite = spriteManager.GetSprite(lastetc);
    }

    GameObject fishDetail;
    GameObject oreDetail;
    GameObject animalDetail;
    GameObject etcDetail;
    GameObject fruitDetail;
}