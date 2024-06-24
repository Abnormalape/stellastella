using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

class SellProductDetailWindow : MonoBehaviour
{
    int page = 0;
    int pages;

    int[] myids;
    int[] mycounts;
    int[] mygrades;
    int[] mygolds;
    public void OpenWindowForDetail(int[] ids, int[] counts, int[] grades, int[] golds)
    {
        if (myids == null)
        {
            myids = ids;
            mycounts = counts;
            mygrades = grades;
            mygolds = golds;
            pages = (int)ids.Length / 16;
        }


        int k = 0;
        for (int i = page * 16; i < ids.Length; i++)
        {
            Debug.Log(myids[i]);
            if (k == 16)
            {
                return;
            }

            Vector3 singleposition;

            if (k <= 7)
            { singleposition = new Vector3(-1250f, 660f - ((i % 8) * 180), 0); }
            else
            { singleposition = new Vector3(100f, 660f - (((i - 8) % 8) * 180), 0); }
            GameObject InstanceSingle;
            ItemDB itemDB = new ItemDB(myids[i]);
            string gradeName = GradetoString(mygrades[i]);
            InstanceSingle = Instantiate(Resources.Load("Prefabs/EndDaySel/SigleSellObject") as GameObject, singleposition, Quaternion.identity, this.transform);
            InstanceSingle.transform.localPosition = singleposition;
            InstanceSingle.GetComponent<SingleSellObject>().PrintDetails(itemDB.name, gradeName, mycounts[i], mygolds[i]);
            k++;
        }
    }

    private string GradetoString(int grade)
    {
        if (grade == 1)
        {
            return "SilverGrade";
        }
        else if (grade == 2)
        {
            return "GoldGrade";
        }
        else if (grade == 3)
        {
            return "IridiumGrade";
        }
        else { return "NoneGrade"; }
    }

    public void CloseWindow()
    {
        Destroy(this.gameObject);
    }

    public void ChangePageBack()
    {
        if (page < pages)
        {
            SingleSellObject[] toDestroy = GetComponentsInChildren<SingleSellObject>();
            for (int i = 0; i < toDestroy.Length; i++)
            {
                Destroy(toDestroy[i].gameObject);
            }
            page++;
            OpenWindowForDetail(myids, mycounts, mygrades, mygolds);
        }
    }
    public void ChangePageForward()
    {
        if (page > 0)
        {
            SingleSellObject[] toDestroy = GetComponentsInChildren<SingleSellObject>();
            for (int i = 0; i < toDestroy.Length; i++)
            {
                Destroy(toDestroy[i].gameObject);
            }
            page--;
            OpenWindowForDetail(myids, mycounts, mygrades, mygolds);
        }
    }
}