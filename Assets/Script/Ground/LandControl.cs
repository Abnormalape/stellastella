using UnityEngine;

public enum LandType
{
    Empty, Weed, Tree, Stick, Stone, Farm
}


class LandControl : MonoBehaviour
{
    public static int thisLandCode { get; private set; } //후에 데이터를 불러올때 필요한 변하지 않는 값
    public int thislandcode { get; } = thisLandCode;

    public string prefabPath;
    public string prefabPath_Crop;
    public LandType landType;
    public int currentHP;
    public int level;
    public int days;
    public bool digged;
    public bool watered;
    public bool seeded;

    private void Awake()
    {
        if(thisLandCode == 0)
        {
            thisLandCode = this.gameObject.GetHashCode();
        }

        landType = LandType.Empty;
        Debug.Log(thisLandCode);
    }

    private void Update()
    {
        LandDataUpdate();
    }


    private void LandDataUpdate()
    {
        if (transform.childCount == 0)
        {   //Empty.
            landType = LandType.Empty;
        }
        else if (GetComponentInChildren<WeedLandWeed>() != null)
        {   //자식이 WeedLandWeed class를 가지고 있다면.

            landType = LandType.Weed; //여기서 설정.
            prefabPath = GetComponent<WeedLand>().prefabPath;  //WeedLand에서 가져옴.
            currentHP = 1;  //WeedLandWeed에서 가져와야 하나 어차피 체력은 1.
                            //level = 0; WeedLandWeed에서 가져와야 하나 얘는 레벨이 없음.
        }
        else if (GetComponentInChildren<FieldStoneObject>() != null)
        {   //돌맹이가 생성된 케이스.
            landType = LandType.Stone;
            prefabPath = GetComponent<StoneLand>().prefabPath;  //StoneLand에서 가져옴.
            currentHP = 1;  //자식오브젝트에서 가져와야 하나 어차피 체력은 1.
                            //얘는 레벨이 없음, 아니 있기는 한데, 그건 다른 오브젝트긴 한데...
        }
        else if (GetComponentInChildren<FieldTreeLandStick>() != null)
        {   //나뭇가지가 생성된 케이스.
            landType = LandType.Stick;
            prefabPath = GetComponent<TreeLand>().prefabPath;
            currentHP = 1;  //자식오브젝트에서 가져와야 하나 어차피 체력은 1.
            //얘는 레벨이 없음.
        }
        else if (GetComponentInChildren<FieldTreeLand>() != null)
        {   //나무가 자란 케이스.
            landType = LandType.Tree;
            prefabPath = GetComponent<TreeLand>().prefabPath;
            currentHP = GetComponentInChildren<FieldTreeLand>().hp;         //FieldTreeObject에서 가져와야함.
            level = GetComponentInChildren<FieldTreeLand>().currentLevel;   //FieldTreeObject에서 가져와야함.
        }
        else if (GetComponentInChildren<FarmLandControl>() != null)
        {   //농사를 한 케이스.
            landType = LandType.Farm;
            if (GetComponent<FarmLand>().prefabPath != null)
            {
                prefabPath = GetComponent<FarmLand>().prefabPath; // 이 프리팹은 물주기와 씨앗심기를 담당한다.
            }
            digged = GetComponent<FarmLand>().digged;
            watered = GetComponentInChildren<FarmLandControl>().watered;
            seeded = GetComponentInChildren<FarmLandControl>().seeded; // 씨앗이 심어진 상태라면, 작물 프리팹이 소환된다.
            if (seeded) // 따라서 작물 프리팹의 경로를 받아와야한다. 
            {   //씨앗의 종류, 성장상태.
                prefabPath_Crop = GetComponentInChildren<FarmLandControl>().prefabPath; //작물의 프리팹 경로.
                days = transform.GetChild(0).GetComponentInChildren<CropControl>().days; //프리팹으로 소환된 작물의 날짜경과(level로 퉁쳐도 되는데 헷갈릴것 같음).
            }
            currentHP = 1; //이건 쓸 일이 있나?
        }
    }


}
