using System;
using UnityEngine;
class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad instance = null;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
    }

}