using UnityEngine;
using UnityEngine.UI;

class FadeManager : MonoBehaviour
{
    GameObject image;
    public GameObject fadingObject;
    private void Awake()
    {
        GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
        image = GetComponentInChildren<Image>().gameObject;
        image.SetActive(false);
    }

    private void Update()
    {
        DoFade();
    }

    float passedTime;
    bool fadeOut = false;
    public bool fadeIn = false;
    private void DoFade()
    {
        if (fadeIn)
        {
            image.SetActive(true);
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
                    fadingObject.GetComponent<PlayerController>().Conversation(false);
                    fadingObject = null;
                }
            }
        }
        else 
        {
            return;
        }
    }
}