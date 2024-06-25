using UnityEngine;
using UnityEngine.SceneManagement;

public class NextDayOkButton : MonoBehaviour
{
    GameManager gameManager;
    SellBoxManager sellBoxManager;
    PlayerController pCon;
    EndDayManager endDayManager;
    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        sellBoxManager = FindFirstObjectByType<SellBoxManager>();    
        pCon = FindFirstObjectByType<PlayerController>();
        endDayManager = FindFirstObjectByType<EndDayManager>();
    }

    public void DayOKButtonClicked()
    {
        gameManager.EndOfTheDay();
        sellBoxManager.ResetAll();
        pCon.currentGold += endDayManager.totalSellPrice;

        SceneManager.LoadScene("InsideHouse");
    }
}