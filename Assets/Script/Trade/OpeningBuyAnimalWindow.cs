using UnityEngine;

class OpeningBuyAnimalWindow : MonoBehaviour
{
    [SerializeField] GameObject myAnimalWindow; // 건물 거래창.
    [SerializeField] public TextAsset mySellList; // 판매 동물 리스트.
    PlayerController pCon;
    [SerializeField] bool CasherOn = false;

    public void OpenBuyAnimalWindow(GameObject playerObject)
    {
        pCon = playerObject.GetComponent<PlayerController>();
        pCon.Conversation(true);

        //건물 건설창을 띄운다.
        GameObject summonedWindow = Instantiate(myAnimalWindow, Vector3.zero, Quaternion.identity, this.transform);
        //데이터를 넘겼다고 한다.
        summonedWindow.GetComponent<BuyAnimalWindow>().pCon = pCon;
    }
}