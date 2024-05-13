using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int currentYear; //햇수 직접적인 영향은 2년차에 상점품목 가격인상과 몇몇 작물 추가 정도 밖에 없다
    int currentSeason; //계절 = month인 세계이지만 일단은 이렇게 쓰자.
    int currentMonth;
    int currentDay; // 모든 달은 28일까지 고정적으로 있다.
    int currentHour; // 시간은 오전 6시에 시작해서 오전 2시에 끝난다.
    int currentMinute; // 7초에 10분? 계산하기 편하게 10초에 10분으로 하자 1분에 한시간
    string ampm;
    bool dayOff;
    bool tempinvenonoff = false;
    GameObject inventory;

    int gold;
    int goldEarn;

    float luck;
    int weather;
    int nextdayweather;

    float timePassed;
    private void Awake()
    {
        inventory = transform.GetChild(0).gameObject; // 인벤토리의 정체
        inventory.SetActive(false); // 비활성화

        currentYear = 1;
        currentSeason = 1;
        currentMonth = 1;
        currentDay = 1;
        currentHour = 6;
        currentMinute = 0;
        ampm = "AM";
        dayOff = false;
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        UpdateTime();
        if (dayOff ) { EndOfTheDay(); }

        OpenInventory(); // 이거 이벤트화 할 수 있으면 해보자
    }
    void UpdateTime()
    {
        if (currentHour == 26) { dayOff = true; }
        if (timePassed >= 10f)
        {
            currentMinute += 10;
            timePassed = 0;
        }
        if (currentMinute == 60)
        {
            currentHour += 1;
        }

        if (currentHour >= 24)
        {
            ampm = "AM";
        }
        else if (currentHour >= 12)
        {
            ampm = "PM";
        }
        else { ampm = "AM"; }
    }

    void EndOfTheDay()
    {
        dayOff = false ;
        currentHour = 6;
        currentMinute = 0;
        ampm = "AM";
        currentDay++;
        if (currentDay > 28) { currentMonth++; currentDay = 1; }
        if (currentMonth > 4) { currentYear++; currentMonth = 1; } // 초기화 및 일차 진행
        // 하루가 종료되었을때 : 침대에서 잠을 자도 dayOff가 true가 된다.
        // 출하상자에 들어간 물품들의 sell price를 합산해 화면에 표시한다
        // 스킬의 레벨이 오르면 알려준다
        // 이벤트(코딩적인 의미 아님)을 진행한다
        // goldEarn = 출하상자에 들어있는 물품들의 가격합
        // 출하상자에 들어있는 물품들의 종류에 따라 다르게 더해서 goldEarn에 넣어도 되긴하는데
        // 거기다가 뭘 얼마나 팔았는지 체크 할 수도 있긴 한데......

        gold += goldEarn;

        // 작물이 물이 뿌려졌는지 확인후 작물 상태에 1을 더하고 물을 초기화한다
        // 동물들의 나이에 1을 더한다
        NewDayBegin();
    }
    void NewDayBegin()
    {
        weather = nextdayweather;
        luck = 0f; // 이건 그냥 랜덤

        if(currentMonth == 1) // 1월, 봄일때
        {
            int randyum = 10; // 랜덤값 (1~101), 임시로 정수 줌
            if (randyum > 70) { nextdayweather = 2; } // 30%확률로 내일 비
            else { nextdayweather = 1; } // 70퍼센트 확률로 내일 맑음
        }
        
        // 다음날 날씨로 날씨변경
        // 행운변경
        // 다음날 날씨 변경
    }

    void OpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.E) && tempinvenonoff == false ) // 인벤토리가 켜져있는 동안에는 게임이 일시정지된다.
        {
            // 인벤토리 창을 만들어야 하겠다
            // 인벤토리오브젝트.SetActive
            // 인벤토리 오브젝트가 켜짐
            inventory.SetActive(true);
            tempinvenonoff = true;
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)) && tempinvenonoff == true)
        {
            inventory.SetActive(false);
            tempinvenonoff = false;
        }
    }
}
