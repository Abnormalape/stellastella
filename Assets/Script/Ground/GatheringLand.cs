using UnityEngine;
public class GatheringLand : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] GatheringPrefabs;
    int currentDate;
    int currentSeason = 0; // 계절 초기화를 위한 숫자설정 : 날짜는 1부터 시작.
    private void Awake() // 게임 시작할 때 초기화
    {
        currentDate = gameManager.currentDay;
    }

    private void Update()
    {

    }
    public void OutSideFarm()   // 농장 밖에서 날이 바뀔때 확률적으로 채집물을 생성한다.
                                // 계절이 바뀌면 채집물의 종류가 달라진다.
    {
        if (currentSeason != gameManager.currentSeason) // 계절이 변경되면
        {
            currentSeason = gameManager.currentSeason; // 계절을 업데이트 하고
            switch (currentSeason)
            {
                case 1: // 봄
                    //4개의 프리팹 지정
                    break;
                case 2:
                    //4개의 프리팹 지정
                    break;
                case 3:
                    //4개의 프리팹 지정
                    break;
                case 4: // 겨울
                    //4개의 프리팹 지정
                    break;

            }

            if (currentDate != gameManager.currentDay)
            {
                // 날이 바뀔때 확률적으로 지정된 채집물 프리팹을 생성한다.
                currentDate = gameManager.currentDay;
            }
        }
    }
}
