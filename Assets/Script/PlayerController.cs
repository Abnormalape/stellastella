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
    bool doToolLag; // ���� ��Ÿ������ �����ϴ� �Ŷ� ����� �ֵ� �ȴ�.
    bool touchObject;
    Rigidbody2D rb;
    public GameObject facing;
    [SerializeField]
    LayerMask objectLayer;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        faceX = 0;
        faceY = -1;
        toolLag = 0f;
        doToolLag = false;
        touchObject = false;
    }

    
    void Update()
    {
        touchObject = Physics2D.Linecast(this.transform.position, new Vector2(this.transform.position.x + faceX * 0.7f, this.transform.position.y + faceY * 0.7f), objectLayer);
        Walk();   
        float x;
        float y;
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (!doToolLag){rb.velocity = new Vector3(x * speed, y * speed, y * speed);}
        else if (doToolLag) { rb.velocity = Vector3.zero; }
        this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.y);
        UseTool();
        Facing(x,y);
        if (!doToolLag) { facing.transform.position = new Vector2(this.transform.position.x + faceX * 0.7f, this.transform.position.y + faceY * 0.7f); }
        else if (doToolLag) { facing.transform.position = facing.transform.position; }
    }

    void Walk()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 3f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
        }
    }
    
    void UseTool()  // ��Ŭ���� ���� ���� -> ��Ŭ���� ��Ŭ�� ���̿��� ���� �־�� �Ѵ�. && ��Ŭ���ߴٸ� �������� ���߾�� �Ѵ�.
                    // �� ������ ���� ��Ŭ�� ����� �ٸ���(���Ѹ���, ���˴�). ������ ���� ��Ŭ���ϸ� ��Ǿ��� ������ �ɴ´�. ����� ���� ���⸶�� �ٸ���. 
                    // �տ� �� �������� �����ϰ� �� ����� ���� DB��� ��� ���� �ҷ��´�.
    {
        if(Input.GetMouseButtonDown(0) && doToolLag == false && touchObject) //������Ʈ ���̾� ��ü�� ������ ���¿��� ��Ŭ���� �Ѵٸ�
        {
            //������ ��ü�� ���¸� ��ȭ ��Ų��
            doToolLag = true;
        }
        else if (Input.GetMouseButtonDown(0) && doToolLag == false) // �ܼ��� Ŭ���� �� ��� + ���� ��Ÿ���� �� ���
        {
            doToolLag = true;
        }

        if (doToolLag) // ���� Ŭ���� �ߴ�.
        {
            toolLag += Time.deltaTime;
            if(toolLag > 0.9f) { GetComponentInChildren<EdgeCollider2D>().enabled = true; }
            if (toolLag > 1f) // ���� Ŭ���� ���� 1�ʰ� �����ٸ�
            {
                
                doToolLag = false; // �ٽ� Ŭ�� �Ҽ� �ְ� �ϰ�
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
}
