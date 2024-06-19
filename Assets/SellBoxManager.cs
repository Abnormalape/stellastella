using System.Collections.Generic;
using UnityEngine;

class SellBoxManager : MonoBehaviour
{
    List<int> SellBoxListNumber;
    List<int> ListItemID;
    List<int> ListItemCount;
    List<int> ListItemGrade;

    private void Awake()
    {
        ResetList();
    }

    public void AddItem()
    {

    }

    public void PullItem()
    {

    }

    void ResetList()
    {
        SellBoxListNumber = new List<int>();
        ListItemID = new List<int>();
        ListItemCount = new List<int>();
        ListItemGrade = new List<int>();
    }
}