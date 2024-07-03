using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

class BuildingManager : MonoBehaviour
{   //빌딩 매니저는 현재 존재하는 건축물들을 관리한다.
    //건축물의 위치와 상세정보를 저장해 두고 이후 로드 할때는 해당 위치에 존재하는 buildcontrol에 소환한다.
    //gamemanager의 load이후에 실행되도록 한다.
    //dont destroy이다.

    public TextAsset BuildingList; // 건축물 리스트.

    [SerializeField] TextAsset BuildingManagerData;

    List<Dictionary<string, string>> BuildingIndex = new List<Dictionary<string, string>>();
    //buildLandObject의 index가 0이라면 list[0]에서 정보를 가져오고, 3이라면 list[3]에서 가져온다.

    int BuildingIndexCount = 0;

    //Dictionary의 저장사항.
    //이름, 문의위치, 입구의 위치, 경계의 좌표 등.

    private void Awake()
    {
        BuildingIndex = new ParseCsvFile().ParseCsv(BuildingManagerData.text);



        //if (BuildingIndex.Count == 0)
        //{
        //    BuildingIndex.Add(new Dictionary<string, string>());

        //    Debug.Log(BuildingIndex.Count);
        //    BuildingIndex[0]["Name"] = "for the king";
        //    Debug.Log(BuildingIndex[0]["Name"]);
        //}
    }
    private bool readyToWhenBuildAtInside = false;
    public void WhenBuildingMadeAtFarm(BuildLandObject caller)
    {
        int i = 0;
        //index 저장.
        if (BuildingIndex.Count == 0)
        {
            caller.buildingIndex = 0;
            i = 0;
        }
        else
        {
            caller.buildingIndex = BuildingIndex.Count; // 하나가 저장된 상태에서의 길이는 1, 할당해야하는 인데스도 1.
            i = BuildingIndex.Count;
        }
        BuildingIndex.Add(new Dictionary<string, string>()); // 그리고 새로운 딕셔너리를 추가한다.
        BuildingIndexCount++;

        Debug.Log("insideSaving");

        //name 저장.
        BuildingIndex[i]["Name"] = caller.buildingName;
        //문의 위치 저장.
        BuildingIndex[i]["DoorPositionX"] = caller.doorPosition.x.ToString();
        BuildingIndex[i]["DoorPositionY"] = (caller.doorPosition.y - 1.5f).ToString();

        readyToWhenBuildAtInside = true;
    }
    public void WhenBuildingMadeAtInsideHouse()
    {
        if (readyToWhenBuildAtInside)
        {
            //이름으로inside prefab소환.
            string buildingPath = BuildingIndex[BuildingIndex.Count - 1]["Name"] + "Inside";

            //생성. 여기서 생성이 제대로 되어야 리스트 번호를 배정받을 수 있다. Todo:
            GameObject refEntrance = Instantiate(Resources.Load($"Prefabs/BuildCanvas/{buildingPath}") as GameObject,
                new Vector3(-20f, 30f * (BuildingIndex.Count - 1))
                , Quaternion.identity);
            BuildingIndex[BuildingIndex.Count - 1]["EntrancePositionX"] = refEntrance.transform.position.x.ToString();
            BuildingIndex[BuildingIndex.Count - 1]["EntrancePositionY"] = (refEntrance.transform.position.y + 1.5f).ToString();

            //Todo: 경계
        }
        readyToWhenBuildAtInside = false;
    }

    List<GameObject> buildInsides;
    public void SetBuildingDataWithBuildingIndexInsideHouse() // 건축물 내부 씬에 생성된 물체들을 저장 하는 방법이 선행되어야 한다.
    {
        BuildInsides[] instBuildIns = FindObjectsOfType<BuildInsides>(); // 내부도 리스트 번호를 가지고 있어야한다.
                                                                         // 내부가 생성되지 않는 건축물도 있다.
                                                                         // 내부가 정상적으로 생겨야 내부는 리스트 번호를 가진다.

        for (int ix = 0; ix < instBuildIns.Length; ix++) // 내부가 가진 인덱스 번호를 사용해, entrance의 목적지를 정해준다.
        {
            instBuildIns[ix].GetComponentInChildren<Entrance>().GoingTo =
                new Vector3(
                    Convert.ToInt32(BuildingIndex[instBuildIns[ix].BuildingIndex]["DoorPositionX"]),
                    Convert.ToInt32(BuildingIndex[instBuildIns[ix].BuildingIndex]["DoorPositionY"]),
                    0);
        }
    }


    List<GameObject> buildCores;
    public void SetBuildingDataWithBuildingIndexAtFarm() //농장으로 나갈 때 건축물의 데이터를 설정하는 방법.
    {
        //인덱스 별로 건물을 찾아서 문 or 입구 컴포넌트를 반한.
        BuildLandObject[] instBuildObs = FindObjectsOfType<BuildLandObject>();

        for (int ix = 0; ix < instBuildObs.Length; ix++)
        {
            if (instBuildObs[ix].buildCore == true)
            {
                buildCores.Add(instBuildObs[ix].gameObject);//빌드코어를 가진 게임오브젝트 선별.
            }
        }

        for (int iy = 0; iy < buildCores.Count; iy++)
        {
            buildCores[iy].transform.GetChild(2).GetComponent<DoorEnter>().GoingTo =
                new Vector3(
                    Convert.ToInt32(BuildingIndex[buildCores[iy].GetComponent<BuildLandObject>().buildingIndex]["EntrancePositionX"]),
                    Convert.ToInt32(BuildingIndex[buildCores[iy].GetComponent<BuildLandObject>().buildingIndex]["EntrancePositionY"]),
                    0);
        }
    }
}