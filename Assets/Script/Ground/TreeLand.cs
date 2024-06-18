using UnityEditor;
using UnityEngine;
using static TreeLand;
using Random = UnityEngine.Random;
public class TreeLand : MonoBehaviour // ���������� ����Ѵ�.
{
    [SerializeField] GameManager gameManager;
    [SerializeField] public GameObject myTreePrefab; // �ܺο�(������ ����), ������ ����.
    [SerializeField] GameObject[] treePrefabs; // ���ο�(3���� �⺻����)
    GameObject onMeTreePrefab; // ���ο�(���� �ڽ����� ������ �ִ� ������ ������)

    int currentDate;
    int currentMonth;

    private string prefabpath = "";
    public string prefabPath
    {
        get { return prefabpath; }
        set
        {
            prefabpath = value;
        }
    }


    [SerializeField] int myPrefabNumber; // myprefab�� ��ȣ : oaktree = 1, mapletree = 2, pinetree = 3;

    [SerializeField]
    public bool AtFarm = false;

    //=======================================//
    public delegate void CurrentTreeLevelUpdated(int newValue);
    public event CurrentTreeLevelUpdated OnCurrentTreeLevelUpdated;

    private int currentlevel;
    public int CurrentLevel
    {
        get { return currentlevel; }
        set
        {
            currentlevel = value;
            OnCurrentTreeLevelUpdated?.Invoke(currentlevel);
        }
    }
    //=======================================//


    private void Awake() // ���� ������ �� �ʱ�ȭ
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentDate = gameManager.currentDay;
        currentMonth = gameManager.currentMonth;

        if (GetComponent<LandControl>() != null)
        {
            landControl = GetComponent<LandControl>();
        }

        if (!AtFarm)
        {
            prefabpath = $"Prefabs/FieldTree/{myTreePrefab.name}";
        }
    }
    private void Start()
    {
        if (GetComponent<LandControl>() != null)
        {
            landControl.OnAValueUpdated += HandleAValueUpdated;
            landControl.OnBValueUpdated += HandleBValueUpdated;
        }
    }




    LandControl landControl;
    bool monthChanged;
    bool dayChanged;
    void HandleAValueUpdated(bool newValue)
    {
        monthChanged = newValue; //���� �ٲ������.

        if (AtFarm)
        {   //���� ���ο� �޼���
            InSideFarmSummonTree();
            //���� ������ ������ ���� ���� �����Ǵ� ���� ������, �ֺ� ������ �������� �ڶ�� ������ �ִ�.
            //�ֺ� ������ �������� �ڶ����� �ش� ������ �� �ڽ����� �ش� ������ �����հ� ���� ������ ��ȯ�Ѵ�.
            //�׷��ٸ� 
        }
        else
        {   //���� �ܺο� �޼���
            OutSideFarmSummonTree();
            //���� �ܺο� ������ ���� ������ �ڶ��� ������, ������ ������¿� ü���� ����Ѵ�.
            //�׷��� �ܺο� ������ LandControl�� summonedprefab�� mytreeprefab�̴�. 
        }
    }

    void HandleBValueUpdated(bool newValue)
    {
        dayChanged = newValue; //���� �ٲ������.

        if (AtFarm)
        {
            InSideFarmSummonTree();
        }
        else
        {
            OutSideFarmSummonTree();
        }
    }
    private void Update()
    {

    }

    public void OutSideFarmSummonTree()
    {
        if (dayChanged)
        {

            //��¥ ����ȭ�� �׻�.
            currentDate = gameManager.currentDay;
            dayChanged = false;

            if (transform.childCount == 0 && gameManager.currentMonth != 3)
            {   // �ڽ��� ���ٸ� 50% Ȯ���� 3�ܰ� ���� ����, �ܿ��� �ƴ�.
                int i = Random.Range(0, 100);
                if (i >= 50)
                {
                    // myTreePrefab �� �̹� unity���� ������ �Ϸ��ߴ�.
                    Instantiate(myTreePrefab, this.transform.position, Quaternion.identity, this.transform);
                    //�ܺο� ������ �����տ� ������ �������� �̹� �����Ǿ� �ִ�.
                    CurrentLevel = 2;
                }

            }
            else if (transform.childCount != 0 && transform.GetComponentInChildren<FieldTreeLand>() != null)
            {   //�ڽ��� �ְ�, �ڽ��� �ʵ�Ʈ���� ������ �ִٸ�.
                int i = Random.Range(0, 100);
                if (i >= 20)
                {   // 80�� Ȯ���� ����.
                    if (currentlevel < 4)
                    {
                        CurrentLevel++;
                    }
                    else
                    {
                        CurrentLevel = CurrentLevel;
                    }
                }
            }
        }
    }

    public void InSideFarmSummonTree()
    {
        //�ٴ��� Ȯ�������� �������� ����
        if (monthChanged)
        {
            monthChanged = false;
            // ��¥ ����ȭ�� �׻�.
            currentMonth = gameManager.currentMonth;
            if (transform.childCount == 0)
            {
                // �������� �������� ��ȯ // �ΰ��� ���� 
                int R = Random.Range(0, 100);
                if (R >= 80)
                {
                    GameObject summonStick;
                    int i = Random.Range(0, 2);
                    if (i == 0) { summonStick = Resources.Load($"Prefabs/FieldTree/Stick{i + 1}") as GameObject; }
                    else { summonStick = Resources.Load($"Prefabs/FieldTree/Stick{i + 1}") as GameObject; }
                    Instantiate(summonStick, this.transform.position, Quaternion.identity).transform.parent = this.transform;
                    prefabPath = $"Prefabs/FieldTree/Stick{i + 1}";
                }
            }
        }
        //���� Ȯ�������� ���ѻ���
        else if (dayChanged)
        {
            dayChanged = false;
            // ��¥ ����ȭ�� �׻�.
            currentDate = gameManager.currentDay;
            // �ڽ� ������Ʈ�� �ְ�, ������ �ܿ��� �ƴϸ�, �ڽĿ�����Ʈ�� "�ʵ�Ʈ�� ������Ʈ"��ũ��Ʈ �� �ִٸ�.
            if (transform.childCount != 0 && gameManager.currentMonth != 3 && transform.GetComponentInChildren<FieldTreeLand>() != null)
            {
                // �׷��� �� �ڽ� ������Ʈ�� ����ܰ谡 4(����)�̶��
                if (transform.GetComponentInChildren<FieldTreeLand>().currentLevel == 4)
                {
                    //Ȯ��������. 
                    int i = Random.Range(0, 100);
                    if (i >= 80)
                    {
                        // ��ó 8ĭ�߿� �ڽ��� ������, ������Ʈ�� TreeLand ��ũ��Ʈ�� ���� ������Ʈ�� �ϳ��� ��� ������ ��ȯ�Ѵ�.
                        // ��ó ������Ʈ �ҷ����� -> �� �� Ư�� ������Ʈ �����ָ� �ҷ����� -> �� �� �ڽ��� ���� �ָ� ã��
                        // �³׵�� �迭 ���� ���� �ϰ� �ϳ��� �ҷ�����
                        // Instantiate�� FieldTreeObject�� ���� �������� level 0���� ����� �ҷ����� ��ó ������Ʈ�� �ڽ����� �־��ֱ�
                    }
                }
            }
        }

    }
}