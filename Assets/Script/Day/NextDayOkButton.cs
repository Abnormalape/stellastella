using UnityEngine;
using UnityEngine.SceneManagement;

public class NextDayOkButton : MonoBehaviour
{
    GameManager gameManager;
    SellBoxManager sellBoxManager;
    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        sellBoxManager = FindFirstObjectByType<SellBoxManager>();
    }

    public void DayOKButtonClicked()
    {
        gameManager.EndOfTheDay();
        sellBoxManager.ResetAll();


        SceneManager.LoadScene("InsideHouse");
    }
}