using UnityEngine.EventSystems;
using UnityEngine;

class SellBoxLastSellSlot : MonoBehaviour, IPointerClickHandler
{
    SellBoxManager sellBoxManager;

    private void Awake()
    {
        sellBoxManager = GetComponentInParent<SellBoxManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            RestoreLastSell();
        }
    }

    private void RestoreLastSell()
    {
        sellBoxManager.RestoreLastSell();
    }
}