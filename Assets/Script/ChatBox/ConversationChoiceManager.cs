using UnityEngine;

public class ConversationChoiceManager : MonoBehaviour
{
    public ConversationChoiceManager() { }

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


    public GameObject Caller;
    public GameObject CallersPlayer;
    public void BuyItem() // 선택지에서 물건구매를 선택했을 때. 로빈, 마니.
    {
        Caller.GetComponent<OpeningTradeWindow>().OpenTradeWindow(CallersPlayer);
        Caller = null;
        CallersPlayer = null;
    }

    public void ConstructBuilding() // 선택지에서 건물 건설을 선택했을 때. 로빈, 마법사.
    {
        Caller.GetComponent<OpeningBuildingWindow>().OpenBuildWindow(CallersPlayer);
        Caller = null;
        CallersPlayer = null;
    }

    public void BuyAnimal() 
    {
        Caller.GetComponent<OpeningBuyAnimalWindow>().OpenBuyAnimalWindow(CallersPlayer);
        Caller = null;
        CallersPlayer = null;
    }
}
