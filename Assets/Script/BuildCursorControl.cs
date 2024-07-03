using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class BuildCursorControl : MonoBehaviour
{

    BuildCursorElement[] childCursorElement;
    public GameObject buildingCoreObject;
    public string buildingName;
    GameManager gameManager;
    CameraManager cameraManager;
    public GameObject player;
    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        cameraManager = FindFirstObjectByType<CameraManager>();

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
                    childCursorElement[ix].touchedLand.GetComponent<BuildLand>().buildingIndex = -1; //건설용 식별자.
                    childCursorElement[ix].touchedLand.GetComponent<BuildLand>().builded = true; //이 시점에서 건축물이 생성된다.
                }
                
                //세워진 건축물이 몇번째 건축물인지 저장한다 (buildManager.CoreIndex = i).
                //세워진 건축물의 이름을 저장한다. (buildManager.buildingName[i] = this.buildingName).
                //세워진 건축물의 문의 위치를 저장한다. (buildManager.DoorPosition[i] = 뭐 아무튼 그거).







                //원래 씬으로 복귀.
                Invoke("GotoOriginScene", 0.5f);

                //복귀후 자재 및 골드 소모. //Todo:
            }
        }
    }
    void GotoOriginScene()
    {
        cameraManager.nowcamera = nowLocation.RobinHouse;
        GetComponentInParent<MyPlayerCursor>().player.SetActive(true);
        GetComponentInParent<MyPlayerCursor>().player.GetComponent<PlayerController>().Conversation(false);
        gameManager.currentSceneName = "InsideHouse";
        gameManager.needSubCam = false;

        Destroy(this.gameObject);
    }
}