using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;

class SellProductDetail : MonoBehaviour, IPointerClickHandler
{
    EndDayManager endDayManager;
    string myname;
    private void Awake()
    {
        endDayManager = transform.parent.parent.GetComponent<EndDayManager>();
        myname = this.gameObject.name;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (thisID.Count > 0)
            {
                ShowDetail();
            }
        }
    }

    private void Update()
    {
        if (thisID.Count <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public List<int> thisID;
    public List<int> thisCount;
    public List<int> thisGrade;
    public List<int> sellPrice;
    private void ShowDetail()
    {
        //해당 데이터들을 이용하여. 결과창 프리팹을 생성.
        //프리팹을 생성해서 DATA를 import.
        GameObject tResource = Resources.Load("Prefabs/EndDaySel/SellDetailWindow") as GameObject;
        GameObject DetailWindow = Instantiate(tResource);
        DetailWindow.GetComponent<SellProductDetailWindow>().OpenWindowForDetail(thisID.ToArray(), thisCount.ToArray(), thisGrade.ToArray(), sellPrice.ToArray());
    }
}