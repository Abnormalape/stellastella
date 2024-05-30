using System;
using Unity.VisualScripting;
using UnityEngine;
class FishingMiniGame : MonoBehaviour // FishGrade미완성
{
    public int fishID;
    int fishGrade;
    FishDB fishDB;
    int difficulty;

    public bool fishingEnd;

    GameObject fishingBar;
    GameObject fish;
    GameObject catchBar;

    public bool getFish;

    private void OnEnable()
    {
        fishDB = new FishDB(fishID);
        difficulty = fishDB.fishDifficulty;
    }
    private void Update()
    {

    }

    public void FishingEnd(bool FF)
    {
        if (FF == true)
        {   //fishingbar 한테서 perfect인지 확인해서 grade를 한단계 높인다.
            Debug.Log("낚시성공!");
            GetComponentInParent<PlayerInventroy>().AddDirectItem(fishID, fishGrade);
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

