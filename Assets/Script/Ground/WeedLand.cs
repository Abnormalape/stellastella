using UnityEngine;
public class WeedLand : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] weedPrefabs;
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
        if (true)
        {
            if (currentDate != gameManager.currentDay)
            {
                // ���� �ٲ� Ȯ�������� ���� �������� �����Ѵ�.
                currentDate = gameManager.currentDay;
            }
        }
    }

    public void InSideFarm()
    {
        if (true)
        {
            if (currentSeason != gameManager.currentSeason)
            {
                // ������ �ٲ� Ȯ�������� Ǯ �������� �����Ѵ�.
                currentDate = gameManager.currentDay;
            }
        }
    }
}
