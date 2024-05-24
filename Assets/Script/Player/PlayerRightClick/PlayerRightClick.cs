﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Unity;
using Unity.VisualScripting;
using UnityEngine;

class PlayerRightClick : MonoBehaviour
// 얘를 플레이어 컨트롤에 넣고, 툴타입을 받아온다
{                       // 툴 타입에 맞는 메서드를 실행한다
                        // 그냥 얘 자체를 update에 실행해도 되는것 아닌가?
    PlayerController pCon;
    PlayerMovement pMov;
    PlayerInventroy pInven;
    ItemDB currentData;
    GameObject HitBox;

    private void Awake()
    {
        pCon = this.gameObject.GetComponent<PlayerController>();
        pMov = this.gameObject.GetComponent<PlayerMovement>();
        pInven = this.gameObject.GetComponent<PlayerInventroy>();
        HitBox = GameObject.Find("RightClickHitBox");
        HitBox.GetComponent<BoxCollider2D>().enabled = false; // 콜라이더를 끄거나 자체를 숨기거나 스프라이트를 뽑아버리거나.
        HitBox.transform.localScale = new Vector2(1.3f, 1.3f);
    }
    private void Update()
    {
        currentData = new ItemDB(pInven.currentInventoryItem);
        ColliderOnOff();
        ColliderLocation();
        ColliderSize();
    }

    void ColliderLocation()
    {
        if (currentData.type == "Seed")
        {
            HitBox.transform.position = GameObject.Find("LeftClickHitBox").transform.position;
        }
        else
        {
            HitBox.transform.position = this.transform.position + (Vector3)pMov.nowFacing * (0.5f);
        }
    }
    void ColliderSize()
    {   //콜라이더 크기 변경
        if (currentData.type == "Seed")
        {
            HitBox.transform.localScale = new Vector2(0.8f, 0.8f);
        }
        else
        {
            HitBox.transform.localScale = new Vector2(1.3f, 1.3f);
        }
    }
    void ColliderOnOff()
    {   //콜라이더 키고 끄기
        currentData = new ItemDB(pInven.currentInventoryItem);

        if (Input.GetMouseButton(1) || Input.GetMouseButtonDown(1))
        {
            HitBox.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            HitBox.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}