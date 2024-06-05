using JetBrains.Annotations;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public int weather = 0; // 0:����
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

    void EndOfTheDay() //dayoff�� true�϶� ������� ȣ��, ������ ����� ��������� ����
    {
        currentHour = 6;
        currentMinute = 0;
        ampm = "AM";
        currentDay++;
        if (currentDay > 28) { currentMonth++; currentDay = 1; }
        if (currentMonth > 4) { currentYear++; currentMonth = 1; } // �ʱ�ȭ �� ���� ����
        // �Ϸ簡 ����Ǿ����� : ħ�뿡�� ���� �ڵ� dayOff�� true�� �ȴ�.
        // ���ϻ��ڿ� �� ��ǰ���� sell price�� �ջ��� ȭ�鿡 ǥ���Ѵ�
        // ��ų�� ������ ������ �˷��ش�
        // �̺�Ʈ(�ڵ����� �ǹ� �ƴ�)�� �����Ѵ�
        // goldEarn = ���ϻ��ڿ� ����ִ� ��ǰ���� ������
        // ���ϻ��ڿ� ����ִ� ��ǰ���� ������ ���� �ٸ��� ���ؼ� goldEarn�� �־ �Ǳ��ϴµ�
        // �ű�ٰ� �� �󸶳� �ȾҴ��� üũ �� ���� �ֱ� �ѵ�......

        gold += goldEarn;

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
    void InitializeLandUnitList()
    {
        landControls = FindObjectsOfType<LandControl>();
    }


    public void SaveLandData()
    {   //LandController�� ���� object���� �ε�.
        InitializeLandUnitList();

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
    } // ���� Farm���� ���� �� ��

    public void LoadLandData()
    {   //LandController�� ���� object���� �ε�.
        InitializeLandUnitList();

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
                }
            }
        }
    } // ���� Farm���� ���� �� ��

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