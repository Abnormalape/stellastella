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
        // Ż�����°� �ƴ� ���·� ���׹̳ʰ� 10 ���ϰ� �ȴٸ� => Ż�����¸� ���
        if (currentStamina < 0 && !exhaust) { currentStamina = 0; exhaust = true; }
        // Ż���� �ƴ� ���¿��� ���׹̳ʰ� 0�̸��� �ȴٸ� 0���Ϸ� ���׹̳ʰ� �������� �ʰ��ϰ�, ���׹̳��� 0���� ��, Ż�����°� ��

        if (currentStamina < -10) { }
        // Ż�� �����϶��� ���׹̳ʰ� ������ �Ǵµ�, �̶� ���� ��ġ�� �Ǹ�, GAMEMANAGER �� �������Ḧ ����
    }

    void ExhaustState() // Ż�����¶�??
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
    {   // ������ ���¸� �� ���⸸ �Ѵٸ� ���´� �׻� Idle�� ���ƿ´�.
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
