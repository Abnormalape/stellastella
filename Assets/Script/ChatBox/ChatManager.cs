using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public ChatManager() { }

    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void BedYes(GameObject player, GameObject chatbox)
    {
        gameManager.DayOff();
        player.GetComponent<PlayerController>().Conversation(false);
        chatbox.SetActive(false);
    }

    public void BedNo(GameObject player, GameObject chatbox)
    {
        player.GetComponent<PlayerController>().Conversation(false);
        chatbox.SetActive(false);
    }
}
