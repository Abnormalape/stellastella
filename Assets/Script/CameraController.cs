using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerController playerController;
    void Start()
    {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    
    void Update()
    {
        this.transform.position = new Vector3(playerController.transform.position.x , playerController.transform.position.y, playerController.transform.position.z -10f);    
    }
}
