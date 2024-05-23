using UnityEditor.SearchService;
using UnityEngine;
using Random = UnityEngine.Random;
public class StoneLand : MonoBehaviour // ���ǿ� ���� ���� �ڽ� ������Ʈ�� �����Ѵ�.
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] stonePrefabs;

    [SerializeField] bool makeOre;  // ä����, ������ ������ �����Ǵ� ��ҿ��� Ŵ.
    [SerializeField] bool makeStone; // ���� �����Ǿ�� �ϴ� ��ҿ��� Ų��.

    [SerializeField] int currentDate;
    [SerializeField] int currentMonth;
    private void Awake() // ���� ������ �� �ʱ�ȭ
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentDate = gameManager.currentDay;
        currentMonth= gameManager.currentMonth;
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
        if (currentMonth != gameManager.currentMonth) // ������ �ٲ�.
        {
            Debug.Log("season change detected");

            if (transform.childCount == 0) // LandController�� �ڽ��� ���ٸ�.
            {
                int i = Random.Range(0, 100);
                if (i >= 90) // 10%�� Ȯ����
                {
                    int j = Random.Range(0, 100);
                    if (j >= 50) { Instantiate(Resources.Load($"Prefabs/FieldStone/FieldStone1") as GameObject, this.transform.position, Quaternion.identity).transform.parent = this.transform; }
                    else { Instantiate(Resources.Load($"Prefabs/FieldStone/FieldStone2") as GameObject, this.transform.position, Quaternion.identity).transform.parent = this.transform; }
                }
            }
            currentMonth = gameManager.currentMonth;
        }
        else if (transform.childCount > 0) // LandController�� �ڽ��� �ִٸ�
        {
            // �ڽ� ������Ʈ�� ���� Component�� �Ǵ��ؼ� ��� ���� ��ȯ�Ѵ�
            currentMonth = gameManager.currentMonth;
        }
    }
}
