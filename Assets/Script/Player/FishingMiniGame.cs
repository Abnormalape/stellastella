using System;
using Unity.VisualScripting;
using UnityEngine;
class FishingMiniGame : MonoBehaviour // FishGrade미완성
{
    [SerializeField]public int fishID;
    int fishGrade;
    FishDB fishDB;
    int difficulty;

    public bool fishingEnd;

    GameObject fishingBar;
    GameObject fish;
    GameObject catchBar;

    public bool getFish;

    bool setfish = false;
    private void Update()
    {
        if(fishID != 0 && setfish == false)
        {
            fishDB = new FishDB(fishID);
            difficulty = fishDB.fishDifficulty;
            GetComponentInChildren<MovingFish>().fishID = this.fishID;
            setfish = true;
        }
    }

    public void FishingEnd(bool FF)
    {
        if (FF == true)
        {   //fishingbar 한테서 perfect인지 확인해서 grade를 한단계 높인다.
            Debug.Log("낚시성공!");
            GetComponentInParent<PlayerInventroy>().AddDirectItem(fishID, fishGrade); //아이템 추가
        }
        else
        {
            Debug.Log("낚시실패!");
        }
        GetComponentInParent<PlayerFishingMinigame>().EndFishing();
    }

    bool perfect;
    void FishGrade()
    {
        if (perfect)
        {

        }
    }
}

