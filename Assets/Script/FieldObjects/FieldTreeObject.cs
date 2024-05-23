using System;
using Unity;
using UnityEditor;
using UnityEngine;
using Unity.VisualScripting;
using Unity.Mathematics;
using System.Net.Http.Headers;

class FieldTreeObject : MonoBehaviour
{ //필드에 존재하는 나무의 속성을 만든다.
    [SerializeField]
    int fieldTreeObjectID; // 외부에서 입력 받는다 <= 프리팹의 기초가 된다.
    int toolType; // 플레이어의 툴 타입과 같아야 데미지를 입는다.
    int hp; //체력

    float fallXY;
    float branchShakeTime = 0;
    float branchFallTime = 0;
    bool branchShake = false;

    string treeName; // 이름 ex)참나무, 단풍나무

    [SerializeField]
    bool branchOn = false; // 가지가 있는 상태인가? => 성체 나무들은 가지가 있지만, 단단한 나무나, 어린 나무들은 가지가 없다. => DB에 추가해야할 내용
    bool branchDrop = false; // 1회 실행을 위한 bool
    bool rootDrop = false; // 1회 실행을 위한 bool

    public int maxLevel; // 성장단계
    public int currentLevel = 0; // 현재성장단계(씨앗 기본)

    
    GameObject[] dropItemPrefab; //드랍아이템의 프리팹


    PlayerInventroy playerInventroy; // 플레이어의 인벤토리
    FieldTreeObjectDb fieldTreeObjectDb; // 필드나무오브젝트DB에서 ID와 일치하는 ID를 가진 나무를 받는다.
    ItemDB[] itemDB; // 아이템 종류를 받는다. 몇개를 받을지는 모른다.
    ItemDB onHandItem;

    SpriteRenderer branch; // 가지 이미지
    [SerializeField] Sprite branchImage;

    SpriteRenderer root; // 기본 이미지
    [SerializeField] Sprite[] rootImages = new Sprite[6]; // 성장은 5단계, 0은 씨앗, 5는 성목, 6은 벌목후 성목.

    void FieldTreeSetting() // 오브젝트의 요소들을 생성한다
    {
        fieldTreeObjectDb = new FieldTreeObjectDb(fieldTreeObjectID); // ID에 따라서 툴타입, 툴레벨, hp, 이름 이 정해진다.
        fieldTreeObjectDb.FieldTreeObjectSetting();
        this.toolType = fieldTreeObjectDb.toolType;
        this.hp = fieldTreeObjectDb.maxHp;
        this.treeName = fieldTreeObjectDb.treeName;

        this.itemDB = new ItemDB[fieldTreeObjectDb.items]; // 아이템DB클래스에 아이템ID를 넘겨준다. 아이템DB는 받은 ID에 맞게 아이템 데이터를 저장한다.
        for (int i = 0; i < fieldTreeObjectDb.items; i++)
        {
            itemDB[i] = new ItemDB(fieldTreeObjectDb.itemID[i]); // 첫번째 아이템은 itemDB에 트리오브젝트의 첫번째 아이템ID를 받은 값
            itemDB[i].itemSetting();
        }
        
        dropItemPrefab = new GameObject[fieldTreeObjectDb.items];
        for (int i = 0; i < fieldTreeObjectDb.items; i++)
        {
            dropItemPrefab[i] = Resources.Load($"Prefabs/FieldItems/{itemDB[i].name}") as GameObject; //1번 프리팹은 나무 2번 프리팹은 수액
        }
    }
    private void Awake()
    {
        FieldTreeSetting();
        root = transform.GetChild(0).GetComponent<SpriteRenderer>();
        branch = transform.GetChild(1).GetComponent<SpriteRenderer>();
        if (branchOn == false)
        {
            branch.enabled = false;
        }
    }
    private void Start()
    {
        
    }
    private void Update() //플레이어가 특정조건을 만족시켰을때 반응해야한다.
    {
        GrowUp();
        treeanimation();
        dropItem();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 부딪힌 콜라이더가 Tool이면서 그것의 부모오브젝트가 존재한다면
        if (collision.tag == "Tool" && collision.GetComponentInParent<Transform>() != null)
        {
            playerInventroy = collision.GetComponentInParent<PlayerInventroy>();
            onHandItem = new ItemDB(playerInventroy.currentInventoryItem);
            onHandItem.itemSetting();

            switch (currentLevel)
            {
                case 0://씨앗
                    if (onHandItem.toolType == 1 || onHandItem.toolType == 2 || onHandItem.toolType == 4) // 도끼거나, 괭이거나, 곡괭이라면.
                    {
                        //씨앗을 뱉어내고 게임 오브젝트를 파괴한다.
                    }
                    break;

                case 1://싹
                    if (onHandItem.toolType == 1 || onHandItem.toolType == 2 || onHandItem.toolType == 4 || onHandItem.toolType == 5) // 도끼거나, 괭이거나, 곡괭이거나, 낫이라면
                    {
                        //게임 오브젝트를 파괴한다.
                    }
                    break;

                case 2://묘목
                    if (onHandItem.toolType == 1) // 도끼라면
                    {
                        this.hp -= onHandItem.hpRestore;
                    }
                    break;

                case 3://묘목
                    if (onHandItem.toolType == 1) // 도끼라면
                    {
                        this.hp -= onHandItem.hpRestore;
                    }
                    break;

                case 4://성목
                    if (onHandItem.toolType == 1) // 도끼라면
                    {
                        FullGrown(collision);
                    }
                    break;
            }
        }
    }

    private void treeanimation() // 가지가 있을때는 가지를 흔들고, 가지가 없을때는 나를 흔들고
    {
        //if (branchShake && branch != null)
        //{
        //    branchShakeTime += Time.deltaTime;
        //    branch.transform.rotation = Quaternion.Euler(0, 0, 1f * math.sin(2 * math.PI * branchShakeTime));
        //    if (branchShakeTime > 1f)
        //    {
        //        branchShakeTime = 0f;
        //        branchShake = false;
        //    }
        //}
        //if (hp == 4)
        //{
        //    branchOn = false;
        //    root.sprite = rootImages[6];
        //}
        //if (!branchOn & branch != null)
        //{
        //    branchFallTime += Time.deltaTime;
        //    branch.transform.rotation = Quaternion.Euler(0, 0, fallXY * branchFallTime * 45f * branchFallTime);
        //    if (branchFallTime * 45f * branchFallTime > 90) { Destroy(branch); }
        //}
        //if (hp <= 0)
        //{
        //    this.gameObject.SetActive(false);
        //    Destroy(this.gameObject);
        //}
    }
    private void dropItem()
    {
        if (hp <= 0 && !rootDrop)
        {
            for (int i = 0; i < fieldTreeObjectDb.items; i++)  // prefab[0] [1]을 생성한다.
            {
                //for (int j = 0; j < fieldTreeObjectDb.dropnumber[i]; j++)
                //{ // prefab[0]을 dropnumber[0]개 만큼 생성한다.
                //    Instantiate(dropItemPrefab[i], this.transform.position, quaternion.identity);
                //}
            }
        }
    }
    private void GrowUp()   //성장관리, 날짜가 진행되면 얘의 부모 오브젝트가 얘한테 날짜 경과를 준다.
    {
        if (currentLevel >= maxLevel && branch != null)
        {   //현재 성장이 최대 성장 이상이면
            currentLevel = maxLevel;    //현재 성장상태를 최대성장단계로 유지하고
            branchOn = true;            //가지가 있다고 알려주고
            branch.enabled = true;      //가지의 스프라이트를 킨다.
            root.sprite = rootImages[currentLevel];
        }
        else if (currentLevel >= maxLevel && branch == null)
        {
            root.sprite = rootImages[maxLevel + 1];
        }
        else if (currentLevel < maxLevel)
        {   //성장이 완료 되지 않았다면
            root.sprite = rootImages[currentLevel];
        }
    }

    private void FullGrown(Collider2D collision)
    {
        if (branchOn)
        {
            this.hp -= onHandItem.hpRestore;
            branchShake = true;
        }
        else if (!branchOn)
        {
            this.hp -= onHandItem.hpRestore;
        }
        if (hp == 4)
        {
            if (this.transform.position.x > collision.GetComponentInParent<PlayerController>().transform.position.x)
            {
                GetComponentInChildren<FieldTreeBranch>().fallXY = -1f; //넘어질 방향 지정
            }
            else { GetComponentInChildren<FieldTreeBranch>().fallXY = 1f; } //넘어질 방향 지정
            BranchCutted();
        }
    }

    private void BranchCutted() // 가지가 잘렸다.
    {   //나무의 HP가 낮아졌을때, 자식오브젝트를 독립시킨다.
        transform.GetChild(1).gameObject.transform.parent = null;
    }
}
