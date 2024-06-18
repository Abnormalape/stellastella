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

    private bool tHarvested;
    public bool harvested
    {
        get { return tHarvested; }

        set
        {
            tHarvested = value;
            Harvested();
        }
    }

    private bool tOnceHarvested;
    public bool onceharvested
    {
        get { return tOnceHarvested; }

        set
        {
            tOnceHarvested = value;
        }
    }


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
            CropDataManage();
            MakeParentsDays();
        }
    }

    void MakeParentsDays()
    {
        transform.parent.parent.GetComponent<LandControl>().days = days;
    }

    [SerializeField] bool tempSetDay;
    [SerializeField] int tempSetDays;

    int level;

    [SerializeField] GameObject harvestControl; //수확을 가능하게 하는 게임오브젝트

    double tempInterval;
    private void OnEnable()
    {
        seedDB = new SeedDB(seedID);
        thisSR = this.GetComponent<SpriteRenderer>();

        tempInterval = (double)(maxDay - 1) / (double)(maxLevel - 1);

        maxDay = seedDB.maxDays;
        maxLevel = seedDB.maxLevle; // 오타 났는데 일단 넘어감
        reHarvset = seedDB.reGather;
        reDay = seedDB.reDays;

        harvestControl = this.gameObject.GetComponentInChildren<HarvestControl>().gameObject;
        harvestControl.SetActive(false); // 일단은 보이지 않게 함
    }

    private void Update()
    {
        //임시코드
        if (tempSetDay)
        {
            days = tempSetDays;
            tempSetDays = 0;
            tempSetDay = false;
        }
    }

    void CropDataManage()
    {   //days가 변경 되었을때.
        UpdateDate();
        UpdateLevel();
        UpdateSprite();

        if (!onceharvested)
        {
            if (days >= maxDay)
            {
                harvestControl.SetActive(true);
            }
        }
        else if (onceharvested)
        {
            if (days >= reDay)
            {
                harvestControl.SetActive(true);
            }
        }
    }

    void UpdateDate()
    {
        if (tDays >= maxDay)  // days는 FLControl에서 관리
        {   //수확할때까지 수확 가능한 상태유지
            tDays = maxDay;
        }
    }
    void UpdateLevel()
    {
        if (!onceharvested)
        {
            if (days == 0)
            {
                level = 0;
            }
            else if (days >= maxDay)
            {
                level = maxLevel;
            }
            else
            {
                int i = 1; // 임시 레벨값
                for (i = 1; i < maxLevel; i++)
                {
                    if (days <= tempInterval * i)
                    {
                        level = i;
                        return;
                    }
                }
            }
        }
        else if (onceharvested)
        {
            if (days < maxDay)
            {
                level = maxLevel - 1;
            }
            else
            {
                level = maxLevel;
            }
        }
    }
    void UpdateSprite()
    {
        if (!onceharvested)
        {
            thisSR.sprite = sprites[level];
        }
        else
        {
            if (days < reDay)
            {
                thisSR.sprite = sprites[maxLevel - 1];
            }
            else
            {
                thisSR.sprite = sprites[maxLevel];
            }
        }
    }
    void Harvested()
    {
        if (harvested)
        {
            if (reHarvset) // 재수확이 가능한 작물이라면.
            {
                days = 0;
                harvested = false;
            }
            else    // 재수확이 불가능한 작물이라면.
            {      // 수확시 파괴.
                Destroy(this.gameObject);
            }
        }
    }
}