using UnityEngine.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;
public class TreeLand : MonoBehaviour // 나무생성을 담당한다.
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject myTreePrefab; // 외부용(지정된 나무)
    [SerializeField] GameObject[] treePrefabs; // 내부용(3개의 기본나무)

    int currentDate;
    int currentMonth;

    [SerializeField]
    bool AtFarm = false;
    private void Awake() // 게임 시작할 때 초기화
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentDate = gameManager.currentDay;
        currentMonth = gameManager.currentMonth;
    }

    private void Update()
    {
        if (currentDate != gameManager.currentDay || currentMonth != gameManager.currentMonth)
        {
            if (AtFarm)
            {   //농장 내부용 메서드
                InSideFarmSummonTree();
            }
            else
            {   //농장 외부용 메서드
                OutSideFarmSummonTree();
            }
        }
    }
    public void OutSideFarmSummonTree()
    {
        if (currentDate != gameManager.currentDay)
        {
            //날짜 동기화는 항상.
            currentDate = gameManager.currentDay;

            if (transform.childCount == 0 && gameManager.currentMonth != 3)
            {   // 자식이 없다면 20% 확률로 3단계 묘목 생성, 겨울이 아님.
                int i = Random.Range(0, 100);
                if (i >= 80)
                {
                    // myTreePrefab 은 이미 unity에서 삽입을 완료했다.
                    myTreePrefab.GetComponent<FieldTreeObject>().currentLevel = 2; // 0단계는 씨앗.
                    Instantiate(myTreePrefab, this.transform.position, Quaternion.identity).transform.parent = this.transform;
                }
            }
        }
    }

    public void InSideFarmSummonTree()
    {
        //다달이 확률적으로 나무조각 생성
        if (currentMonth != gameManager.currentMonth)
        {
            // 날짜 동기화는 항상.
            currentMonth = gameManager.currentMonth;
            if (transform.childCount == 0)
            {
                // 나무도막 프리팹을 소환
                // 두가지 형태 
                // GameObject summonStick;
                // int i = Random.Range(0,2);
                // if (int i = 0){ GameObject summonStick = Resource.Load($"Prefabs/FieldTree/Stick{i+1}") as GameObject; } 
                // else { GameObject summonStick = Resource.Load($"Prefabs/FieldTree/Stick{i+1}") as GameObject; } 
                // Instantiate(summonStick,this.transform.position,Quaternion.identity).transform.parent = this.transform;
                // 
            }
        }
        //매일 확률적으로 씨앗생성
        else if (currentDate != gameManager.currentDay)
        {
            // 날짜 동기화는 항상.
            currentDate = gameManager.currentDay;
            // 자식 오브젝트가 있고, 계절이 겨울이 아니며, 자식오브젝트에 "필드트리 오브젝트"스크립트 가 있다면.
            if (transform.childCount != 0 && gameManager.currentMonth != 3 && transform.GetComponentInChildren<FieldTreeObject>() != null)
            {
                // 그런데 그 자식 오브젝트가 성장단계가 4(성목)이라면
                if (transform.GetComponentInChildren<FieldTreeObject>().currentLevel == 4)
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