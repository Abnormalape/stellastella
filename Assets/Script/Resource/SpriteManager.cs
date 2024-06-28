using System;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{   //스프라이트 매니저는 싱글톤을 상속 받는데, 싱글톤의 제네릭이 스프라이트 매니저이다.

    [SerializeField] private string texturePath;
    [SerializeField] private string texturePath_1;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Sprite[] sprites_1;

    private void OnEnable()
    {   //입력된 경로의 모든 스프라이트를 배열로 저장.
        if (texturePath != "" || texturePath != null)
        {
            sprites = Resources.LoadAll<Sprite>(texturePath);
        }
        else if (texturePath_1 != "" || texturePath_1 != null)
        {
            sprites_1 = Resources.LoadAll<Sprite>(texturePath_1);
        }
    }

    public Sprite GetSprite(string name)
    {
        Sprite outSprite;

        if(Array.Find(sprites, sprite => sprite.name == name) != null)
        {
            return Array.Find(sprites, sprite => sprite.name == name);
        }
        else if (Array.Find(sprites_1, sprite => sprite.name == name) != null)
        {
            return Array.Find(sprites_1, sprite => sprite.name == name);
        }

        return null;
    }
}