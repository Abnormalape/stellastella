using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

class BuildLandObject : MonoBehaviour
{
    public string buildingName; // 전해받은 buildName으로 csv파일을 훑어서 자신이 건설한 건물의 정보를 확인한다.
                                // 상세정보는 얘가 core일때만 기능한다.

    TextAsset myBuildingList;

    public int buildingIndex = -1; // 건축물의 순서.

    public Dictionary<string, string> myBuildingData; // 이녀석이 사용할 건축물 데이터.

    private bool tBuildCore;
    public bool buildCore
    {
        get { return tBuildCore; }
        set
        {
            tBuildCore = value;

            CallBuildingData(buildingName); // 데이터를 뽑았으니.

            if (value == true)
            {
                ComponentSet();

                if (buildingIndex == -1) // 최초 건설시점에는 index가 -1, 이후 빌드 매니저가 인덱스를 배정해주고 나면 해당 인덱스를 저장.
                    //이후 저장된 인덱스가 먼저 index에 할당이 되고, 여기에 돌아올땐, -1이 아님.
                    //Todo: 인덱스 할당을 먼저하고, 빌드코어 설정을 나중에 할것.
                {
                    SendDataToBuildingManager();
                }
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
            //Todo: 이 녀석의 건축날짜 등을 저장하고 스프라이트를 출력해야한다.
            //Todo: 이 녀석이 가질수 있는 동물의 종류, 등의 상세 데이터를 저장해야한다.
        }
    }

    BuildingManager buildingManager;
    private void CallBuildingData(string tBuildingName)
    {
        if (buildingManager == null)
        {
            buildingManager = FindFirstObjectByType<BuildingManager>();
        }

        myBuildingList = buildingManager.BuildingList;

        List<Dictionary<string, string>> buildingData = new ParseCsvFile().ParseCsv(myBuildingList.text);

        int listNumber = 0;

        for (int i = 0; i < buildingData.Count; i++)
        {
            if (buildingData[i]["BuildingName"] == tBuildingName)
            {
                listNumber = i;
                break;
            }
        }
        myBuildingData = buildingData[listNumber];
    }

    public Vector3 doorPosition;
    private void ComponentSet()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = FindFirstObjectByType<SpriteManager>().GetSprite(buildingName);
        transform.GetChild(0).transform.localPosition = new Vector3(-0.5f, -0.5f, 0); // 건축물의 스프라이트는 피벗이 무조건 좌하단이어야 한다.

        GetComponent<BoxCollider2D>().offset = new Vector2
            ((float)Convert.ToDouble(myBuildingData["Length"]) / 2f - 0.5f,
            (float)Convert.ToDouble(myBuildingData["Height"]) / 2f - 0.5f);
        GetComponent<BoxCollider2D>().size = new Vector2
            ((float)Convert.ToDouble(myBuildingData["Length"]),
            (float)Convert.ToDouble(myBuildingData["Height"]));

        if (myBuildingData["DoorLocationX"] != "") // 문의 위치가 존재 할때.
        {
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = FindFirstObjectByType<SpriteManager>().GetSprite(buildingName + "Door");
            transform.GetChild(1).transform.localPosition = new Vector3(
                (float)Convert.ToDouble(myBuildingData["DoorLocationX"]) - 1f,
                (float)Convert.ToDouble(myBuildingData["DoorLocationY"]) - 1f,
                0f);

            transform.GetChild(1).GetComponent<BoxCollider2D>().size = transform.GetChild(1).GetComponent<SpriteRenderer>().sprite.bounds.size;
        }

        if (myBuildingData["PlayerDoorX"] != "") // 플레이어의 문이 존재할때.
        {
            doorPosition = new Vector3(
                (float)Convert.ToDouble(myBuildingData["PlayerDoorX"]) - 1f,
                (float)Convert.ToDouble(myBuildingData["PlayerDoorY"]) - 1f,
                0f);

            transform.GetChild(2).transform.localPosition = doorPosition;

            doorPosition = transform.GetChild(2).transform.position; // 문의 이동위치를 판정하기 위해 로컬이 아닌 좌표값으로 재설정.

            transform.GetChild(2).GetComponent<BoxCollider2D>().size = new Vector2(1,2);
        }
    }

    private void SendDataToBuildingManager()
    {
        buildingManager.WhenBuildingMadeAtFarm(this);
    }
}