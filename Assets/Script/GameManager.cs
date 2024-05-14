using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour    // ������ �������� �ൿ�� �����ϰ� ��κ��� ������Ʈ�� �긦 �����Ѵ�
                                            // �߰��� 1ȸ�� ȹ�� ������ �����, �ذ񿭼�, ������� �ڷᵵ �갡 �޴´�.
{
    // �ð�����
    int currentYear; // year
    int currentSeason; // = month
    int currentMonth;
    int currentDay; // 28
    int currentHour; // 06:00 ~ 02:00
    int currentMinute; // 1min real = 1hour
    string ampm;
    bool dayOff; // ���� �������� ����, Ȥ�� ���� ����?

    // �κ��丮 ����
    bool tempinvenonoff = false;
    GameObject inventory;

    // �� ����
    int gold;
    int goldEarn;

    // ���� ���� ����
    bool wetherTotemUse; // ���� ����
    float luck; // ���
    int wetherTotemNum;
    int weather; // ���� : ���ڷ� ����
    int nextdayweather; // ������ ����, ���ǿ� ���� Ȯ���� ����

    // �ð�üũ
    float timePassed;

    private void Awake()
    {
        // �κ��丮 ȣ�� �� ��Ȱ��ȭ
        inventory = transform.GetChild(0).gameObject;
        inventory.SetActive(false);

        // �ʱ� �ð� ����
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
        // �ð���� üũ
        timePassed += Time.deltaTime;

        // ����ð� ���
        UpdateTime();

        // ���� �����, ���� ����޼��� ����
        if (dayOff ) { EndOfTheDay(); }

        // �κ��丮 ����
        OpenInventory(); // �̺�Ʈȭ ���

        // ������, �ٸ�������Ʈ�� ���������� gold�� ���� �ʰ� goldearn�� ���� ����: �ٵ� �̰� �ǹ� �ֳ�?
        if (goldEarn != 0){gold += goldEarn; goldEarn = 0;} // �̺�Ʈȭ ���
    }
    void UpdateTime()
    {
        // ���� �ð��� ������Ʈ
        if (currentHour == 26) { dayOff = true; }
        if (timePassed >= 10f) {currentMinute += 10; timePassed = 0;}
        if (currentMinute == 60){currentHour += 1;}

        // ���� ���� ��¿�
        if (currentHour >= 24)
        {ampm = "AM";}
        else if (currentHour >= 12)
        {ampm = "PM";}
        else { ampm = "AM"; }
    }

    void EndOfTheDay() //dayoff�� true�϶� ������� ȣ��, ������ ����� ��������� ����
    {
        dayOff = false ;
        currentHour = 6;
        currentMinute = 0;
        ampm = "AM";
        currentDay++;
        if (currentDay > 28) { currentMonth++; currentDay = 1; }
        if (currentMonth > 4) { currentYear++; currentMonth = 1; } // �ʱ�ȭ �� ���� ����
        // �Ϸ簡 ����Ǿ����� : ħ�뿡�� ���� �ڵ� dayOff�� true�� �ȴ�.
        // ���ϻ��ڿ� �� ��ǰ���� sell price�� �ջ��� ȭ�鿡 ǥ���Ѵ�
        // ��ų�� ������ ������ �˷��ش�
        // �̺�Ʈ(�ڵ����� �ǹ� �ƴ�)�� �����Ѵ�
        // goldEarn = ���ϻ��ڿ� ����ִ� ��ǰ���� ������
        // ���ϻ��ڿ� ����ִ� ��ǰ���� ������ ���� �ٸ��� ���ؼ� goldEarn�� �־ �Ǳ��ϴµ�
        // �ű�ٰ� �� �󸶳� �ȾҴ��� üũ �� ���� �ֱ� �ѵ�......

        gold += goldEarn;

        // �۹��� ���� �ѷ������� Ȯ���� �۹� ���¿� 1�� ���ϰ� ���� �ʱ�ȭ�Ѵ�
        // �������� ���̿� 1�� ���Ѵ�
        NewDayBegin();
    }
    void NewDayBegin() // ���� ������ ���ƿ�, ��������� ���� ���� ���� �͵� ����
    {
        weather = nextdayweather;
        luck = 0f; // �̰� �׳� ����

        if(currentMonth == 1) // 1��, ���϶�
        {
            int randyum = 10; // ������ (1~101), �ӽ÷� ���� ��
            if (randyum > 70) { nextdayweather = 2; } // 30%Ȯ���� ���� ��
            else { nextdayweather = 1; } // 70�ۼ�Ʈ Ȯ���� ���� ����
        }
        
        // ������ ������ ��������
        // ����
        // ������ ���� ����
    }

    void weatherControll() // ���� ����
    {
        if (!wetherTotemUse)
        {
            switch (currentMonth)
            {
                case 1: //�� �϶�
                    nextdayweather = 1; return; // Ȯ���� ���� ��, ���� ����
                case 2: //���� �϶�
                    nextdayweather = 1; return; // Ȯ���� ���� ��, �����, ��ǳ, ���� ����
                case 3: //���� �϶�
                    nextdayweather = 1; return; // Ȯ���� ���� ��, ��ǳ, ����, �ٶ� ����
                case 4: //�ܿ� �϶�
                    nextdayweather = 1; return; // Ȯ���� ���� ��, ���� ����
            }
        }
        if (wetherTotemUse)
        {
            switch (wetherTotemNum)
            {
                case 1: //���� 1
                    nextdayweather = 1; return;
                case 2: //���� 2
                    nextdayweather = 2; return;
                case 3: //���� 3
                    nextdayweather = 3; return;
                case 4: //���� 4
                    nextdayweather = 4; return;
            }
        }
    }

    void OpenInventory() // �κ��丮 ���� , ���� �Ͻ�����
    {
        if (Input.GetKeyDown(KeyCode.E) && tempinvenonoff == false ) //�κ��� �����־�� ��
        {
            // �κ��丮 â�� ������ �ϰڴ�
            // �κ��丮������Ʈ.SetActive
            // �κ��丮 ������Ʈ�� ����
            inventory.SetActive(true);
            tempinvenonoff = true;
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)) && tempinvenonoff == true) //�κ��� �����־����
        {
            inventory.SetActive(false);
            tempinvenonoff = false;
        }
    }
}
