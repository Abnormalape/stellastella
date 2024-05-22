using UnityEditor.SearchService;
using UnityEngine;
using Random = UnityEngine.Random;
public class StoneLand : MonoBehaviour // 조건에 따라 광석 자식 오브젝트를 생성한다.
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] stonePrefabs;

    [SerializeField] bool makeOre;  // 채석장, 던전등 광석이 스폰되는 장소에서 킴.
    [SerializeField] bool makeStone; // 돌만 스폰되어야 하는 장소에서 킨다.
    
    
    int currentDate;
    int currentSeason;
    private void Awake() // 게임 시작할 때 초기화
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentDate = gameManager.currentDay;
        currentSeason = gameManager.currentSeason;
    }

    private void Update()
    {
        if (makeOre)
        {
            MakeOre();
        }
        if (makeStone)
        {
            MakeStone();
        }
    }
    public void MakeOre() // 돌을 포함한 광석을 만드는 메서드 : 
    {
        // 던전에서 땅위에 돌을 생성한다.
        if (true) //내 씬이 로드되었다면
        {
            // 씬의 정보에 따라 다른 프리팹을 확률에 따라 생성한다. (예: 던전 100층이면 구리:10%, 금 30%).
            // 이 프리팹들은 단순하게 부서지고 아이템을 드랍하는것만 본다.
        }
    }

    public void MakeStone()
    { 
        if (transform.childCount == 0) // LandController에 자식이 없다면.
        {
            if (currentSeason != gameManager.currentSeason) // 계절이 바뀌었다면
            {
                int i = Random.Range(1, 100);
                if (i > 90) // 10%의 확률로
                {
                    // Instantiate(); // 프리팹을 생성한다.
                }
                // 확률적으로 프리팹을 생성하라 = (프리팹 : 돌1, 돌2).
                currentSeason = gameManager.currentSeason;
            }
        }
        else if (transform.childCount > 0) // LandController에 자식이 있다면
        {
            // 자식 오브젝트가 가진 Component를 판단해서 대신 나를 소환한다
        }
    }
}
