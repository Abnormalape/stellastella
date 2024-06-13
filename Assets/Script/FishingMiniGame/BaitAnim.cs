using System.Collections;
using UnityEngine;

class BaitAnim : MonoBehaviour
{

    float passedtime;


    float originx;
    float originy;
    float originz;

    private void Start()
    {
        originx = transform.position.x;
        originy = transform.position.y;
        originz = transform.position.z;
    }
    private void Update()
    {

        passedtime += Time.deltaTime;
        transform.position = new Vector3(originx, originy + 2.5f + Mathf.Sin(passedtime * 2 * Mathf.PI) * 0.5f, originz);

        if (passedtime > 0.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
