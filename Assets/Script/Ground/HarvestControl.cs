using UnityEngine;
using Random = UnityEngine.Random;

class HarvestControl : MonoBehaviour // cropcontrol이 가지는 자식 오브젝트의 스크립트, 중요)ID로 아이템을 생성한다
{
    int seedID;
    int numbers;
    ItemDB[] ItemDB;
    HarvestDB harvestDB;
    GameObject[] dropItemPrefab;
    public bool harvested = false;
    public bool handHarvest;

    public Collider2D touchedObject;
    private void OnEnable()
    {
        seedID = GetComponentInParent<CropControl>().seedID; // 부모의 아이디 추출

        harvestDB = new HarvestDB(seedID);                  //수확물 추출
        harvestDB.HarvestDBSetting();                       //수확물 셋팅
        dropItemPrefab = new GameObject[harvestDB.items];   //수확물 종류 만큼 프리팹 공간 생성
        ItemDB = new ItemDB[harvestDB.items];               //수확물 종류 만큼 아이템 공간 생성

        for (int i = 0; i < harvestDB.items; i++)
        {
            ItemDB[i] = new ItemDB(harvestDB.itemID[i]);    //아이템 생성
            ItemDB[i].itemSetting();                        //아이템 정보 생성
            dropItemPrefab[i] = Resources.Load($"Prefabs/FieldItems/{ItemDB[i].name}") as GameObject;
        }
    }

    private void Update()
    {
        if (handHarvest)
        {
            HandHarvest();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LeftClick" && collision.gameObject.GetComponent<EdgeCollider2D>() != null) // 좌클릭과 접촉했는데, 그놈에게 엣지콜라이더가 있다면(낫을 휘둘렀을때)
        {
            MakeDropItems(touchedObject);
            this.gameObject.GetComponentInParent<CropControl>().onceharvested = true;
            this.gameObject.GetComponentInParent<CropControl>().harvested = true;

            if (this != null)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    //얘가 만들어낸 프리팹은 collider rigidbody itemdrop fielditem 을 가진다.
    void MakeDropItems(Collider2D collision)
    {
        for (int i = 0; i < harvestDB.items; i++)  // prefab[i]를 생성한다.
        {
            for (int j = 0; j < harvestDB.dropnumber[i]; j++)
            {   // prefab[i]을 dropnumber[i]개 만큼 생성한다.

                int farmlevel = collision.gameObject.GetComponentInParent<PlayerController>().farmLevel; // 수확자의 농사레벨 확인
                int R = Random.Range(0, 100);

                if (ItemDB[i].type == "Fruit")
                {
                    if (R > 80 - farmlevel) //금 20% 10레벨에 30퍼
                    {
                        dropItemPrefab[i].GetComponent<FieldItem>().grade = 3;
                    }
                    else if (R > 60 - farmlevel * 3) //은 20%, 10레벨에 40퍼
                    {
                        dropItemPrefab[i].GetComponent<FieldItem>().grade = 2;
                    }
                    else //무 60%, 10레벨에 30퍼
                    {
                        dropItemPrefab[i].GetComponent<FieldItem>().grade = 1;
                    }
                }
                Instantiate(dropItemPrefab[i], this.transform.position, Quaternion.identity); // 등급을 설정하고 아이템을 만든다.
            }
        }
    }
    void HandHarvest()
    {
        MakeDropItems(touchedObject); // todo: 여기 touchedobject가 null일 가능성.
        this.gameObject.GetComponentInParent<CropControl>().onceharvested = true;
        this.gameObject.GetComponentInParent<CropControl>().harvested = true;

        if (this != null)
        {
            this.gameObject.SetActive(false);
        }
        handHarvest = false;
    }
}

