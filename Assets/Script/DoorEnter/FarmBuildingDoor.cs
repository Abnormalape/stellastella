using UnityEngine;

class FarmBuildingDoor : MonoBehaviour
{
    BuildingManager buildingManager;
    private void Awake()
    {
        buildingManager = FindFirstObjectByType<BuildingManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "RightClick")
        {
            Debug.Log(GetComponentInParent<BuildLandObject>().buildingIndex + " 문짝의 인덱스");
            buildingManager.EnterTobuilding = GetComponentInParent<BuildLandObject>().buildingIndex;
        }
    }
}