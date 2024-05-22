using UnityEditor.SearchService;
using UnityEngine;
using Random = UnityEngine.Random;
public class StoneLand : MonoBehaviour // ���ǿ� ���� ���� �ڽ� ������Ʈ�� �����Ѵ�.
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] stonePrefabs;

    [SerializeField] bool makeOre;  // ä����, ������ ������ �����Ǵ� ��ҿ��� Ŵ.
    [SerializeField] bool makeStone; // ���� �����Ǿ�� �ϴ� ��ҿ��� Ų��.
    
    
    int currentDate;
    int currentSeason;
    private void Awake() // ���� ������ �� �ʱ�ȭ
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentDate = gameManager.currentDay;
        currentSeason = gameManager.currentSeason;
    }

    private void Update()
    {
        if (makeOre)
        {
            MakeOre();
        }
        if (makeStone)
        {
            MakeStone();
        }
    }
    public void MakeOre() // ���� ������ ������ ����� �޼��� : 
    {
        // �������� ������ ���� �����Ѵ�.
        if (true) //�� ���� �ε�Ǿ��ٸ�
        {
            // ���� ������ ���� �ٸ� �������� Ȯ���� ���� �����Ѵ�. (��: ���� 100���̸� ����:10%, �� 30%).
            // �� �����յ��� �ܼ��ϰ� �μ����� �������� ����ϴ°͸� ����.
        }
    }

    public void MakeStone()
    { 
        if (transform.childCount == 0) // LandController�� �ڽ��� ���ٸ�.
        {
            if (currentSeason != gameManager.currentSeason) // ������ �ٲ���ٸ�
            {
                int i = Random.Range(1, 100);
                if (i > 90) // 10%�� Ȯ����
                {
                    // Instantiate(); // �������� �����Ѵ�.
                }
                // Ȯ�������� �������� �����϶� = (������ : ��1, ��2).
                currentSeason = gameManager.currentSeason;
            }
        }
        else if (transform.childCount > 0) // LandController�� �ڽ��� �ִٸ�
        {
            // �ڽ� ������Ʈ�� ���� Component�� �Ǵ��ؼ� ��� ���� ��ȯ�Ѵ�
        }
    }
}
