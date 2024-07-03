using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

class DoorEnter : MonoBehaviour
{
    [SerializeField] public Vector3 GoingTo;
    [SerializeField] public string GoingScene;
    string currentSceneName;
    GameManager gameManager;
    CameraManager cameraManager;

    [SerializeField] nowLocation ToPlace; // 이동하는 장소의 이름.

    FadeManager fadeManager;
    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cameraManager = FindFirstObjectByType<CameraManager>();
        fadeManager = FindFirstObjectByType<FadeManager>();
    }

    

    Collider2D collisionn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "RightClick" && collision.transform.parent.GetComponent<PlayerController>() != null)
        {   //우클릭과 접촉했는데, 그 부모에 플레이어 컨트롤러가 있다면.
            //문이 가진 씬의 좌표로 플레이어를 전송한다.
            collisionn = collision;
            
            Invoke("Transporting", 0.5f);

            collision.GetComponentInParent<PlayerController>().Conversation(true);

            fadeManager.fadeIn = true;
            fadeManager.fadingObject = collision.transform.parent.gameObject;
        }
    }

    void Transporting()
    {
        gameManager.currentSceneName = GoingScene;
        collisionn.transform.parent.transform.position = GoingTo;
        cameraManager.nowcamera = ToPlace;
        collisionn = null;
    }
}
