using UnityEngine;

class CaughtAnim : MonoBehaviour
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
        transform.position = new Vector3(originx, originy + 3f + Mathf.Sin(passedtime * 2 * Mathf.PI) * 0.5f, originz);

        if (passedtime < 0.1f)
        {
            transform.localScale = Vector3.one * ((passedtime * 2) + 1f);
        }
        else
        {
            transform.localScale = Vector3.one * (1.2f - ((passedtime - 0.1f) * 3f));
        }

        if (passedtime > 0.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
