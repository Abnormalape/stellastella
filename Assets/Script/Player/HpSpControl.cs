using UnityEngine;
using UnityEngine.UI;

class HpSpControl : MonoBehaviour
{
    PlayerController pCon;
    Image image;

    Vector3 firstScale;
    RectTransform rectTransform;
    private void Awake()
    {
        pCon = transform.parent.parent.parent.GetComponent<PlayerController>();
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();

        firstScale = rectTransform.localScale;
    }

    [SerializeField] private bool thisHP;
    [SerializeField] private bool thisSP;

    private void Update()
    {
        if (thisHP)
        {
            HpBarSize();
            HpBarColor();
        }
        else if(thisSP)
        {
            SpBarSize();
            SpBarColor();
        }
    }

    void HpBarSize()
    {
        rectTransform.localScale = new Vector3(1f, (float)pCon.currentHp / (float)pCon.maxHp , 1f);
    }

    void HpBarColor()
    {
        if (pCon.currentHp >= (pCon.maxHp / 2f))
        {
            image.color = new Color(0f, (pCon.currentHp - (pCon.maxHp / 2f)) / (pCon.maxHp / 2f), 0);
        }
        else if (pCon.currentHp < (pCon.maxHp / 2f))
        {
            image.color = new Color(1 - ((pCon.currentHp) / (pCon.maxHp / 2f)), 0, 0);
        }
    }

    void SpBarSize()
    {
        rectTransform.localScale = new Vector3(1f, (float)pCon.currentStamina / (float)pCon.maxStamina, 1f);
    }
    void SpBarColor()
    {
        if (pCon.currentStamina >= (pCon.maxStamina / 2f))
        {
            image.color = new Color(1-((pCon.currentStamina-(pCon.maxStamina/2f))/((pCon.maxStamina / 2f))) ,1f,0);
        }
        else if (pCon.currentStamina < (pCon.maxStamina / 2f))
        {
            image.color = new Color(1f, pCon.currentStamina / (pCon.maxStamina / 2f), 0);
        }
    }
}
