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

    public int days; // 초기 day는 0
    int level;

    [SerializeField] GameObject harvestControl; //수확을 가능하게 하는 게임오브젝트


    private void OnEnable()
    {
        seedDB = new SeedDB(seedID);
        thisSR = this.GetComponent<SpriteRenderer>();

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

    void UpdateLevel()
    {
        if (days == 0)
        {
            level = 0;
        }
        else if (days == 1)
        {
            level = 1;
        }
        else if (days == maxDay)
        {
            level = maxLevel;
        }
        else
        {
            for (int i = 0; i <= maxLevel; i++)
            {
                if (days <= (maxDay * i) / maxLevel)
                {
                    level = i;
                    return;
                }
            }
        }
    }
    void UpdateSprite()
    {
        if (seedDB.reGather == false)
        {
            if (days == 0)
            {
                thisSR.sprite = sprites[0]; // 씨앗
            }
            else if (days == 1)
            {
                thisSR.sprite = sprites[1];
            }
            else if (days == maxDay)
            {
                thisSR.sprite = sprites[maxLevel];
            }
            else
            {
                thisSR.sprite = sprites[level]; // 현재 레벨의 스프라이트로 변경
            }
        }
        else if (seedDB.reGather == true) 
        {
            if (onceharvested == false)
            {
                if (days == 0)
                {
                    thisSR.sprite = sprites[0]; // 씨앗
                }
                else if (days == 1)
                {
                    thisSR.sprite = sprites[1];
                }
                else if (days == maxDay)
                {
                    thisSR.sprite = sprites[maxLevel];
                }
                else
                {
                    thisSR.sprite = sprites[level]; // 현재 레벨의 스프라이트로 변경
                }
            }
            else if (onceharvested == true)
            {
                if (days == maxDay)
                {
                    thisSR.sprite = sprites[maxLevel];
                }
                else
                {
                    thisSR.sprite = sprites[maxLevel -1];
                }
            }
        }
    }


}