using JetBrains.Annotations;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour    // ������ �������� �ൿ�� �����ϰ� ��κ��� ������Ʈ�� �긦 �����Ѵ�
                                            // �߰��� 1ȸ�� ȹ�� ������ �����, �ذ񿭼�, ������� �ڷᵵ �갡 �޴´�.
{
    // �ð�����
    public int currentYear; // year
    public int currentSeason; // = month
    public int currentMonth;
    public int currentDay; // 28
    public int currentHour; // 06:00 ~ 02:00
    public int currentMinute; // 1min real = 1hour
    string ampm;
    public bool dayOff; // ���� �������� ����, Ȥ�� ���� ����?



    // �� ����
    int gold;
    int goldEarn;

    // ���� ���� ����
    bool wetherTotemUse; // ���� ����
    float luck; // ���
    int wetherTotemNum;
    public int weather; // ���� : ���ڷ� ����
    int nextdayweather; // ������ ����, ���ǿ� ���� Ȯ���� ����

    // �ð�üũ
    float timePassed;

    private void Awake()
    {
        currentYear = 1;
        currentSeason = 1;
        currentMonth = 1;
        currentDay = 1;
        currentHour = 6;
        currentMinute = 0;
        ampm = "AM";
        dayOff = false;
        // �κ��丮 ȣ�� �� ��Ȱ��ȭ
        inventory = transform.GetChild(0).gameObject;
        inventory.SetActive(false);


    }

    private void Update()
    {
        // �ð���� üũ
        timePassed = timePassed + Time.deltaTime;
        // ����ð� ���
        UpdateTime();

        // �κ��丮 ����
        OpenInventory(); // �̺�Ʈȭ ���

        // �Ʒ��� �����ϴ� UI ��������
        OffInvenUI();

        // ���� �����, ���� ����޼��� ����
        if (dayOff) { EndOfTheDay(); }

        // ������, �ٸ�������Ʈ�� ���������� gold�� ���� �ʰ� goldearn�� ���� ����: �ٵ� �̰� �ǹ� �ֳ�?
        if (goldEarn != 0) { gold += goldEarn; goldEarn = 0; } // �̺�Ʈȭ ���
    }
    void UpdateTime()
    {
        if (timePassed >= 10f)
        {
            currentMinute += 10;
            timePassed = 0;
        }

        if (currentMinute >= 60)
        {
            currentHour += 1;
            currentMinute = 0;
        }

        if (currentHour == 26)
        { dayOff = true; }

        if (currentHour < 12 || currentHour >= 24)
        { ampm = "AM"; }
        else { ampm = "PM"; }
    }

    void EndOfTheDay() //dayoff�� true�϶� ������� ȣ��, ������ ����� ��������� ����
    {
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

        dayOff = false;
    }
    void NewDayBegin() // ���� ������ ���ƿ�, ��������� ���� ���� ���� �͵� ����
    {
        weather = nextdayweather;
        luck = 0f; // �̰� �׳� ����

        if (currentMonth == 1) // 1��, ���϶�
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


    // �κ��丮 ����
    GameObject inventory;
    public bool isInventoryOn; // �̰� True�϶� movement�� leftclick, rightclick Ŭ���� ���� ���´�.
    float originalTimeScale = 1f;
    void OpenInventory() // �κ��丮 ���� , ���� �Ͻ�����
    {
        if (Input.GetKeyDown(KeyCode.E) && isInventoryOn == false) //�κ��� �����־�� ��
        {
            isInventoryOn = true;
            // �κ��丮 â�� ������ �ϰڴ�
            // �κ��丮������Ʈ.SetActive
            // �κ��丮 ������Ʈ�� ����
            inventory.SetActive(true);
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)) && isInventoryOn == true) //�κ��� �����־����
        {
            isInventoryOn = false;
            inventory.SetActive(false);
        }

        if (isInventoryOn)
        {
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = originalTimeScale;
        }
    }


    void OffInvenUI()
    {
        GameObject aa = GameObject.Find("InventoryOffBackUI");
        if (isInventoryOn)
        {   
            aa.transform.position = new Vector3(aa.transform.position.x , aa.transform.position.y , -10000f);
        }
        else
        {
            aa.transform.position = new Vector3(aa.transform.position.x, aa.transform.position.y, 0f);
        }
    }
}
