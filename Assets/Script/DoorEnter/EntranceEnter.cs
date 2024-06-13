using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

class Entrance : MonoBehaviour
{
    [SerializeField] Vector3 GoingTo;
    [SerializeField] string GoingScene;
    string currentSceneName;

    [SerializeField] nowLocation ToPlace; // 이동하는 장소의 이름.

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    Collider2D collisionn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collisionn = collision;
        }

        if(GoingScene == "" && collisionn != null && collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().Conversation(true);
            collision.GetComponentInChildren<Fading>().fadeIn = true;

            Invoke("TeleportPlayer", 0.5f);
        }
        else if (collision.tag == "Player" && collisionn != null)
        {   //플레이어와 접촉했다면.
            //입구가 가진 씬의 좌표로 플레이어를 전송한다.

            if (currentSceneName == "Farm")
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().SaveLandInFarmData();
            }

            collision.GetComponent<PlayerController>().Conversation(true);
            collision.GetComponentInChildren<Fading>().fadeIn = true;

            Invoke("TransformPlayer", 0.5f);
        }
    }

    void TransformPlayer()
    {
        
        collisionn.transform.position = GoingTo;
        collisionn.transform.GetComponent<PlayerController>().nowLocation = ToPlace;
        
        SceneManager.LoadScene(GoingScene);
        collisionn = null;
    }
    
    void TeleportPlayer()
    {
        collisionn.transform.position = GoingTo;
        collisionn.transform.GetComponent<PlayerController>().nowLocation = ToPlace;
        collisionn = null;
    }
}
