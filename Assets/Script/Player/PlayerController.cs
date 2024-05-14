using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    float faceX;
    float faceY;
    float toolLag; // 도구별로 쿨타임이 달라서 외부에서 받아와야 하나 임시로 여기에 둔다
    public bool toolUsing = false; // 현재 쿨타임인지 판정하는 거라 여기다 둬도 된다.
    bool touchObject;
    public bool getbite = false;
    Rigidbody2D rb;
    public GameObject facing;
    [SerializeField]
    LayerMask objectLayer;
    PlayerInventroy PlayerInventroy;
    ItemDB currentItemDB;
    PlayerImportantItem PlayerImportantItem;
    PlayerLeftClick LeftClick;

    public int stamina;
    public int hp;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        faceX = 0;
        faceY = -1;
        toolLag = 0f;
        touchObject = false;
        PlayerInventroy = this.GetComponent<PlayerInventroy>(); // 이 게임 오브젝트가 가진 player인벤토리를 참고
    }

    
    void Update()
    {
        


        touchObject = Physics2D.Linecast(this.transform.position, new Vector2(this.transform.position.x + faceX * 0.7f, this.transform.position.y + faceY * 0.7f), objectLayer);
        Walk();   
        float x;
        float y;
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (!toolUsing){rb.velocity = new Vector3(x * speed, y * speed, y * speed);}
        else if (toolUsing) { rb.velocity = Vector3.zero; }
        this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.y);
        UseTool();
        Facing(x,y);
        if (!toolUsing) { facing.transform.position = new Vector2(this.transform.position.x + faceX * 0.7f, this.transform.position.y + faceY * 0.7f); }
        else if (toolUsing) { facing.transform.position = facing.transform.position; }

        currentItemDB = new ItemDB(PlayerInventroy.currentInventoryItem);

        Debug.Log(PlayerInventroy.currentInventoryItem); // 정상적으로 불러와짐
    }

    void Walk()
    {
        if(Input.GetKey(KeyCode.LeftShift)) {speed = 3f;}
        if (Input.GetKeyUp(KeyCode.LeftShift)) {speed = 5f;}
    }
    
    void UseTool()  // 좌클릭에 대한 반응 -> 좌클릭과 좌클릭 사이에는 텀이 있어야 한다. && 좌클릭했다면 움직임이 멈추어야 한다.
                    // 각 도구에 따라 좌클릭 모션이 다르다(물뿌리개, 낚싯대). 씨앗의 경우는 좌클릭하면 모션없이 씨앗을 심는다. 무기는 텀이 무기마다 다르다. 
                    // 손에 든 아이템을 판정하고 그 결과에 따라 DB등에서 모션 등을 불러온다.
                    // 아래 낚싯대를 참고해서, 도끼, 곡괭이, 괭이, 낫 등를 분화한다.
    {
        if(Input.GetMouseButtonDown(0) && toolUsing == false && touchObject) //오브젝트 레이어 물체와 접촉한 상태에서 좌클릭을 한다면
        {
            //접촉한 물체의 상태를 변화 시킨다
            toolUsing = true;
        }
        else if (Input.GetMouseButtonDown(0) && toolUsing == false) // 단순히 클릭만 한 경우 + 도구 쿨타임이 돈 경우
        {
            toolUsing = true;
        }

        if (toolUsing) // 만약 클릭을 했다.
        {
            toolLag += Time.deltaTime;
            if(toolLag > 0.9f) { GetComponentInChildren<EdgeCollider2D>().enabled = true; }
            if (toolLag > 1f) // 만약 클릭을 한지 1초가 지났다면
            {
                
                toolUsing = false; // 다시 클릭 할수 있게 하고
                toolLag = 0f; // 쿨타임을 초기화한다.
            }
        }
        if (toolLag == 0f) { GetComponentInChildren<EdgeCollider2D>().enabled = false;}
    }
    
    void Facing(float x, float y) // 현재바라보는 방향 = 상호작용방향,스프라이트방향
    {
        if (MathF.Abs(x) > 0)
        {
            faceX = x;
            faceY = 0;
        }
        else if (MathF.Abs(y) > 0)
        {
            faceX = 0;
            faceY = y;
        }
    }

    void Fishing()  // 이걸 이벤트화 할 수 있으면 해보자
                    // 낚시는 물 오브젝트와 플레이어오브젝트의 상호작용이다.
    {
        float chargeTime = 0;
        // float 입질시간 = 0f;
        bool charge = false;
        bool rodThrow = false;
        // bool 입질 = false;
        //낚싯대 이면서 왼쪽버튼을누르고 있다면
        if (currentItemDB.toolType == 9 && Input.GetMouseButton(0) && !rodThrow) //플레이어 인벤토리에서 ID를 받아와서 여기서 ItemDB를 호출해서 현재 인벤토리 아이템의 정보를 받아와서, 판정
        {
            charge = true; // 차징상태로 만든다.
            if (chargeTime >= 0f)
            {
                chargeTime += Time.deltaTime;
            } // 차징시간이 늘어난다 // 최대치가 되면 줄어든다. 반복한다
        }
        if (charge && Input.GetMouseButtonUp(0))
        {
            chargeTime = 0;
            charge = false;
            rodThrow = true;    // 낚싯대를 던졌다는 정보를 누가 받아야 할까? -> 찌object player의 자식 오브젝트로 차징 시간에 비례하여 facing 방향으로 던져지고
                                // 찌가 stop한 위치에 있는 물 오브젝트와 플레이어가 상호작용 하는 상태가 된다.
                                // 찌가 stop하면 스테미너를 소모한다.
                                // 물은 조건에 따라 입질과 물고기 정보를 플레이어에게 전달한다.
                                // 그전 까지 플레이어는 대기 상태가 된다.
        }
        // charge일때와 rodThrow일때는 다른 상호작용이 모두 꺼진다.
        //
        //
        //
        //if (rodThrow && getbite == true) // 찌가 던져진 상태에서 입질와서 물 오브젝트가 나를 변화시킨다면
        //{ 
        //  bite = true;
        //  입질시간 += Time.deltaTime
        //  if (입질시간 > 0.5f)  // 입질한지 0.5초가 지나면 고기를 놓친다.
        //  {
        //  bite = false;
        //  }
        //
        //  if (input.GetMouseButtonDown(0)) // 찌가 던져진 상태로, 입질일때 좌클릭을 한다면
        //  {
        //      //낚시미니게임.active = true;
        //      //낚시 미니게임을 시작한다.
        //  }
        //}
        //else if (rodThrow && input.GetmouseButtonDown(0)){ // 찌가 던져진 상태로 입질이 아닐때 좌클릭을 한다면.
        //  //모션을 출력하고 기초 상태로 복귀
        //}
        //
        //if(낚시미니게임.성공 == true) //낚시 미니게임이 나에게 true를 준다면 
        //{ 
        //  // 플레이어에게 낚시 경험치를 부여
        //  // 낚시미니게임.active = false; // 미니게임 창이 꺼짐
        //  // rodThrow = false; // 찌를 회수
        //  // 낚시성공상태 = true; // 성공상태가 true;
        //} 
        //else
        //{ 그렇지 않다면 아무것도 반환하지 않고 미니게임을 종료
        //  낚시미니게임.active = false; // 미니게임 창이 꺼짐
        //  //rodThrow = false; // 찌를 회수
        //}
        //
        //if (낚시성공상태 == true)
        //{
        //  // for문을 써 인벤토리를 훑어서 null이 반환되면 탈출하고 해당 인벤토리에 "물"이 가진 물고기 정보를 할당 -> 자동으로 가장 앞쪽 인벤토리에 할당
        //  // 만약 null인 인벤토리가 없다면, 낚시 인벤토리를 열어 상호작용
        //  // 내가 가진 낚싯대의 미끼 갯수 -1
        //  // 내가 가진 낚싯대찌 내구도 -1
        //}
        //

        
        
    }
}
