using UnityEditor;
using UnityEngine;
using static TreeLand;
using Random = UnityEngine.Random;
public class TreeLand : MonoBehaviour // 나무생성을 담당한다.
{
    [SerializeField] GameManager gameManager;
    [SerializeField] public GameObject myTreePrefab; // 외부용(지정된 나무), 설정된 나무.
    [SerializeField] GameObject[] treePrefabs; // 내부용(3개의 기본나무)
    GameObject onMeTreePrefab; // 내부용(내가 자식으로 가지고 있는 나무의 프리팹)

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


    [SerializeField] int myPrefabNumber; // myprefab의 번호 : oaktree = 1, mapletree = 2, pinetree = 3;

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


    private void Awake() // 게임 시작할 때 초기화
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
        monthChanged = newValue; //달이 바뀌었을때.

        if (AtFarm)
        {   //농장 내부용 메서드
            InSideFarmSummonTree();
            //농장 내부의 나무는 계절 마다 생성되는 나무 도막과, 주변 나무의 영향으로 자라는 나무가 있다.
            //주변 나무의 영향으로 자랄때는 해당 나무가 내 자식으로 해당 나무의 프리팹과 같은 나무를 소환한다.
            //그렇다면 
        }
        else
        {   //농장 외부용 메서드
            OutSideFarmSummonTree();
            //농장 외부용 나무는 오직 나무만 자랄수 있으며, 나무의 성장상태와 체력을 계승한다.
            //그러니 외부용 나무의 LandControl의 summonedprefab은 mytreeprefab이다. 
        }
    }

    void HandleBValueUpdated(bool newValue)
    {
        dayChanged = newValue; //일이 바뀌었을때.

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

            //날짜 동기화는 항상.
            currentDate = gameManager.currentDay;
            dayChanged = false;

            if (transform.childCount == 0 && gameManager.currentMonth != 3)
            {   // 자식이 없다면 50% 확률로 3단계 묘목 생성, 겨울이 아님.
                int i = Random.Range(0, 100);
                if (i >= 50)
                {
                    // myTreePrefab 은 이미 unity에서 삽입을 완료했다.
                    Instantiate(myTreePrefab, this.transform.position, Quaternion.identity, this.transform);
                    //외부용 나무는 프리팹에 생성할 프리팹이 이미 배정되어 있다.
                    CurrentLevel = 2;
                }

            }
            else if (transform.childCount != 0 && transform.GetComponentInChildren<FieldTreeLand>() != null)
            {   //자식이 있고, 자식이 필드트리를 가지고 있다면.
                int i = Random.Range(0, 100);
                if (i >= 20)
                {   // 80퍼 확률로 성장.
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
        //다달이 확률적으로 나무조각 생성
        if (monthChanged)
        {
            monthChanged = false;
            // 날짜 동기화는 항상.
            currentMonth = gameManager.currentMonth;
            if (transform.childCount == 0)
            {
                // 나무도막 프리팹을 소환 // 두가지 형태 
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
        //매일 확률적으로 씨앗생성
        else if (dayChanged)
        {
            dayChanged = false;
            // 날짜 동기화는 항상.
            currentDate = gameManager.currentDay;
            // 자식 오브젝트가 있고, 계절이 겨울이 아니며, 자식오브젝트에 "필드트리 오브젝트"스크립트 가 있다면.
            if (transform.childCount != 0 && gameManager.currentMonth != 3 && transform.GetComponentInChildren<FieldTreeLand>() != null)
            {
                // 그런데 그 자식 오브젝트가 성장단계가 4(성목)이라면
                if (transform.GetComponentInChildren<FieldTreeLand>().currentLevel == 4)
                {
                    //확률적으로. 
                    int i = Random.Range(0, 100);
                    if (i >= 80)
                    {
                        // 근처 8칸중에 자식이 없으며, 컴포넌트로 TreeLand 스크립트를 가진 오브젝트중 하나를 골라 씨앗을 소환한다.
                        // 근처 오브젝트 불러오기 -> 그 중 특정 컴포넌트 가진애만 불러오기 -> 그 중 자식이 없는 애만 찾기
                        // 걔네들로 배열 만들어서 랜덤 하게 하나를 불러오기
                        // Instantiate로 FieldTreeObject를 가진 프리팹을 level 0으로 만들어 불러와진 근처 오브젝트의 자식으로 넣어주기
                    }
                }
            }
        }

    }
}