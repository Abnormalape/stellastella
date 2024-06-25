using UnityEngine;
public class WeedLand : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] weedPrefabs;
    LandControl LandControl;

    [SerializeField] bool atFarm;

    [SerializeField] int currentDate;
    [SerializeField] int currentMonth;
    private void Awake() // 게임 시작할 때 초기화
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentDate = gameManager.currentDay;
        currentMonth = gameManager.currentMonth;

        landControl = GetComponent<LandControl>();
        landControl.OnAValueUpdated += HandleAValueUpdated;
        landControl.OnBValueUpdated += HandleBValueUpdated;
    }
    private void Start()
    {

    }

    LandControl landControl;
    bool monthChanged;
    bool dayChanged;

    void HandleAValueUpdated(bool newValue)
    {
        monthChanged = newValue;
        if (atFarm == false)
        {
            OutSideFarm();
        }
        else if (atFarm == true)
        {
            InSideFarm();
        }
    }

    void HandleBValueUpdated(bool newValue)
    {
        dayChanged = newValue;
        if (atFarm == false)
        {
            OutSideFarm();
        }
        else if (atFarm == true)
        {
            InSideFarm();
        }
    }
    private void Update()
    {
    }

    GameObject summonWeed;
    public string prefabPath = "";
    public void OutSideFarm()
    {
        if (dayChanged)
        {
            currentDate = gameManager.currentDay;
            currentMonth = gameManager.currentMonth;
            if (transform.childCount == 0 && gameManager.currentMonth != 3)
            {   // 농장 바깥에선 나날이 잡초생성 60%
                int R = Random.Range(0, 100);
                if (R >= 40)
                {
                    int i = Random.Range(0, 2);
                    if (i == 0)
                    {
                        summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i + 1}") as GameObject;
                    }
                    else
                    {
                        summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i + 1}") as GameObject;
                    }
                    Instantiate(summonWeed, this.transform.position, Quaternion.identity).transform.parent = this.transform;
                    prefabPath = $"Prefabs/Weedprefabs/Weed{i + 1}";
                }
            }

            dayChanged = false;
        }
    }

    public void InSideFarm()
    {
        if (monthChanged)
        {   // 달에한번
            // 날짜 동기화는 항상.
            monthChanged = false;
            dayChanged = false;

            currentMonth = gameManager.currentMonth;
            currentDate = gameManager.currentDay;
            if (transform.childCount == 0)
            {
                int R = Random.Range(0, 100);
                if (R >= 80)
                {
                    GameObject summonWeed;
                    int i = Random.Range(0, 2);
                    if (i == 0)
                    {
                        summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i + 1}") as GameObject;
                        prefabPath = $"Prefabs/Weedprefabs/Weed{i + 1}";
                    }
                    else
                    {
                        summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i + 1}") as GameObject;
                        prefabPath = $"Prefabs/Weedprefabs/Weed{i + 1}";
                    }
                    Instantiate(summonWeed, this.transform.position, Quaternion.identity).transform.parent = this.transform;
                }
                return;
            }
        }
    }
}
