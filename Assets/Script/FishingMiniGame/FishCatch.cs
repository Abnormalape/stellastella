using System;
using System.Net.Sockets;
using UnityEngine;

class FishCatch : MonoBehaviour
{
    float catched;
    SpriteRenderer fishCatch;


    private void OnEnable()
    {
        fishCatch = GetComponent<SpriteRenderer>();
        catched = 8f;
    }

    private void Update()
    {
        CurrentCatch();
        CatchContorl();
    }

    private void CatichngFish()
    {
        catched = catched + Time.unscaledDeltaTime * 3;
    }
    private void LoosingFish()
    {
        catched = catched - Time.unscaledDeltaTime * 4; // timescale 이 0일때 쓰려고 unscaled 넣었는데, 문제가 좀 있다.
    }

    bool catching = false;
    public void IsCatching()
    {
        catching = true;
    }
    public void NotCatching()
    {
        catching = false;
    }

    private void CurrentCatch()
    {
        if (catching)
        {
            CatichngFish();
        }
        else
        {
            LoosingFish();
        }
    }
    private void CatchContorl()
    {
        if (catched >= 0f && catched <= 24f)
        {
            transform.localScale = new Vector3(transform.localScale.x, catched, transform.localScale.z);
            fishCatch.color = new Color(1f, (catched / 12f), (catched / 124f));
        }
        else if (catched > 24f)
        {
            catched = 24f;
            FishEnd(true);
        }
        else if (catched < 0f)
        {
            catched = 0f;
            FishEnd(false);
        }
    }

    void FishEnd(bool FF)
    {
        GetComponentInParent<FishingMiniGame>().FishingEnd(FF);
    }
}