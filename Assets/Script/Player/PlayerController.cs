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
        // 탈진상태가 아닌 상태로 스테미너가 10 이하가 된다면 => 탈진상태를 경고
        if (currentStamina < 0 && !exhaust) {currentStamina = 0; exhaust = true; }
        // 탈진이 아닌 상태에서 스테미너가 0미만이 된다면 0이하로 스테미너가 떨어지지 않게하고, 스테미나를 0으로 함, 탈진상태가 됨

        if (currentStamina < -10) { }
        // 탈진 상태일때만 스테미너가 음수가 되는데, 이때 일정 수치가 되면, GAMEMANAGER 에 일자종료를 전달
    }

    void ExhaustState() // 탈진상태란??
    {
        // 이동속도가 대폭 줄어들고
        // 다음날 스테미너 회복에 악영향을 끼침
        // 스테미나가 0 이상이 되면 이동속도 패널티는 사라짐
    }

    
}
