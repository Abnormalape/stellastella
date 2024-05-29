using System;
using Unity;
using UnityEngine;

class Class1 : MonoBehaviour
{
    [SerializeField]bool asdf = false;
    [SerializeField] GameObject asdfasdf;
    private void Update()
    {
        if (asdf)
        {
            Instantiate(asdfasdf, this.transform.position, Quaternion.identity);
            asdf = false;
        }
    }
}

