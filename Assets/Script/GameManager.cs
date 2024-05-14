using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour    // 게임의 전반적인 행동을 조정하고 대부분의 오브젝트가 얘를 참조한다
                                            // 추가로 1회만 획득 가능한 별방울, 해골열쇠, 가방등의 자료도 얘가 받는다.
{
    // 시간관리
    int currentYear; // year
    int currentSeason; // = month
    int currentMonth;
    int currentDay; // 28
    int currentHour; // 06:00 ~ 02:00
    int currentMinute; // 1min real = 1hour
    string ampm;
    bool dayOff; // 날이 끝났음을 전달, 혹은 씬의 변경?

    // 인벤토리 관리
    bool tempinvenonoff = false;
    GameObject inventory;

    // 돈 관리
    int gold;
    int goldEarn;

    // 매일 상태 관리
    bool wetherTotemUse; // 날씨 토템
    float luck; // 행운
    int wetherTotemNum;
    int weather; // 날씨 : 숫자로 관리
    int nextdayweather; // 다음날 날씨, 조건에 따라 확률적 배정

    // 시간체크
    float timePassed;

    private void Awake()
    {
        // 인벤토리 호출 및 비활성화
        inventory = transform.GetChild(0).gameObject;
        inventory.SetActive(false);

        // 초기 시간 설정
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
        // 시간경과 체크
        timePassed += Time.deltaTime;

        // 현재시간 출력
        UpdateTime();

        // 일자 종료시, 일자 종료메서드 실행
        if (dayOff ) { EndOfTheDay(); }

        // 인벤토리 관리
        OpenInventory(); // 이벤트화 요소

        // 골드관리, 다른오브젝트가 직접적으로 gold를 쓰지 않고 goldearn을 통해 관리: 근데 이거 의미 있나?
        if (goldEarn != 0){gold += goldEarn; goldEarn = 0;} // 이벤트화 요소
    }
    void UpdateTime()
    {
        // 현재 시간을 업데이트
        if (currentHour == 26) { dayOff = true; }
        if (timePassed >= 10f) {currentMinute += 10; timePassed = 0;}
        if (currentMinute == 60){currentHour += 1;}

        // 오전 오후 출력용
        if (currentHour >= 24)
        {ampm = "AM";}
        else if (currentHour >= 12)
        {ampm = "PM";}
        else { ampm = "AM"; }
    }

    void EndOfTheDay() //dayoff가 true일때 정산씬을 호출, 나머지 기능은 정산씬에서 실행
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
    void NewDayBegin() // 게임 씬으로 돌아옴, 정산씬에서 관리 하지 않은 것들 관리
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

    void weatherControll() // 날씨 관리
    {
        if (!wetherTotemUse)
        {
            switch (currentMonth)
            {
                case 1: //봄 일때
                    nextdayweather = 1; return; // 확률에 따라 비, 맑음 결정
                case 2: //여름 일때
                    nextdayweather = 1; return; // 확률에 따라 비, 녹색비, 폭풍, 맑음 결정
                case 3: //가을 일때
                    nextdayweather = 1; return; // 확률에 따라 비, 폭풍, 맑음, 바람 결정
                case 4: //겨울 일때
                    nextdayweather = 1; return; // 확률에 따라 눈, 맑음 결정
            }
        }
        if (wetherTotemUse)
        {
            switch (wetherTotemNum)
            {
                case 1: //날씨 1
                    nextdayweather = 1; return;
                case 2: //날씨 2
                    nextdayweather = 2; return;
                case 3: //날씨 3
                    nextdayweather = 3; return;
                case 4: //날씨 4
                    nextdayweather = 4; return;
            }
        }
    }

    void OpenInventory() // 인벤토리 오픈 , 게임 일시정지
    {
        if (Input.GetKeyDown(KeyCode.E) && tempinvenonoff == false ) //인벤이 꺼져있어야 함
        {
            // 인벤토리 창을 만들어야 하겠다
            // 인벤토리오브젝트.SetActive
            // 인벤토리 오브젝트가 켜짐
            inventory.SetActive(true);
            tempinvenonoff = true;
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)) && tempinvenonoff == true) //인벤이 켜져있어야함
        {
            inventory.SetActive(false);
            tempinvenonoff = false;
        }
    }
}
