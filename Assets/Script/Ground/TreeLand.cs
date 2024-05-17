using UnityEngine.SceneManagement;
using UnityEngine;
public class TreeLand : MonoBehaviour // ���ǿ� ���� ���� �ڽ� ������Ʈ�� �����Ѵ�.
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] treePrefabs;

    int currentDate;
    int currentSeason;
    private void Awake() // ���� ������ �� �ʱ�ȭ
    {
        currentDate = gameManager.currentDay;
    }

    private void Update()
    {
        // �� ���� �������� �ƴ����� ���� if���� ����Ͽ� �Ʒ� �ΰ��� �޼��带 �����Ѵ�
    }
    public void OutSideFarm()
    {
        // ���� �ܺο��� �ڿ������� ������ �ڶ�� ���� �� �ڸ��� �ٸ����� �������� �ʴ´�
        // �� �ڽ� ������Ʈ�� ���ٴ� ���� ������ ���ٴ� ���̴�.
        if (transform.childCount == 0 && gameManager.currentMonth != 3) // ������ ���µ� �ܿ��� �ƴ϶��
        {
            if(currentDate != gameManager.currentDay) // ���� ���� ��¥�� �ý����� ��¥�� ���� �ʴٸ� = ���� �����ٸ�.
            {
                // Ȯ�������� �������� �����϶� = (������ : ��������).
                // ���������� �ڱⰡ �˾Ƽ� ��¥�� �ް� �����Ѵ�.
                currentDate = gameManager.currentDay;
            }
        }
    }

    public void InSideFarm()
    {
        // �� ������ �ƹ��͵� ���µ�, ��ó�� ������ �ִٸ�
        if (transform.childCount == 0 && gameManager.currentMonth != 3) // ��ó�� ������ ã�Ƽ� ����Ʈ�� ����� �����ϰ� �ϳ��� ã�� �� ������ ������ ���������� �θ���.
        {
            if (currentDate != gameManager.currentDay) // ���� ���� ��¥�� �ý����� ��¥�� ���� �ʴٸ� = ���� �����ٸ�.
            {
                // Ȯ�������� �������� �����϶� = (������ : ��������).
                // ���������� �ڱⰡ �˾Ƽ� ��¥�� �ް� �����Ѵ�.
                currentDate = gameManager.currentDay;
            }

            if (currentSeason != gameManager.currentSeason) // ������ �ٲ���ٸ�
            {
                // Ȯ�������� ���� ������ �����Ѵ�.
                currentSeason = gameManager.currentSeason;
            }
        }
    }
}
