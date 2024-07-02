using System.Collections.Generic;
using UnityEngine;

class BuildingManager : MonoBehaviour
{   //빌딩 매니저는 현재 존재하는 건축물들을 관리한다.
    //건축물의 위치와 상세정보를 저장해 두고 이후 로드 할때는 해당 위치에 존재하는 buildcontrol에 소환한다.
    //gamemanager의 load이후에 실행되도록 한다.
    //dont destroy이다.

    public TextAsset BuildingList; // 건축물 리스트.

    private void Awake()
    {
        
    }
}