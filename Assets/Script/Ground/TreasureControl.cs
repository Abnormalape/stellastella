using System;
using System.Net.Sockets;
using Unity.Properties;
using UnityEngine;
using Random = UnityEngine.Random;
class TreasureControl : MonoBehaviour // 얘는 장소에 따라서 드랍 테이블이 달라지는데...
{   // 얘는 괭이질을 받으면 드랍테이블에 따라 아이템을 뱉고, 경작된 땅 스프라이트를 적용한다.
    // 여기다가 물을 줄 수는 없다.
    SpriteRenderer spriteRenderer;
    ItemDB itemDB;

    [SerializeField]
    Sprite[] treasureSprite = new Sprite[2]; // 보물 스프라이트 2개
    [SerializeField]
    Sprite[] diggedGround = new Sprite[16]; // 한번만 노가다 해서 깔끔하게 끝내자

    Collider2D[] nearGround;

    GameObject treasureObject; // 팠을때 나오는 보물 목록

    [SerializeField] public bool digged = false;
    [SerializeField] bool upDigged = false;
    [SerializeField] bool downDigged = false;
    [SerializeField] bool leftDigged = false;
    [SerializeField] bool rightDigged = false;

    private void OnEnable()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        CheckNearDigged();
        MakeDigSprite();

        if (!digged)
        {
            spriteRenderer.sprite = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        itemDB = new ItemDB(collision.gameObject.GetComponentInParent<PlayerInventroy>().currentInventoryItem);
        if (itemDB.toolType == 2 && collision.tag == "Tool") // 부딪힌 놈의 부모는 괭이이면서, 부딪힌 놈은 툴이라면
        {
            //Instantiate(treasureObject); //확률적으로 계산된 게임 오브젝트 프리팹을 만든다.
            digged = true; // 땅이 파진 상태로 만들면 다른애가 스프라이트 만들어 줌
        }
    }
    private void CheckNearDigged()
    {
        nearGround = Physics2D.OverlapCircleAll(this.transform.position, 0.6f);

        
        for (int i = 0; i < nearGround.Length; i++)
        {
            if (nearGround[i] != null && nearGround[i].tag == "FarmLand")
            {
                if (nearGround[i].transform.position.x - this.transform.position.x == 1 && nearGround[i].transform.position.y - this.transform.position.y == 0)
                {
                    if (nearGround[i].GetComponent<TreasureControl>().digged == true)
                    {
                        rightDigged = true;
                    }
                    else
                    {
                        rightDigged = false;
                    }
                }
                else if (nearGround[i].transform.position.x - this.transform.position.x == -1 && nearGround[i].transform.position.y - this.transform.position.y == 0)
                {
                    if (nearGround[i].GetComponent<TreasureControl>().digged == true)
                    {
                        leftDigged = true;
                    }
                    else
                    {
                        leftDigged = false;
                    }
                }
                else if (nearGround[i].transform.position.x - this.transform.position.x == 0 && nearGround[i].transform.position.y - this.transform.position.y == 1)
                {
                    if (nearGround[i].GetComponent<TreasureControl>().digged == true)
                    {
                        upDigged = true;
                    }
                    else
                    {
                        upDigged = false;
                    }
                }
                else if (nearGround[i].transform.position.x - this.transform.position.x == 0 && nearGround[i].transform.position.y - this.transform.position.y == -1)
                {
                    if (nearGround[i].GetComponent<TreasureControl>().digged == true)
                    {
                        downDigged = true;
                    }
                    else
                    {
                        downDigged = false;
                    }
                }
            }
        }
    }
    private void MakeDigSprite()
    {
        if (!upDigged && !downDigged && !leftDigged && !rightDigged) // 나만 파져있다
        {
            spriteRenderer.sprite = diggedGround[0];
        }
        else if (upDigged && !downDigged && !leftDigged && !rightDigged) // 위만 파져있다
        {
            spriteRenderer.sprite = diggedGround[1];
        }
        else if (!upDigged && downDigged && !leftDigged && !rightDigged) // 아래만 파져있다
        {
            spriteRenderer.sprite = diggedGround[2];
        }
        else if (!upDigged && !downDigged && leftDigged && !rightDigged) // 좌만 파져있다
        {
            spriteRenderer.sprite = diggedGround[3];
        }
        else if (!upDigged && !downDigged && !leftDigged && rightDigged) // 우만 파져있다
        {
            spriteRenderer.sprite = diggedGround[4];
        }
        else if (upDigged && downDigged && !leftDigged && !rightDigged) // 상하만 파져있다
        {
            spriteRenderer.sprite = diggedGround[5];
        }
        else if (!upDigged && !downDigged && leftDigged && rightDigged) // 좌우만 파져있다
        {
            spriteRenderer.sprite = diggedGround[6];
        }
        else if (upDigged && !downDigged && !leftDigged && rightDigged) // 상우만 파져있다
        {
            spriteRenderer.sprite = diggedGround[7];
        }
        else if (!upDigged && downDigged && !leftDigged && rightDigged) // 우하만 파져있다
        {
            spriteRenderer.sprite = diggedGround[8];
        }
        else if (!upDigged && downDigged && leftDigged && !rightDigged) // 좌하만 파져있다
        {
            spriteRenderer.sprite = diggedGround[9];
        }
        else if (upDigged && !downDigged && leftDigged && !rightDigged) // 좌상만 파져있다
        {
            spriteRenderer.sprite = diggedGround[10];
        }
        else if (!upDigged && downDigged && leftDigged && rightDigged) // 상만 안파져있다
        {
            spriteRenderer.sprite = diggedGround[11];
        }
        else if (upDigged && !downDigged && leftDigged && rightDigged) // 하만 안파져있다
        {
            spriteRenderer.sprite = diggedGround[12];
        }
        else if (upDigged && downDigged && !leftDigged && rightDigged) // 좌만 안파져있다
        {
            spriteRenderer.sprite = diggedGround[13];
        }
        else if (upDigged && downDigged && leftDigged && !rightDigged) // 우만 안파져있다
        {
            spriteRenderer.sprite = diggedGround[14];
        }
        else if (upDigged && downDigged && leftDigged && rightDigged) // 전부 파져있다
        {
            spriteRenderer.sprite = diggedGround[15];
        }
    }
}