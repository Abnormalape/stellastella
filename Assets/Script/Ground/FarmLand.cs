using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;
public class FarmLand : MonoBehaviour   // �ٸ� Land���� �ڽĿ�����Ʈ�� ���� �� �������� �ڽ� ������Ʈ�� �����
                                        // ���� ����� ����ܺο��� ���� �������� ����� �����̰�
                                        // ���� ���ο��� �������� �޾����� ������ �������� �����ϴ� �����̴�
                                        // ��� �ڽ��� ���� GameObject�� �ڽ� ������Ʈ�� ������, �������� �ϸ� ������ �������� �ڽĿ�����Ʈ�� ����� ����� �Ѵ�.
{
    ItemDB itemDB;
    [SerializeField] GameManager gameManager;
    int currentDate;
    int currentSeason;
    private void Awake() // ���� ������ �� �ʱ�ȭ
    {
        currentDate = gameManager.currentDay;
    }

    private void Update()
    {

    }
    public void OutSideFarm() // ����ܺο��� ������ �߰ߵȴ�.
    {
        if (currentDate != gameManager.currentDay) // ���� �ٲ� Ȯ�������� ���� �������� �����Ѵ�.
        {
            // ���� �������� ���� �����
            currentDate = gameManager.currentDay;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision) // �浹�� �޾�����.
    {
        if (transform.childCount == 0 && true) // �ڽ��� �����鼭, ���� �̸��� �����̶��.
        {
            itemDB = new ItemDB(collision.gameObject.GetComponentInParent<PlayerInventroy>().currentInventoryItem); // �浹ü�� Item�����͸� �ҷ��ͼ�.
            if (itemDB.toolType == 2) // �װ��� �����϶�.
            {
                GameObject.Instantiate(gameObject, Vector3.zero, Quaternion.identity).transform.parent = this.transform; // ������ �������� ���� �׳��� �θ� ���� �����϶�.
            }
        }
    }
}
