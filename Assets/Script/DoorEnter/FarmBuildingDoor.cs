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
            buildingManager.EnterTobuilding = GetComponentInParent<BuildLandObject>().buildingIndex;
        }
    }
}