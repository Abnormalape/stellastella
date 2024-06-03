using System;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.UI;

class PlayerMoney : MonoBehaviour
{
    Text moneytext;
    PlayerController pCon;
    int currentgold;
    private void Awake()
    {
        moneytext = GetComponentInChildren<Text>();
        pCon = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        currentgold = pCon.currentGold;
        moneytext.text = currentgold.ToString();
    }
}