using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class BuyAnimalWindow : MonoBehaviour
{
    private PlayerController tPCon;
    private PlayerInventroy pInven;
    private SpriteManager spriteManager;
    private TextAsset sellAnimalListCsv;
    private List<Dictionary<string, string>> SellAnimalData;

    MyPlayerCursor myPlayerCursor;
    GameManager gameManager;
    CameraManager cameraManager;
    Camera cam;

    // 구매 가능한 동물만 검지 않은색으로 표현한다.
    // 동물을 누르면 씬으로 넘어가면서 건물을 클릭 할 수 있다.
    // 건물을 클릭하면 끝.
    public PlayerController pCon { get { return tPCon; } set { tPCon = value; pInven = pCon.GetComponent<PlayerInventroy>(); WhenDataImported(); } }

    private void WhenDataImported() // 기초 셋팅과, 판매할 동물을 고르기.
    {
        sellAnimalListCsv = GetComponentInParent<OpeningBuyAnimalWindow>().mySellList;
        SellAnimalData = new ParseCsvFile().ParseCsv(sellAnimalListCsv.text);

        spriteManager = FindFirstObjectByType<SpriteManager>();
        gameManager = FindFirstObjectByType<GameManager>();
        myPlayerCursor = FindFirstObjectByType<MyPlayerCursor>();
        cameraManager = FindFirstObjectByType<CameraManager>();
        cam = Camera.main;


        CanSellList(); // 판매가능한 목록의 index를 받아온다.
        MakeSprite(); // 화면에 보여준다. 
    }

    private List<int> CanSellIndex = new List<int>(8);

    private void CanSellList()
    {
        for (int ix = 0; ix < SellAnimalData.Count; ix++)
        {
            int canSell = 0;
            if (SellAnimalData[ix]["PlayerFarmOption"] == "" || true) //Todo: 조건 찾기.
            {
                canSell++;
            }
            if (SellAnimalData[ix]["Option2"] == "" || true)
            {
                canSell++;
            }
            if (SellAnimalData[ix]["Option3"] == "" || true)
            {
                canSell++;
            }
            if (SellAnimalData[ix]["Option4"] == "" || true)
            {
                canSell++;
            }

            if (canSell == 4)
            {
                CanSellIndex.Add(ix);
            }
            else
            {
                CanSellIndex.Add(-1); //-1은 판매할 수 없는 물품.
            }
        }
    }
    Color black = Color.black;

    GameObject[] animalImage;
    private void MakeSprite()
    {
        int childcount = transform.GetChild(0).transform.Find("Grid").childCount;



        animalImage = new GameObject[childcount];

        for (int ix = 0; ix < childcount; ix++) //자식의 갯수 만큼.
        {
            animalImage[ix] = transform.GetChild(0).transform.Find("Grid").GetChild(ix).gameObject;
        }

        for (int ix = 0; ix < childcount; ix++)
        {
            if (ix >= SellAnimalData.Count)
            {
                animalImage[ix].SetActive(false);
            }
        }

        for (int iy = 0; iy < SellAnimalData.Count; iy++) // 동물의 수만큼.
        {
            if (CanSellIndex[iy] == -1)
            {   //스프라이트의 색을 검게.
                animalImage[iy].GetComponent<Image>().sprite = spriteManager.GetSprite(SellAnimalData[iy]["AnimalName"]); //동물의 얼굴모양.
                animalImage[iy].GetComponent<Image>().color = black;
                animalImage[iy].GetComponentInChildren<Button>().gameObject.SetActive(false);
            }
            else
            {
                animalImage[iy].GetComponent<Image>().sprite = spriteManager.GetSprite(SellAnimalData[iy]["AnimalName"]); //동물의 얼굴모양.
                animalImage[iy].GetComponent<Image>().SetNativeSize();
                string instAnimalName = SellAnimalData[iy]["AnimalName"];

                animalImage[iy].GetComponentInChildren<Button>().onClick.AddListener
                    (delegate
                    {
                        //동물을 클릭하면.
                        myPlayerCursor.animalName = instAnimalName;
                        cameraManager.nowcamera = nowLocation.Farm;
                        FindFirstObjectByType<MyPlayerCursor>().player = pCon.gameObject;
                        gameManager.needSubCam = true;
                        pCon.gameObject.SetActive(false);
                        gameManager.SubCamLocation = "Farm";
                    });
            }
        }
    }
}