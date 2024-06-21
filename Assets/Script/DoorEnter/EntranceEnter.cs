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
    GameManager gameManager;
    FadeManager fadeManager;

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        fadeManager = FindFirstObjectByType<FadeManager>();
    }

    Collider2D collisionn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collisionn = collision;
        }

        if (GoingScene == "" && collisionn != null && collision.tag == "Player")
        {   //씬이 변하지 않는.
            collision.GetComponent<PlayerController>().Conversation(true);
            fadeManager.fadeIn = true;
            fadeManager.fadingObject = collision.gameObject;

            Invoke("TeleportPlayer", 0.5f);
        }
        else if (collision.tag == "Player" && collisionn != null)
        {   //씬이 변하는.
            collision.GetComponent<PlayerController>().Conversation(true);
            fadeManager.fadeIn = true;
            fadeManager.fadingObject = collision.gameObject;

            Invoke("TransformPlayer", 0.5f);
        }
    }

    void TransformPlayer()
    {   //Scene이 변하는 entrance.
        collisionn.transform.position = GoingTo;
        collisionn.transform.GetComponent<PlayerController>().nowLocation = ToPlace;
        gameManager.currentSceneName = GoingScene;
        collisionn = null;
    }

    void TeleportPlayer()
    {   //Scene이 변하지 않는 entrance.
        collisionn.transform.position = GoingTo;
        collisionn.transform.GetComponent<PlayerController>().nowLocation = ToPlace;
        collisionn = null;
    }
}
