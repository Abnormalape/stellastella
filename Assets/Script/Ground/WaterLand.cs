using System;
using UnityEngine;
using Random = UnityEngine.Random;
class WaterLand : MonoBehaviour
{
    [SerializeField] int goldPercent;   //낚시 지점의 금등급 확률 : 직접 적어줘야 함
    [SerializeField] int silverPercent; //낚시 지점의 은등급 확률 : 직접 적어줘야 함
    int grade;

    private void Awake()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "LeftClick")
        {
            ItemDB itemDB = new ItemDB(collision.GetComponentInParent<PlayerInventroy>().currentInventoryItem);
            //좌클릭 콜라이더와 접촉하고 그 주인이 낚싯대를 들고 있다면
            if(itemDB.toolType == 9)
            {
                // 플레이어를 입질대기 상태로 만든다.
                collision.GetComponentInParent<PlayerLeftClick>().waitingForBait = true;
                collision.GetComponentInParent<PlayerFishingMinigame>().fishID = 1; //확률에 따라 물고기를 전달

                // 플레이어낚시 미니게임에 물고기 정보를 전달한다.
                SelectFish(collision);
            }
        }
    }

    int[] fishPercentage;
    int totalFishPercentage;
    void SelectFish(Collider2D collision)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); // 날씨 장소 판정을 위함

        int RG = Random.Range(0, 100); // 이렇게 쓰는 이유는 낚시 레벨 및 찌와 미끼 사용에 따른 확률 변동을 감지하기 위해서
        if (RG < goldPercent)
        {
            grade = 2;//금등급
        }
        else if (RG >= 100 - silverPercent)
        {
            grade = 1;//은등급
        }
        else
        {
            grade = 0;//등급 외
        }

        //물고기 두 종류 배정
        FishDB[] fishDBs = new FishDB[2];
        fishPercentage = new int[fishDBs.Length];

        for (int i = 0; i < 2; i++) 
        {
            fishDBs[i] = new FishDB(i+1);
            totalFishPercentage = totalFishPercentage + fishDBs[i].fishPercentage; 
            fishPercentage[i] = totalFishPercentage;
        }

        //fishpercentage[0] = 10, [1] = 10+90, [2] = 10+90+...
        


        int R = Random.Range(0, totalFishPercentage); //(10+90+50)이라면 10이하일때 1번 물고기, 100이하일때 2번, 150이하 일때 3번
        
        for(int i = 0;i < fishDBs.Length;i++)
        {
            if(R < fishPercentage[i])
            {
                collision.GetComponentInParent<PlayerFishingMinigame>().fishID = fishDBs[i].fishID; // 플레이어의 낚시 클래스에 물고기 ID전달
                collision.GetComponentInParent<PlayerFishingMinigame>().fishGrade = grade; // 플레이어의 낚시 클래스에 물고기 등급전달
                return;
            }
        }
        //일단 봄 물고기 두종류만
    }
}

