using System;
using Unity;
using UnityEngine;
using Random = UnityEngine.Random;

class HarvestControl : MonoBehaviour // cropcontrol이 가지는 자식 오브젝트의 스크립트, 중요)ID로 아이템을 생성한다
{
    int seedID;
    HarvestDB harvestDB;
    [SerializeField] GameObject[] dropItemPrefab;

    private void OnEnable()
    {
        seedID = GetComponentInParent<CropControl>().seedID; // 부모의 아이디를 가진다.
        harvestDB = new HarvestDB(seedID); // 당신의 수확물은 무엇인가요?
        dropItemPrefab = new GameObject[harvestDB.items]; //몇종류의 수확물을 가지나요?.
        for (int i = 0; i < harvestDB.items; i++)
        {
            dropItemPrefab[i] = Resources.Load($"Prefabs/{harvestDB.cropName[i]}") as GameObject; //수확물 데이터에서 이름을 찾아 그 이름과 일치하는 프리팹을 할당한다.
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.parent.tag == "Player" && collision.gameObject.transform.tag == "Tool") // 접촉한게 도구면서 그 부모가 플레이어라면
        {
            for (int i = 0; i < harvestDB.items; i++)  // prefab[i]를 생성한다.
            {
                for (int j = 0; j < harvestDB.dropnumber[i]; j++)
                {   // prefab[i]을 dropnumber[i]개 만큼 생성한다.

                    int farmlevel = collision.gameObject.GetComponentInParent<PlayerController>().farmLevel; // 수확자의 농사레벨 확인
                    int R = Random.Range(0, 100);
                    if(R > 80 - farmlevel) //금 20% 10레벨에 30퍼
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
                    Instantiate(dropItemPrefab[i]); // 등급을 설정하고 아이템을 만든다.
                }
            }
        }
    }

    //얘가 만들어낸 프리팹은 collider rigidbody itemdrop fielditem 을 가진다.
}

