using UnityEngine;
using Random = UnityEngine.Random;
public class FarmLand : MonoBehaviour
    // 다른 Land들은 자식오브젝트가 없을 때 프리팹을 자식 오브젝트로 만든다
    // 얘의 기능은 농장외부에선 보물 프리팹은 만드는 역할이고
    // 괭이질을 받으면, 자신의 스프라이트를 변경하고 작물 스프라이트를 만드는 FarmLandControl을 만든다
    // 얘는 자신이 속한 GameObject가 자식 오브젝트가 없을때, 괭이질을 하면 경작지 프리팹을 자식오브젝트에 만드는 기능을 한다.
{
    ItemDB itemDB;
    [SerializeField] GameManager gameManager;
    int currentDate;
    int currentSeason;

    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite[] diggedGround = new Sprite[16]; //땅 스프라이트
    Collider2D[] nearGround; //근처 땅 탐색용

    [SerializeField] public bool digged = false; //본인 상태 확인용, 외부 전달용
    [SerializeField] bool upDigged = false;
    [SerializeField] bool downDigged = false;
    [SerializeField] bool leftDigged = false;
    [SerializeField] bool rightDigged = false;

    public string prefabPath;
    private void Awake() // 게임 시작할 때 초기화
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentDate = gameManager.currentDay;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        DayChange();

        if (digged) // 스프라이트 관리
        {
            CheckNearDigged();
            MakeDigSprite();
        }
        else if (!digged)
        {
            if (this.gameObject.GetComponentInChildren<FarmLandControl>() != null)
            {
                this.gameObject.GetComponentInChildren<FarmLandControl>().watered = false;
                Destroy(this.transform.GetComponentInChildren<FarmLandControl>().gameObject);
            }
            spriteRenderer.sprite = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // 충돌을 받았을때.
    {
        if (collision.tag == "LeftClick")
        {
            itemDB = new ItemDB(collision.gameObject.GetComponentInParent<PlayerInventroy>().currentInventoryItem); // 충돌체의 부모를 찾아 플레이어이면 Item데이터를 불러와서.
            if (transform.childCount == 0 && !digged) // 자식이 없으면서, 씬의 이름이 농장임.
            {
                if (itemDB.toolType == 2) // 그것이 괭이일때.
                {
                    digged = true;
                    currentDate = gameManager.currentDay;
                    // 경작지 프리팹을 만들어서 그놈의 부모를 나와 같게하라.
                    GameObject.Instantiate(Resources.Load($"Prefabs/LandPrefabs/FarmLandController") as GameObject, this.transform.position, Quaternion.identity).transform.parent = this.transform;
                    prefabPath = $"Prefabs/LandPrefabs/FarmLandController";
                    // 경작지 프리팹은 FarmLnadControl을 가지는 게임 오브젝트이다.
                }
            }
            if (digged && itemDB.toolType == 4) // 땅이 파진 상태, 곡괭이 일때
            {
                digged = false;
            }
        }
    }
    void DayChange()
    {
        if (currentDate != gameManager.currentDay) // 날짜가 바뀐다면.
        {
            currentDate = gameManager.currentDay; // 날짜를 업데이트 하고.
            if (digged) 
            {
                if (this.transform.GetComponentInChildren<FarmLandControl>().seeded == false)
                {   // 심어지지 않은 상황이라면.
                    int i = Random.Range(0, 100); // 10% 확률로.
                    if (i >= 90)
                    {
                        digged = false;
                    }
                }
            }
        }
    }
    
    private void CheckNearDigged()
    {
        nearGround = Physics2D.OverlapCircleAll(this.transform.position, 0.6f);


        for (int i = 0; i < nearGround.Length; i++)
        {
            if (nearGround[i] != null && nearGround[i].tag == "FarmLand")
            {
                if (nearGround[i].transform.position.x - this.transform.position.x == 1 && nearGround[i].transform.position.y - this.transform.position.y == 0)
                {
                    if (nearGround[i].GetComponent<FarmLand>().digged == true)
                    {
                        rightDigged = true;
                    }
                    else
                    {
                        rightDigged = false;
                    }
                }
                else if (nearGround[i].transform.position.x - this.transform.position.x == -1 && nearGround[i].transform.position.y - this.transform.position.y == 0)
                {
                    if (nearGround[i].GetComponent<FarmLand>().digged == true)
                    {
                        leftDigged = true;
                    }
                    else
                    {
                        leftDigged = false;
                    }
                }
                else if (nearGround[i].transform.position.x - this.transform.position.x == 0 && nearGround[i].transform.position.y - this.transform.position.y == 1)
                {
                    if (nearGround[i].GetComponent<FarmLand>().digged == true)
                    {
                        upDigged = true;
                    }
                    else
                    {
                        upDigged = false;
                    }
                }
                else if (nearGround[i].transform.position.x - this.transform.position.x == 0 && nearGround[i].transform.position.y - this.transform.position.y == -1)
                {
                    if (nearGround[i].GetComponent<FarmLand>().digged == true)
                    {
                        downDigged = true;
                    }
                    else
                    {
                        downDigged = false;
                    }
                }
            }
        }
    }
    private void MakeDigSprite()
    {
        if (!upDigged && !downDigged && !leftDigged && !rightDigged) // 나만 파져있다
        {
            spriteRenderer.sprite = diggedGround[0];
        }
        else if (upDigged && !downDigged && !leftDigged && !rightDigged) // 위만 파져있다
        {
            spriteRenderer.sprite = diggedGround[1];
        }
        else if (!upDigged && downDigged && !leftDigged && !rightDigged) // 아래만 파져있다
        {
            spriteRenderer.sprite = diggedGround[2];
        }
        else if (!upDigged && !downDigged && leftDigged && !rightDigged) // 좌만 파져있다
        {
            spriteRenderer.sprite = diggedGround[3];
        }
        else if (!upDigged && !downDigged && !leftDigged && rightDigged) // 우만 파져있다
        {
            spriteRenderer.sprite = diggedGround[4];
        }
        else if (upDigged && downDigged && !leftDigged && !rightDigged) // 상하만 파져있다
        {
            spriteRenderer.sprite = diggedGround[5];
        }
        else if (!upDigged && !downDigged && leftDigged && rightDigged) // 좌우만 파져있다
        {
            spriteRenderer.sprite = diggedGround[6];
        }
        else if (upDigged && !downDigged && !leftDigged && rightDigged) // 상우만 파져있다
        {
            spriteRenderer.sprite = diggedGround[7];
        }
        else if (!upDigged && downDigged && !leftDigged && rightDigged) // 우하만 파져있다
        {
            spriteRenderer.sprite = diggedGround[8];
        }
        else if (!upDigged && downDigged && leftDigged && !rightDigged) // 좌하만 파져있다
        {
            spriteRenderer.sprite = diggedGround[9];
        }
        else if (upDigged && !downDigged && leftDigged && !rightDigged) // 좌상만 파져있다
        {
            spriteRenderer.sprite = diggedGround[10];
        }
        else if (!upDigged && downDigged && leftDigged && rightDigged) // 상만 안파져있다
        {
            spriteRenderer.sprite = diggedGround[11];
        }
        else if (upDigged && !downDigged && leftDigged && rightDigged) // 하만 안파져있다
        {
            spriteRenderer.sprite = diggedGround[12];
        }
        else if (upDigged && downDigged && !leftDigged && rightDigged) // 좌만 안파져있다
        {
            spriteRenderer.sprite = diggedGround[13];
        }
        else if (upDigged && downDigged && leftDigged && !rightDigged) // 우만 안파져있다
        {
            spriteRenderer.sprite = diggedGround[14];
        }
        else if (upDigged && downDigged && leftDigged && rightDigged) // 전부 파져있다
        {
            spriteRenderer.sprite = diggedGround[15];
        }
    }
}
