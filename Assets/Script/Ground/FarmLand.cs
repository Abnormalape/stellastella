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
    int currentSeason;

    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite[] diggedGround = new Sprite[16]; //�� ��������Ʈ
    Collider2D[] nearGround; //��ó �� Ž����

    [SerializeField] public bool digged = false; //���� ���� Ȯ�ο�, �ܺ� ���޿�
    [SerializeField] bool upDigged = false;
    [SerializeField] bool downDigged = false;
    [SerializeField] bool leftDigged = false;
    [SerializeField] bool rightDigged = false;

    public string prefabPath;
    private void Awake() // ���� ������ �� �ʱ�ȭ
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentDate = gameManager.currentDay;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        DayChange();

        if (digged) // ��������Ʈ ����
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
                    GameObject.Instantiate(Resources.Load($"Prefabs/LandPrefabs/FarmLandController") as GameObject, this.transform.position, Quaternion.identity).transform.parent = this.transform;
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
        if (currentDate != gameManager.currentDay) // ��¥�� �ٲ�ٸ�.
        {
            currentDate = gameManager.currentDay; // ��¥�� ������Ʈ �ϰ�.
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
