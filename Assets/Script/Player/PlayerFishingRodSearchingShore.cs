using System;
using Unity.VisualScripting;
using UnityEngine;
class PlayerFishingRodSearchingShore : MonoBehaviour
{
    private void Update()
    {
        
    }
    [SerializeField] GameObject playerfishingrodschorecollider;
    void SearchShore()
    {
        //낚싯대 라면.
        if (GetComponentInParent<PlayerInventroy>().currentItemToolType() == 9)
        {   //주변의 물가를 탐색한다.
            Instantiate(playerfishingrodschorecollider, this.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            SearchShore();
        }
    }
}

