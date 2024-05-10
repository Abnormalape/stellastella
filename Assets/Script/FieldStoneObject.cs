using System;
using Unity;
using UnityEditor;
using UnityEngine;
using Unity.VisualScripting;
using Unity.Mathematics;
using Unity.Properties;

class FieldStoneObject : MonoBehaviour
{ //�ʵ忡 �����ϴ� ������ �Ӽ��� �����.
    [SerializeField]
    int fieldStoneObjectID; // �ܺο��� �Է� �޴´� <= �������� ���ʰ� �ȴ�.
    int toolType; // �÷��̾��� �� Ÿ�԰� ���ƾ� �������� �Դ´�.
    int toolLevel; // �÷��̾��� �� �������� ������ �������� ����.
    int hp; //ü��
    string stoneName; // �̸� ex)��, �ܴ��� ��
    GameObject[] dropItemPrefab; //����������� ������

    [SerializeField]
    PlayerInventroy playerInventroy;
    FieldStoneObjectDB fieldStoneObjectDB; // �� ������Ʈ
    ItemDB[] itemDB; // ������ ������ �޴´�. ��� �������� �𸥴�.
    ItemDB onHandItem; // �÷��̾ ��� �ִ� ������
    SpriteRenderer stoneSprite;
    void FieldStoneSetting() // ������Ʈ�� ��ҵ��� �����Ѵ�
    {
        fieldStoneObjectDB = new FieldStoneObjectDB(fieldStoneObjectID); // ID�� ���� ��Ÿ��, ������, hp, �̸� �� ��������.
        fieldStoneObjectDB.FieldStoneObjectSetting();
        this.toolType = fieldStoneObjectDB.toolType;
        this.toolLevel = fieldStoneObjectDB.toolLevel;
        this.hp = fieldStoneObjectDB.hp;
        this.stoneName = fieldStoneObjectDB.stoneName;
        this.itemDB = new ItemDB[fieldStoneObjectDB .items]; // ������DBŬ������ ������ID�� �Ѱ��ش�. ������DB�� ���� ID�� �°� ������ �����͸� �����Ѵ�.
        for (int i = 0; i < fieldStoneObjectDB.items; i++)
        {
            itemDB[i] = new ItemDB(fieldStoneObjectDB.itemID[i]); // ù��° �������� itemDB�� Ʈ��������Ʈ�� ù��° ������ID�� ���� ��
            itemDB[i].itemSetting();
        }
    }
    private void Awake()
    {
        FieldStoneSetting();
        stoneSprite = this.GetComponent<SpriteRenderer>();
        dropItemPrefab = new GameObject[fieldStoneObjectDB.items]; //DB���� �������� �ҷ��� GameObject�� �����Ѵ�
        for (int i = 0; i < fieldStoneObjectDB.items; i++)
        {
            dropItemPrefab[i] = Resources.Load($"Prefabs/{itemDB[i].name}") as GameObject; //
        }
    }
    private void Start()
    {
        
    }
    private void Update() //�÷��̾ Ư�������� ������������ �����ؾ��Ѵ�.
    {
        dropItem();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInventroy = collision.GetComponentInParent<PlayerInventroy>();

        onHandItem = new ItemDB(playerInventroy.currentInventoryItem);
        onHandItem.itemSetting();
        if (onHandItem.toolType == this.toolType)
        {
            if (onHandItem.grade >= this.toolLevel)
            {
                this.hp -= onHandItem.hpRestore;
            }
            else if (onHandItem.grade < this.toolLevel)
            {
                Debug.Log("������ ����� ������ ���� �� ����.");
            }
        }
    }

    
    private void dropItem()
    {
        if (hp <= 0)
        {
            for (int i = 0; i < fieldStoneObjectDB.items; i++)  // prefab[0] [1]�� �����Ѵ�.
            {
                for (int j = 0; j < fieldStoneObjectDB.dropnumber[i]; j++)
                { // prefab[0]�� dropnumber[0]�� ��ŭ �����Ѵ�.
                    Instantiate(dropItemPrefab[i], this.transform.position, quaternion.identity);
                }
            }
            Destroy(this.gameObject);
        }
    }

}