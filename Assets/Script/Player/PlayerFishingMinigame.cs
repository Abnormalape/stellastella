using System;
using UnityEngine;
using Random = UnityEngine.Random;
class PlayerFishingMinigame : MonoBehaviour //플레이어의 낚시 레벨, 사용하는 미끼 찌, 낚싯대 등에 따라 낚시 박스의 크기가 커진다.
{
    public int fishID = 0;
    public int fishGrade;
    PlayerController pCon;

    private void Awake()
    {
        pCon = GetComponent<PlayerController>();
    }

    float passedTime;
    float baitTime;
    private void Update()
    {
        if (pCon.waitingForBait)
        { Baiting(); }
    }

    private void Baiting()
    {
        passedTime += Time.deltaTime;
        float R = Random.Range(1f, 10f);
        if (passedTime > R)
        {
            baitTime += Time.deltaTime;
            if (baitTime <= 0.5f && Input.GetMouseButtonDown(0))
            {
                Debug.Log("물었다!");
                FishingMiniGame();
            }
            else if (baitTime > 0.5f && Input.GetMouseButtonDown(0))
            {
                Debug.Log("놓쳐버렸다.");
                pCon.WaitingForBait(false);
                pCon.Motion(true);
            }
        }
        else if (passedTime <= R && Input.GetMouseButtonDown(0))
        {
            Debug.Log("아직 물지 않았다.");
            pCon.WaitingForBait(false);
            pCon.Motion(true);
        }
    }

    public void FishingMiniGame()
    {
        pCon.WaitingForBait(false);
        pCon.Minigame(true);

        this.fishID = GetComponent<PlayerFishingManager>().GiveFishtoPlayerFishMiniGame();
        //Time.timeScale = 0f;
        Instantiate((GameObject)Resources.Load($"Prefabs/FishingPrefabs/FishingMiniGame"), this.transform.position, Quaternion.identity).transform.parent = this.transform;
        GetComponentInChildren<FishingMiniGame>().fishID = this.fishID;
    }
    public void EndFishing()
    {
        pCon.Minigame(false);
        pCon.Motion(true);
    }
}