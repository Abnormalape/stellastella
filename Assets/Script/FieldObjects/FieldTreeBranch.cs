using System;
using Unity;
using UnityEngine;

class FieldTreeBranch : MonoBehaviour // 나무의 가지에 삽입하는 스크립트
{
    
    public float fallXY;
    float fallTime=0f;

    private void OnEnable() // 생성되었을때
    {
    }
    private void Update()
    {
        if(GetComponentInParent<FieldTreeObject>() == null)
        {
            branchAnimation();
        }
    }
    void branchAnimation()
    {
        fallTime += Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(0, 0, fallXY * fallTime * 45f * fallTime);
        if (fallTime * 45f * fallTime > 90) { makeItems(); }
    }
    void makeItems()
    {
        Destroy(this.gameObject);
    }
}
