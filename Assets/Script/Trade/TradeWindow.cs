using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//스크롤 기능 만듬.
//현재 판매 목록 메소드 만듬.
//화면에 4개의 품목을 보여주는 메소드 만듬.

//판매품목 DB를 어찌해야 할까...

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
    // 플레이어의 자식 오브젝트로 생성한다.


    [SerializeField] SellingType sellingType; // 판매 타입을 엔진에서 설정.
    public Sprite portrait { set { transform.GetChild(5).transform.GetChild(0).GetComponent<Image>().sprite = value; } }

    [SerializeField] int[] SellDB; // 일단 엔진에서 입력하긴 하는데, 제대로 데이터 관리해서 넣어야함.
    [SerializeField] int[] SellCount; // 각 목록의 갯수

    int[] nowSelling; // 현재 파는 품목.
    int[] nowSellingCount; // 품목의 갯수.

    public PlayerController pCon;
    public PlayerInventroy pInven;
    GameManager gameManager;

    private void Awake()
    {
        pCon = GetComponentInParent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        GenerateSellingList();
        ChildGameObject();
    }

    private void OnEnable()
    {
    }

    GameObject[] sellListUI = new GameObject[4];
    void ChildGameObject()
    {
        for (int i = 0; i < 4; i++)
        {
            sellListUI[i] = transform.GetChild(i + 1).gameObject;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            EndTrade();
        }
        ChangeCurrentRow();
        GenerateSellingUI();
    }



    void GenerateSellingList()
    {   //해당 상점이 판매하는 모든 아이템의 정보중 조건에 맞는 물품만 골라내는 메서드.

        if (sellingType == SellingType.Weekly)
        {
            // SellDB의 전체 품목중 SellTime이 GameManager의 currentdate % 7과 같은 품목을 저장.
        }
        else if (sellingType == SellingType.Seasonal)
        {
            // SellDB의 전체 품목중 SellTime이 GameManager의 currentMonth 와 같은 품목을 저장 및 sellCount도 저장.
        }
        else if (sellingType == SellingType.NoneChange)
        {
            nowSelling = SellDB; // 현재 판매 품목.
            nowSellingCount = SellCount;
        }
    }


    public int[] sellList { get; private set; } = new int[4]; //sellList : 화면에 보이는 판매 아이템 목록.
    public int[] sellListCount { get; private set; } = new int[4]; // 화면에 보이는 판매 아이템 갯수.

    ItemDB[] nowSellingUIDB = new ItemDB[4]; // 화면에 올라온 아이템의 정보.
    public int currentRow { get; private set; } = 0; // 현재 스크롤 상태.

    public int SellLength = 4;
    void GenerateSellingUI()
    {   //업데이트에 들어갈 메서드.
        
        if (nowSelling.Length < 4)
        {
            SellLength = nowSelling.Length;
            sellList = new int[SellLength];
            sellListCount = new int[SellLength];
            nowSellingUIDB = new ItemDB[SellLength];
        }

        Debug.Log(nowSellingUIDB.Length);

        for (int i = 0; i < SellLength; i++)
        {
            //텍스트 컴포넌트는 3개 getChild()로 오브젝트 받아서 실행 알맞은 오브젝트의 text에 삽입.
            nowSellingUIDB[i] = new ItemDB(nowSelling[currentRow + i]);


            sellList[i] = nowSelling[currentRow + i];

            sellListUI[i].transform.GetChild(0).GetComponent<Text>().text = nowSellingUIDB[i].name; // 이름, 첫째 오브젝트.
            sellListUI[i].transform.GetChild(1).GetComponent<Text>().text = Convert.ToString(nowSellingUIDB[i].buyPrice); // 가격, 둘째 오브젝트.

            if (nowSellingCount[currentRow + i] == -1)
            { // 갯수, 셋째 오브젝트. 
                sellListUI[i].transform.GetChild(3).GetComponent<Text>().text = "";
            }
            else
            {
                sellListUI[i].transform.GetChild(3).GetComponent<Text>().text = Convert.ToString(nowSellingCount[currentRow + i]);
            }

            if (SpriteManager.Instance.GetSprite(nowSellingUIDB[i].name) != null)
            {   // 이미지를 이름으로 불러냄.
                sellListUI[i].transform.GetChild(2).GetComponent<Image>().sprite = SpriteManager.Instance.GetSprite(nowSellingUIDB[i].name);
            }
            else
            {
                sellListUI[i].transform.GetChild(2).GetComponent<Image>().sprite = null;
            }
        }
    }

    void ChangeCurrentRow()
    {   //스크롤에 따라 현재 줄이 바뀐다.
        float wheelscroll = Input.GetAxis("Mouse ScrollWheel");

        if(nowSelling.Length < 4)
        {
            currentRow = 0;
            return;
        }

        if (wheelscroll > 0)
        {
            currentRow--;
        }
        else if (wheelscroll < 0)
        {
            currentRow++;
        }

        if (currentRow < 0)
        {
            currentRow = 0;
        }
        else if (currentRow >= nowSelling.Length - 4)
        {
            currentRow = nowSelling.Length - 4;
        }
    }
    void EndTrade()
    {
        Destroy(this.gameObject);
    }
}

