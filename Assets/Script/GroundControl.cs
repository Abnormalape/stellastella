using System;
using Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

class GroundControl : MonoBehaviour // 씬에 따라 기능이 변화
{
    Scene currentScene;
    string sceneName;
    bool Farm;
    bool gotSomething; // => 자식 오브젝트가 있어요
    private void Awake()
    {
        currentScene = GetComponent<Scene>();
        if (sceneName == "Farm") // 농장이라면
        {Farm = true;}
        else {Farm = false;}
    }
    private void Update()
    {
        if (gotSomething) // 자식 오브젝트가 있다면
        {
            // 이 땅에는 다른 자식이 생기지 않고, 자식을 없애기 전까진 다른 상호작용도 안되요
        }
        else // 자식이 없다면
        {
            // 이 땅에는 다른 자식이 생길 수 있고, 네가 자식을 만들수도 있어요
            // 프리팹에 따라 변동 -> 프리팹이 부족이 보류
        }

            //그거랑 상관없이 날씨가 폭풍이라면 여기엔 번개가 내리칠거에요
    }
}