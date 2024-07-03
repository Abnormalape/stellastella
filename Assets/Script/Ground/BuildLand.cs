using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

class BuildLand : MonoBehaviour
{
    private bool tBuildCore;
    public bool buildCore { get { return tBuildCore; }  
        set { 
            tBuildCore = value;  
        } 
    }

    public string buildingName;
    public int buildingIndex = -1;
    public string prefabPath = "Prefabs/BuildCanvas/BuildingPrefab"; // 건물 생성을 위한 베이스 프리팹.
    
    GameObject buildCoreChildObject;

    private bool tBuilded;
    public bool builded {  get { return tBuilded; }
        set
        {
            tBuilded = value;
            
            if (value == true)
            {
                if (prefabPath == null || prefabPath == "")
                {
                    prefabPath = "Prefabs/BuildCanvas/BuildingPrefab";
                }
                

                if (!buildCore)
                {
                    buildCoreChildObject =
                        Instantiate(Resources.Load(prefabPath) as GameObject, transform.position, Quaternion.identity, this.transform);
                    buildCoreChildObject.GetComponent<BuildLandObject>().buildCore = false;
                }
                else if (buildCore)
                {
                    buildCoreChildObject =
                        Instantiate(Resources.Load(prefabPath) as GameObject, transform.position, Quaternion.identity, this.transform);
                    buildCoreChildObject.GetComponent<BuildLandObject>().buildingName = buildingName;
                    buildCoreChildObject.GetComponent<BuildLandObject>().buildingIndex = buildingIndex;
                    buildCoreChildObject.GetComponent<BuildLandObject>().buildCore = true;
                }
            }
        }
    }
}