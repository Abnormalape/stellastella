using UnityEngine;

class aboutprefabs : MonoBehaviour
{
    GameObject summonedPrefab;
    [SerializeField] GameObject test;
    private void LateUpdate()
    {
        
        Instantiate(summonedPrefab, transform.position, Quaternion.identity);
    }


    
}

