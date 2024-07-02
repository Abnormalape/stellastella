using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

class SubCamChild : MonoBehaviour
{
    public bool IsMouseOn;

    private void Update()
    {
        Vector2 mousePosition = FindFirstObjectByType<Camera>().ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hit = Physics2D.OverlapPointAll(mousePosition);

        //Ray ray = FindFirstObjectByType<Camera>().ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        for(int i= 0;i < hit.Length; i++)
        {
            if (hit[i] != null && hit[i].gameObject == gameObject)
            {
                IsMouseOn = true;
                return;
            }
        }

        IsMouseOn = false;
    }
}