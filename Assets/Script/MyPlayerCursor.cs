using System;
using Unity;
using UnityEditor;
using UnityEngine;

class MyPlayerCursor : MonoBehaviour
{
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

