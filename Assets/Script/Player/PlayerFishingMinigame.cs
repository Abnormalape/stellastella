using System;
using UnityEngine;
class PlayerFishingMinigame : MonoBehaviour
{   //플레이어 에게 삽입되는 클래스
    //물 한테서 물고기의 종류를 받아 낚시 미니게임을 생성한다.
    //플레이어의 낚시 레벨, 사용하는 미끼 찌, 낚싯대 등에 따라 낚시 박스의 크기가 커진다.

    public int fishID;
    public int fishGrade;

    public void FishingMiniGame()
    {
        Time.timeScale = 0f;

        GameObject gameObject = (GameObject)Resources.Load($"Prefabs/FishingPrefabs/FishingMiniGame");
        gameObject.GetComponent<FishingMiniGame>().fishID = this.fishID;
        //낚시 미니게임을 소환한다.
        Instantiate(gameObject, this.transform.position, Quaternion.identity).transform.parent = this.transform;
    }

    public void AddFish()
    {
        GetComponent<PlayerInventroy>().AddFishItem(fishID, fishGrade);
    }
}

