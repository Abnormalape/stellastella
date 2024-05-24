using System;
using System.Runtime.InteropServices;
using Unity;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

//각 성장 상태 별로 별개의 개체라는 사실을 인지해야 했으나 일단은 그냥 진행함
class FieldTreeLand : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] int fieldTreeObjectID;
    FieldTreeObjectDb thisTree;
    
    [SerializeField] int hp;

    int currentDay;
    int currentMonth;

    bool branchOn = false;

    [SerializeField] public int currentLevel = 0;

    GameObject[] dropItemPrefab;
    PlayerInventroy playerInventroy;
    FieldTreeObjectDb fieldTreeObjectDb;
    ItemDB[] itemDB;
    ItemDB onHandItem;

    BoxCollider2D mybox;

    SpriteRenderer branch;
    [SerializeField] Sprite branchImage;
    SpriteRenderer root;
    [SerializeField] Sprite[] rootImages = new Sprite[6];

    private void OnEnable()
    {
        mybox = GetComponent<BoxCollider2D>();
        mybox.isTrigger = true;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        thisTree = new FieldTreeObjectDb(fieldTreeObjectID);
        root = transform.GetChild(0).GetComponent<SpriteRenderer>();
        branch = transform.GetChild(1).GetComponent<SpriteRenderer>();
        branch.sprite = null;
        branch.enabled = false;
        itemDB = new ItemDB[thisTree.items];
        dropItemPrefab = new GameObject[thisTree.items];
        for (int i = 0; i < thisTree.items; i++)
        {
            itemDB[i] = new ItemDB(thisTree.itemID[i]);
            itemDB[i].itemSetting();
            dropItemPrefab[i] = Resources.Load($"Prefabs/FieldItems/{itemDB[i].name}") as GameObject;
        }
        WhenLevelChanged();
    }
    private void Update()
    {
        UpdateDate();
        MakeSprite();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LeftClick" && collision.GetComponentInParent<PlayerController>() != null)
        {
            onHandItem = new ItemDB(collision.GetComponentInParent<PlayerInventroy>().currentInventoryItem);
            switch (currentLevel)
            {
                case 0://씨앗
                    if (onHandItem.toolType == 1 || onHandItem.toolType == 2 || onHandItem.toolType == 4) // 도끼거나, 괭이거나, 곡괭이라면.
                    {
                        DestroyTree();
                    }
                    break;

                case 1://싹
                    if (onHandItem.toolType == 1 || onHandItem.toolType == 2 || onHandItem.toolType == 4 || onHandItem.toolType == 5) // 도끼거나, 괭이거나, 곡괭이거나, 낫이라면
                    {
                        
                        DestroyTree();
                    }
                    break;

                case 2:
                case 3:
                case 4: //성목
                case 5:
                    if (onHandItem.toolType == 1) // 도끼라면
                    {
                        this.hp -= onHandItem.hpRestore;
                        BranchDestroy(collision);
                        DestroyTree();
                    }
                    break;
            }
        }
    }
    void UpdateDate()
    {
        if (currentMonth != gameManager.currentMonth || currentDay != gameManager.currentDay)
        {
            currentMonth = gameManager.currentMonth;
            currentDay = gameManager.currentDay;

            int i = Random.Range(0, 100);
            if (currentMonth != 3 && i >= 80 && currentLevel < 4)
            {
                currentLevel++;
                WhenLevelChanged();
            }
        }
    }

    void MakeSprite()
    {
        root.sprite = rootImages[currentLevel];
        if (branchOn)
        {
            branch.enabled = true;
            branch.sprite = branchImage;
        }
    }
    void WhenLevelChanged()
    {
        DropItemController();
        switch (currentLevel)
        {
            case 0:
                mybox.isTrigger = true;
                break;
            case 1:
                mybox.isTrigger = true;
                break;
            case 2:
                mybox.isTrigger = false;
                hp = 1;
                break;
            case 3:
                mybox.isTrigger = false;
                hp = 5;
                break;
            case 4:
                mybox.isTrigger = false;
                branchOn = true;
                hp = 15;
                break;
        }
    }
    void DestroyTree()
    {
        switch (currentLevel)
        {
            case 0:
                DropItemMake();
                Destroy(this.gameObject);
                break;
            case 1:
                DropItemMake();
                Destroy(this.gameObject);
                break;
            case 2:
                if (hp <= 0)
                {
                    DropItemMake();
                    Destroy(this.gameObject);
                }
                break;
            case 3:
                if (hp <= 0)
                {
                    DropItemMake();
                    Destroy(this.gameObject);
                }
                break;
            case 4:
                if (hp <= 0)
                {
                    DropItemMake();
                    Destroy(this.gameObject);
                }
                break;
            case 5:
                if (hp <= 0)
                {
                    DropItemMake();
                    Destroy(this.gameObject);
                }
                break;
        }
    }

    void DropItemController()
    {   //드랍률로 드랍양 컨트롤
        switch (currentLevel)
        {
            case 0:
                thisTree.droprate[0] = 0;
                thisTree.droprate[1] = 0;
                thisTree.droprate[2] = 100;
                break;
            case 1:
                thisTree.droprate[0] = 0;
                thisTree.droprate[1] = 0;
                thisTree.droprate[2] = 0;
                break;
            case 2:
                thisTree.droprate[0] = 100;
                thisTree.droprate[1] = 0;
                thisTree.droprate[2] = 0;
                break;
            case 3:
                thisTree.droprate[0] = 500;
                thisTree.droprate[1] = 0;
                thisTree.droprate[2] = 0;
                break;
            case 4:
                thisTree.droprate[0] = 0;
                thisTree.droprate[1] = 0;
                thisTree.droprate[2] = 0;
                break;
            case 5:
                thisTree.droprate[0] = 1500;
                thisTree.droprate[1] = 500;
                thisTree.droprate[2] = 100;
                break;
        }
    }

    void DropItemMake()
    {   //드랍률에 따라 아이템을 드랍하는 메소드
        for (int i = 0; i < thisTree.items; i++)
        {
            for (int j = 100; j <= thisTree.droprate[i]; j = j + 100)
            {
                Instantiate(dropItemPrefab[i], transform.position, Quaternion.identity);
            }
            int r = Random.Range(0, 100);
            if (r < thisTree.droprate[i] % 100)
            {
                
                Instantiate(dropItemPrefab[i], transform.position, Quaternion.identity);
            }
        }
    }

    void BranchDestroy(Collider2D collision)
    {
        if (currentLevel == 4 & hp <= 5)
        {
            branch.gameObject.GetComponent<FieldTreeLandBranch>().fallXY = XY(collision);


            currentLevel = 5;
            WhenLevelChanged();
            branchOn = false;
            branch.transform.parent = null;
        }
    }

    public float XY(Collider2D collision)
    {
        float XY;
        if (collision.transform.parent.position.x - this.transform.position.x > 0)
        {
            XY = 1;
        }
        else { XY = -1; }

        return XY;
    }

}