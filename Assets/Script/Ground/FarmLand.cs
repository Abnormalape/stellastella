using UnityEngine;
using Random = UnityEngine.Random;
public class FarmLand : MonoBehaviour
// �ٸ� Land���� �ڽĿ�����Ʈ�� ���� �� �������� �ڽ� ������Ʈ�� �����
// ���� ����� ����ܺο��� ���� �������� ����� �����̰�
// �������� ������, �ڽ��� ��������Ʈ�� �����ϰ� �۹� ��������Ʈ�� ����� FarmLandControl�� �����
// ��� �ڽ��� ���� GameObject�� �ڽ� ������Ʈ�� ������, �������� �ϸ� ������ �������� �ڽĿ�����Ʈ�� ����� ����� �Ѵ�.
{
    ItemDB itemDB;
    [SerializeField] GameManager gameManager;
    int currentDate;
    public int currentSeason;

    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite[] diggedGround = new Sprite[16]; //�� ��������Ʈ
    Collider2D[] nearGround; //��ó �� Ž����

    private bool tDigged = false;
    public bool digged { get { return tDigged; } set { tDigged = value; DiggedSprite(); } }
    public bool upDigged = false;
    public bool downDigged = false;
    public bool leftDigged = false;
    public bool rightDigged = false;

    public string prefabPath;
    private void Awake() // ���� ������ �� �ʱ�ȭ
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentDate = gameManager.currentDay;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        landControl = GetComponent<LandControl>();
    }
    private void Start()
    {
        landControl.OnAValueUpdated += HandleAValueUpdated;
        landControl.OnBValueUpdated += HandleBValueUpdated;
    }

    LandControl landControl;
    bool monthChanged;

    private bool tDayChanged;
    public bool dayChanged { get { return tDayChanged; } set { tDayChanged = value; if(value == true)DayChange(); } }
    void HandleAValueUpdated(bool newValue)
    {
        monthChanged = newValue;
    }

    void HandleBValueUpdated(bool newValue)
    {
        dayChanged = newValue;
    }
    private void Update()
    {
        
    }

    public void DiggedSprite(GameObject caller = null)
    {
        if (digged) // ��������Ʈ ����.
        {
            CheckNearDigged(); // ��ó���� �Ŀ����� Ȯ���ϰ�.
            //�̶� �� ���� �ִ� �༮�� diggedŸ���� �ٲپ��.
            MakeDigSprite(); // ��������Ʈ�� ������.
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

    private void OnTriggerEnter2D(Collider2D collision) // �浹�� �޾�����.
    {
        if (collision.tag == "LeftClick")
        {
            itemDB = new ItemDB(collision.gameObject.GetComponentInParent<PlayerInventroy>().currentInventoryItem); // �浹ü�� �θ� ã�� �÷��̾��̸� Item�����͸� �ҷ��ͼ�.
            if (transform.childCount == 0 && !digged) // �ڽ��� �����鼭, ���� �̸��� ������.
            {
                if (itemDB.toolType == 2) // �װ��� �����϶�.
                {
                    digged = true;
                    currentDate = gameManager.currentDay;
                    // ������ �������� ���� �׳��� �θ� ���� �����϶�.
                    GameObject.Instantiate(Resources.Load($"Prefabs/LandPrefabs/FarmLandController") as GameObject, this.transform.position, Quaternion.identity, this.transform);
                    prefabPath = $"Prefabs/LandPrefabs/FarmLandController";
                    // ������ �������� FarmLnadControl�� ������ ���� ������Ʈ�̴�.
                }
            }
            if (digged && itemDB.toolType == 4) // ���� ���� ����, ��� �϶�
            {
                digged = false;
            }
        }
    }
    void DayChange()
    {
        if (dayChanged) // ��¥�� �ٲ�ٸ�.
        {
            currentDate = gameManager.currentDay; // ��¥�� ������Ʈ �ϰ�.
            dayChanged = false;
            monthChanged = false;
            if (digged)
            {
                if (this.transform.GetComponentInChildren<FarmLandControl>().seeded == false)
                {   // �ɾ����� ���� ��Ȳ�̶��.
                    int i = Random.Range(0, 100); // 10% Ȯ����.
                    if (i >= 90)
                    {
                        digged = false;
                    }
                }
            }
        }
    }

    private void CheckNearDigged(GameObject caller = null)
    {
        nearGround = Physics2D.OverlapCircleAll(this.transform.position, 0.6f);
        //Todo:

        for (int i = 0; i < nearGround.Length; i++)
        {
            if (nearGround[i] != null && nearGround[i].GetComponent<FarmLand>() != null)
            {
                if (nearGround[i].transform.position.x - this.transform.position.x == 1 && nearGround[i].transform.position.y - this.transform.position.y == 0)
                {
                    if (nearGround[i].GetComponent<FarmLand>().digged == true)
                    {
                        rightDigged = true;
                        nearGround[i].GetComponent<FarmLand>().leftDigged = true;
                        nearGround[i].GetComponent<FarmLand>().MakeDigSprite();
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
                        nearGround[i].GetComponent<FarmLand>().rightDigged = true;
                        nearGround[i].GetComponent<FarmLand>().MakeDigSprite();
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
                        nearGround[i].GetComponent<FarmLand>().downDigged = true;
                        nearGround[i].GetComponent<FarmLand>().MakeDigSprite();
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
                        nearGround[i].GetComponent<FarmLand>().upDigged = true;
                        nearGround[i].GetComponent<FarmLand>().MakeDigSprite();
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
        if (!upDigged && !downDigged && !leftDigged && !rightDigged) // ���� �����ִ�
        {
            spriteRenderer.sprite = diggedGround[0];
        }
        else if (upDigged && !downDigged && !leftDigged && !rightDigged) // ���� �����ִ�
        {
            spriteRenderer.sprite = diggedGround[1];
        }
        else if (!upDigged && downDigged && !leftDigged && !rightDigged) // �Ʒ��� �����ִ�
        {
            spriteRenderer.sprite = diggedGround[2];
        }
        else if (!upDigged && !downDigged && leftDigged && !rightDigged) // �¸� �����ִ�
        {
            spriteRenderer.sprite = diggedGround[3];
        }
        else if (!upDigged && !downDigged && !leftDigged && rightDigged) // �츸 �����ִ�
        {
            spriteRenderer.sprite = diggedGround[4];
        }
        else if (upDigged && downDigged && !leftDigged && !rightDigged) // ���ϸ� �����ִ�
        {
            spriteRenderer.sprite = diggedGround[5];
        }
        else if (!upDigged && !downDigged && leftDigged && rightDigged) // �¿츸 �����ִ�
        {
            spriteRenderer.sprite = diggedGround[6];
        }
        else if (upDigged && !downDigged && !leftDigged && rightDigged) // ��츸 �����ִ�
        {
            spriteRenderer.sprite = diggedGround[7];
        }
        else if (!upDigged && downDigged && !leftDigged && rightDigged) // ���ϸ� �����ִ�
        {
            spriteRenderer.sprite = diggedGround[8];
        }
        else if (!upDigged && downDigged && leftDigged && !rightDigged) // ���ϸ� �����ִ�
        {
            spriteRenderer.sprite = diggedGround[9];
        }
        else if (upDigged && !downDigged && leftDigged && !rightDigged) // �»� �����ִ�
        {
            spriteRenderer.sprite = diggedGround[10];
        }
        else if (!upDigged && downDigged && leftDigged && rightDigged) // �� �������ִ�
        {
            spriteRenderer.sprite = diggedGround[11];
        }
        else if (upDigged && !downDigged && leftDigged && rightDigged) // �ϸ� �������ִ�
        {
            spriteRenderer.sprite = diggedGround[12];
        }
        else if (upDigged && downDigged && !leftDigged && rightDigged) // �¸� �������ִ�
        {
            spriteRenderer.sprite = diggedGround[13];
        }
        else if (upDigged && downDigged && leftDigged && !rightDigged) // �츸 �������ִ�
        {
            spriteRenderer.sprite = diggedGround[14];
        }
        else if (upDigged && downDigged && leftDigged && rightDigged) // ���� �����ִ�
        {
            spriteRenderer.sprite = diggedGround[15];
        }
    }
}
