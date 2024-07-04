using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class PlayerRightClickCollider : MonoBehaviour
{
    float harvestTerm;
    bool motion = false;

    PlayerController pCon;
    private void Awake()
    {
        pCon = GetComponentInParent<PlayerController>();
    }
    private void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HarvestControl>() != null)
        {
            if (!motion)
            {
                collision.gameObject.GetComponents<HarvestControl>()[0].handHarvest = true;
                collision.gameObject.GetComponents<HarvestControl>()[0].touchedObject = this.GetComponent<BoxCollider2D>();
                //텀을 두고 순차적으로 하고 싶은데
                pCon.Motion(true);
                motion = true;
                StartCoroutine(CheckHarvestTerm());
            }
        }
    }

    IEnumerator CheckHarvestTerm()
    {
        while (harvestTerm < 1f)
        {
            harvestTerm += Time.deltaTime;
            yield return null;
        }
        motion = false;
        harvestTerm = 0;
    }
}