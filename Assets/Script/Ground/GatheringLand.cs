using UnityEngine;
public class GatheringLand : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] GatheringPrefabs;
    int currentDate;
    int currentSeason = 0; // ���� �ʱ�ȭ�� ���� ���ڼ��� : ��¥�� 1���� ����.
    private void Awake() // ���� ������ �� �ʱ�ȭ
    {
        currentDate = gameManager.currentDay;
    }

    private void Update()
    {

    }
    public void OutSideFarm()   // ���� �ۿ��� ���� �ٲ� Ȯ�������� ä������ �����Ѵ�.
                                // ������ �ٲ�� ä������ ������ �޶�����.
    {
        if (currentSeason != gameManager.currentSeason) // ������ ����Ǹ�
        {
            currentSeason = gameManager.currentSeason; // ������ ������Ʈ �ϰ�
            switch (currentSeason)
            {
                case 1: // ��
                    //4���� ������ ����
                    break;
                case 2:
                    //4���� ������ ����
                    break;
                case 3:
                    //4���� ������ ����
                    break;
                case 4: // �ܿ�
                    //4���� ������ ����
                    break;

            }

            if (currentDate != gameManager.currentDay)
            {
                // ���� �ٲ� Ȯ�������� ������ ä���� �������� �����Ѵ�.
                currentDate = gameManager.currentDay;
            }
        }
    }
}
