using UnityEngine;
using UnityEngine.UI;

class Fading : MonoBehaviour
{
    GameObject image;
    PlayerController pCon;
    private void Awake()
    {
        pCon = GetComponentInParent<PlayerController>();
        GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
        image = GetComponentInChildren<Image>().gameObject;
        image.SetActive(false);
    }

    float passedTime = 0;
    public bool fadeIn = false;
    bool fadeOut = false;

    private void Update()
    {
        if (fadeIn)
        {
            
            image.SetActive(true);
            DoFade();
        }
    }

    public void DoFade()
    {
        passedTime += Time.deltaTime;
        if (fadeOut == false)
        {
            GetComponentInChildren<Image>().color = new Color(0, 0, 0, passedTime * 3f);
            if (passedTime > 0.5f)
            {
                fadeOut = true;
                passedTime = 0f;
            }
        }
        else if (fadeOut == true)
        {
            GetComponentInChildren<Image>().color = new Color(0, 0, 0, 1 - passedTime * 3f);
            if (passedTime > 0.5f)
            {
                fadeOut = false;
                fadeIn = false;
                passedTime = 0f;
                GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
                image.SetActive(false);
                pCon.Conversation(false);
            }
        }
    }
}