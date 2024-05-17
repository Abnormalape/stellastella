using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;
public class FarmLand : MonoBehaviour   // 다른 Land들은 자식오브젝트가 없을 때 프리팹을 자식 오브젝트로 만든다
                                        // 얘의 기능은 농장외부에선 보물 프리팹은 만드는 역할이고
                                        // 농장 내부에선 괭이질을 받았을때 경작지 프리팹을 생성하는 역할이다
                                        // 얘는 자신이 속한 GameObject가 자식 오브젝트가 없을때, 괭이질을 하면 경작지 프리팹을 자식오브젝트에 만드는 기능을 한다.
{
    ItemDB itemDB;
    [SerializeField] GameManager gameManager;
    int currentDate;
    int currentSeason;
    private void Awake() // 게임 시작할 때 초기화
    {
        currentDate = gameManager.currentDay;
    }

    private void Update()
    {

    }
    public void OutSideFarm() // 농장외부에선 보물이 발견된다.
    {
        if (currentDate != gameManager.currentDay) // 날이 바뀔때 확률적으로 보물 프리팹을 생성한다.
        {
            // 보물 프리팹은 따로 만든다
            currentDate = gameManager.currentDay;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision) // 충돌을 받았을때.
    {
        if (transform.childCount == 0 && true) // 자식이 없으면서, 씬의 이름이 농장이라면.
        {
            itemDB = new ItemDB(collision.gameObject.GetComponentInParent<PlayerInventroy>().currentInventoryItem); // 충돌체의 Item데이터를 불러와서.
            if (itemDB.toolType == 2) // 그것이 괭이일때.
            {
                GameObject.Instantiate(gameObject, Vector3.zero, Quaternion.identity).transform.parent = this.transform; // 경작지 프리팹을 만들어서 그놈의 부모를 나와 같게하라.
            }
        }
    }
}
