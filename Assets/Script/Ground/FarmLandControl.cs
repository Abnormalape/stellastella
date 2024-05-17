using System;
using System.Threading;
using Unity;
using Unity.Mathematics;
using UnityEngine;

public class FarmLandControl : MonoBehaviour // 경작지 프리팹에 들어가는 클래스, 이미 갈린상태
{
    [SerializeField] GameManager gameManager;
    ItemDB itemDB;
    int currentDate;
    bool watered = false;
    bool seeded = false;
    bool scareCrow = false;

    private void OnEnable()
    {
        currentDate = gameManager.currentDay;    
    }

    private void Update()
    {
        if (gameManager.weather == 2) //새로운 날이 시작 할때 2번날씨( = 비 )라면
        {
            watered = true;
        }

        if(!seeded && currentDate!=gameManager.currentDay) // 아무것도 심어지지 않은 상황에 날이 바뀌었다면.
        {   //확률적으로 자신을 파괴한다.
            Destroy(this.gameObject);
        }
        else if (seeded && !scareCrow) // 씨앗은 심었는데 허수아비가 없다면.
        {   //확률적으로 자식오브젝트를 파괴한다.
            Destroy(this.gameObject.GetComponentInChildren<Transform>());
        }
        else if(seeded && watered && currentDate!=gameManager.currentDay) // 심어진 상태로 물이 뿌려진 상태로 날이 바뀌었다면.
        {   //자식오브젝트의 성장단계를 +1한다.
            // gameManager.GetComponentInChildren<seedGrowth>().growth++;
            watered = false; // 물뿌림을 초기화 한다.
        }   // 심어진 상태로 물이 뿌려지지 않았다면... 냅둔다

    }

    private void OnTriggerEnter2D(Collider2D collision) // 플레이어가 "씨앗"을 들고 "우클릭"시 생성되는 트리거에 접촉했을때
    {
        itemDB = new ItemDB(collision.gameObject.GetComponentInParent<PlayerInventroy>().currentInventoryItem);
        if (itemDB.type == "씨앗")
        {
            collision.gameObject.GetComponentInParent<PlayerInventroy>().itemCount--; // 갯수를 하나 줄이고.
            Instantiate(gameObject,this.transform.position,Quaternion.identity).transform.parent = this.transform; //내가 가진 아이템 ID와 맞는 프리팹을 만들어서 그놈의 부모를 나로 만들어라.
        }
        else { return; }
    }
}