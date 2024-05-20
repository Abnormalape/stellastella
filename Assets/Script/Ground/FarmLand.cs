using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;
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
        if (currentDate != gameManager.currentDay && transform.childCount == 0) // ���� �ٲ�, �ڽ� ������Ʈ(����, ������)�� ���ٸ�
        {
            int judge = Random.Range(0, 100);
            if (judge >= 90) //10% Ȯ��
            {
                Instantiate(gameObject); //TreasureControl�� ���� ���� �������� �����
            }
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
                // ������ �������� FarmLnadControl�� ������ ���� ������Ʈ�̴�.
            }
        }
    }
}
