﻿using UnityEngine;
using Random = UnityEngine.Random;
class PlayerFishingMinigame : MonoBehaviour //플레이어의 낚시 레벨, 사용하는 미끼 찌, 낚싯대 등에 따라 낚시 박스의 크기가 커진다.
{
    public int fishID = 0;
    public int fishGrade;
    PlayerController pCon;

    [SerializeField] GameObject bite;
    [SerializeField] GameObject caught;

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

    bool annbait;
    void AnnounceBait()
    {
        Instantiate(bite, this.transform.position, Quaternion.identity);
    }
    bool anncaught;
    void AnnounceCaught()
    {
        Instantiate(caught, this.transform.position, Quaternion.identity);
    }

    bool makeR = false;
    float R;
    private void Baiting()
    {
        if (makeR == false)
        {
            R = Random.Range(1f, 10f);
            makeR = true;
        }
        passedTime += Time.deltaTime;

        if (passedTime > R)
        {
            if (!annbait)
            {
                
                AnnounceBait();
                annbait = true;
            }


            baitTime += Time.deltaTime;
            if (baitTime <= 1f && Input.GetMouseButtonDown(0))
            {
                if (!anncaught)
                {
                    AnnounceCaught();
                    anncaught = true;
                }


                passedTime = 0;
                baitTime = 0;

                makeR = false;
                Invoke("FishingMiniGame",0.5f);
                //FishingMiniGame();
            }
            else if (baitTime > 1f && Input.GetMouseButtonDown(0))
            {
                Debug.Log("놓쳐버렸다.");
                passedTime = 0;
                baitTime = 0;

                makeR = false;
                pCon.Motion(true);
                pCon.WaitingForBait(false);
                annbait = false;
                anncaught = false;
            }
        }
        else if (passedTime <= R && Input.GetMouseButtonDown(0))
        {
            Debug.Log("아직 물지 않았다.");
            passedTime = 0;
            baitTime = 0;

            makeR = false;
            pCon.Motion(true);
            pCon.WaitingForBait(false);
            annbait = false;
            anncaught = false;
        }
    }

    public void FishingMiniGame()
    {
        pCon.Minigame(true);
        pCon.WaitingForBait(false);


        this.fishID = GetComponent<PlayerFishingManager>().GiveFishtoPlayerFishMiniGame();
        //Time.timeScale = 0f;
        Instantiate((GameObject)Resources.Load($"Prefabs/FishingPrefabs/FishingMiniGame"), this.transform.position, Quaternion.identity).transform.parent = this.transform;
        GetComponentInChildren<FishingMiniGame>().fishID = this.fishID;
    }
    public void EndFishing()
    {
        Destroy(GetComponentInChildren<FishingMiniGame>().gameObject);

        anncaught = false;
        annbait = false;

        pCon.Minigame(false);
        pCon.Motion(true);
    }
}