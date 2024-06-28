using UnityEngine;
using UnityEngine.SceneManagement;

public class WhenSceneLoaded : MonoBehaviour
{
    private void Update()
    {
        SceneManager.LoadScene("InsideHouse");
    }
}
