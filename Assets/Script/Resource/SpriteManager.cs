using System;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{   //스프라이트 매니저는 싱글톤을 상속 받는데, 싱글톤의 제네릭이 스프라이트 매니저이다.


    [SerializeField] private string texturePath;

    [SerializeField] private Sprite[] sprites;

    private void OnEnable()
    {   //입력된 경로의 모든 스프라이트를 배열로 저장.
        sprites = Resources.LoadAll<Sprite>(texturePath);
    }

    public Sprite GetSprite(string name)
    {
        return Array.Find(sprites , sprite => sprite.name == name);
    }
}