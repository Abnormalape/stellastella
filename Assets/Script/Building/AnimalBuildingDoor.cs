using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AnimalBuildingDoor : MonoBehaviour
{
    bool isOpen = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "RightClick" && collision.transform.parent.tag == "Player" && isMoving == false)
        {
            
            if (isOpen)
            {
                isOpen = false;
                //문이 닫히는 애니메이션.
                StartCoroutine(closeDoor());
            }
            else if (!isOpen)
            {
                
                isOpen = true;
                //문이 열리는 애니메이션.
                StartCoroutine(openDoor());
            }
        }
    }

    Vector2 firstLocation;
    bool isMoving = false;

    private IEnumerator openDoor()
    {
        float passedTime = 0f;

        firstLocation = transform.position;

        while(passedTime < 0.5f)
        {
            passedTime += Time.deltaTime;
            transform.position += Vector3.up * Time.deltaTime * 2; // 0.5초동안 위로 1만큼 올라가는.
            GetComponent<BoxCollider2D>().offset += Vector2.down * Time.deltaTime * 2;
            isMoving = true;
            yield return null;
        }

        isMoving = false;
    }

    private IEnumerator closeDoor()
    {
        float passedTime = 0f;

        while (passedTime < 0.5f)
        {
            passedTime += Time.deltaTime;
            transform.position += Vector3.down * Time.deltaTime * 2; // 0.5초동안 위로 1만큼 올라가는.
            GetComponent<BoxCollider2D>().offset += Vector2.up * Time.deltaTime * 2;
            yield return null;
            isMoving = true;
        }

        transform.position = firstLocation;
        GetComponent<BoxCollider2D>().offset = Vector2.zero;

        isMoving = false;
    }
}