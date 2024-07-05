using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

class BuildingManager : MonoBehaviour
{   //빌딩 매니저는 현재 존재하는 건축물들을 관리한다.
    //건축물의 위치와 상세정보를 저장해 두고 이후 로드 할때는 해당 위치에 존재하는 buildcontrol에 소환한다.
    //gamemanager의 load이후에 실행되도록 한다.
    //dont destroy이다.

    public TextAsset BuildingList; // 건축물 리스트.
    List<Dictionary<string, string>> BuildingData;
    AnimalManager animalManager;

    public class BuildingDataModel
    {
        public BuildingDataModel(string name, string doorPositionX, string doorPositionY, string enterancePositionX, string enterancePositionY, string currentAnimals)
        {
            this.Name = name;
            this.DoorPositionX = doorPositionX;
            this.DoorPositionY = doorPositionY;
            this.EntrancePositionX = enterancePositionX;
            this.EntrancePositionY = enterancePositionY;
            this.CurrentAnimals = currentAnimals;
        }

        public string Name;
        public string DoorPositionX;
        public string DoorPositionY;
        public string EntrancePositionX;
        public string EntrancePositionY;
        public string CurrentAnimals;
    }
    //List<BuildingDataModel> buildingDataList = new List<BuildingDataModel>();


    public int EnterTobuilding = -1;

    [SerializeField] TextAsset BuildingManagerData;

    List<Dictionary<string, string>> BuildingIndex; //저장된 건축물
    //buildLandObject의 index가 0이라면 list[0]에서 정보를 가져오고, 3이라면 list[3]에서 가져온다.
    //데이터를 저장 할 땐, dictionary를 사용하지 말자. 지금 이 코드에 엄청난 폭탄이 있는데, 그거 해결 못했다.

    int BuildingIndexCount = 0;

    //Dictionary의 저장사항.
    //이름, 문의위치, 입구의 위치, 경계의 좌표 등.

    private void Awake()
    {
        EnterTobuilding = -1;
        BuildingData = new ParseCsvFile().ParseCsv(BuildingList.text);
        BuildingIndex = new ParseCsvFile().ParseCsv(BuildingManagerData.text);
        animalManager = FindFirstObjectByType<AnimalManager>();

        string[] inddd = BuildingManagerData.text.Split(',');
        for (int i = 0; i < inddd.Length; i++){
            Debug.Log(inddd[i]);
        }
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
        //buildingDataList.Add(new BuildingDataModel
        //    (
        //        name: caller.buildingName,
        //        doorPositionX: caller.doorPosition.x.ToString(),
        //        doorPositionY: caller.doorPosition.y.ToString(),
        //    ));
        BuildingIndexCount++;

        Debug.Log("insideSaving");

        //name 저장.
        BuildingIndex[i]["Name"] = caller.buildingName;
        //문의 위치 저장.
        BuildingIndex[i]["DoorPositionX"] = caller.doorPosition.x.ToString();

        Debug.Log(caller.doorPosition.x.ToString());
        Debug.Log(BuildingIndex[i]["DoorPositionX"]);

        BuildingIndex[i]["DoorPositionY"] = (caller.doorPosition.y - 1.5f).ToString();

        BuildingIndex[i]["EntrancePositionX"] = BuildingData[caller.buildingID - 1]["EntranceLocationX"]; // n번째 문이 이동할 위치
        BuildingIndex[i]["EntrancePositionY"] = BuildingData[caller.buildingID - 1]["EntranceLocationY"];

    }

    List<Entrance> buildingEntrances;
    public void SetBuildingDataWithBuildingIndexInsideHouse() // 건축물 내부 에 들어올때 정보 셋팅.
    {
        if (EnterTobuilding != -1) // 건축물의 문을 통해 건물로 들어 왔을때.
        {
            Entrance[] findentrance = FindObjectsOfType<Entrance>(); // 모든 입구를 찾아서

            int targetindex = 0;
            for (int ix = 0; ix < findentrance.Length; ix++) //모든 입구의 이름을 들어온 문의 이름과 비교해서
            {
                if (BuildingIndex[EnterTobuilding]["Name"] + "Entrance" == findentrance[ix].name)
                {   //같다면.
                    targetindex = ix;
                    break;
                }
            }
            //해당입구가 이동할 위치.
            findentrance[targetindex].GoingTo = new Vector3(
                (float)Convert.ToDouble(BuildingIndex[EnterTobuilding]["DoorPositionX"]),
                (float)Convert.ToDouble(BuildingIndex[EnterTobuilding]["DoorPositionY"]),
                0);

            animalManager.SetAnimalsInsideFarm(targetindex);
        }
    }



    List<GameObject> buildCores;
    public void SetBuildingDataWithBuildingIndexAtFarm() //농장으로 나갈 때 건축물의 데이터를 설정하는 방법.
    {
        buildCores = new List<GameObject>();
        //인덱스 별로 건물을 찾아서 문 or 입구 컴포넌트를 반한.
        BuildLandObject[] instBuildObs = FindObjectsOfType<BuildLandObject>();

        for (int ix = 0; ix < instBuildObs.Length; ix++)
        {
            if (instBuildObs[ix].buildCore == true)
            {
                buildCores.Add(instBuildObs[ix].gameObject);//빌드코어를 가진 게임오브젝트 선별.
            }
        }

        if (buildCores == null) { return; }

        for (int iy = 0; iy < buildCores.Count; iy++)
        {
            buildCores[iy].transform.GetChild(2).GetComponent<DoorEnter>().GoingTo =
                new Vector3(
                    (float)Convert.ToDouble(BuildingIndex[buildCores[iy].GetComponent<BuildLandObject>().buildingIndex]["EntrancePositionX"]),
                    (float)Convert.ToDouble(BuildingIndex[buildCores[iy].GetComponent<BuildLandObject>().buildingIndex]["EntrancePositionY"]),
                    0);
        }
    }

    public bool CheckAnimalCounts(BuildLandObject Caller, int CallersIndex) //caller는 호출한 오브젝트, index는 caller의 건축물 번호.
    {
        if (BuildingIndex[CallersIndex].ContainsKey("CurrentAnimals") == false)
        {
            BuildingIndex[CallersIndex]["CurrentAnimals"] = "0";
        }

        if (Convert.ToInt32(BuildingIndex[CallersIndex]["CurrentAnimals"]) < Convert.ToInt32(BuildingData[CallersIndex]["MaxAnimals"]))
        {   //현재 동물의 숫자가 최대 수용량보다 적다면.
            BuildingIndex[CallersIndex]["CurrentAnimals"]
                = (Convert.ToInt32(BuildingIndex[CallersIndex]["CurrentAnimals"]) + 1).ToString();

            Debug.Log(BuildingIndex[CallersIndex]["CurrentAnimals"]);
            return true;
        }   //현재 동물의 숫자가 최대 수용량 이상이라면.
        else return false;
    }
}