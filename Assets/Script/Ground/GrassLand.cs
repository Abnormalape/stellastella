using UnityEngine;
public class GrassLand : MonoBehaviour // 풀 자식 오브젝트를 생성한다.
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] GrassPrefabs;

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
        // 얘는 근처에 풀이 있으면 그냥 자란다.
        if (true) // 근처에 풀이 있다면.
        {
            if (currentDate != gameManager.currentDay)
            {
                // 날이 바뀔때 확률적으로 풀 프리팹을 생성한다.
                currentDate = gameManager.currentDay;
            }
        }
    }

    public void InSideFarm()
    {
        // 얘는 근처에 풀이 있으면 그냥 자란다.
        if (true) // 근처에 풀이 있다면.
        {
            if (currentDate != gameManager.currentDay)
            {
                // 날이 바뀔때 확률적으로 풀 프리팹을 생성한다.
                currentDate = gameManager.currentDay;
            }
        }
    }
}
