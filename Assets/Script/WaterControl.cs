using Unity;
using System;
using UnityEngine;
using Unity.VisualScripting;

class WaterControl : MonoBehaviour
{/* ���߿� �ٽ� ���ƿ���
    GameObject bobber;
    PlayerInventroy fishingrod;
    PlayerController playerController;
    System.Random bite; // �ƿ� ���� ��ȣ�� ����
    int biteLimit;
    int biteReal;
    float passedTime = 0;
    int fishID;
    private void OnTriggerEnter2D(Collider2D collision) // bobber(trigger)�� �����Ѵٸ�, ������ ���� ������ ����� ������ ��ü�� ����....?
    {
        if (collision.gameObject.tag == "bobber") // ������� �����ߴٸ�
        {
            fishingStart(collision); // ���ø� ����
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bobber") // ������� �������̶��
        {
            passedTime += Time.deltaTime; // �ð������ üũ�ϰ�
            
            if(passedTime*100 > biteReal) // �ð������ �����ð����� Ŀ���� ������ �÷��̾�� �����Ѵ�.
            {
                playerController.getbite = true;
            }
        }
    }
    void fishingStart(Collider2D collision)
    {
        bobber = collision.gameObject; // �ε��� �༮�� ���ӿ�����Ʈ�� �θ���.
        fishingrod = bobber.GetComponentInParent<PlayerInventroy>(); // �ε��� �༮�� �θ������Ʈ���� playerinventory�� ȣ�� => ������ ���˴�
        playerController = collision.GetComponent<PlayerController>();
        // playerinventory�� ����� �̳�, �� ���� ����ؼ� bite�� �����Ѵ�.
        biteLimit = 10; // �⺻���� 10�ʿ��� ���ǿ� ���� �� ���δ�.
        biteReal = bite.Next(20, 100 * biteLimit);
    }

    void JudgeFish()    // ����� ���� : ���� ����, ����, �ð�, �����κ����� �Ÿ� �� ������ �޴´�. 
                        // switch�� ������ ���� ���� �ð� �����ϰ� �������� �Ÿ� �����ؼ� ���� �̻��϶��� Ư�� �����, �ָ� �������� ���� ��޳�����
    {
        
    }*/
}