using UnityEngine;
using UnityEngine.UI;

class SingleSellObject : MonoBehaviour
{
    GameObject item;
    GameObject grade;
    GameObject count;
    GameObject gold;
    SpriteManager spriteManager;
    private void Awake()
    {
        item = transform.Find("Item").gameObject;
        grade = transform.Find("Grade").gameObject;
        count = transform.Find("Count").gameObject;
        gold = transform.Find("Gold").gameObject;
        spriteManager = FindFirstObjectByType<SpriteManager>();
    }
    public void PrintDetails(string uitem, string ugrade, int ucount, int ugold)
    {
        item.GetComponent<Image>().sprite = spriteManager.GetSprite(uitem);
        grade.GetComponent<Image>().sprite = spriteManager.GetSprite(ugrade);
        count.GetComponent<Text>().text = $"X {ucount}";
        gold.GetComponent<Text>().text = $"{ugold} G";
    }
}
