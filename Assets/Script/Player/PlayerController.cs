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
    }

    void Start()
    {
    
    }

    
    void Update()
    {
        CurrentMax(currentHp,maxHp);
        CurrentMax(currentStamina, maxStamina);
    }

    void CurrentMax(int current, int max)
    {
        if(current > max)
        {current = max;}
    }
    void Exhaust()
    {
        if (currentStamina <= 10 && !exhaust) {  } 
        // Ż�����°� �ƴ� ���·� ���׹̳ʰ� 10 ���ϰ� �ȴٸ� => Ż�����¸� ���
        if (currentStamina < 0 && !exhaust) {currentStamina = 0; exhaust = true; }
        // Ż���� �ƴ� ���¿��� ���׹̳ʰ� 0�̸��� �ȴٸ� 0���Ϸ� ���׹̳ʰ� �������� �ʰ��ϰ�, ���׹̳��� 0���� ��, Ż�����°� ��

        if (currentStamina < -10) { }
        // Ż�� �����϶��� ���׹̳ʰ� ������ �Ǵµ�, �̶� ���� ��ġ�� �Ǹ�, GAMEMANAGER �� �������Ḧ ����
    }

    void ExhaustState() // Ż�����¶�??
    {
        // �̵��ӵ��� ���� �پ���
        // ������ ���׹̳� ȸ���� �ǿ����� ��ħ
        // ���׹̳��� 0 �̻��� �Ǹ� �̵��ӵ� �г�Ƽ�� �����
    }

    
}
