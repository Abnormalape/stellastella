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
    float toolLag; // �������� ��Ÿ���� �޶� �ܺο��� �޾ƿ;� �ϳ� �ӽ÷� ���⿡ �д�
    public bool toolUsing = false; // ���� ��Ÿ������ �����ϴ� �Ŷ� ����� �ֵ� �ȴ�.
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
        PlayerInventroy = this.GetComponent<PlayerInventroy>(); // �� ���� ������Ʈ�� ���� player�κ��丮�� ����
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

        Debug.Log(PlayerInventroy.currentInventoryItem); // ���������� �ҷ�����
    }

    void Walk()
    {
        if(Input.GetKey(KeyCode.LeftShift)) {speed = 3f;}
        if (Input.GetKeyUp(KeyCode.LeftShift)) {speed = 5f;}
    }
    
    void UseTool()  // ��Ŭ���� ���� ���� -> ��Ŭ���� ��Ŭ�� ���̿��� ���� �־�� �Ѵ�. && ��Ŭ���ߴٸ� �������� ���߾�� �Ѵ�.
                    // �� ������ ���� ��Ŭ�� ����� �ٸ���(���Ѹ���, ���˴�). ������ ���� ��Ŭ���ϸ� ��Ǿ��� ������ �ɴ´�. ����� ���� ���⸶�� �ٸ���. 
                    // �տ� �� �������� �����ϰ� �� ����� ���� DB��� ��� ���� �ҷ��´�.
                    // �Ʒ� ���˴븦 �����ؼ�, ����, ���, ����, �� � ��ȭ�Ѵ�.
    {
        if(Input.GetMouseButtonDown(0) && toolUsing == false && touchObject) //������Ʈ ���̾� ��ü�� ������ ���¿��� ��Ŭ���� �Ѵٸ�
        {
            //������ ��ü�� ���¸� ��ȭ ��Ų��
            toolUsing = true;
        }
        else if (Input.GetMouseButtonDown(0) && toolUsing == false) // �ܼ��� Ŭ���� �� ��� + ���� ��Ÿ���� �� ���
        {
            toolUsing = true;
        }

        if (toolUsing) // ���� Ŭ���� �ߴ�.
        {
            toolLag += Time.deltaTime;
            if(toolLag > 0.9f) { GetComponentInChildren<EdgeCollider2D>().enabled = true; }
            if (toolLag > 1f) // ���� Ŭ���� ���� 1�ʰ� �����ٸ�
            {
                
                toolUsing = false; // �ٽ� Ŭ�� �Ҽ� �ְ� �ϰ�
                toolLag = 0f; // ��Ÿ���� �ʱ�ȭ�Ѵ�.
            }
        }
        if (toolLag == 0f) { GetComponentInChildren<EdgeCollider2D>().enabled = false;}
    }
    
    void Facing(float x, float y) // ����ٶ󺸴� ���� = ��ȣ�ۿ����,��������Ʈ����
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

    void Fishing()  // �̰� �̺�Ʈȭ �� �� ������ �غ���
                    // ���ô� �� ������Ʈ�� �÷��̾������Ʈ�� ��ȣ�ۿ��̴�.
    {
        float chargeTime = 0;
        // float �����ð� = 0f;
        bool charge = false;
        bool rodThrow = false;
        // bool ���� = false;
        //���˴� �̸鼭 ���ʹ�ư�������� �ִٸ�
        if (currentItemDB.toolType == 9 && Input.GetMouseButton(0) && !rodThrow) //�÷��̾� �κ��丮���� ID�� �޾ƿͼ� ���⼭ ItemDB�� ȣ���ؼ� ���� �κ��丮 �������� ������ �޾ƿͼ�, ����
        {
            charge = true; // ��¡���·� �����.
            if (chargeTime >= 0f)
            {
                chargeTime += Time.deltaTime;
            } // ��¡�ð��� �þ�� // �ִ�ġ�� �Ǹ� �پ���. �ݺ��Ѵ�
        }
        if (charge && Input.GetMouseButtonUp(0))
        {
            chargeTime = 0;
            charge = false;
            rodThrow = true;    // ���˴븦 �����ٴ� ������ ���� �޾ƾ� �ұ�? -> ��object player�� �ڽ� ������Ʈ�� ��¡ �ð��� ����Ͽ� facing �������� ��������
                                // � stop�� ��ġ�� �ִ� �� ������Ʈ�� �÷��̾ ��ȣ�ۿ� �ϴ� ���°� �ȴ�.
                                // � stop�ϸ� ���׹̳ʸ� �Ҹ��Ѵ�.
                                // ���� ���ǿ� ���� ������ ����� ������ �÷��̾�� �����Ѵ�.
                                // ���� ���� �÷��̾�� ��� ���°� �ȴ�.
        }
        // charge�϶��� rodThrow�϶��� �ٸ� ��ȣ�ۿ��� ��� ������.
        //
        //
        //
        //if (rodThrow && getbite == true) // � ������ ���¿��� �����ͼ� �� ������Ʈ�� ���� ��ȭ��Ų�ٸ�
        //{ 
        //  bite = true;
        //  �����ð� += Time.deltaTime
        //  if (�����ð� > 0.5f)  // �������� 0.5�ʰ� ������ ��⸦ ��ģ��.
        //  {
        //  bite = false;
        //  }
        //
        //  if (input.GetMouseButtonDown(0)) // � ������ ���·�, �����϶� ��Ŭ���� �Ѵٸ�
        //  {
        //      //���ù̴ϰ���.active = true;
        //      //���� �̴ϰ����� �����Ѵ�.
        //  }
        //}
        //else if (rodThrow && input.GetmouseButtonDown(0)){ // � ������ ���·� ������ �ƴҶ� ��Ŭ���� �Ѵٸ�.
        //  //����� ����ϰ� ���� ���·� ����
        //}
        //
        //if(���ù̴ϰ���.���� == true) //���� �̴ϰ����� ������ true�� �شٸ� 
        //{ 
        //  // �÷��̾�� ���� ����ġ�� �ο�
        //  // ���ù̴ϰ���.active = false; // �̴ϰ��� â�� ����
        //  // rodThrow = false; // � ȸ��
        //  // ���ü������� = true; // �������°� true;
        //} 
        //else
        //{ �׷��� �ʴٸ� �ƹ��͵� ��ȯ���� �ʰ� �̴ϰ����� ����
        //  ���ù̴ϰ���.active = false; // �̴ϰ��� â�� ����
        //  //rodThrow = false; // � ȸ��
        //}
        //
        //if (���ü������� == true)
        //{
        //  // for���� �� �κ��丮�� �Ⱦ null�� ��ȯ�Ǹ� Ż���ϰ� �ش� �κ��丮�� "��"�� ���� ����� ������ �Ҵ� -> �ڵ����� ���� ���� �κ��丮�� �Ҵ�
        //  // ���� null�� �κ��丮�� ���ٸ�, ���� �κ��丮�� ���� ��ȣ�ۿ�
        //  // ���� ���� ���˴��� �̳� ���� -1
        //  // ���� ���� ���˴��� ������ -1
        //}
        //

        
        
    }
}
