using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public int maxStamina = 200;
    public int maxHp = 200;
    public int currentStamina = 200;
    public int currentHp = 200;

    public bool exhaust;
    public bool dead;

    public int farmLevel = 1;

    PlayerInventroy pInven;

    private void Awake()
    {
        currentHp = maxHp;
        currentStamina = maxStamina;
        pInven = GetComponent<PlayerInventroy>();
        inventoryBarUI = transform.Find("InventoryBarUI").gameObject;
        inventoryUI = transform.Find("InventoryUI").gameObject;
        inventoryUI.SetActive(false);
        inventoryBarUI.SetActive(true);
    }

    void Start()
    {

    }


    void Update()
    {
        MakeIdleState();
        CurrentMax(currentHp, maxHp);
        CurrentMax(currentStamina, maxStamina);
        InvenToryButton();
        //if (idle)
        //{
        //    Debug.Log("idle");
        //}
        //if (moving)
        //{
        //    Debug.Log("moving");
        //}
        //if (conversation)
        //{
        //    Debug.Log("conversation");
        //}
        //if (charge)
        //{
        //    Debug.Log("charge");
        //}
        //if (fishCharge)
        //{
        //    Debug.Log("fishCharge");
        //}
        //if (minigame)
        //{
        //    Debug.Log("minigame");
        //}
        //if (waitingForBait)
        //{
        //    Debug.Log("waitingForBait");
        //}
        //if (inventory)
        //{
        //    Debug.Log("inventory");
        //}
        //if (motion)
        //{
        //    Debug.Log("motion");
        //}
    }

    void CurrentMax(int current, int max)
    {
        if (current > max)
        { current = max; }
    }



    void Exhaust()
    {
        if (currentStamina <= 10 && !exhaust) { }
        // 탈진상태가 아닌 상태로 스테미너가 10 이하가 된다면 => 탈진상태를 경고
        if (currentStamina < 0 && !exhaust) { currentStamina = 0; exhaust = true; }
        // 탈진이 아닌 상태에서 스테미너가 0미만이 된다면 0이하로 스테미너가 떨어지지 않게하고, 스테미나를 0으로 함, 탈진상태가 됨

        if (currentStamina < -10) { }
        // 탈진 상태일때만 스테미너가 음수가 되는데, 이때 일정 수치가 되면, GAMEMANAGER 에 일자종료를 전달
    }

    void ExhaustState() // 탈진상태란??
    {
    }


    public bool idle { get; private set; }

    public bool conversation { get; private set; }
    public void Conversation(bool i) { conversation = i; }
    public bool motion { get; private set; }
    public void Motion(bool i) { motion = i; }
    public bool moving { get; private set; }
    public void Moving(bool i) { moving = i; }
    public bool waitingForBait { get; private set; }
    public void WaitingForBait(bool i) { waitingForBait = i; }
    public bool fishCharge { get; private set; }
    public void FishCharge(bool i) { fishCharge = i; }
    public bool charge { get; private set; }
    public void Charge(bool i) { charge = i; }
    public bool minigame { get; private set; }
    public void Minigame(bool i) { minigame = i; }
    public bool inventory { get; private set; }
    public void Inventroy(bool i) { inventory = i; }

    private void MakeIdleState()
    {   // 본인의 상태를 잘 끄기만 한다면 상태는 항상 Idle로 돌아온다.
        if (conversation || motion || moving || waitingForBait || fishCharge || charge || minigame || inventory)
        { idle = false; }
        else { idle = true; }
    }

    GameObject inventoryUI;
    GameObject inventoryBarUI;


    private void InvenToryButton()
    {
        if (idle || moving || inventory)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!inventory)
                {
                    inventoryUI.SetActive(true);
                    inventoryBarUI.SetActive(false);
                    inventory = true;
                }
                else if (inventory)
                {
                    inventoryUI.SetActive(false);
                    inventoryBarUI.SetActive(true);
                    inventory = false;
                }
            }
        }
    }
}
