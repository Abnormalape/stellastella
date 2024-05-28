using System;
using Unity;
using UnityEngine;


class TimeNeedleControl : MonoBehaviour
{
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(0,0, 180f - ((3f/20f) * gameManager.dayTimePassed));
    }
}

