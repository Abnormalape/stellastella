using System;
using System.Net.Sockets;
using UnityEngine;

class FishCatch : MonoBehaviour
{
    public float catched;
    private void Update()
    {
        if (catched > 100f)
        {
            GetComponentInParent<FishingMiniGame>().GetFish();
        }
    }
}