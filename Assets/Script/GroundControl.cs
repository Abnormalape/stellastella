using System;
using Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

class GroundControl : MonoBehaviour // ���� ���� ����� ��ȭ
{
    Scene currentScene;
    string sceneName;
    bool Farm;
    bool gotSomething; // => �ڽ� ������Ʈ�� �־��
    private void Awake()
    {
        currentScene = GetComponent<Scene>();
        if (sceneName == "Farm") // �����̶��
        {Farm = true;}
        else {Farm = false;}
    }
    private void Update()
    {
        if (gotSomething) // �ڽ� ������Ʈ�� �ִٸ�
        {
            // �� ������ �ٸ� �ڽ��� ������ �ʰ�, �ڽ��� ���ֱ� ������ �ٸ� ��ȣ�ۿ뵵 �ȵǿ�
        }
        else // �ڽ��� ���ٸ�
        {
            // �� ������ �ٸ� �ڽ��� ���� �� �ְ�, �װ� �ڽ��� ������� �־��
            // �����տ� ���� ���� -> �������� ������ ����
        }

            //�װŶ� ������� ������ ��ǳ�̶�� ���⿣ ������ ����ĥ�ſ���
    }
}