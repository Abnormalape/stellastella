using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

class EndDayManager : MonoBehaviour
{
    SellBoxManager sellBoxManager;

    List<int> IDs;
    List<int> Grades;
    List<int> Counts;

    int[] aIDs;
    int[] aGrades;
    int[] aCounts;

    private void Awake()
    {
        sellBoxManager = FindFirstObjectByType<SellBoxManager>();

        IDs = new List<int>();
        Grades = new List<int>();
        Counts = new List<int>();

        MakeSum();
    }


    private void MakeSum()
    {
        aIDs = new int[sellBoxManager.ListItemID.Count];
        aGrades = new int[sellBoxManager.ListItemID.Count];
        aCounts = new int[sellBoxManager.ListItemID.Count];

        for (int i = 0; i < sellBoxManager.ListItemID.Count ; i++)
        {
            aIDs[i] = sellBoxManager.ListItemID.ToArray()[i];
            aGrades[i] = sellBoxManager.ListItemGrade.ToArray()[i];
            aCounts[i] = sellBoxManager.ListItemCount.ToArray()[i];
        }

        int L;
        int K = 0;
        L = aIDs.Length;

        for (int i = 0; i < L; i++)
        {
            if (aIDs[i] == 0 || aCounts[i] == 0)
            {
                continue;
            }

            if (K == 0)
            {
                IDs.Add(aIDs[i]);
                Grades.Add(aGrades[i]);
                Counts.Add(aCounts[i]);
                K++;
                continue;
            }

            for (int j = 0; j < K; j++)
            {
                if (IDs[j] == aIDs[i]) // 저장하려는 ID가 목록에 있다면.
                {
                    if (Grades[j] == aGrades[i]) // 해당위치의 등급이 내가 저장하려는 등급과 같다면. (같은 저장이라면).
                    {
                        Counts[j] += aCounts[i];
                        break;
                    }
                    else // 해당위치의 등급이 내가 저장하려는 등급과 다르다면.
                    {
                        for (int n = j + 1 ; n < K; n++)
                        {
                            if (Grades[n] == aGrades[i])
                            {
                                Counts[j] += aCounts[i];
                                break;
                            }
                            if (n+1 == K)
                            {
                                IDs.Add(aIDs[i]);
                                Grades.Add(aGrades[i]);
                                Counts.Add(aCounts[i]);
                                K++;
                                break;
                            }
                        }
                        break;
                    }
                }
                else // 저장하려는 ID가 목록에 없다면.
                {
                    IDs.Add(aIDs[i]);
                    Grades.Add(aGrades[i]);
                    Counts.Add(aCounts[i]);
                    K++;
                    break;
                }
            }
        }

        for (int i = 0; i < IDs.Count; i++)
        {
            Debug.Log($"{i}번째 / ID : {IDs[i]} / Grade : {Grades[i]} / Counts : {Counts[i]}");
        }
    }
}