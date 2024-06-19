using UnityEngine;
using UnityEngine.UI;

public enum ChatType
{
    Conversation,
    BedChoice,
    EatChoice,
}


class ChatBox : MonoBehaviour
{
    public ChatType chatType;

    public Button[] ChoiceButtons;
    public GameObject[] Choices { get; private set; } = new GameObject[4];

    private void Awake()
    {
        for (int i = 0; i < ChoiceButtons.Length; i++)
        {
            Choices[i] = ChoiceButtons[i].gameObject;
            Choices[i].SetActive(false);
        }
    }

    public void ActivateButton(int i)
    {
        Choices[i].SetActive(true);
    }

    public void ButtonText(int i,string text)
    {
        Choices[i].GetComponentInChildren<Text>().text = text;
    }


}
