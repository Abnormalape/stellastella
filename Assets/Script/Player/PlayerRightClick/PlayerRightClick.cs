using UnityEngine;
using UnityEngine.UI;
class PlayerRightClick : MonoBehaviour
// 얘를 플레이어 컨트롤에 넣고, 툴타입을 받아온다
{                       // 툴 타입에 맞는 메서드를 실행한다
                        // 그냥 얘 자체를 update에 실행해도 되는것 아닌가?
    PlayerController pCon;
    PlayerMovement pMov;
    PlayerInventroy pInven;
    ItemDB currentData;
    GameObject HitBox;

    float buttonTime;

    private void Awake()
    {
        pCon = this.gameObject.GetComponent<PlayerController>();
        pMov = this.gameObject.GetComponent<PlayerMovement>();
        pInven = this.gameObject.GetComponent<PlayerInventroy>();
        HitBox = GameObject.Find("RightClickHitBox");
        HitBox.GetComponent<BoxCollider2D>().enabled = false; // 콜라이더를 끄거나 자체를 숨기거나 스프라이트를 뽑아버리거나.
        HitBox.transform.localScale = new Vector2(1.3f, 1.3f);
    }


    private void Update()
    {
        currentData = new ItemDB(pInven.currentInventoryItem);
        if (pCon.idle || pCon.moving) // 움직이거나, 대기상태에서만 우클릭이 가능하다.
        {
            ColliderLocation();
            ColliderSize();
            ColliderOnOff();
        }
    }

    void ColliderLocation()
    {
        if (currentData.type == "Seed")
        {
            HitBox.transform.position = GameObject.Find("LeftClickHitBox").transform.position;
        }
        else
        {
            HitBox.transform.position = this.transform.position + (Vector3)pMov.nowFacing * (0.5f);
        }
    }
    void ColliderSize()
    {   //콜라이더 크기 변경
        if (currentData.type == "Seed")
        {
            HitBox.transform.localScale = new Vector2(0.8f, 0.8f);
        }
        else
        {
            HitBox.transform.localScale = new Vector2(1.3f, 1.3f);
        }
    }
    void ColliderOnOff()
    {   //콜라이더 키고 끄기
        currentData = new ItemDB(pInven.currentInventoryItem);

        if (currentData.eatable == false)
        {
            if (Input.GetMouseButtonDown(1))
            {
                HitBox.GetComponent<BoxCollider2D>().enabled = true;
            }
            else if (Input.GetMouseButton(1))
            {
                HitBox.GetComponent<BoxCollider2D>().enabled = true;

                buttonTime += Time.deltaTime;
                if (buttonTime > 0.5f)
                {
                    HitBox.GetComponent<BoxCollider2D>().enabled = false;
                    buttonTime = 0f;
                }
            }
            else if (Input.GetMouseButtonUp(1) || !Input.GetMouseButton(1))
            {
                HitBox.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else if (currentData.eatable == true)
        {   //손에 든게 먹을수 있고, 우클릭을 했을때.
            if (Input.GetMouseButtonDown(1))
            {   //정말 먹을 것인지 메세지 박스를 출력.
                AskEat();
            }
        }
    }

    ChatManager chatManager;
    private void AskEat()
    {
        chatManager = GameObject.Find("ChatManager").GetComponent<ChatManager>();
        GameObject chatboxPrefabinstance = Instantiate(Resources.Load("Prefabs/ChatBox/ChatBoxCanvas") as GameObject, this.transform);
        GetComponent<PlayerController>().Conversation(true);
        chatboxPrefabinstance.GetComponentInChildren<Text>().text = "정말로 먹을까요?";
        ChatBox chatbox = chatboxPrefabinstance.GetComponent<ChatBox>();

        chatbox.chatType = ChatType.EatChoice;

        chatbox.ActivateButton(0);
        chatbox.ButtonText(0, "정말로 먹는다.");
        chatbox.ChoiceButtons[0].onClick.AddListener
            (() => chatManager.EatYes(pCon, pInven, currentData.hpRestore, currentData.staminaRestor, chatboxPrefabinstance)); // chatManager의 EatYes 메서드를 onClick Event에 등록.
        chatbox.Choices[0].transform.localPosition = new Vector3(0, 60, 0);

        chatbox.ActivateButton(1);
        chatbox.ButtonText(1, "먹지 않는다.");
        chatbox.Choices[1].transform.localPosition = new Vector3(0, -90, 0);
        chatbox.ChoiceButtons[1].onClick.AddListener
            (() => chatManager.EatNo(pCon, currentData.hpRestore, currentData.staminaRestor, chatboxPrefabinstance)); // chatManager의 EatNo 메서드를 onClick Event에 등록.
    }
}
