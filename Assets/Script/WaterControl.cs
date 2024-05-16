using Unity;
using System;
using UnityEngine;
using Unity.VisualScripting;

class WaterControl : MonoBehaviour
{/* 나중에 다시 돌아오마
    GameObject bobber;
    PlayerInventroy fishingrod;
    PlayerController playerController;
    System.Random bite; // 아오 슈발 모호한 참조
    int biteLimit;
    int biteReal;
    float passedTime = 0;
    int fishID;
    private void OnTriggerEnter2D(Collider2D collision) // bobber(trigger)에 접촉한다면, 어차피 물은 낚시찌 말고는 접촉할 물체가 없다....?
    {
        if (collision.gameObject.tag == "bobber") // 낚시찌와 접촉했다면
        {
            fishingStart(collision); // 낚시를 실행
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bobber") // 낚시찌와 접촉중이라면
        {
            passedTime += Time.deltaTime; // 시간경과를 체크하고
            
            if(passedTime*100 > biteReal) // 시간경과가 입질시간보다 커지면 입질을 플레이어에게 전송한다.
            {
                playerController.getbite = true;
            }
        }
    }
    void fishingStart(Collider2D collision)
    {
        bobber = collision.gameObject; // 부딪힌 녀석을 게임오브젝트로 부른다.
        fishingrod = bobber.GetComponentInParent<PlayerInventroy>(); // 부딪힌 녀석의 부모오브젝트에서 playerinventory를 호출 => 어차피 낚싯대
        playerController = collision.GetComponent<PlayerController>();
        // playerinventory에 저장된 미끼, 찌 등을 고려해서 bite를 구성한다.
        biteLimit = 10; // 기본으로 10초에서 조건에 따라 좀 줄인다.
        biteReal = bite.Next(20, 100 * biteLimit);
    }

    void JudgeFish()    // 물고기 판정 : 현재 날씨, 계절, 시간, 육지로부터의 거리 에 영향을 받는다. 
                        // switch문 돌려서 날씨 계절 시간 판정하고 육지부터 거리 판정해서 일정 이상일때만 특정 물고기, 멀리 던질수록 높은 등급나오게
    {
        
    }*/
}