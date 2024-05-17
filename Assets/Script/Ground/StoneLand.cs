using UnityEngine;
public class StoneLand : MonoBehaviour // ���ǿ� ���� ���� �ڽ� ������Ʈ�� �����Ѵ�.
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] stonePrefabs;

    int currentDate;
    int currentSeason;
    private void Awake() // ���� ������ �� �ʱ�ȭ
    {
        currentDate = gameManager.currentDay;
    }

    private void Update()
    {
        // �� ���� �������� ���������� ���� �Ʒ� �޼��带 �����Ѵ�
    }
    public void InDungeon()
    {
        // �������� ������ ���� �����Ѵ�.
        if (true) //�� ���� �ε�Ǿ��ٸ�
        {
            // ���� ������ ���� �ٸ� �������� Ȯ���� ���� �����Ѵ�. (��: ���� 100���̸� ����:10%, �� 30%).
            // �� �����յ��� �ܼ��ϰ� �μ����� �������� ����ϴ°͸� ����.
        }
    }

    public void InSideFarm()
    {
        // �� ������ �ƹ��͵� ���ٸ�
        if (transform.childCount == 0)
        {
            if (currentSeason != gameManager.currentSeason) // ������ �ٲ���ٸ�
            {
                // Ȯ�������� �������� �����϶� = (������ : ��1, ��2).
                currentSeason = gameManager.currentSeason;
            }
        }
    }
}
