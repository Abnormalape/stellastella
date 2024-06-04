using UnityEngine;


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
        if (collision.gameObject.GetComponent<HarvestControl>() != null)
        {
            if (!motion)
            {
                Debug.Log($"{collision.name}");
                collision.gameObject.GetComponent<HarvestControl>().handHarvest = true;
                collision.gameObject.GetComponent<HarvestControl>().touchedObject = this.GetComponent<BoxCollider2D>();
                //텀을 두고 순차적으로 하고 싶은데
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