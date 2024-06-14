using System.Linq;
using TreeEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour    // 게임의 전반적인 행동을 조정하고 대부분의 오브젝트가 얘를 참조한다
                                            // 추가로 1회만 획득 가능한 별방울, 해골열쇠, 가방등의 자료도 얘가 받는다.
{
    // 시간관리
    public int currentYear; // year

    private int currentseason;
    public int currentSeason 
    {
        get
        {
            return currentseason; 
        }
        private set 
        {
            currentseason = value;
            seasonUiSprite.WhenSeasonChange(currentseason); 
        } 
    } // 0:봄

    public int currentMonth;
    public int currentDay; // 28
    public int currentHour; // 06:00 ~ 02:00
    public int currentMinute; // 1min real = 1hour
    public string ampm;
    public bool dayOff; // 날이 끝났음을 전달, 혹은 씬의 변경?



    // 돈 관리
    int gold;
    int goldEarn;

    // 매일 상태 관리
    bool wetherTotemUse; // 날씨 토템
    float luck; // 행운
    int wetherTotemNum;
    public int weather { get; private set; } = 0; // 0:맑음
    int nextdayweather; // 다음날 날씨, 조건에 따라 확률적 배정

    // 시간체크
    float timePassed;
    public float dayTimePassed = 0; //하루 20시간(06~02), 10초=10분 60초=1시간, 

    SeasonUiSprite seasonUiSprite;
    private void Awake()
    {
        seasonUiSprite = GetComponentInChildren<SeasonUiSprite>();

        currentYear = 1;
        currentSeason = 0; // 봄
        currentMonth = 1;
        currentDay = 1;
        currentHour = 6;
        currentMinute = 0;
        ampm = "AM";
        dayOff = false;
        currentSceneName = SceneManager.GetActiveScene().name;
    }



    private void Update()
    {
        dayTimePassed = dayTimePassed + Time.deltaTime;

        // 시간경과 체크
        timePassed = timePassed + Time.deltaTime;
        // 현재시간 출력
        UpdateTime();

        // 일자 종료시, 일자 종료메서드 실행
        if (dayOff) { EndOfTheDay(); }

        // 골드관리, 다른오브젝트가 직접적으로 gold를 쓰지 않고 goldearn을 통해 관리: 근데 이거 의미 있나?
        if (goldEarn != 0) { gold += goldEarn; goldEarn = 0; } // 이벤트화 요소

        WhenSceneChanged();
    }
    void UpdateTime()
    {
        if (timePassed >= 10f)
        {
            currentMinute += 10;
            timePassed = 0;
        }

        if (currentMinute >= 60)
        {
            currentHour += 1;
            currentMinute = 0;
        }

        if (currentHour == 26)
        { dayOff = true; }

        if (currentHour < 12 || currentHour >= 24)
        { ampm = "AM"; }
        else { ampm = "PM"; }
    }

    public void DayOff()
    {
        dayOff = true;
    }
    void EndOfTheDay() //dayoff가 true일때 정산씬을 호출, 나머지 기능은 정산씬에서 실행
    {
        dayTimePassed = 0;
        currentHour = 6;
        currentMinute = 0;
        ampm = "AM";
        currentDay++;


        bool monthChanged = false;

        if (currentDay > 28) { currentMonth++; currentDay = 1; monthChanged = true; }
        if (currentMonth > 4) { currentYear++; currentMonth = 1; } // 초기화 및 일차 진행

        currentSeason = currentMonth - 1;

        if (landData != null)
        {
            for (int i = 0; i < landData.Length; i++)
            {
                landData[i].dayChanged = true;
                landData[i].monthChanged = monthChanged;
            }
        }
        if (landTreeData != null)
        {
            for (int i = 0; i < landTreeData.Length; i++)
            {
                landTreeData[i].dayChanged = true;
                landTreeData[i].monthChanged = monthChanged;
            }
        }
        if (landWeedData != null)
        {
            for (int i = 0; i < landWeedData.Length; i++)
            {
                landWeedData[i].dayChanged = true;
                landWeedData[i].monthChanged = monthChanged;
            }
        }


        // 하루가 종료되었을때 : 침대에서 잠을 자도 dayOff가 true가 된다.
        // 출하상자에 들어간 물품들의 sell price를 합산해 화면에 표시한다
        // 스킬의 레벨이 오르면 알려준다
        // 이벤트(코딩적인 의미 아님)을 진행한다
        // goldEarn = 출하상자에 들어있는 물품들의 가격합
        // 출하상자에 들어있는 물품들의 종류에 따라 다르게 더해서 goldEarn에 넣어도 되긴하는데
        // 거기다가 뭘 얼마나 팔았는지 체크 할 수도 있긴 한데......
        // 작물이 물이 뿌려졌는지 확인후 작물 상태에 1을 더하고 물을 초기화한다
        // 동물들의 나이에 1을 더한다
        NewDayBegin();

        dayOff = false;
    }
    void NewDayBegin() // 게임 씬으로 돌아옴, 정산씬에서 관리 하지 않은 것들 관리
    {
        weather = nextdayweather;
        luck = 0f; // 이건 그냥 랜덤

        if (currentMonth == 1) // 1월, 봄일때
        {
            int randyum = 10; // 랜덤값 (1~101), 임시로 정수 줌
            if (randyum > 70) { nextdayweather = 2; } // 30%확률로 내일 비
            else { nextdayweather = 1; } // 70퍼센트 확률로 내일 맑음
        }

        // 다음날 날씨로 날씨변경
        // 행운변경
        // 다음날 날씨 변경
    }

    void weatherControll() // 날씨 관리
    {
        if (!wetherTotemUse)
        {
            switch (currentMonth)
            {
                case 1: //1월 일때
                    nextdayweather = 1; return; // 확률에 따라 비, 맑음 결정
                case 2: //2월 일때
                    nextdayweather = 1; return; // 확률에 따라 비, 녹색비, 폭풍, 맑음 결정
                case 3: //3월 일때
                    nextdayweather = 1; return; // 확률에 따라 비, 폭풍, 맑음, 바람 결정
                case 4: //4월 일때
                    nextdayweather = 1; return; // 확률에 따라 눈, 맑음 결정
            }
        }
        if (wetherTotemUse)
        {
            switch (wetherTotemNum)
            {
                case 1: //날씨 1
                    nextdayweather = 1; return;
                case 2: //날씨 2
                    nextdayweather = 2; return;
                case 3: //날씨 3
                    nextdayweather = 3; return;
                case 4: //날씨 4
                    nextdayweather = 4; return;
            }
        }
    }




    // 인벤토리 관리
    GameObject inventory;
    public bool isInventoryOn { get; private set; }
    PlayerLeftClick playerLeftClick;



    //씬이 변경되기 전에 각 개체의 데이터를 저장해야 한다.
    LandData[] landData;
    LandControl[] landControls;
    GameObject[] LandInFarm;
    void InitializeLandInFarmUnitList()
    {   //농장에 있는 Land들;
        LandInFarm = GameObject.FindGameObjectsWithTag("LandInFarm");
        landControls = new LandControl[LandInFarm.Length];
        for (int i = 0; i < LandInFarm.Length; i++)
        {
            landControls[i] = LandInFarm[i].GetComponent<LandControl>();
        }
    }

    LandControl[] LandTreeControls;
    LandData[] landTreeData;
    void InitializeTreeLand()
    {   //나무만 자라는 Land.
        GameObject[] treelands = GameObject.FindGameObjectsWithTag("TreeLand");
        LandTreeControls = new LandControl[treelands.Length];
        for (int i = 0; i < treelands.Length; i++)
        {
            LandTreeControls[i] = treelands[i].GetComponent<LandControl>();
        }
    }

    LandControl[] LandWeedControls;
    LandData[] landWeedData;
    void InitializeWeedLand()
    {   //잡초만 자라는 Land.
        GameObject[] weedlands = GameObject.FindGameObjectsWithTag("WeedLand");
        LandWeedControls = new LandControl[weedlands.Length];

        for (int i = 0; i < weedlands.Length; i++)
        {
            LandWeedControls[i] = weedlands[i].GetComponent<LandControl>();
        }
    }


    public void SaveLandInFarmData()
    {   //LandController를 가진 object들을 로드.
        InitializeLandInFarmUnitList();

        landData = new LandData[landControls.Length]; //LandData를 생성
        //목록을 생성해서.
        if (landControls == null)
        {
            return;
        }
        else if (landControls.Length > 0)
        {   //목록의 갯수가 1개 이상이라면.
            //각 LandData에 LandUnitList.get

            for (int i = 0; i < landControls.Length; i++)
            {
                landData[i] = new LandData();
                landData[i].landType = landControls[i].landType;
                landData[i].savePosition = landControls[i].savePosition;
                landData[i].dayChanged = false;
                landData[i].monthChanged = false;

                if (landData[i].landType == LandType.Empty) { }

                else if (landData[i].landType == LandType.Weed ||
                         landData[i].landType == LandType.Stone ||
                         landData[i].landType == LandType.Stick)
                {
                    landData[i].prefabPath = landControls[i].prefabPath;
                    landData[i].currentHP = landControls[i].currentHP;
                }
                else if (landData[i].landType == LandType.Tree)
                {
                    landData[i].prefabPath = landControls[i].prefabPath;
                    landData[i].currentHP = landControls[i].currentHP;
                    landData[i].level = landControls[i].level;
                }
                else if (landData[i].landType == LandType.Farm)
                {
                    landData[i].prefabPath = landControls[i].prefabPath;
                    landData[i].currentHP = landControls[i].currentHP;
                    landData[i].digged = landControls[i].digged;
                    landData[i].watered = landControls[i].watered;
                    landData[i].seeded = landControls[i].seeded;
                    if (landData[i].seeded)
                    {
                        landData[i].prefabPath_Crop = landControls[i].prefabPath_Crop;
                        landData[i].days = landControls[i].days;
                    }
                }
            }
        }

        SaveLandTreeData();
        SaveLandWeedDate();
    } // 씬이 Farm에서 변경 될 때

    public void LoadLandInFarmData()    
    {   //LandController를 가진 object들을 로드.
        InitializeLandInFarmUnitList();

        if (landData == null)
        {
            return;
        }
        else if (landControls.Length > 0)
        {   //목록의 갯수가 1개 이상이라면.
            //각 LandUnitList에 자식오브젝트를 생성 및, 자식오브젝트의 설정 변경.
            for (int i = 0; i < landControls.Length; i++)
            {
                landControls[i].transform.position = landData[i].savePosition;

                landControls[i].landType = landData[i].landType;    // 해당 LandController의 타입을 변경

                if (landData[i].landType == LandType.Empty) 
                {
                    landControls[i].dayChanged = landData[i].dayChanged;
                    landControls[i].monthChanged = landData[i].monthChanged;
                    continue;
                }     // empty일 경우의 행동.
                else if (landData[i].landType == LandType.Weed ||   // 잡초, 자갈, 막대기의 경우.
                         landData[i].landType == LandType.Stone ||
                         landData[i].landType == LandType.Stick)
                {
                    Instantiate(Resources.Load(landData[i].prefabPath) as GameObject,
                        Vector3.zero, Quaternion.identity, landControls[i].transform);
                    landControls[i].GetComponent<LandControl>().prefabPath = landData[i].prefabPath;

                    landControls[i].transform.GetChild(0).transform.localPosition = Vector2.zero;

                    landControls[i].dayChanged = landData[i].dayChanged;
                    landControls[i].monthChanged = landData[i].monthChanged;
                    continue;
                }
                else if (landData[i].landType == LandType.Tree)     // 나무일 경우.
                {
                    Instantiate(Resources.Load(landData[i].prefabPath) as GameObject,
                        Vector3.zero, Quaternion.identity, landControls[i].transform);
                    GameObject childObject = landControls[i].GetComponentInChildren<FieldTreeLand>().gameObject;
                    landControls[i].GetComponent<TreeLand>().CurrentLevel = landData[i].level;
                    childObject.GetComponent<FieldTreeLand>().hp = landData[i].currentHP;
                    
                    landControls[i].dayChanged = landData[i].dayChanged;
                    landControls[i].monthChanged = landData[i].monthChanged;
                    continue;
                }
                else if (landData[i].landType == LandType.Farm)     // 농경지일 경우.
                {
                    landControls[i].transform.gameObject.GetComponent<LandControl>().prefabPath = landData[i].prefabPath;
                    GameObject child = Resources.Load(landData[i].prefabPath) as GameObject;
                    Instantiate(child, landControls[i].transform.position, Quaternion.identity).transform.parent = landControls[i].transform;
                    //생성된 자식 오브젝트의 속성(경작여부, 관개여부, 파종여부, 파종종류(프리팹), 성장단계)을 변경한다. 이 경우에는 farmlandcontrol 과 cropcontrol이다.
                    FarmLandControl farmLandControl = landControls[i].GetComponentInChildren<FarmLandControl>();
                    landControls[i].GetComponent<FarmLand>().digged = landData[i].digged;
                    farmLandControl.watered = landData[i].watered;
                    farmLandControl.seeded = landData[i].seeded;
                    if (landData[i].seeded)
                    {
                        landControls[i].transform.gameObject.GetComponent<LandControl>().prefabPath_Crop = landData[i].prefabPath_Crop;
                        GameObject grandChild = Resources.Load(landData[i].prefabPath_Crop) as GameObject;
                        Instantiate(grandChild, landControls[i].transform.position, Quaternion.identity).transform.parent = landControls[i].transform.GetChild(0).transform;
                        CropControl cropControl = landControls[i].transform.GetChild(0).GetComponentInChildren<CropControl>();
                        cropControl.days = landData[i].days;
                    }

                    farmLandControl.dayChanged = landData[i].dayChanged;
                    farmLandControl.monthChanged = landData[i].monthChanged;
                    landControls[i].dayChanged = landData[i].dayChanged;
                    landControls[i].monthChanged = landData[i].monthChanged;
                    continue;
                }
            }
        }
        LoadLandTreeData();
        LoadLandWeedData();
    } // 씬이 Farm으로 변경 될 때

    public void SaveLandTreeData() // for only TreeLand.
    {
        InitializeTreeLand();
        landTreeData = new LandData[LandTreeControls.Length];
        if (LandTreeControls == null) { return; }
        else if (LandTreeControls.Length > 0)
        {
            for (int i = 0; i < LandTreeControls.Length; i++)
            {
                landTreeData[i] = new LandData();
                landTreeData[i].landType = LandTreeControls[i].landType;
                landTreeData[i].dayChanged = false;
                landTreeData[i].monthChanged = false;
                landTreeData[i].savePosition = LandTreeControls[i].savePosition;

                if (landTreeData[i].landType == LandType.Empty) { }
                else if (landTreeData[i].landType == LandType.Tree)
                {   //비어있거나 나무이거나.
                    landTreeData[i].prefabPath = LandTreeControls[i].prefabPath;
                    landTreeData[i].currentHP = LandTreeControls[i].currentHP;
                    landTreeData[i].level = LandTreeControls[i].level;
                }

            }


        }
    }

    public void LoadLandTreeData() // for only TreeLand.
    {
        InitializeTreeLand();
        if (landTreeData == null)
        {
            return;
        }
        else if (LandTreeControls.Length > 0)
        {   //목록의 갯수가 1개 이상이라면.
            //각 LandUnitList에 자식오브젝트를 생성 및, 자식오브젝트의 설정 변경.
            for (int i = 0; i < LandTreeControls.Length; i++)
            {
                LandTreeControls[i].transform.position = landTreeData[i].savePosition;
                LandTreeControls[i].landType = landTreeData[i].landType;    // 해당 LandController의 타입을 변경
                if (landTreeData[i].landType == LandType.Empty)
                {
                    LandTreeControls[i].dayChanged = landTreeData[i].dayChanged;
                    LandTreeControls[i].monthChanged = landTreeData[i].monthChanged;
                }     // empty일 경우의 행동.
                else if (landTreeData[i].landType == LandType.Tree)
                {
                    Instantiate(Resources.Load(landTreeData[i].prefabPath) as GameObject,
                        Vector3.zero, Quaternion.identity, LandTreeControls[i].transform);
                    GameObject childObject = LandTreeControls[i].GetComponentInChildren<FieldTreeLand>().gameObject;

                    LandTreeControls[i].GetComponent<TreeLand>().CurrentLevel = landTreeData[i].level;
                    //childObject.GetComponent<FieldTreeLand>().CurrentLevel(landTreeData[i].level);
                    childObject.GetComponent<FieldTreeLand>().hp = landTreeData[i].currentHP;

                    LandTreeControls[i].dayChanged = landTreeData[i].dayChanged;
                    LandTreeControls[i].monthChanged = landTreeData[i].monthChanged;
                }

            }
        }
    }

    public void SaveLandWeedDate() // for only WeedLand.
    {
        InitializeWeedLand();
        landWeedData = new LandData[LandWeedControls.Length];


        if (LandWeedControls == null) { return; }
        else if (LandWeedControls.Length > 0)
        {
            for (int i = 0; i < LandWeedControls.Length; i++)
            {
                landWeedData[i] = new LandData();
                landWeedData[i].landType = LandWeedControls[i].landType;
                landWeedData[i].dayChanged = false;
                landWeedData[i].monthChanged = false;
                landWeedData[i].savePosition = LandWeedControls[i].savePosition;


                if (landWeedData[i].landType == LandType.Empty) { }
                else if (landWeedData[i].landType == LandType.Weed)
                {   //비어있거나 나무이거나.
                    landWeedData[i].prefabPath = LandWeedControls[i].prefabPath;
                    landWeedData[i].currentHP = LandWeedControls[i].currentHP;
                }
            }
        }
    }

    public void LoadLandWeedData() // for only WeedLand.
    {
        InitializeWeedLand();
        if (landWeedData == null)
        {
            return;
        }
        else if (LandWeedControls.Length > 0)
        {   //목록의 갯수가 1개 이상이라면.
            //각 LandUnitList에 자식오브젝트를 생성 및, 자식오브젝트의 설정 변경.
            for (int i = 0; i < LandWeedControls.Length; i++)
            {
                LandWeedControls[i].transform.position = landWeedData[i].savePosition;
                LandWeedControls[i].dayChanged = landWeedData[i].dayChanged;
                LandWeedControls[i].monthChanged = landWeedData[i].monthChanged;
                LandWeedControls[i].landType = landWeedData[i].landType;    // 해당 LandController의 타입을 변경



                if (landWeedData[i].landType == LandType.Empty) { }     // empty일 경우의 행동.
                else if (landWeedData[i].landType == LandType.Weed)
                {
                    LandWeedControls[i].transform.gameObject.GetComponent<LandControl>().prefabPath = landWeedData[i].prefabPath;
                    GameObject child = Resources.Load(landWeedData[i].prefabPath) as GameObject;
                    Instantiate(child, LandWeedControls[i].transform.position, Quaternion.identity).transform.parent = LandWeedControls[i].transform;
                }
            }
        }
    }


    string currentSceneName;
    private void WhenSceneChanged()
    {
        if (currentSceneName != SceneManager.GetActiveScene().name)
        {
            if (SceneManager.GetActiveScene().name == "Farm")
            {
                LoadLandInFarmData();
            }
            currentSceneName = SceneManager.GetActiveScene().name;
        }
    }
}

public class LandData
{
    public Vector3 savePosition;
    public string prefabPath;
    public string prefabPath_Crop;
    public LandType landType;
    public int currentHP;
    public int level;
    public int days;
    public bool digged;
    public bool watered;
    public bool seeded;

    public bool dayChanged = true;
    public bool monthChanged = true;
    public LandData()
    {

    }
}