using UnityEngine.EventSystems;
using UnityEngine;

class SellBoxSlot : MonoBehaviour, IPointerClickHandler
{   //Slot이 클릭 등의 기능을 받았을때 실행되는 메서드.

    public int thisSlotNumber;

    SellBoxManager sellBoxManager;

    private void Awake()
    {
        sellBoxManager = GetComponentInParent<SellBoxManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    //OnLeftClick : sellboxmanager에 addall을 실행
    private void OnLeftClick()
    {
        sellBoxManager.AddAllItemFromSlot(thisSlotNumber);
    }


    //OnRightClick : sellboxmanager에 addsigle을 실행
    private void OnRightClick()
    {
        sellBoxManager.AddSigleItemFromSlot(thisSlotNumber);
    }
}