using UnityEngine;
public class StoneLand : MonoBehaviour // 조건에 따라 광석 자식 오브젝트를 생성한다.
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] stonePrefabs;

    int currentDate;
    int currentSeason;
    private void Awake() // 게임 시작할 때 초기화
    {
        currentDate = gameManager.currentDay;
    }

    private void Update()
    {
        // 이 씬이 던전인지 농장인지에 따라 아래 메서드를 실행한다
    }
    public void InDungeon()
    {
        // 던전에서 땅위에 돌을 생성한다.
        if (true) //내 씬이 로드되었다면
        {
            // 씬의 정보에 따라 다른 프리팹을 확률에 따라 생성한다. (예: 던전 100층이면 구리:10%, 금 30%).
            // 이 프리팹들은 단순하게 부서지고 아이템을 드랍하는것만 본다.
        }
    }

    public void InSideFarm()
    {
        // 이 땅위에 아무것도 없다면
        if (transform.childCount == 0)
        {
            if (currentSeason != gameManager.currentSeason) // 계절이 바뀌었다면
            {
                // 확률적으로 프리팹을 생성하라 = (프리팹 : 돌1, 돌2).
                currentSeason = gameManager.currentSeason;
            }
        }
    }
}
