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
        motion = pCon.motion;
        CheckHarvestTerm();
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