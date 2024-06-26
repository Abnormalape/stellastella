using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

class BedInteraction :MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            AskSleep(collision);
        }
    }

    GameObject chatBoxPrefab;
    ChatManager chatManager;

    private void Awake()
    {
        chatManager = GameObject.Find("ChatManager").GetComponent<ChatManager>();
    }

    void AskSleep(Collider2D collision)
    {
        GameObject chatboxPrefabinstance = Instantiate(Resources.Load("Prefabs/ChatBox/ChatBoxCanvas") as GameObject, collision.transform);

        collision.transform.GetComponent<PlayerController>().Conversation(true);

        chatboxPrefabinstance.GetComponentInChildren<Text>().text = "정말로 잠을 잘까요?";

        chatboxPrefabinstance.GetComponent<ChatBox>().chatType = ChatType.BedChoice;

        chatboxPrefabinstance.GetComponent<ChatBox>().ActivateButton(0);
        chatboxPrefabinstance.GetComponent<ChatBox>().ButtonText(0, "예");
        chatboxPrefabinstance.GetComponent<ChatBox>().ChoiceButtons[0].onClick.AddListener(() => chatManager.BedYes(collision.gameObject, chatboxPrefabinstance));
        chatboxPrefabinstance.GetComponent<ChatBox>().Choices[0].transform.localPosition = new Vector3(0,60,0);

        chatboxPrefabinstance.GetComponent<ChatBox>().ActivateButton(1);
        chatboxPrefabinstance.GetComponent<ChatBox>().ButtonText(1, "아니오");
        chatboxPrefabinstance.GetComponent<ChatBox>().Choices[1].transform.localPosition = new Vector3(0,-90,0);
        chatboxPrefabinstance.GetComponent<ChatBox>().ChoiceButtons[1].onClick.AddListener(() => chatManager.BedNo(collision.gameObject , chatboxPrefabinstance));
    }
}