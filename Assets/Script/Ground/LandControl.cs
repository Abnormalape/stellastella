using System;
using Unity.VisualScripting;
using UnityEngine;

public enum LandType
{
    Empty, Weed, Tree, Stick, Stone, Farm
}


class LandControl : MonoBehaviour
{

    public Vector3 savePosition;
    public string prefabPath;
    public string prefabPath_Crop;
    public LandType landType;
    public int currentHP;
    public int level;
    public int days;
    public bool digged;
    public bool watered;
    public bool seeded;
    public bool onceharvested;
    //=====================================
    public delegate void AValueUpdated(bool newValue);
    public event AValueUpdated OnAValueUpdated;
    public delegate void BValueUpdated(bool newValue);
    public event BValueUpdated OnBValueUpdated;

    private bool a;
    private bool b;
    public bool monthChanged
    {
        get { return a; }
        set
        {
            a = value; // value는 this.bool 의 값과 같다.
            OnAValueUpdated.Invoke(a); // A 값이 변경될 때 이벤트 호출
        }
    }

    public bool dayChanged
    {
        get { return b; }
        set
        {
            b = value;
            //OnBValueUpdated?.Invoke(b); // B 값이 변경될 때 이벤트 호출
            OnBValueUpdated.Invoke(b); // B 값이 변경될 때 이벤트 호출
        }
    }
    //=====================================

    private void Update()
    {
        //씬이 바뀌기 전에만 실행하면 됨.
        LandDataUpdate();
    }


    private void LandDataUpdate()
    {
        string tempstring = prefabPath;
        string tempstring_Crop = prefabPath_Crop;

        if(GetComponent<TreeLand>() != null)
        {
            if (!GetComponent<TreeLand>().AtFarm)
            {
                prefabPath = GetComponent<TreeLand>().prefabPath;
                Debug.Log(tag + " : " + prefabPath);
            }
        }

        if (transform.childCount == 0)
        {   //Empty.
            landType = LandType.Empty;
        }
        //==========================================//
        else if (GetComponentInChildren<WeedLandWeed>() != null)
        {   //자식이 WeedLandWeed class를 가지고 있다면.
            landType = LandType.Weed; //여기서 설정.
            if (GetComponent<WeedLand>().prefabPath != "")
            {
                prefabPath = GetComponent<WeedLand>().prefabPath;
                currentHP = 1;
            }
            else
            {
                prefabPath = tempstring;
                currentHP = 1;
            }
        }
        else if (GetComponentInChildren<FieldStoneObject>() != null)
        {   //돌맹이가 생성된 케이스.
            landType = LandType.Stone;
            if (GetComponent<StoneLand>().prefabPath != "")
            {
                prefabPath = GetComponent<StoneLand>().prefabPath;
                currentHP = 1;
            }
            else
            {
                prefabPath = tempstring;
                currentHP = 1;
            }
        }
        else if (GetComponentInChildren<FieldTreeLandStick>() != null)
        {   //나뭇가지가 생성된 케이스.
            landType = LandType.Stick;
            if (GetComponent<TreeLand>().prefabPath != "")
            {
                prefabPath = GetComponent<TreeLand>().prefabPath;
                currentHP = 1;
            }
            else
            {
                prefabPath = tempstring;
                currentHP = 1;
            }
        }
        //==========================================//
        else if (GetComponentInChildren<FieldTreeLand>() != null)
        {   //나무가 자란 케이스. => 농장외부와 농장내부 =>
            //농장외부의 경우 본인이 가진 프리팹을 전달하면 되는데, 농장 내부의 경우 그때마다 별도의 경로를 지닌다.

            landType = LandType.Tree;

            //path, hp, level
            if (transform.childCount != 0)
            {
                prefabPath = GetComponent<TreeLand>().prefabPath;
            }
            level = GetComponent<TreeLand>().CurrentLevel;
            currentHP = GetComponentInChildren<FieldTreeLand>().hp;
        }
        else if (GetComponentInChildren<FarmLandControl>() != null)
        {   //농사를 한 케이스.
            landType = LandType.Farm;
            if (GetComponent<FarmLand>().prefabPath != "")
            {
                prefabPath = GetComponent<FarmLand>().prefabPath; // 이 프리팹은 물주기와 씨앗심기를 담당한다.
            }
            digged = GetComponent<FarmLand>().digged;
            watered = GetComponentInChildren<FarmLandControl>().watered;
            seeded = GetComponentInChildren<FarmLandControl>().seeded; // 씨앗이 심어진 상태라면, 작물 프리팹이 소환된다.
            if (seeded) // 따라서 작물 프리팹의 경로를 받아와야한다. 
            {   //씨앗의 종류, 성장상태.
                if (GetComponentInChildren<FarmLandControl>().prefabPath != "")
                {
                    prefabPath_Crop = GetComponentInChildren<FarmLandControl>().prefabPath;
                } //작물의 프리팹 경로.
                if (transform.GetChild(0).GetComponentInChildren<CropControl>() != null)
                {
                    days = transform.GetChild(0).GetComponentInChildren<CropControl>().days;
                    onceharvested = transform.GetChild(0).GetComponentInChildren<CropControl>().onceharvested;
                } //프리팹으로 소환된 작물의 날짜경과(level로 퉁쳐도 되는데 헷갈릴것 같음).
            }
            currentHP = 1; //이건 쓸 일이 있나?
        }
        if (savePosition != transform.position)
        {
            savePosition = transform.position;
        }
    }
}
