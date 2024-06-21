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
        //player.GetComponent<PlayerController>().Conversation(false);
        Destroy(chatbox);
        gameManager.DayOff(player);
        //player.
    }

    public void BedNo(GameObject player, GameObject chatbox)
    {
        player.GetComponent<PlayerController>().Conversation(false);
        Destroy(chatbox);
    }

    //chatManager.EatYes(pCon, currentData.hpRestore, currentData.staminaRestor, chatboxPrefabinstance)); 
    public void EatYes(PlayerController pcon, PlayerInventroy pInven, int hprestore, int sprestore, GameObject chatbox)
    {
        pcon.Conversation(false);
        pcon.Motion(true);
        pcon.currentHp += hprestore;
        pcon.currentStamina += sprestore;
        pInven.changeCount = -1;
        Destroy(chatbox);
    }

    public void EatNo(PlayerController pcon, int hprestore, int sprestore, GameObject chatbox)
    {
        pcon.Conversation(false);
        Destroy(chatbox);
    }
}
