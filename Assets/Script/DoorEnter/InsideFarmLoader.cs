
using UnityEngine;

class InsideFarmLoader : MonoBehaviour
{
    public bool sceneLoaded = false;

    private void Update()
    {
        if (!sceneLoaded)
        {
            sceneLoaded = true;
            return;
        }
    }
}