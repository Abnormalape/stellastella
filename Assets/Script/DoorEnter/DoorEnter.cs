using System;
using UnityEngine;
using UnityEngine.SceneManagement;

class DoorEnter : MonoBehaviour
{
    [SerializeField] Vector3 GoingTo;
    [SerializeField] string GoingSnece;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "RightClick" && collision.transform.parent.GetComponent<PlayerController>() != null)
        {   //우클릭과 접촉했는데, 그 부모에 플레이어 컨트롤러가 있다면.
            //문이 가진 씬의 좌표로 플레이어를 전송한다.
            SceneManager.LoadScene(GoingSnece);
            collision.transform.parent.transform.position = GoingTo;
        }
    }
}
