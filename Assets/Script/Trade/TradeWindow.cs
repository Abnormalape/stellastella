using System;
using UnityEngine;
using UnityEngine.UI;


enum SellingType
{
    Seasonal,
    Weekly,
    NoneChange,
}
class TradeWindow : MonoBehaviour
// 얘도 애초에 생성되는 오브젝트이기 때문에, awake에 둬도 된다.
{   // 아이템DB에서 아이템ID를 받아와, 이미지, 이름, 가격을 띄워주는 윈도우 생성.
    // 상점창에 띄울 아이템 리스트는 계절별로 따로 받아온다.
    // 자식오브젝트로 몇가지 정보가 담긴 묶음을 소환한다.

    [SerializeField] SellingType sellingType; // 판매 타입을 엔진에서 설정.

    //계절이 아닌, 날짜에 연관한 판매 묶음 일수도 있는데.
    [SerializeField] int[] firstSellDB;
    [SerializeField] int[] firstSellCount;
    [SerializeField] int[] secondSellDB;
    [SerializeField] int[] secondSellCount;
    [SerializeField] int[] thirdSellDB;
    [SerializeField] int[] thirdSellCount;
    [SerializeField] int[] fourthSellDB;
    [SerializeField] int[] fourthSellCount;
    [SerializeField] int[] fifthSellDB;
    [SerializeField] int[] fifthSellCount;
    [SerializeField] int[] sixthSellDB;
    [SerializeField] int[] sixthSellCount;
    [SerializeField] int[] seventhSellDB;
    [SerializeField] int[] seventhSellCount;


    int[] nowSellingID;
    int[] noewSellingCount;


    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }


    int[] sellList = new int[4]; //sellList : 화면에 보이는 판매 아이템 목록.

    void GenerateSellingList()
    {
        switch (sellingType)
        {
            case SellingType.Weekly:
                switch(gameManager.currentDay % 7)
                {
                    case 0: // 0 == 일
                        nowSellingID = firstSellDB;
                        break;

                    case 1: // 월

                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    case 5:

                        break;

                    case 6: // 토

                        break;

                }

                break;

            case SellingType.Seasonal:
                switch (gameManager.currentSeason)
                {
                    case 0:
                        nowSellingID = firstSellDB;
                        break;
                    case 1:
                        nowSellingID = secondSellDB;
                        break;
                    case 2:
                        nowSellingID = thirdSellDB;
                        break;
                    case 3:
                        nowSellingID = fourthSellDB;
                        break;
                }
                break;

            case SellingType.NoneChange:


                break;

        }


        


        sellList[0] = nowSellingID[0]; //최초 생성.
        sellList[1] = nowSellingID[1];
        sellList[2] = nowSellingID[2];
        sellList[3] = nowSellingID[3];
    }


    GameObject[] sellListUI = new GameObject[4];
    ItemDB[] nowSellingDB = new ItemDB[4];
    void GenerateSellingUI()
    {   //업데이트에 들어갈 메서드.


        for (int i = 0; i < 4; i++)
        {
            nowSellingDB[i] = new ItemDB(sellList[0]);


            //transform.GetChild(1).GetComponent<Image>().sprite = SpriteManager.Instance.GetSprite(itemName);
            sellListUI[0].GetComponentInChildren<Image>().sprite = SpriteManager.Instance.GetSprite(nowSellingDB[i].name); // 이미지를 이름으로 불러냄.
            sellListUI[0].GetComponentInChildren<Text>().text = nowSellingDB[i].name; // 이름.
            sellListUI[0].GetComponentInChildren<Text>().text = Convert.ToString(nowSellingDB[i].sellPrice); // 가격.
            sellListUI[0].GetComponentInChildren<Text>().text = ; // 남은 갯수.
        }
    }
}

