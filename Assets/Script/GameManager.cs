using JetBrains.Annotations;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour    // 게임의 전반적인 행동을 조정하고 대부분의 오브젝트가 얘를 참조한다
                                            // 추가로 1회만 획득 가능한 별방울, 해골열쇠, 가방등의 자료도 얘가 받는다.
{
    // 시간관리
    public int currentYear; // year
    public int currentSeason; // 0:봄
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
    public int weather = 0; // 0:맑음
    int nextdayweather; // 다음날 날씨, 조건에 따라 확률적 배정

    // 시간체크
    float timePassed;
    public float dayTimePassed = 0; //하루 20시간(06~02), 10초=10분 60초=1시간, 

    private void Awake()
    {
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

    void EndOfTheDay() //dayoff가 true일때 정산씬을 호출, 나머지 기능은 정산씬에서 실행
    {
        currentHour = 6;
        currentMinute = 0;
        ampm = "AM";
        currentDay++;
        if (currentDay > 28) { currentMonth++; currentDay = 1; }
        if (currentMonth > 4) { currentYear++; currentMonth = 1; } // 초기화 및 일차 진행
        // 하루가 종료되었을때 : 침대에서 잠을 자도 dayOff가 true가 된다.
        // 출하상자에 들어간 물품들의 sell price를 합산해 화면에 표시한다
        // 스킬의 레벨이 오르면 알려준다
        // 이벤트(코딩적인 의미 아님)을 진행한다
        // goldEarn = 출하상자에 들어있는 물품들의 가격합
        // 출하상자에 들어있는 물품들의 종류에 따라 다르게 더해서 goldEarn에 넣어도 되긴하는데
        // 거기다가 뭘 얼마나 팔았는지 체크 할 수도 있긴 한데......

        gold += goldEarn;

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
    void InitializeLandUnitList()
    {
        landControls = FindObjectsOfType<LandControl>();
    }


    public void SaveLandData()
    {   //LandController를 가진 object들을 로드.
        InitializeLandUnitList();

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
                landData[i].savePosition = landControls[i].savePosition;
            }
        }
    } // 씬이 Farm에서 변경 될 때

    public void LoadLandData()
    {   //LandController를 가진 object들을 로드.
        InitializeLandUnitList();

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
                if (landData[i].landType == LandType.Empty) { }     // empty일 경우의 행동.
                else if (landData[i].landType == LandType.Weed ||   // 잡초, 나뭇가지, 막대기의 경우.
                         landData[i].landType == LandType.Stone ||
                         landData[i].landType == LandType.Stick)
                {
                    landControls[i].transform.gameObject.GetComponent<LandControl>().prefabPath = landData[i].prefabPath;
                    GameObject child = Resources.Load(landData[i].prefabPath) as GameObject;//경로의 프리팹을 child라고 이름 붙여서 생성하는데.
                    Instantiate(child, landControls[i].transform.position, Quaternion.identity).transform.parent = landControls[i].transform;//landcontroller의 자식으로 배정한다.
                                                                                                                                             //사실 체력을 배정해야 하긴 하나, 어차피 생성되면서 체력이 1로 초기화 되기 때문에 생략한다.
                }
                else if (landData[i].landType == LandType.Tree)     // 나무일 경우.
                {
                    landControls[i].transform.gameObject.GetComponent<LandControl>().prefabPath = landData[i].prefabPath;
                    GameObject child = Resources.Load(landData[i].prefabPath) as GameObject;
                    Instantiate(child, landControls[i].transform.position, Quaternion.identity).transform.parent = landControls[i].transform;
                    // 이후 생성된 자식 오브젝트의 속성(체력과 성장단계)을 변경한다. 이 경우에는 FieldTreeLand 이다.
                    FieldTreeLand fieldTreeLand = landControls[i].GetComponentInChildren<FieldTreeLand>();
                    fieldTreeLand.hp = landData[i].currentHP;
                    fieldTreeLand.currentLevel = landData[i].level;
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
                }
            }
        }
    } // 씬이 Farm으로 변경 될 때

    string currentSceneName;
    private void WhenSceneChanged()
    {
        if (currentSceneName != SceneManager.GetActiveScene().name)
        {
            if (SceneManager.GetActiveScene().name == "Farm")
            {
                LoadLandData();
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
    public LandData()
    {
        //prefabPath = string.Empty;
        //prefabPath_Crop = string.Empty;
        //landType = LandType.Empty;
        //currentHP = 1;
        //level = 0;
        //days = 0;
        //digged = false;
        //watered = false;
        //seeded = false;
    }
}