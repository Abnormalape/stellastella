using UnityEngine;
public class WeedLand : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] weedPrefabs;
    int currentDate;
    int currentSeason;
    private void Awake() // 게임 시작할 때 초기화
    {
        currentDate = gameManager.currentDay;
    }

    private void Update()
    {

    }
    public void OutSideFarm()
    {
        if (true)
        {
            if (currentDate != gameManager.currentDay)
            {
                // 날이 바뀔때 확률적으로 잡초 프리팹을 생성한다.
                currentDate = gameManager.currentDay;
            }
        }
    }

    public void InSideFarm()
    {
        if (true)
        {
            if (currentSeason != gameManager.currentSeason)
            {
                // 계절이 바뀔때 확률적으로 풀 프리팹을 생성한다.
                currentDate = gameManager.currentDay;
            }
        }
    }
}
