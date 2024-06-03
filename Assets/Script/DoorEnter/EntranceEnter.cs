using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

class Entrance : MonoBehaviour
{
    [SerializeField] Vector3 GoingTo;
    [SerializeField] string GoingScene;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        TransformPlayer(collision);
    }

    void TransformPlayer(Collider2D collision)
    {
        if (collision.tag == "Player" && collision != null)
        {   //우클릭과 접촉했는데, 그 부모에 플레이어 컨트롤러가 있다면.
            //문이 가진 씬의 좌표로 플레이어를 전송한다.
            SceneManager.LoadScene(GoingScene);
            collision.transform.position = GoingTo;
        }
    }
}
