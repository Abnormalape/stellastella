using System;
using Unity;
using UnityEditor;
using UnityEngine;

class MyPlayerCursor : MonoBehaviour
{   // 플레이어 커서를 따라 다니는 포인터
    // 인벤토리에서 집은 아이템은 얘의 자식 오브젝트로 배정

    public int itemID;
    public int itemCounts;
    public bool itemOnHand = false;

    Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;   
    }
    private void Update()
    {
        this.transform.position = new Vector3 (mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y, this.transform.position.z);
    }
}

