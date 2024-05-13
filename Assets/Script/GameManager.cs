using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int currentYear; //�޼� �������� ������ 2������ ����ǰ�� �����λ�� ��� �۹� �߰� ���� �ۿ� ����
    int currentSeason; //���� = month�� ���������� �ϴ��� �̷��� ����.
    int currentMonth;
    int currentDay; // ��� ���� 28�ϱ��� ���������� �ִ�.
    int currentHour; // �ð��� ���� 6�ÿ� �����ؼ� ���� 2�ÿ� ������.
    int currentMinute; // 7�ʿ� 10��? ����ϱ� ���ϰ� 10�ʿ� 10������ ���� 1�п� �ѽð�
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
        inventory = transform.GetChild(0).gameObject; // �κ��丮�� ��ü
        inventory.SetActive(false); // ��Ȱ��ȭ

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

        OpenInventory(); // �̰� �̺�Ʈȭ �� �� ������ �غ���
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
    void NewDayBegin()
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

    void OpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.E) && tempinvenonoff == false ) // �κ��丮�� �����ִ� ���ȿ��� ������ �Ͻ������ȴ�.
        {
            // �κ��丮 â�� ������ �ϰڴ�
            // �κ��丮������Ʈ.SetActive
            // �κ��丮 ������Ʈ�� ����
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
