using System.Text;
using UnityEngine;


class CropControl : MonoBehaviour // FarmLandControl이 불러온 씨앗에 맞는 프리팹. 그래서 작물프리팹 => 양산성 제품
                                  // 중요) ID로 성장을 관리한다.
{
    [SerializeField] public int seedID; // 아이디는 unity에서 입력 받는다.

    SeedDB seedDB;
    SpriteRenderer thisSR;

    [SerializeField] Sprite[] sprites;
    [SerializeField] int maxDay;
    [SerializeField] int maxLevel;
    [SerializeField] bool reHarvset;
    [SerializeField] int reDay;

    public bool harvested;
    bool onceharvested = false;


    private int tDays;
    public int days 
    { 
        get 
        { 
            return tDays; 
        }
        set
        {
            tDays = value;
            Debug.Log($"CropDataSet : {tDays}");
            CropDataManage();
        }
    }

    int level;

    [SerializeField] GameObject harvestControl; //수확을 가능하게 하는 게임오브젝트

    double tempInterval;
    private void OnEnable()
    {
        seedDB = new SeedDB(seedID);
        thisSR = this.GetComponent<SpriteRenderer>();

        tempInterval = (double)(maxDay-1) / (double)(maxLevel-1);

        maxDay = seedDB.maxDays;
        maxLevel = seedDB.maxLevle; // 오타 났는데 일단 넘어감
        reHarvset = seedDB.reGather;
        reDay = seedDB.reDays;

        harvestControl = this.gameObject.GetComponentInChildren<HarvestControl>().gameObject;
        harvestControl.SetActive(false); // 일단은 보이지 않게 함
    }



    private void Start()
    {
        
    }
    private void Update()
    {
        if (harvested && reHarvset == false) 
        {
            Destroy(this.gameObject);
        }
    }


    void CropDataManage()
    {   //days가 변경 되었을때.
        UpdateDate();
        UpdateLevel();
        UpdateSprite();
    }

    void UpdateDate()
    {
        if (days >= maxDay)  // days는 FLControl에서 관리
        {   //수확할때까지 수확 가능한 상태유지
            days = maxDay;

            
            if (harvestControl != null)
            {
                harvestControl.SetActive(true);
            }
            else // 수확오브젝트가 없다면 나를 파괴한다. 임시코드.  
            {
                Destroy(this.gameObject);
            }
        }
        if(harvested)
        {
            if (maxDay != reDay)
            {
                maxDay = reDay;
            }
            days = 0;
            harvested = false;

            if (onceharvested == false)
            {
                onceharvested = true;
            }
        }
    }


    int templevel = 1;

    int RecursiveTempIntervalCal(int tlevel, int tdays)
    {
        TempIntervalCal(tlevel, tdays);
        return tlevel;
    }
    int TempIntervalCal(int tlevel, int tdays)
    {
        if(tdays <= tempInterval * tlevel)
        {
            return tlevel;
        }
        else
        {
            int ttlevel;
            ttlevel = tlevel + 1;
            return TempIntervalCal(ttlevel, tdays);
        }
    }
    void UpdateLevel()
    {
        if(days == 0)
        {
            level = 0;
        }
        else if(days == maxDay)
        {
            level = maxLevel;
        }
        else
        {
            level = RecursiveTempIntervalCal(templevel, days);
            Debug.Log(level);
        }
    }
    void UpdateSprite()
    {
        thisSR.sprite = sprites[level];

        if (reHarvset) // 재수확
        {

        }
    }
}