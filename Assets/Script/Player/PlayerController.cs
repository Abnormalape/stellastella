using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public int maxStamina = 200;
    public int maxHp = 200;

    private int tCurrentStamina;
    public int currentStamina
    {
        get { return tCurrentStamina; }
        set
        {
            if (value >= maxStamina)
            {
                tCurrentStamina = maxStamina;
            }
            else { tCurrentStamina = value; }
        }
    }

    private int tCurrentHp;
    public int currentHp
    {
        get { return tCurrentHp; }
        set
        {
            if (value >= maxHp)
            {
                tCurrentHp = maxHp;
            }
            else { tCurrentHp = value; }
        }
    }


    private int tCurrentGold;
    public int tempGold;
    private int goldIntervals;
    public int currentGold
    {
        get { return tCurrentGold; }
        set
        {
            goldIntervals = Mathf.Abs(value - tCurrentGold);
            tCurrentGold = value;
        }
    }
    public nowLocation nowLocation;

    public UnityEvent asdf;

    public bool exhaust;
    public bool dead;

    PlayerInventroy pInven;

    //��¥�� ������ �ݾ�����, ���������� ���κ��� �ǽ��Ѵ�.
    //���� ���ӸŴ����� �Ϸ簡 ����Ǿ��ٴ� ��ȣ�� �÷��̾� ���� ������.



    private void Awake()
    {
        currentHp = maxHp;
        currentStamina = maxStamina;
        tCurrentGold = 500;
        tempGold = currentGold;
        pInven = GetComponent<PlayerInventroy>();
        inventoryBarUI = transform.Find("InventoryBarUI").gameObject;
        inventoryUI = transform.Find("InventoryUI").gameObject;

        inventoryUI.SetActive(false);
        inventoryBarUI.SetActive(true);

        nowLocation = nowLocation.FarmHouse;
    }

    void Start()
    {

    }

    public bool firstBag { get; private set; } = true;
    public bool secondBag { get; private set; } = true;

    void Update()
    {
        MakeIdleState();
        CurrentMax(currentHp, maxHp);
        CurrentMax(currentStamina, maxStamina);
        InvenToryButton();
        GoldChanging();
    }

    void CurrentMax(int current, int max)
    {
        if (current > max)
        { current = max; }
    }

    void GoldChanging()
    {
        int plusGold;
        if (tempGold < currentGold)
        {
            if ((int)(goldIntervals / 120f) < 1)
            {
                plusGold = 1;
            }
            else { plusGold = (int)(goldIntervals / 120f); }

            tempGold += plusGold;
            if (tempGold > currentGold) { tempGold = currentGold; }
        }
        else if (tempGold > currentGold)
        {
            if ((int)(goldIntervals / 120f) < 1)
            {
                plusGold = 1;
            }
            else { plusGold = (int)(goldIntervals / 120f); }

            tempGold -= plusGold;
            if (tempGold < currentGold) { tempGold = currentGold; }
        }
    }

    void Exhaust()
    {
        if (currentStamina <= 10 && !exhaust) { }
        // Ż�����°� �ƴ� ���·� ���׹̳ʰ� 10 ���ϰ� �ȴٸ� => Ż�����¸� ���
        if (currentStamina < 0 && !exhaust) { currentStamina = 0; exhaust = true; }
        // Ż���� �ƴ� ���¿��� ���׹̳ʰ� 0�̸��� �ȴٸ� 0���Ϸ� ���׹̳ʰ� �������� �ʰ��ϰ�, ���׹̳��� 0���� ��, Ż�����°� ��

        if (currentStamina < -10) { }
        // Ż�� �����϶��� ���׹̳ʰ� ������ �Ǵµ�, �̶� ���� ��ġ�� �Ǹ�, GAMEMANAGER �� �������Ḧ ����
    }

    void ExhaustState() // Ż�����¶�??
    {
    }


    public bool idle { get; private set; }

    public bool conversation { get; private set; }
    public void Conversation(bool i) { conversation = i; }
    public bool motion { get; private set; }
    public void Motion(bool i) { motion = i; }
    public bool moving { get; private set; }
    public void Moving(bool i) { moving = i; }
    public bool waitingForBait { get; private set; }
    public void WaitingForBait(bool i) { waitingForBait = i; }
    public bool fishCharge { get; private set; }
    public void FishCharge(bool i) { fishCharge = i; }
    public bool charge { get; private set; }
    public void Charge(bool i) { charge = i; }
    public bool minigame { get; private set; }
    public void Minigame(bool i) { minigame = i; }
    public bool inventory { get; private set; }
    public void Inventroy(bool i) { inventory = i; }
    public bool trade { get; private set; }
    public void Trade(bool i) { trade = i; }

    private void MakeIdleState()
    {   // ������ ���¸� �� ���⸸ �Ѵٸ� ���´� �׻� Idle�� ���ƿ´�.
        if (conversation || motion || moving || waitingForBait || fishCharge || charge || minigame || inventory || trade)
        { idle = false; }
        else { idle = true; }
    }

    GameObject inventoryUI;
    GameObject inventoryBarUI;


    private void InvenToryButton()
    {
        if (idle || moving || inventory)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!inventory)
                {
                    inventoryUI.SetActive(true);
                    inventoryBarUI.SetActive(false);
                    inventory = true;
                }
                else if (inventory)
                {
                    inventoryUI.SetActive(false);
                    inventoryBarUI.SetActive(true);
                    inventory = false;
                }
            }
        }
    }


    //======================================================//
    private int tMineLevel;
    public int mineLevel
    {
        get { return tMineLevel; }
        set
        {
            tMineLevel = value;
        }
    }
    private int tMineEXP;
    public int mineEXP
    {
        get { return tMineEXP; }
        set
        {
            if (mineLevel < 10)
            {   //����ġ�� 9���� ������ ������ �ִ�.
                if (value >= NeedEXP(mineLevel)) // ����ġ ���� ������ �´� �ʿ� ����ġ�� ���� ũ�ٸ�
                {
                    tMineEXP = value = NeedEXP(mineLevel);
                    mineLevel++;
                }
                else
                {
                    tMineEXP = value;
                }
            }
            else { return; }
        }
    }
    //======================================================//
    private int tGatherLevel;
    public int gatherLevel
    {
        get { return tGatherLevel; }
        set
        {
            tGatherLevel = value;
        }
    }
    private int tGatherEXP;
    public int gatherEXP
    {
        get { return tGatherEXP; }
        set
        {
            if (gatherLevel < 10)
            {   //����ġ�� 9���� ������ ������ �ִ�.
                if (value >= NeedEXP(gatherLevel)) // ����ġ ���� ������ �´� �ʿ� ����ġ�� ���� ũ�ٸ�
                {
                    tGatherEXP = value = NeedEXP(gatherLevel);
                    gatherLevel++;
                }
                else
                {
                    tGatherEXP = value;
                }
            }
            else { return; }
        }
    }

    public int gatherGold { get; private set; } = 800; // 0�����϶�.
    public int gatherSilver { get; private set; } = 500; // 0�����϶�.
    //======================================================//
    private int tFishLevel;
    public int fishLevel
    {
        get { return tFishLevel; }
        set
        {
            tFishLevel = value;
        }
    }
    private int tFishEXP;
    public int fishEXP
    {
        get { return tFishEXP; }
        set
        {
            if (fishLevel < 10)
            {   //����ġ�� 9���� ������ ������ �ִ�.
                if (value >= NeedEXP(fishLevel)) // ����ġ ���� ������ �´� �ʿ� ����ġ�� ���� ũ�ٸ�
                {
                    tFishEXP = value = NeedEXP(fishLevel);
                    fishLevel++;
                }
                else
                {
                    tFishEXP = value;
                }
            }
            else { return; }
        }
    }
    //======================================================//
    private int tCombatLevel;
    public int combatLevel
    {
        get { return tCombatLevel; }
        set
        {
            tCombatLevel = value;
        }
    }
    private int tCombatEXP;
    public int combatEXP
    {
        get { return tCombatEXP; }
        set
        {
            if (combatLevel < 10)
            {   //����ġ�� 9���� ������ ������ �ִ�.
                if (value >= NeedEXP(combatLevel)) // ����ġ ���� ������ �´� �ʿ� ����ġ�� ���� ũ�ٸ�
                {
                    tCombatEXP = value = NeedEXP(combatLevel);
                    combatLevel++;
                }
                else
                {
                    tCombatEXP = value;
                }
            }
            else { return; }
        }
    }
    //======================================================//
    private int tFarmLevel;
    public int farmLevel
    {
        get { return tFarmLevel; }
        set
        {
            tFarmLevel = value;
        }
    }
    private int tFarmEXP;
    public int farmEXP
    {
        get { return tFarmEXP; }
        set
        {
            if (farmLevel < 10)
            {   //����ġ�� 9���� ������ ������ �ִ�.
                if (value >= NeedEXP(farmLevel)) // ����ġ ���� ������ �´� �ʿ� ����ġ�� ���� ũ�ٸ�
                {
                    tFarmEXP = value = NeedEXP(farmLevel);
                    farmLevel++;
                }
                else
                {
                    tFarmEXP = value;
                }
            }
            else { return; }
        }
    }
    //======================================================//
    private int NeedEXP(int level)
    {
        switch (level)
        {
            case 1:
                return 100;
            case 2:
                return 200;
            case 3:
                return 300;
            case 4:
                return 400;
            case 5:
                return 500;
            case 6:
                return 600;
            case 7:
                return 700;
            case 8:
                return 800;
            case 9:
                return 900;
            case 10:
                return 1000;
        }

        return 0; // ��������
    }

    //======================================================//
    public bool PlayerFarmOption { get; set; } = false;
    public bool Option2 { get; set; } = false;
    public bool Option3 { get; set; } = false;
    public bool Option4 { get; set; } = false;
}
