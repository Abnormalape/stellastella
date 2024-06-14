using UnityEngine;
using UnityEngine.UI;

class SeasonUiSprite : MonoBehaviour
{
    [SerializeField] Sprite[] SeasonSprite;
    GameManager gameManager;
    Image myImage;

    private void Awake()
    {
        gameManager = GetComponentInParent<GameManager>();
        myImage = GetComponent<Image>();
    }

    public void WhenSeasonChange(int season) // 0 : 봄, 3 : 겨울.
    {
        if (myImage == null)
        {
            myImage = GetComponent<Image>();
        }
        myImage.sprite = SeasonSprite[season];
    }
}