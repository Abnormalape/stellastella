using UnityEngine;
using Random = UnityEngine.Random;

class FieldTreeLandBranch : MonoBehaviour
{
    public float fallXY;
    float branchFallTime;

    [SerializeField] int fieldTreeObjectID;

    GameObject[] dropItemPrefab;
    ItemDB[] itemDB;
    FieldTreeObjectDb thisTree;

    private void OnEnable()
    {
        thisTree = new FieldTreeObjectDb(fieldTreeObjectID);
        itemDB = new ItemDB[thisTree.items];
        dropItemPrefab = new GameObject[thisTree.items];
        for (int i = 0; i < thisTree.items; i++)
        {
            itemDB[i] = new ItemDB(thisTree.itemID[i]);
            itemDB[i].itemSetting();
            dropItemPrefab[i] = Resources.Load($"Prefabs/FieldItems/{itemDB[i].name}") as GameObject;
        }

    }
    private void Update()
    {
        ParentMissing();
    }

    void ParentMissing()
    {
        if (transform.parent == null)
        {
            branchFallTime += Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(0, 0, fallXY * branchFallTime * 45f * branchFallTime);
            if (branchFallTime * 45f * branchFallTime > 90) { MakeDropItem(); Destroy(this.gameObject); }
        }
    }
    void MakeDropItem()
    {
        thisTree.droprate[0] = 1500;
        thisTree.droprate[1] = 500;
        thisTree.droprate[2] = 100;

        for (int i = 0; i < thisTree.items; i++)
        {
            for (int j = 100; j <= thisTree.droprate[i]; j = j + 100)
            {
                Instantiate(dropItemPrefab[i], transform.position + new Vector3(fallXY * -3,0,0), Quaternion.identity);
            }
            int r = Random.Range(0, 100);
            if (r < thisTree.droprate[i] % 100)
            {

                Instantiate(dropItemPrefab[i], transform.position + new Vector3(fallXY * -3, 0, 0), Quaternion.identity);
            }
        }
    }

}