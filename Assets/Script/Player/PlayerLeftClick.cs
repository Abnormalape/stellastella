using System;
using Unity;
using Unity.VisualScripting;
using UnityEngine;

class PlayerLeftClick : MonoBehaviour
// �긦 �÷��̾� ��Ʈ�ѿ� �ְ�, ��Ÿ���� �޾ƿ´�
{                       // �� Ÿ�Կ� �´� �޼��带 �����Ѵ�
                        // �׳� �� ��ü�� update�� �����ص� �Ǵ°� �ƴѰ�?

    PlayerController pCon;
    PlayerMovement pMov;
    PlayerInventroy pInven;
    ItemDB currentData;
    float coolDownTime = 0.5f;
    float passedTime = 0f;
    float chargeTime = 0f;
    float throwPower = 0f;
    public bool toolUsed;
    bool onCharge = false;
    private void Awake()
    {
        pCon = this.gameObject.GetComponent<PlayerController>();
        pMov = this.gameObject.GetComponent<PlayerMovement>();
        pInven = this.gameObject.GetComponent<PlayerInventroy>();
    }
    private void Update()
    {
        currentData = new ItemDB(pInven.currentInventoryItem);

        if (!toolUsed)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            {
                switch (currentData.toolType)
                {
                    case 1:
                        UseAxe();
                        return;
                    case 2:
                        UseHoe();
                        return;
                    case 3:
                        UseWaterCan();
                        return;
                    case 4:
                        UsePickAxe();
                        return;
                    case 5:
                        UseSickle();
                        return;
                    case 9:
                        UseFishingRod();
                        return;
                }
            }
        }

        if (toolUsed)
        {
            passedTime += Time.deltaTime;
        }
        CoolDown();

        if (onCharge && Input.GetMouseButtonUp(0))
        {
            MakeChargeColliderAct();
        }
    }
    // Ű�� �������� ���� => ���� ���Ѹ��� ���ô�
    // Ű�� ������ ������ ��¡
    // Ű�� �������� ����

    void UseAxe() // �Ϸ�
    {
        MakeCollider();
        StopPlayer();
        StaminaUse();
    }
    //n���� �ٶ󺸴� ���⿡ �ݶ��̴� ���� �� ����, �ֵθ��� ���� �ٸ� ������ �Ұ�, ���׹̳� �Ҹ�
    void UseHoe() // �̿� : boxcollider
    {
        if (currentData.grade == 1)
        {
            StopPlayer();
            StaminaUse();
            MakeCollider();
        }
        else if (currentData.grade > 1)
        {
            MakeChargeColliderCharge();
        }
    }
    //charge�� �� ��ġ�� �������� ���濡 �ټ��� �ݶ��̴� ���� �� ����,n���� �ٶ󺸴� ���⿡ �ݶ��̴� ���� �� ����,
    //��¡�� ������ ����, ����� �ٸ� ������ �Ұ�, ���׹̳� �Ҹ�
    void UseWaterCan() // �̿� : boxcollider
    {
        if (currentData.grade == 1)
        {
            StopPlayer();
            StaminaUse();
            MakeCollider();
        }
        else if (currentData.grade > 1)
        {
            MakeChargeColliderCharge();
        }
    }
    //charge�� �� ��ġ�� �������� ���濡 �ټ��� �ݶ��̴� ���� �� ����,n���� �ٶ󺸴� ���⿡ �ݶ��̴� ���� �� ����,
    //��¡�� ������ ����, ����� �ٸ� ������ �Ұ�, ���׹̳� �Ҹ�
    //�� ä��� ����
    void UsePickAxe() // �Ϸ�
    {
        MakeCollider();
        StopPlayer();
        StaminaUse();
    }
    //n�ʵ��� �����̴� �ݶ��̴� ���� ���� ����, �ֵθ��� ���� �ٸ� ������ �Ұ�, ���׹̳� �Ҹ�
    void UseSickle()
    {
        StopPlayer();
    }
    //n���� �ٶ󺸴� ���⿡ �ݶ��̴� ���� �� ����, �ֵθ��� ���� �ٸ� ������ �Ұ�
    void UseFishingRod()
    {

    }
    //charge�� ���濡 �ݶ��̴� ����, 

    void StaminaUse() { } // �÷��̾� ��Ʈ�ѿ� ü�� ���׹̳� ����

    void StopPlayer() { toolUsed = true; } // toolUsed�� true�϶� �������Ұ�, �̵��Ұ�, ���

    void MakeCollider() //�����ݶ��̴� Ŵ, ��ġ, ȸ���� movement����
    { this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = true; }

    void MakeChargeColliderCharge() //������ �ڽ� �ݶ��̴��� ����, �ʿ�� ũ�� ��ġ ����
    { // ����� 2�̻��̸� Ŭ���ϰų� ������ ������, ��¡ �ð��� ���Ǹ�, ������ ���ǿ� �´� ũ���� �ݶ��̴��� �����ȴ�.
        if (Input.GetMouseButton(0)) // ������ �ִµ��� �Ʒ��� ����
        {
            this.gameObject.GetComponent<PlayerMovement>().chargeMove = true;// �������� ��¡���¸� true�� ����
            chargeTime += Time.deltaTime;
            onCharge = true;
        }

    } // ������ �ڽ��ݶ��̴�

    void MakeChargeColliderAct()
    {
        if (chargeTime < 1f)
        {
            MakeCollider();
        }
        else if (chargeTime >= 1f && currentData.grade >= 2) //1�ܰ� ����, ��� 2 �̻�� 3ĭ
        {
            //�ݶ��̴� ũ�� ���� -> getchildcomponent<boxcollider>.size
            //�ݶ��̴� ��ġ ���� -> transform.position
        }
        else if (chargeTime >= 2f && currentData.grade >= 3) //2�ܰ� ���� 5ĭ
        {

        }
        else if (chargeTime >= 3f && currentData.grade >= 4) //3�ܰ� ���� 9ĭ
        {

        }
        else if (chargeTime >= 4f && currentData.grade >= 5) //4�ܰ� ���� 18ĭ
        {

        }
        StaminaUse();
        StopPlayer();
        chargeTime = 0f;
        this.gameObject.GetComponent<PlayerMovement>().chargeMove = false;
        //this.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true; // ��� �ݶ��̴��� Ų��. �ٵ� ������ �ϴ� ������.
    }

    void MakeThrowColliderCharge()
    {

    }

    void MakeThrowColliderAct()
    {

    }
    void MakeMovingCollider() // �̰� ������ �𸣰ڴ�
    {
        this.gameObject.GetComponentInChildren<EdgeCollider2D>().enabled = true;
    }

    void WaterLevel()
    {

    }

    void CoolDown()
    {
        if (passedTime > coolDownTime)
        {
            toolUsed = false;
            passedTime = 0;
        }
    }
}
