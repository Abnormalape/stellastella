using System.Linq;
using TreeEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour    // ������ �������� �ൿ�� �����ϰ� ��κ��� ������Ʈ�� �긦 �����Ѵ�
                                            // �߰��� 1ȸ�� ȹ�� ������ �����, �ذ񿭼�, ������� �ڷᵵ �갡 �޴´�.
{
    // �ð�����
    public int currentYear; // year
    public int currentSeason; // 0:��
    public int currentMonth;
    public int currentDay; // 28
    public int currentHour; // 06:00 ~ 02:00
    public int currentMinute; // 1min real = 1hour
    public string ampm;
    public bool dayOff; // ���� �������� ����, Ȥ�� ���� ����?



    // �� ����
    int gold;
    int goldEarn;

    // ���� ���� ����
    bool wetherTotemUse; // ���� ����
    float luck; // ���
    int wetherTotemNum;
    public int weather { get; private set; } = 0; // 0:����
    int nextdayweather; // ������ ����, ���ǿ� ���� Ȯ���� ����

    // �ð�üũ
    float timePassed;
    public float dayTimePassed = 0; //�Ϸ� 20�ð�(06~02), 10��=10�� 60��=1�ð�, 

    private void Awake()
    {


        currentYear = 1;
        currentSeason = 0; // ��
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

        // �ð���� üũ
        timePassed = timePassed + Time.deltaTime;
        // ����ð� ���
        UpdateTime();

        // ���� �����, ���� ����޼��� ����
        if (dayOff) { EndOfTheDay(); }

        // ������, �ٸ�������Ʈ�� ���������� gold�� ���� �ʰ� goldearn�� ���� ����: �ٵ� �̰� �ǹ� �ֳ�?
        if (goldEarn != 0) { gold += goldEarn; goldEarn = 0; } // �̺�Ʈȭ ���

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
    void EndOfTheDay() //dayoff�� true�϶� ������� ȣ��, ������ ����� ��������� ����
    {
        dayTimePassed = 0;
        currentHour = 6;
        currentMinute = 0;
        ampm = "AM";
        currentDay++;

        Debug.Log("EndOfDay Called");

        bool monthChanged = false;
        if (currentDay > 28) { currentMonth++; currentDay = 1; monthChanged = true; }
        if (currentMonth > 4) { currentYear++; currentMonth = 1; } // �ʱ�ȭ �� ���� ����


        if (landData != null)
        {
            for (int i = 0; i < landData.Length; i++)
            {
                landData[i].dayChanged = true;
                landData[i].monthChanged = monthChanged;
            }
        }
        if(landTreeData != null)
        {
            for(int i = 0; i < landTreeData.Length; i++)
            {
                landTreeData[i].dayChanged = true;
                landTreeData[i].monthChanged = monthChanged;
            }
        }
        if(landWeedData != null)
        {
            for(int i = 0; i<landWeedData.Length; i++)
            {
                landTreeData[i].dayChanged = true;
                landTreeData[i].monthChanged = monthChanged;
            }
        }


        // �Ϸ簡 ����Ǿ����� : ħ�뿡�� ���� �ڵ� dayOff�� true�� �ȴ�.
        // ���ϻ��ڿ� �� ��ǰ���� sell price�� �ջ��� ȭ�鿡 ǥ���Ѵ�
        // ��ų�� ������ ������ �˷��ش�
        // �̺�Ʈ(�ڵ����� �ǹ� �ƴ�)�� �����Ѵ�
        // goldEarn = ���ϻ��ڿ� ����ִ� ��ǰ���� ������
        // ���ϻ��ڿ� ����ִ� ��ǰ���� ������ ���� �ٸ��� ���ؼ� goldEarn�� �־ �Ǳ��ϴµ�
        // �ű�ٰ� �� �󸶳� �ȾҴ��� üũ �� ���� �ֱ� �ѵ�......
        // �۹��� ���� �ѷ������� Ȯ���� �۹� ���¿� 1�� ���ϰ� ���� �ʱ�ȭ�Ѵ�
        // �������� ���̿� 1�� ���Ѵ�
        NewDayBegin();

        dayOff = false;
    }
    void NewDayBegin() // ���� ������ ���ƿ�, ��������� ���� ���� ���� �͵� ����
    {
        weather = nextdayweather;
        luck = 0f; // �̰� �׳� ����

        if (currentMonth == 1) // 1��, ���϶�
        {
            int randyum = 10; // ������ (1~101), �ӽ÷� ���� ��
            if (randyum > 70) { nextdayweather = 2; } // 30%Ȯ���� ���� ��
            else { nextdayweather = 1; } // 70�ۼ�Ʈ Ȯ���� ���� ����
        }

        // ������ ������ ��������
        // ����
        // ������ ���� ����
    }

    void weatherControll() // ���� ����
    {
        if (!wetherTotemUse)
        {
            switch (currentMonth)
            {
                case 1: //1�� �϶�
                    nextdayweather = 1; return; // Ȯ���� ���� ��, ���� ����
                case 2: //2�� �϶�
                    nextdayweather = 1; return; // Ȯ���� ���� ��, �����, ��ǳ, ���� ����
                case 3: //3�� �϶�
                    nextdayweather = 1; return; // Ȯ���� ���� ��, ��ǳ, ����, �ٶ� ����
                case 4: //4�� �϶�
                    nextdayweather = 1; return; // Ȯ���� ���� ��, ���� ����
            }
        }
        if (wetherTotemUse)
        {
            switch (wetherTotemNum)
            {
                case 1: //���� 1
                    nextdayweather = 1; return;
                case 2: //���� 2
                    nextdayweather = 2; return;
                case 3: //���� 3
                    nextdayweather = 3; return;
                case 4: //���� 4
                    nextdayweather = 4; return;
            }
        }
    }




    // �κ��丮 ����
    GameObject inventory;
    public bool isInventoryOn { get; private set; }
    PlayerLeftClick playerLeftClick;



    //���� ����Ǳ� ���� �� ��ü�� �����͸� �����ؾ� �Ѵ�.
    LandData[] landData;
    LandControl[] landControls;
    GameObject[] LandInFarm;
    void InitializeLandInFarmUnitList()
    {   //���忡 �ִ� Land��;
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
    {   //������ �ڶ�� Land.
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
    {   //���ʸ� �ڶ�� Land.
        GameObject[] weedlands = GameObject.FindGameObjectsWithTag("WeedLand");
        LandWeedControls = new LandControl[weedlands.Length];
        for (int i = 0; i < weedlands.Length; i++)
        {
            LandWeedControls[i] = weedlands[i].GetComponent<LandControl>();
        }
    }


    public void SaveLandInFarmData()
    {   //LandController�� ���� object���� �ε�.
        InitializeLandInFarmUnitList();

        landData = new LandData[landControls.Length]; //LandData�� ����
        //����� �����ؼ�.
        if (landControls == null)
        {
            return;
        }
        else if (landControls.Length > 0)
        {   //����� ������ 1�� �̻��̶��.
            //�� LandData�� LandUnitList.get

            for (int i = 0; i < landControls.Length; i++)
            {
                landData[i] = new LandData();
                landData[i].landType = landControls[i].landType;

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
                landData[i].savePosition = landControls[i].savePosition;
            }
        }

        SaveLandTreeData();
        SaveLandWeedDate();
    } // ���� Farm���� ���� �� ��

    public void LoadLandInFarmData()
    {   //LandController�� ���� object���� �ε�.
        InitializeLandInFarmUnitList();

        if (landData == null)
        {
            return;
        }
        else if (landControls.Length > 0)
        {   //����� ������ 1�� �̻��̶��.
            //�� LandUnitList�� �ڽĿ�����Ʈ�� ���� ��, �ڽĿ�����Ʈ�� ���� ����.
            for (int i = 0; i < landControls.Length; i++)
            {
                landControls[i].transform.position = landData[i].savePosition;
                landControls[i].dayChanged = landData[i].dayChanged;
                landControls[i].monthChanged = landData[i].monthChanged;

                landControls[i].landType = landData[i].landType;    // �ش� LandController�� Ÿ���� ����
                if (landData[i].landType == LandType.Empty) { }     // empty�� ����� �ൿ.
                else if (landData[i].landType == LandType.Weed ||   // ����, ��������, ������� ���.
                         landData[i].landType == LandType.Stone ||
                         landData[i].landType == LandType.Stick)
                {
                    landControls[i].transform.gameObject.GetComponent<LandControl>().prefabPath = landData[i].prefabPath;
                    GameObject child = Resources.Load(landData[i].prefabPath) as GameObject;//����� �������� child��� �̸� �ٿ��� �����ϴµ�.
                    Instantiate(child, landControls[i].transform.position, Quaternion.identity).transform.parent = landControls[i].transform;//landcontroller�� �ڽ����� �����Ѵ�.
                                                                                                                                             //��� ü���� �����ؾ� �ϱ� �ϳ�, ������ �����Ǹ鼭 ü���� 1�� �ʱ�ȭ �Ǳ� ������ �����Ѵ�.
                }
                else if (landData[i].landType == LandType.Tree)     // ������ ���.
                {
                    landControls[i].transform.gameObject.GetComponent<LandControl>().prefabPath = landData[i].prefabPath;
                    GameObject child = Resources.Load(landData[i].prefabPath) as GameObject;
                    Instantiate(child, landControls[i].transform.position, Quaternion.identity).transform.parent = landControls[i].transform;
                    // ���� ������ �ڽ� ������Ʈ�� �Ӽ�(ü�°� ����ܰ�)�� �����Ѵ�. �� ��쿡�� FieldTreeLand �̴�.
                    FieldTreeLand fieldTreeLand = landControls[i].GetComponentInChildren<FieldTreeLand>();
                    fieldTreeLand.hp = landData[i].currentHP;
                    fieldTreeLand.currentLevel = landData[i].level;
                }
                else if (landData[i].landType == LandType.Farm)     // ������� ���.
                {
                    landControls[i].transform.gameObject.GetComponent<LandControl>().prefabPath = landData[i].prefabPath;
                    GameObject child = Resources.Load(landData[i].prefabPath) as GameObject;
                    Instantiate(child, landControls[i].transform.position, Quaternion.identity).transform.parent = landControls[i].transform;
                    //������ �ڽ� ������Ʈ�� �Ӽ�(���ۿ���, ��������, ��������, ��������(������), ����ܰ�)�� �����Ѵ�. �� ��쿡�� farmlandcontrol �� cropcontrol�̴�.
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
                }
            }
        }
        LoadLandTreeData();
        LoadLandWeedData();
    } // ���� Farm���� ���� �� ��

    public void SaveLandTreeData()
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
                else if (landTreeData[i].landType == LandType.Stick)
                {
                    landTreeData[i].prefabPath = LandTreeControls[i].prefabPath;
                    landTreeData[i].currentHP = LandTreeControls[i].currentHP;
                }
                else if (landTreeData[i].landType == LandType.Tree)
                {   //����ְų� �����̰ų�.
                    landTreeData[i].prefabPath = LandTreeControls[i].prefabPath;
                    landTreeData[i].currentHP = LandTreeControls[i].currentHP;
                    landTreeData[i].level = LandTreeControls[i].level;
                }
                Debug.Log(landTreeData[i].prefabPath);
                
            }

            
        }
    }

    public void LoadLandTreeData()
    {
        InitializeTreeLand();
        if (landTreeData == null)
        {
            return;
        }
        else if (LandTreeControls.Length > 0)
        {   //����� ������ 1�� �̻��̶��.
            //�� LandUnitList�� �ڽĿ�����Ʈ�� ���� ��, �ڽĿ�����Ʈ�� ���� ����.
            for (int i = 0; i < LandTreeControls.Length; i++)
            {
                LandTreeControls[i].transform.position = landTreeData[i].savePosition;
                LandTreeControls[i].dayChanged = landTreeData[i].dayChanged;
                LandTreeControls[i].monthChanged = landTreeData[i].monthChanged;

                LandTreeControls[i].landType = landTreeData[i].landType;    // �ش� LandController�� Ÿ���� ����

                if (landTreeData[i].landType == LandType.Empty) { }     // empty�� ����� �ൿ.
                else if (landTreeData[i].landType == LandType.Stick)
                {
                    LandTreeControls[i].transform.gameObject.GetComponent<LandControl>().prefabPath = landTreeData[i].prefabPath;
                    GameObject child = Resources.Load(landTreeData[i].prefabPath) as GameObject;
                    Instantiate(child, LandTreeControls[i].transform.position, Quaternion.identity).transform.parent = LandTreeControls[i].transform;
                }
                else if (landTreeData[i].landType == LandType.Tree)
                {
                    LandTreeControls[i].transform.gameObject.GetComponent<LandControl>().prefabPath = landTreeData[i].prefabPath;
                    GameObject child = Resources.Load(landTreeData[i].prefabPath) as GameObject;
                    Instantiate(child, LandTreeControls[i].transform.position, Quaternion.identity).transform.parent = LandTreeControls[i].transform;
                    // ���� ������ �ڽ� ������Ʈ�� �Ӽ�(ü�°� ����ܰ�)�� �����Ѵ�. �� ��쿡�� FieldTreeLand �̴�.
                    FieldTreeLand fieldTreeLand = LandTreeControls[i].GetComponentInChildren<FieldTreeLand>();
                    fieldTreeLand.hp = landTreeData[i].currentHP;
                    fieldTreeLand.currentLevel = landData[i].level;
                }

                Debug.Log(landTreeData[i].dayChanged);
            }
        }
    }

    public void SaveLandWeedDate()
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
                {   //����ְų� �����̰ų�.
                    landWeedData[i].prefabPath = LandWeedControls[i].prefabPath;
                    landWeedData[i].currentHP = LandWeedControls[i].currentHP;
                }
            }
        }
    }

    public void LoadLandWeedData()
    {
        InitializeWeedLand();
        if (landWeedData == null)
        {
            return;
        }
        else if (LandWeedControls.Length > 0)
        {   //����� ������ 1�� �̻��̶��.
            //�� LandUnitList�� �ڽĿ�����Ʈ�� ���� ��, �ڽĿ�����Ʈ�� ���� ����.
            for (int i = 0; i < LandWeedControls.Length; i++)
            {
                LandWeedControls[i].transform.position = landWeedData[i].savePosition;
                LandWeedControls[i].dayChanged = landWeedData[i].dayChanged;
                LandWeedControls[i].monthChanged = landWeedData[i].monthChanged;

                LandWeedControls[i].landType = landWeedData[i].landType;    // �ش� LandController�� Ÿ���� ����

                if (landWeedData[i].landType == LandType.Empty) { }     // empty�� ����� �ൿ.
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