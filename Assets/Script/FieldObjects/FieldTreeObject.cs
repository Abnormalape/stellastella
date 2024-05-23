using System;
using Unity;
using UnityEditor;
using UnityEngine;
using Unity.VisualScripting;
using Unity.Mathematics;
using System.Net.Http.Headers;

class FieldTreeObject : MonoBehaviour
{ //�ʵ忡 �����ϴ� ������ �Ӽ��� �����.
    [SerializeField]
    int fieldTreeObjectID; // �ܺο��� �Է� �޴´� <= �������� ���ʰ� �ȴ�.
    int toolType; // �÷��̾��� �� Ÿ�԰� ���ƾ� �������� �Դ´�.
    int hp; //ü��

    float fallXY;
    float branchShakeTime = 0;
    float branchFallTime = 0;
    bool branchShake = false;

    string treeName; // �̸� ex)������, ��ǳ����

    [SerializeField]
    bool branchOn = false; // ������ �ִ� �����ΰ�? => ��ü �������� ������ ������, �ܴ��� ������, � �������� ������ ����. => DB�� �߰��ؾ��� ����
    bool branchDrop = false; // 1ȸ ������ ���� bool
    bool rootDrop = false; // 1ȸ ������ ���� bool

    public int maxLevel; // ����ܰ�
    public int currentLevel = 0; // ���缺��ܰ�(���� �⺻)

    
    GameObject[] dropItemPrefab; //����������� ������


    PlayerInventroy playerInventroy; // �÷��̾��� �κ��丮
    FieldTreeObjectDb fieldTreeObjectDb; // �ʵ峪��������ƮDB���� ID�� ��ġ�ϴ� ID�� ���� ������ �޴´�.
    ItemDB[] itemDB; // ������ ������ �޴´�. ��� �������� �𸥴�.
    ItemDB onHandItem;

    SpriteRenderer branch; // ���� �̹���
    [SerializeField] Sprite branchImage;

    SpriteRenderer root; // �⺻ �̹���
    [SerializeField] Sprite[] rootImages = new Sprite[6]; // ������ 5�ܰ�, 0�� ����, 5�� ����, 6�� ������ ����.

    void FieldTreeSetting() // ������Ʈ�� ��ҵ��� �����Ѵ�
    {
        fieldTreeObjectDb = new FieldTreeObjectDb(fieldTreeObjectID); // ID�� ���� ��Ÿ��, ������, hp, �̸� �� ��������.
        fieldTreeObjectDb.FieldTreeObjectSetting();
        this.toolType = fieldTreeObjectDb.toolType;
        this.hp = fieldTreeObjectDb.maxHp;
        this.treeName = fieldTreeObjectDb.treeName;

        this.itemDB = new ItemDB[fieldTreeObjectDb.items]; // ������DBŬ������ ������ID�� �Ѱ��ش�. ������DB�� ���� ID�� �°� ������ �����͸� �����Ѵ�.
        for (int i = 0; i < fieldTreeObjectDb.items; i++)
        {
            itemDB[i] = new ItemDB(fieldTreeObjectDb.itemID[i]); // ù��° �������� itemDB�� Ʈ��������Ʈ�� ù��° ������ID�� ���� ��
            itemDB[i].itemSetting();
        }
        
        dropItemPrefab = new GameObject[fieldTreeObjectDb.items];
        for (int i = 0; i < fieldTreeObjectDb.items; i++)
        {
            dropItemPrefab[i] = Resources.Load($"Prefabs/FieldItems/{itemDB[i].name}") as GameObject; //1�� �������� ���� 2�� �������� ����
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
    private void Update() //�÷��̾ Ư�������� ������������ �����ؾ��Ѵ�.
    {
        GrowUp();
        treeanimation();
        dropItem();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �ε��� �ݶ��̴��� Tool�̸鼭 �װ��� �θ������Ʈ�� �����Ѵٸ�
        if (collision.tag == "Tool" && collision.GetComponentInParent<Transform>() != null)
        {
            playerInventroy = collision.GetComponentInParent<PlayerInventroy>();
            onHandItem = new ItemDB(playerInventroy.currentInventoryItem);
            onHandItem.itemSetting();

            switch (currentLevel)
            {
                case 0://����
                    if (onHandItem.toolType == 1 || onHandItem.toolType == 2 || onHandItem.toolType == 4) // �����ų�, ���̰ų�, ��̶��.
                    {
                        //������ ���� ���� ������Ʈ�� �ı��Ѵ�.
                    }
                    break;

                case 1://��
                    if (onHandItem.toolType == 1 || onHandItem.toolType == 2 || onHandItem.toolType == 4 || onHandItem.toolType == 5) // �����ų�, ���̰ų�, ��̰ų�, ���̶��
                    {
                        //���� ������Ʈ�� �ı��Ѵ�.
                    }
                    break;

                case 2://����
                    if (onHandItem.toolType == 1) // �������
                    {
                        this.hp -= onHandItem.hpRestore;
                    }
                    break;

                case 3://����
                    if (onHandItem.toolType == 1) // �������
                    {
                        this.hp -= onHandItem.hpRestore;
                    }
                    break;

                case 4://����
                    if (onHandItem.toolType == 1) // �������
                    {
                        FullGrown(collision);
                    }
                    break;
            }
        }
    }

    private void treeanimation() // ������ �������� ������ ����, ������ �������� ���� ����
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
            for (int i = 0; i < fieldTreeObjectDb.items; i++)  // prefab[0] [1]�� �����Ѵ�.
            {
                //for (int j = 0; j < fieldTreeObjectDb.dropnumber[i]; j++)
                //{ // prefab[0]�� dropnumber[0]�� ��ŭ �����Ѵ�.
                //    Instantiate(dropItemPrefab[i], this.transform.position, quaternion.identity);
                //}
            }
        }
    }
    private void GrowUp()   //�������, ��¥�� ����Ǹ� ���� �θ� ������Ʈ�� ������ ��¥ ����� �ش�.
    {
        if (currentLevel >= maxLevel && branch != null)
        {   //���� ������ �ִ� ���� �̻��̸�
            currentLevel = maxLevel;    //���� ������¸� �ִ뼺��ܰ�� �����ϰ�
            branchOn = true;            //������ �ִٰ� �˷��ְ�
            branch.enabled = true;      //������ ��������Ʈ�� Ų��.
            root.sprite = rootImages[currentLevel];
        }
        else if (currentLevel >= maxLevel && branch == null)
        {
            root.sprite = rootImages[maxLevel + 1];
        }
        else if (currentLevel < maxLevel)
        {   //������ �Ϸ� ���� �ʾҴٸ�
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
                GetComponentInChildren<FieldTreeBranch>().fallXY = -1f; //�Ѿ��� ���� ����
            }
            else { GetComponentInChildren<FieldTreeBranch>().fallXY = 1f; } //�Ѿ��� ���� ����
            BranchCutted();
        }
    }

    private void BranchCutted() // ������ �߷ȴ�.
    {   //������ HP�� ����������, �ڽĿ�����Ʈ�� ������Ų��.
        transform.GetChild(1).gameObject.transform.parent = null;
    }
}
