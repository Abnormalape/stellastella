using System;
using Unity;
using UnityEditor;
using UnityEngine;
class FieldItem : MonoBehaviour
{
    public int itemID = 0; // 아이템 ID 
    public int grade = 0; // 아이템 품질, 등급
    public int numbers = 0; // 아이템 갯수 : 인벤토리에서 아이템을 버릴때 필요
}
