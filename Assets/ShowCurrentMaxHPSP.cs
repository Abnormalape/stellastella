using UnityEngine;
using UnityEngine.UI;


class ShowCurrentMaxHPSP : MonoBehaviour
{
    private enum HPSP
    {
        HP, SP,
    }
    [SerializeField] HPSP hpsp;

    HpSpControl hscon;
    Text text;
    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        hscon = GetComponentInChildren<HpSpControl>();
        text.gameObject.SetActive(false);
    }

    
    public void MouseOn()
    {
        Debug.Log("Mouse ON");

        if(hpsp == HPSP.HP)
        {
            text.gameObject.SetActive(true);
            text.text = hscon.mystring;
            text.gameObject.transform.position = Input.mousePosition;
        }
        else if (hpsp == HPSP.SP)
        {
            text.gameObject.SetActive(true);
            text.text = hscon.mystring;
            text.gameObject.transform.position = Input.mousePosition;
        }
    }

    public void MouseOff()
    {
        text.gameObject.SetActive(false);
    }
}
