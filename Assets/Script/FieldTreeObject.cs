using System;
using Unity;
using UnityEditor;
using UnityEngine;
using Unity.VisualScripting;
using Unity.Mathematics;

class FieldTreeObject : MonoBehaviour
{ //�ʵ忡 �����ϴ� ������ �Ӽ��� �����.
    [SerializeField]
    int fieldTreeObjectID; // �ܺο��� �Է� �޴´� <= �������� ���ʰ� �ȴ�.
    int toolType; // �÷��̾��� �� Ÿ�԰� ���ƾ� �������� �Դ´�.
    int toolLevel; // �÷��̾��� �� �������� ������ �������� ����.
    int hp; //ü��
    float fallXY;
    float branchShakeTime = 0;
    float branchFallTime = 0;
    string treeName; // �̸� ex)������, ��ǳ����
    bool branchShake = false;
    [SerializeField]
    bool branchOn = true; // ������ �ִ� �����ΰ�? => ��ü �������� ������ ������, �ܴ��� ������, � �������� ������ ����. => DB�� �߰��ؾ��� ����
    bool branchDrop=false; // 1ȸ ������ ���� bool
    bool rootDrop=false; // 1ȸ ������ ���� bool

    GameObject[] dropItemPrefab; //����������� ������
    
    [SerializeField]
    PlayerInventroy playerInventroy; // �÷��̾��� �κ��丮
    FieldTreeObjectDb fieldTreeObjectDb; // �ʵ峪��������ƮDB���� ID�� ��ġ�ϴ� ID�� ���� ������ �޴´�.
    ItemDB[] itemDB; // ������ ������ �޴´�. ��� �������� �𸥴�.
    ItemDB onHandItem;
    SpriteRenderer branch;
    SpriteRenderer root;
    [SerializeField]
    Sprite afterRoot;
    void FieldTreeSetting() // ������Ʈ�� ��ҵ��� �����Ѵ�
    {
        fieldTreeObjectDb = new FieldTreeObjectDb(fieldTreeObjectID); // ID�� ���� ��Ÿ��, ������, hp, �̸� �� ��������.
        fieldTreeObjectDb.FieldTreeObjectSetting();
        this.toolType = fieldTreeObjectDb.toolType;
        this.toolLevel = fieldTreeObjectDb.toolLevel;
		this.hp = fieldTreeObjectDb.hp;
		this.treeName = fieldTreeObjectDb.treeName;
	    this.itemDB = new ItemDB[fieldTreeObjectDb.items]; // ������DBŬ������ ������ID�� �Ѱ��ش�. ������DB�� ���� ID�� �°� ������ �����͸� �����Ѵ�.
		for (int i = 0 ; i<fieldTreeObjectDb.items ; i++){
            itemDB[i] = new ItemDB(fieldTreeObjectDb.itemID[i]); // ù��° �������� itemDB�� Ʈ��������Ʈ�� ù��° ������ID�� ���� ��
            itemDB[i].itemSetting();}
	}
    private void Awake()
    {
        FieldTreeSetting();
        branch = transform.GetChild(1).GetComponent<SpriteRenderer>();
        root = transform.GetChild(0).GetComponent<SpriteRenderer>();
        dropItemPrefab = new GameObject[fieldTreeObjectDb.items]; //DB���� �������� �ҷ��� GameObject�� �����Ѵ�
        for (int i = 0 ;i<fieldTreeObjectDb.items; i++)
        {
            dropItemPrefab[i] = Resources.Load($"Prefabs/{itemDB[i].name}") as GameObject; //1�� �������� ���� 2�� �������� ����
        }
    }
    private void Start()
    {
        if(branch == null) {branchOn = false;}
    }
    private void Update() //�÷��̾ Ư�������� ������������ �����ؾ��Ѵ�.
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
                if (branchOn) // ������ �ִ� ��쿡 ���ؼ� -> ������ ���� ��찡 �� ���� ����
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
                Debug.Log("������ ����� ������ ���� �� ����.");
            }
        }
        if (hp == 4)
        {
            if (this.transform.position.x > collision.GetComponentInParent<Transform>().transform.position.x) // ������ �� �����ʿ� �ִٸ�
            {
                fallXY = -1f; // ���������� �Ѿ�����
            }
            else { fallXY = 1f; } // ������ �� ���ʿ� �ִٸ� �������� �Ѿ�����
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
        if (branch == null && !branchDrop) // ������ �ν������� ������ ���
        {
            for (int i = 0; i < fieldTreeObjectDb.items; i++)  // prefab[0] [1]�� �����Ѵ�.
            {
                for (int j = 0; j < fieldTreeObjectDb.dropnumber[i]; j++)
                { // prefab[0]�� dropnumber[0]�� ��ŭ �����Ѵ�.
                    Instantiate(dropItemPrefab[i], new Vector2 (this.transform.position.x + (fallXY * -4f), this.transform.position.y), quaternion.identity);
                }
            }
            branchDrop = true;
        }
        if (hp <= 0 && !rootDrop)
        {
            for (int i = 0; i < fieldTreeObjectDb.items; i++)  // prefab[0] [1]�� �����Ѵ�.
            {
                for (int j = 0; j < fieldTreeObjectDb.dropnumber[i]; j++)
                { // prefab[0]�� dropnumber[0]�� ��ŭ �����Ѵ�.
                    Instantiate(dropItemPrefab[i], this.transform.position, quaternion.identity);
                }
            }
        }
    }

}
