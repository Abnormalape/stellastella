using System;
using Unity.VisualScripting;
using UnityEngine;
class FishingMiniGame : MonoBehaviour
{   //낚시 미니게임 프리팹
    public int fishID;
    int fishGrade;
    FishDB fishDB;
    int difficulty;

    GameObject fishingBar; // 내가 움직이는 낚시 바
    GameObject fish; // 알아서 움직이는 물고기
    GameObject catchBar; // 물고기와 낚시바가 일치 한다면 상승

    public bool getFish;

    private void OnEnable()
    {
        fishID = GetComponentInParent<PlayerFishingMinigame>().fishID;
        fishDB = new FishDB(fishID);
        difficulty = fishDB.fishDifficulty;
    }
    private void Update()
    {
        
    }

    bool perfect;
    void FishGrade()
    {
        if (perfect) 
        {
            GetComponentInParent<PlayerFishingMinigame>().fishGrade++;
        }
    }

    public void GetFish()
    {
        FishGrade(); // 부모의 물고기 등급을 높여줌
        GetComponentInParent<PlayerFishingMinigame>().AddFish();
    }
}

