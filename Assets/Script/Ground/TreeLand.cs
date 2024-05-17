using UnityEngine.SceneManagement;
using UnityEngine;
public class TreeLand : MonoBehaviour // 조건에 따라 나무 자식 오브젝트를 생성한다.
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] treePrefabs;

    int currentDate;
    int currentSeason;
    private void Awake() // 게임 시작할 때 초기화
    {
        currentDate = gameManager.currentDay;
    }

    private void Update()
    {
        // 이 씬이 농장인지 아닌지에 따라 if문을 사용하여 아래 두가지 메서드를 실행한다
    }
    public void OutSideFarm()
    {
        // 농장 외부에서 자연적으로 나무가 자라는 땅은 그 자리에 다른것이 생성되지 않는다
        // 즉 자식 오브젝트가 없다는 말은 나무가 없다는 뜻이다.
        if (transform.childCount == 0 && gameManager.currentMonth != 3) // 나무가 없는데 겨울이 아니라면
        {
            if(currentDate != gameManager.currentDay) // 내가 가진 날짜가 시스템의 날짜와 맞지 않다면 = 날이 지났다면.
            {
                // 확률적으로 프리팹을 생성하라 = (프리팹 : 나무씨앗).
                // 나무씨앗은 자기가 알아서 날짜를 받고 성장한다.
                currentDate = gameManager.currentDay;
            }
        }
    }

    public void InSideFarm()
    {
        // 이 땅위에 아무것도 없는데, 근처에 나무가 있다면
        if (transform.childCount == 0 && gameManager.currentMonth != 3) // 근처의 나무를 찾아서 리스트로 만들고 랜덤하게 하나를 찾아 그 나무의 씨앗을 프리팹으로 부른다.
        {
            if (currentDate != gameManager.currentDay) // 내가 가진 날짜가 시스템의 날짜와 맞지 않다면 = 날이 지났다면.
            {
                // 확률적으로 프리팹을 생성하라 = (프리팹 : 나무씨앗).
                // 나무씨앗은 자기가 알아서 날짜를 받고 성장한다.
                currentDate = gameManager.currentDay;
            }

            if (currentSeason != gameManager.currentSeason) // 계절이 바뀌었다면
            {
                // 확률적으로 나무 도막을 생성한다.
                currentSeason = gameManager.currentSeason;
            }
        }
    }
}
