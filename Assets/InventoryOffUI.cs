using Unity.VisualScripting;
using UnityEngine;

public class InventoryOffUI : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponentInParent<GameManager>();
    }
    private void Update()
    {
        
    }
}
