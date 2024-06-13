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

            // ������Ʈ�� Ȱ��ȭ�Ǿ� �ְ� Renderer�� �ִ� ��쿡�� ������Ʈ
            if (obj.activeInHierarchy && obj.GetComponent<Renderer>() != null && position.y != position.z)
            {
                position.z = position.y;
                obj.transform.position = position;
            }
        }
    }
}
