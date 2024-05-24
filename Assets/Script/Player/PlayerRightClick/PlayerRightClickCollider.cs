using System;
using Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;


class PlayerRightClickCollider : MonoBehaviour
{
    float harvestTerm;
    bool motion = false;
    private void Update()
    {
        CheckHarvestTerm();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<HarvestControl>() != null)
        {
            if (!motion)
            {
                collision.gameObject.GetComponent<HarvestControl>().handHarvest = true;
                //텀을 두고 순차적으로 하고 싶은데
                motion = true;

                collision.gameObject.GetComponent<HarvestControl>().handHarvest = false;
            }
        }
    }

    void CheckHarvestTerm()
    {
        if (motion)
        {
            harvestTerm += Time.deltaTime;
        }
        if (harvestTerm > 1f)
        {
            motion = false;
            harvestTerm = 0;
        }
    }
}

