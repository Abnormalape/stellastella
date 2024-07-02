using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


class BuildCursorElement : MonoBehaviour
{
    public bool firstElement = false;
    public GameObject touchedLand;

    private void Update()
    {
        float x;
        float y;
        if (transform.position.x > 0)
        {
            x = (int)transform.position.x + 0.5f;
        }
        else
        {
            x = (int)transform.position.x - 0.5f;
        }

        if (transform.position.y > 0)
        {
            y = (int)transform.position.y + 0.5f;
        }
        else
        {
            y = (int)transform.position.y - 0.5f;
        }

        OnBuildLand();

        transform.GetChild(0).position = new Vector3(x, y, 0);
        if (colorGreen)
        {
            transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().color = Color.red;
        }
    }

    public bool colorGreen { get; private set; }
    
    private void OnBuildLand()
    {
        Vector2 elementPosition = transform.position;
        Collider2D[] hit = Physics2D.OverlapPointAll(elementPosition);

        //Ray ray = FindFirstObjectByType<Camera>().ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i] != null && hit[i].gameObject.GetComponent<BuildLand>() && hit[i].gameObject.transform.childCount == 0)
            {
                colorGreen = true;
                touchedLand = hit[i].gameObject.GetComponent<BuildLand>().gameObject;
                if (firstElement)
                {
                    transform.parent.GetComponent<BuildCursorControl>().buildingCoreObject 
                        = hit[i].gameObject.GetComponent<BuildLand>().gameObject;
                }

                return;
            }
        }
        colorGreen = false;
    }


}