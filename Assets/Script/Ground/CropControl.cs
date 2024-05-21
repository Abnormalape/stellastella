using UnityEngine;


[ExecuteInEditMode]
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
    
    public int days = 0; // 초기 day는 0
    int levle;

    [SerializeField] GameObject harvestControl; //수확을 가능하게 하는 게임오브젝트

    private void OnEnable()
    {
        seedDB = new SeedDB(seedID);
        thisSR = this.GetComponent<SpriteRenderer>();

        maxDay = seedDB.maxDays;
        maxLevel = seedDB.maxLevle; // 오타 났는데 일단 넘어감
        reHarvset = seedDB.reGather;
        reDay = seedDB.reDays;

        harvestControl = Resources.Load($"Prefabs/HarvestPrefabs/HarvestController") as GameObject;
        harvestControl.SetActive(false); // 일단은 보이지 않게 함
        sprites = new Sprite[maxLevel + 1]; // 씨앗 1개 추가
    }
    private void Update()
    {
        UpdateDate();
    }
    void UpdateDate()
    {
        if (seedDB.reGather == false)   //재수확이 불가능한 작물
        {
            if (days >= maxDay)  // days는 FLControl에서 관리
            {   //수확할때까지 수확 가능한 상태유지
                days = maxDay;
                harvestControl.SetActive(true);
            }
        }
        else //재수확이 가능한 작물
        {
            if (reHarvset == false) //성장상태
            {
                if (days >= maxDay)  // days는 FLControl에서 관리
                {   //수확할때까지 수확 가능한 상태유지
                    days = maxDay;
                    harvestControl.SetActive(true);
                    reHarvset = true;
                    days = 0; // 날짜초기화
                }
            }
            else //재수확 상태
            {
                if (days >= seedDB.reDays)
                {
                    days = seedDB.reDays;
                    harvestControl.SetActive(true);
                    days = 0;
                }
            }
        }
        UpdateLevel();
        UpdateSprite();
    }

    void UpdateLevel()
    {
        if (days == 0)
        {
            levle = 0;
        }
        else if (days == 1)
        {
            levle = 1;
        }
        else if (days == maxDay)
        {
            levle = maxLevel;
        }
        else
        {
            for (int i = 0; i <= maxLevel; i++)
            {
                if (days <= (maxDay * i) / maxLevel)
                {
                    levle = i;
                    return;
                }
            }
        }
    }
    void UpdateSprite()
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
            thisSR.sprite = sprites[maxDay];
        }
        else
        {
            thisSR.sprite = sprites[levle]; // 현재 레벨의 스프라이트로 변경
        }
    }
    
    
}