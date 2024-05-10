using System;
using Unity;
using UnityEditor;
using UnityEngine;
using Unity.VisualScripting;
using Unity.Mathematics;

class FieldTreeObject : MonoBehaviour
{ //필드에 존재하는 나무의 속성을 만든다.
    [SerializeField]
    int fieldTreeObjectID; // 외부에서 입력 받는다 <= 프리팹의 기초가 된다.
    int toolType; // 플레이어의 툴 타입과 같아야 데미지를 입는다.
    int toolLevel; // 플레이어의 툴 레벨보다 높으면 데미지가 없다.
    int hp; //체력
    float fallXY;
    float branchShakeTime = 0;
    float branchFallTime = 0;
    string treeName; // 이름 ex)참나무, 단풍나무
    bool branchShake = false;
    [SerializeField]
    bool branchOn = true; // 가지가 있는 상태인가? => 성체 나무들은 가지가 있지만, 단단한 나무나, 어린 나무들은 가지가 없다. => DB에 추가해야할 내용
    bool branchDrop=false; // 1회 실행을 위한 bool
    bool rootDrop=false; // 1회 실행을 위한 bool

    GameObject[] dropItemPrefab; //드랍아이템의 프리팹
    
    [SerializeField]
    PlayerInventroy playerInventroy; // 플레이어의 인벤토리
    FieldTreeObjectDb fieldTreeObjectDb; // 필드나무오브젝트DB에서 ID와 일치하는 ID를 가진 나무를 받는다.
    ItemDB[] itemDB; // 아이템 종류를 받는다. 몇개를 받을지는 모른다.
    ItemDB onHandItem;
    SpriteRenderer branch;
    SpriteRenderer root;
    [SerializeField]
    Sprite afterRoot;
    void FieldTreeSetting() // 오브젝트의 요소들을 생성한다
    {
        fieldTreeObjectDb = new FieldTreeObjectDb(fieldTreeObjectID); // ID에 따라서 툴타입, 툴레벨, hp, 이름 이 정해진다.
        fieldTreeObjectDb.FieldTreeObjectSetting();
        this.toolType = fieldTreeObjectDb.toolType;
        this.toolLevel = fieldTreeObjectDb.toolLevel;
		this.hp = fieldTreeObjectDb.hp;
		this.treeName = fieldTreeObjectDb.treeName;
	    this.itemDB = new ItemDB[fieldTreeObjectDb.items]; // 아이템DB클래스에 아이템ID를 넘겨준다. 아이템DB는 받은 ID에 맞게 아이템 데이터를 저장한다.
		for (int i = 0 ; i<fieldTreeObjectDb.items ; i++){
            itemDB[i] = new ItemDB(fieldTreeObjectDb.itemID[i]); // 첫번째 아이템은 itemDB에 트리오브젝트의 첫번째 아이템ID를 받은 값
            itemDB[i].itemSetting();}
	}
    private void Awake()
    {
        FieldTreeSetting();
        branch = transform.GetChild(1).GetComponent<SpriteRenderer>();
        root = transform.GetChild(0).GetComponent<SpriteRenderer>();
        dropItemPrefab = new GameObject[fieldTreeObjectDb.items]; //DB에서 가짓수를 불러와 GameObject를 생성한다
        for (int i = 0 ;i<fieldTreeObjectDb.items; i++)
        {
            dropItemPrefab[i] = Resources.Load($"Prefabs/{itemDB[i].name}") as GameObject; //1번 프리팹은 나무 2번 프리팹은 수액
        }
    }
    private void Start()
    {
        if(branch == null) {branchOn = false;}
    }
    private void Update() //플레이어가 특정조건을 만족시켰을때 반응해야한다.
    {
        treeanimation();
        dropItem();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInventroy = collision.GetComponentInParent<PlayerInventroy>();

        onHandItem = new ItemDB(playerInventroy.currentInventoryItem);
        onHandItem.itemSetting();
        if(onHandItem.toolType == this.toolType)
        {
            
            if(onHandItem.grade >= this.toolLevel)
            {   
                if (branchOn) // 가지가 있는 경우에 대해서 -> 가지가 없는 경우가 더 많기 때문
                {
                    this.hp -= onHandItem.hpRestore;
                    branchShake = true;
                }
                if (!branchOn)
                {
                    this.hp -= onHandItem.hpRestore;
                }
            }
            else if (onHandItem.grade < this.toolLevel)
            {
                Debug.Log("도구가 충분히 강하지 않은 것 같다.");
            }
        }
        if (hp == 4)
        {
            if (this.transform.position.x > collision.GetComponentInParent<Transform>().transform.position.x) // 나무가 더 오른쪽에 있다면
            {
                fallXY = -1f; // 오른쪽으로 넘어져라
            }
            else { fallXY = 1f; } // 나무가 더 왼쪽에 있다면 왼쪽으로 넘어져라
        }
    }

    private void treeanimation()
    {
        if (branchShake && branch != null)
        {
            branchShakeTime += Time.deltaTime;
            branch.transform.rotation = Quaternion.Euler(0, 0, 1f * math.sin(2 * math.PI * branchShakeTime));
            if (branchShakeTime > 1f)
            {
                branchShakeTime = 0f;
                branchShake = false;
            }
        }
        if (hp == 4)
        {
            branchOn = false;
            root.sprite = afterRoot;
        }
        if (!branchOn & branch != null)
        {
            branchFallTime += Time.deltaTime;
            branch.transform.rotation = Quaternion.Euler(0, 0, fallXY * branchFallTime * 45f * branchFallTime);
            if (branchFallTime * 45f * branchFallTime > 90) { Destroy(branch); }
        }
        if (hp <= 0)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
    private void dropItem()
    {
        if (branch == null && !branchDrop) // 가지가 부숴졌을때 아이템 드랍
        {
            for (int i = 0; i < fieldTreeObjectDb.items; i++)  // prefab[0] [1]을 생성한다.
            {
                for (int j = 0; j < fieldTreeObjectDb.dropnumber[i]; j++)
                { // prefab[0]을 dropnumber[0]개 만큼 생성한다.
                    Instantiate(dropItemPrefab[i], new Vector2 (this.transform.position.x + (fallXY * -4f), this.transform.position.y), quaternion.identity);
                }
            }
            branchDrop = true;
        }
        if (hp <= 0 && !rootDrop)
        {
            for (int i = 0; i < fieldTreeObjectDb.items; i++)  // prefab[0] [1]을 생성한다.
            {
                for (int j = 0; j < fieldTreeObjectDb.dropnumber[i]; j++)
                { // prefab[0]을 dropnumber[0]개 만큼 생성한다.
                    Instantiate(dropItemPrefab[i], this.transform.position, quaternion.identity);
                }
            }
        }
    }

}
