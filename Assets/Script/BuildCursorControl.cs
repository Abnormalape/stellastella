using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class BuildCursorControl : MonoBehaviour
{

    BuildCursorElement[] childCursorElement;
    public GameObject buildingCoreObject;
    public string buildingName;
    PlayerController pCon;
    GameManager gameManager;
    private void Awake()
    {
        pCon = transform.parent.parent.GetComponent<PlayerController>();
        gameManager = FindFirstObjectByType<GameManager>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (childCursorElement == null)
            {
                childCursorElement = new BuildCursorElement[GetComponentsInChildren<BuildCursorElement>().Length];
                for (int i = 0; i < childCursorElement.Length; i++)
                {
                    childCursorElement[i] = GetComponentsInChildren<BuildCursorElement>()[i];
                }
            }
            int canBuild = 0;

            for (int i = 0; i < childCursorElement.Length; i++)
            {
                if (childCursorElement[i].colorGreen)
                {
                    canBuild++;
                }
            }

            if (canBuild < childCursorElement.Length)
            {
                Debug.Log("건설 불가.");
            }
            else if (canBuild == childCursorElement.Length)
            {
                buildingCoreObject.GetComponent<BuildLand>().buildingName = buildingName; //inst로 생성할 건물.
                buildingCoreObject.GetComponent<BuildLand>().buildCore = true; // 해당 오브젝트는 건축물 코어이다.

                for (int ix = 0; ix < childCursorElement.Length; ix++)
                {
                    childCursorElement[ix].touchedLand.GetComponent<BuildLand>().builded = true;
                }

                //원래 씬으로 복귀.
                Invoke("GotoOriginScene",0.5f);

                //복귀후 자재 및 골드 소모. //Todo:


                Debug.Log("건물짓기.");
            }
        }
    }
    void GotoOriginScene()
    {
        pCon.nowLocation = nowLocation.Farm;
        gameManager.needSubCam = false;
    }
}