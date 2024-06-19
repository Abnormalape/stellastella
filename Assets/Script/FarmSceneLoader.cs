using UnityEngine;
using UnityEngine.SceneManagement;
class FarmSceneLoader : MonoBehaviour
{
    GameManager gameManager;
    private void Awake()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameManager.LoadLandInFarmData();
        SceneManager.sceneLoaded -= SceneLoaded;
    }
}

