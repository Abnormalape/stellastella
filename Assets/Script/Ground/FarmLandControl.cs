using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FarmLandControl : MonoBehaviour // 경작지 프리팹에 들어가는 클래스, 이미 갈린상태
{
    [SerializeField] GameManager gameManager;
    ItemDB itemDB;
    int currentDate;
    [SerializeField] public bool watered = false;
    [SerializeField] bool upWatered = false;
    [SerializeField] bool downWatered = false;
    [SerializeField] bool leftWatered = false;
    [SerializeField] bool rightWatered = false;

    public string seedName;

    [SerializeField] public bool seeded = false;
    bool scareCrow = false;

    [SerializeField] // 씨앗심기를 관리하는 bool
    bool atFarm = true;
    
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite[] wateredGround = new Sprite[16];
    Collider2D[] nearWatered;


    LandControl landControl;

    public bool monthChanged;
    public bool dayChanged;
    void HandleAValueUpdated(bool newValue)
    {
        monthChanged = newValue;
    }

    void HandleBValueUpdated(bool newValue)
    {
        dayChanged = newValue;
        CropData();
    }

    private void Awake()
    {
        landControl = GetComponentInParent<LandControl>();
        landControl.OnAValueUpdated += HandleAValueUpdated;
        landControl.OnBValueUpdated += HandleBValueUpdated;

    }
    private void OnEnable()
    {
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentDate = gameManager.currentDay;
        spriteRenderer = GetComponent<SpriteRenderer>();
        //if (scene.name != farm){atfarm = false;} => 농장이 아니라면 atfarm을 false로
    }

    private void Update() // 계절 변경에 따른 작물 및 경작지 초기화 아직 못함
    {

        Rainy();
        
        if (watered) // 스프라이트 관리
        {
            CheckNearWatered();
            MakeWateredSprite();
        }
        if (!watered)
        {
            spriteRenderer.sprite = null;
        }

        if (transform.childCount == 0)
        { seeded = false; }
    }

    public string prefabPath;
    private void OnTriggerEnter2D(Collider2D collision)
    {   //충돌이 일어 났다
        if (collision.tag == "RightClick")
        {   //충돌체의 부모 오브젝트가 플레이어다
            itemDB = new ItemDB(collision.gameObject.GetComponentInParent<PlayerInventroy>().currentInventoryItem); // 플레이어가 들고있는 아이템의 정보
                                                                                                                    //접촉한 물체가 Tool이다
            if (itemDB.type == "Seed" && itemDB.season == gameManager.currentSeason) //플레이어 손에 씨앗이 있으면서, 씨앗의 계절이 현재 계절과 맞다면.
            {   //그런데 농장에서만
                if (atFarm && !seeded)
                {
                    collision.gameObject.GetComponentInParent<PlayerInventroy>().changeCount = -1; // 콜라이더의 부모 오브젝트의 인벤토리에게 요청보냄
                    seedName = new ItemDB(collision.gameObject.GetComponentInParent<PlayerInventroy>().currentInventoryItem).name; // 현재 인벤토리의 번호에 맞는 이름
                    seedName = seedName.Replace("Seed", "");
                    Instantiate((GameObject)Resources.Load($"Prefabs/CropPrefabs/{seedName}"), this.transform.position, Quaternion.identity, this.transform);
                    //내가 가진 아이템 ID와 맞는 프리팹을 만들어서 그놈의 부모를 나로 만들어라.
                    seeded = true;
                    prefabPath = $"Prefabs/CropPrefabs/{seedName}";
                }
            }
            else if (itemDB.type == "Seed" && itemDB.season != gameManager.currentSeason)
            {
                Debug.Log("현재 계절과 맞지 않는 씨앗.");


                //Todo: 메세지 박스 띄우기
            }
            else if (itemDB.type == "비료") //플레이어 손에 비료가 있다.
            {
                if (atFarm)
                {   //농장에서만

                }
            }
        }
        else if (collision.tag == "LeftClick")
        {
            itemDB = new ItemDB(collision.gameObject.GetComponentInParent<PlayerInventroy>().currentInventoryItem);
            if (itemDB.type == "Tool" && itemDB.toolType == 3) //손에 도구가 있다, 도구가 물뿌리개다.
            {
                this.watered = true;
            }
        }
    } // 씨앗 심기와 물 주기 비료 주기 등등
    void Rainy()
    {
        if (gameManager.weather == 2) //새로운 날이 시작 할때 2번날씨( = 비 )라면
        {
            watered = true;
        }
    }
    void CropData()
    {
        if (dayChanged)
        {
            dayChanged = false;
            this.currentDate = gameManager.currentDay;

            if (seeded && watered) // 심어진 상태로 물이 뿌려진 상태로 날이 바뀌었다면.
            {
                //자식오브젝트의 성장단계를 +1한다.
                int tDay;
                tDay = landControl.days + 1;

                if (GetComponentInChildren<CropControl>() != null)
                {
                    gameObject.GetComponentInChildren<CropControl>().days = tDay;
                }
                else
                {
                    Debug.Log("No Crop Control Found");
                }
                watered = false; // 물뿌림을 초기화 한다.
            }
            else if (seeded) // 씨앗을 심은 상태로 날이 바뀌었다면.
            {
                int i = Random.Range(0, 100); // 5% 확률로 까마귀 등장.
                if (i >= 95)
                {
                    if (!scareCrow) // 허수아비 없음.
                    {
                        Debug.Log($"작물 파괴됨 : {this.transform.position}");
                        Destroy(this.gameObject.GetComponentInChildren<CropControl>().gameObject); // 작물(자식오브젝트)파괴.
                        seeded = false;                                                              //Instantiate(); => 까마귀 생성.
                    }
                    else if (scareCrow) // 허수아비 있음.
                    {
                        // 가장 가까운 허수아비에 까마귀 카운트 +1.
                    }
                }
            }
            else if (watered)
            {
                watered = false;
            }
        }
        
    }//자식(작물)관리

    private void CheckNearWatered()
    {
        for (int i = 0; i < 4; i++)
        {
            int x = 0;
            int y = 0;

            switch (i)
            {
                case 0:
                    x = 1;
                    y = 0;
                    break;
                case 1:
                    x = -1;
                    y = 0;
                    break;
                case 2:
                    x = 0;
                    y = 1;
                    break;
                case 3:
                    x = 0;
                    y = -1;
                    break;
            }
            //우 좌 상 하 순서로 팔을 뻗어서
            RaycastHit2D[] touched = Physics2D.LinecastAll((Vector2)this.transform.position, (Vector2)(this.transform.position + new Vector3(x, y, 0)));
            RaycastHit2D realTouched = new RaycastHit2D();
            //만져지지 않았거나, 만져진 녀석의 watered가 false라면 
            for (int j = 1; j < touched.Length; j++)
            {
                if (touched[j].transform != null && touched[j].transform.gameObject != this.gameObject && touched[j].transform.gameObject.tag == "FarmLandControl")
                {
                    realTouched = touched[j]; break;
                }
            }
            if (realTouched.transform == null)
            {
                switch (i)
                {
                    case 0:
                        rightWatered = false;
                        break;
                    case 1:
                        leftWatered = false;
                        break;
                    case 2:
                        upWatered = false;
                        break;
                    case 3:
                        downWatered = false;
                        break;
                }
            }
            else if (realTouched.transform.GetComponent<FarmLandControl>().watered == false)
            {
                switch (i)
                {
                    case 0:
                        rightWatered = false;
                        break;
                    case 1:
                        leftWatered = false;
                        break;
                    case 2:
                        upWatered = false;
                        break;
                    case 3:
                        downWatered = false;
                        break;
                }
            }
            else if (realTouched.transform.GetComponent<FarmLandControl>().watered == true)
            {   //만져진 녀석이 있고, 그녀석의 watered가 true라면
                switch (i)
                {
                    case 0:
                        rightWatered = true;
                        break;
                    case 1:
                        leftWatered = true;
                        break;
                    case 2:
                        upWatered = true;
                        break;
                    case 3:
                        downWatered = true;
                        break;
                }
            }
        }
    }//근처 물뿌린 땅 관측
    private void MakeWateredSprite()
    {
        if (!upWatered && !downWatered && !leftWatered && !rightWatered) // 나만 파져있다
        {
            spriteRenderer.sprite = wateredGround[0];
        }
        else if (upWatered && !downWatered && !leftWatered && !rightWatered) // 위만 파져있다
        {
            spriteRenderer.sprite = wateredGround[1];
        }
        else if (!upWatered && downWatered && !leftWatered && !rightWatered) // 아래만 파져있다
        {
            spriteRenderer.sprite = wateredGround[2];
        }
        else if (!upWatered && !downWatered && leftWatered && !rightWatered) // 좌만 파져있다
        {
            spriteRenderer.sprite = wateredGround[3];
        }
        else if (!upWatered && !downWatered && !leftWatered && rightWatered) // 우만 파져있다
        {
            spriteRenderer.sprite = wateredGround[4];
        }
        else if (upWatered && downWatered && !leftWatered && !rightWatered) // 상하만 파져있다
        {
            spriteRenderer.sprite = wateredGround[5];
        }
        else if (!upWatered && !downWatered && leftWatered && rightWatered) // 좌우만 파져있다
        {
            spriteRenderer.sprite = wateredGround[6];
        }
        else if (upWatered && !downWatered && !leftWatered && rightWatered) // 상우만 파져있다
        {
            spriteRenderer.sprite = wateredGround[7];
        }
        else if (!upWatered && downWatered && !leftWatered && rightWatered) // 우하만 파져있다
        {
            spriteRenderer.sprite = wateredGround[8];
        }
        else if (!upWatered && downWatered && leftWatered && !rightWatered) // 좌하만 파져있다
        {
            spriteRenderer.sprite = wateredGround[9];
        }
        else if (upWatered && !downWatered && leftWatered && !rightWatered) // 좌상만 파져있다
        {
            spriteRenderer.sprite = wateredGround[10];
        }
        else if (!upWatered && downWatered && leftWatered && rightWatered) // 상만 안파져있다
        {
            spriteRenderer.sprite = wateredGround[11];
        }
        else if (upWatered && !downWatered && leftWatered && rightWatered) // 하만 안파져있다
        {
            spriteRenderer.sprite = wateredGround[12];
        }
        else if (upWatered && downWatered && !leftWatered && rightWatered) // 좌만 안파져있다
        {
            spriteRenderer.sprite = wateredGround[13];
        }
        else if (upWatered && downWatered && leftWatered && !rightWatered) // 우만 안파져있다
        {
            spriteRenderer.sprite = wateredGround[14];
        }
        else if (upWatered && downWatered && leftWatered && rightWatered) // 전부 파져있다
        {
            spriteRenderer.sprite = wateredGround[15];
        }
    }//자신의 물뿌린땅 만들기
}