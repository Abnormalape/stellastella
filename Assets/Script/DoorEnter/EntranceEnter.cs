using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

class Entrance : MonoBehaviour
{
    [SerializeField] Vector3 GoingTo;
    [SerializeField] string GoingScene;
    string currentSceneName;



    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    Collider2D collisionn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionn = collision;
        if (collisionn.tag == "Player" && collisionn != null)
        {   //플레이어와 접촉했다면.
            //입구가 가진 씬의 좌표로 플레이어를 전송한다.

            if (currentSceneName == "Farm")
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().SaveLandData();
            }

            Invoke("TransformPlayer", 0.5f);
        }
    }

    void TransformPlayer()
    {
        SceneManager.LoadScene(GoingScene);
        collisionn.transform.position = GoingTo;
        collisionn = null;
    }
}
