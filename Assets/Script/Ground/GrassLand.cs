using UnityEngine;
public class GrassLand : MonoBehaviour // Ǯ �ڽ� ������Ʈ�� �����Ѵ�.
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] GrassPrefabs;

    int currentDate;
    int currentSeason;
    private void Awake() // ���� ������ �� �ʱ�ȭ
    {
        currentDate = gameManager.currentDay;
    }

    private void Update()
    {
        
    }
    public void OutSideFarm()
    {
        // ��� ��ó�� Ǯ�� ������ �׳� �ڶ���.
        if (true) // ��ó�� Ǯ�� �ִٸ�.
        {
            if (currentDate != gameManager.currentDay)
            {
                // ���� �ٲ� Ȯ�������� Ǯ �������� �����Ѵ�.
                currentDate = gameManager.currentDay;
            }
        }
    }

    public void InSideFarm()
    {
        // ��� ��ó�� Ǯ�� ������ �׳� �ڶ���.
        if (true) // ��ó�� Ǯ�� �ִٸ�.
        {
            if (currentDate != gameManager.currentDay)
            {
                // ���� �ٲ� Ȯ�������� Ǯ �������� �����Ѵ�.
                currentDate = gameManager.currentDay;
            }
        }
    }
}
