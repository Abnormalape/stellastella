using UnityEngine;

public class SortingManager : MonoBehaviour
{
    private void LateUpdate()
    {
        UpdateZValues();
    }


    void UpdateZValues()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            Vector3 position = obj.transform.position;

            // 오브젝트가 활성화되어 있고 Renderer가 있는 경우에만 업데이트
            if (obj.activeInHierarchy && obj.GetComponent<Renderer>() != null && position.y != position.z)
            {
                position.z = position.y;
                obj.transform.position = position;
            }
        }
    }
}
