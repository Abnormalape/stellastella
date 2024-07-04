using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningBuildingWindow : MonoBehaviour//카운터에 말을 걸었을 때, 상점 주인이 있는 경우 상점창 출력.
{   //카운터는 본인의 아이템 품목을 플레이어가 지닌 tradewindow에 넘긴다.
    //카운터는 본인이 저장한 이미지를 tradewindow에 넘겨 portrait를 설정한다.

    [SerializeField] GameObject myBuildWindow; // 건물 거래창.
    [SerializeField] TextAsset mySellList; // 건축물 리스트.
    PlayerController pCon;
    [SerializeField] bool CasherOn = false;

    //건축물의 경우엔 대화하고 바로 넘어가는 경우가... 마법사의 탑?
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CasherOn && collision.tag == "RightClick" && collision.transform.parent.tag == "Player"
            && GetComponent<PrintMessageBox>() == null)
        {
            OpenBuildWindow(collision.transform.parent.gameObject);
        }
    }

    public void OpenBuildWindow(GameObject playerObject)
    {

        //pCon은 필요하다. (농장 내 건물 파악)  
        pCon = playerObject.GetComponent<PlayerController>();
        pCon.Conversation(true);
        
        //건물 건설창을 띄운다.
        GameObject summonedWindow = Instantiate(myBuildWindow, Vector3.zero, Quaternion.identity, this.transform);
        //데이터를 넘겼다고 한다.
        summonedWindow.GetComponent<BuildingWindow>().pCon = pCon;
    }
}