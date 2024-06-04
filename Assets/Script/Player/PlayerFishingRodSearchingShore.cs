using UnityEngine;
class PlayerFishingRodSearchingShore : MonoBehaviour
{
    PlayerController pCon;

    private void Awake()
    {
        pCon = GetComponentInParent<PlayerController>();
    }
    private void Update()
    {
    }
    [SerializeField] GameObject playerfishingrodschorecollider;
    void SearchShore()
    {
        Instantiate(playerfishingrodschorecollider, this.transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water") && GetComponentInParent<PlayerInventroy>().currentItemToolType() == 9)
        {   //낚시찌가 물과 접촉했다면, 찌의 위치로부터 물가를 탐색하고.
            //플레이어를 입질 대기상태로 만든다.
            SearchShore();
            pCon.WaitingForBait(true);
        }
    }
}