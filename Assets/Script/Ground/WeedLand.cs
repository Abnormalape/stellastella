using UnityEngine;
public class WeedLand : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] weedPrefabs;

    [SerializeField] bool atFarm;

    [SerializeField] int currentDate;
    [SerializeField] int currentMonth;
    private void Awake() // 게임 시작할 때 초기화
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentDate = gameManager.currentDay;
        currentMonth = gameManager.currentMonth;
    }

    private void Update()
    {
        if(atFarm == false)
        {
            OutSideFarm();
        }
        else if (atFarm == true)
        {
            InSideFarm();
        }
    }
    public void OutSideFarm()
    {
        if (currentDate != gameManager.currentDay)
        {
            //날짜 동기화는 항상.
            currentDate = gameManager.currentDay;
            currentMonth = gameManager.currentMonth;
            if (transform.childCount == 0 && gameManager.currentMonth != 3)
            {   // 농장 바깥에선 나날이 잡초생성 60%
                int R = Random.Range(0, 100);
                if (R >= 40)
                {
                    GameObject summonWeed;
                    int i = Random.Range(0,2);
                    if (i == 0){ summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i+1}") as GameObject; } 
                    else { summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i+1}") as GameObject; } 
                    Instantiate(summonWeed,this.transform.position,Quaternion.identity).transform.parent = this.transform;
                }
            }
        }
    }

    public void InSideFarm()
    {
        if (currentMonth != gameManager.currentMonth)
        {   // 달에한번
            // 날짜 동기화는 항상.
            currentMonth = gameManager.currentMonth;
            currentDate = gameManager.currentDay;
            if (transform.childCount == 0)
            {
                int R = Random.Range(0, 100);
                if (R >= 80)
                {
                    GameObject summonWeed;
                    int i = Random.Range(0, 2);
                    if (i == 0) { summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i + 1}") as GameObject; }
                    else { summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i + 1}") as GameObject; }
                    Instantiate(summonWeed, this.transform.position, Quaternion.identity).transform.parent = this.transform;
                }
                return;
            }
        }
        //else if (currentDate !=  gameManager.currentDay)
        //{   // 나날이
        //    currentDate = gameManager.currentDay;
        //    if(transform.childCount == 0)
        //    {
        //        int R = Random.Range(0, 100);
        //        if (R >= 90)
        //        {
        //            GameObject summonWeed;
        //            int i = Random.Range(0, 2);
        //            if (i == 0) { summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i + 1}") as GameObject; }
        //            else { summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i + 1}") as GameObject; }
        //            Instantiate(summonWeed, this.transform.position, Quaternion.identity).transform.parent = this.transform;
        //        }
        //    }
        //}
    }
}
