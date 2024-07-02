using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

class BuildLandObject : MonoBehaviour
{
    public string buildingName; // 전해받은 buildName으로 csv파일을 훑어서 자신이 건설한 건물의 정보를 확인한다.
                                // 상세정보는 얘가 core일때만 기능한다.

    TextAsset myBuildingList;

    public Dictionary<string,string> myBuildingData; // 이녀석이 사용할 건축물 데이터.

    private bool tBuildCore;
    public bool buildCore
    {
        get { return tBuildCore; }
        set
        {
            tBuildCore = value;
            CallBuildingData(buildingName); // 데이터를 뽑았으니.

            CallSprite();

            //Todo: 이 녀석의 건축날짜 등을 저장하고 스프라이트를 출력해야한다.
            //Todo: 이 녀석이 가질수 있는 동물의 종류, 등의 상세 데이터를 저장해야한다.
        }
    }

    BuildingManager buildingManager;
    private void CallBuildingData(string tBuildingName)
    {
        if(buildingManager == null)
        {
            buildingManager = FindFirstObjectByType<BuildingManager>();
        }

        myBuildingList = buildingManager.BuildingList;

        List<Dictionary<string,string>> buildingData = new ParseCsvFile().ParseCsv(myBuildingList.text);

        int listNumber = 0;

        for (int i = 0; i < buildingData.Count; i++)
        {
            if (buildingData[i]["BuildingName"] == tBuildingName)
            {
                listNumber = i;
                return;
            }
        }
        myBuildingData = buildingData[listNumber];
    }

    private void CallSprite()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = FindFirstObjectByType<SpriteManager>().GetSprite(buildingName);
        transform.GetChild(0).transform.localPosition = new Vector3(-0.5f, -0.5f,0);
    }
}