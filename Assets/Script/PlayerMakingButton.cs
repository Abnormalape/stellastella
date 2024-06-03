using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

class PlayerMakingButton : MonoBehaviour
{
    public void ahosdnmfj()
    {
        SceneManager.LoadScene("InsideHouse");
        GameObject.Find("Player").transform.position = new Vector3(10f, 13f, 0f);
    }
}