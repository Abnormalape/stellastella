using UnityEngine;
using Random = UnityEngine.Random;
public class TreeLand : MonoBehaviour // ���������� ����Ѵ�.
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject myTreePrefab; // �ܺο�(������ ����), ������ ����.
    [SerializeField] GameObject[] treePrefabs; // ���ο�(3���� �⺻����)
    GameObject onMeTreePrefab; // ���ο�(���� �ڽ����� ������ �ִ� ������ ������)

    int currentDate;
    int currentMonth;

    public string prefabPath;

    [SerializeField]
    bool AtFarm = false;

    private void Awake() // ���� ������ �� �ʱ�ȭ
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentDate = gameManager.currentDay;
        currentMonth = gameManager.currentMonth;
    }

    private void Update()
    {
        if (currentDate != gameManager.currentDay || currentMonth != gameManager.currentMonth)
        {
            if (AtFarm)
            {   //���� ���ο� �޼���
                InSideFarmSummonTree();
                //���� ������ ������ ���� ���� �����Ǵ� ���� ������, �ֺ� ������ �������� �ڶ�� ������ �ִ�.
                //�ֺ� ������ �������� �ڶ����� �ش� ������ �� �ڽ����� �ش� ������ �����հ� ���� ������ ��ȯ�Ѵ�.
                //�׷��ٸ� 
            }
            else
            {   //���� �ܺο� �޼���
                OutSideFarmSummonTree();
                //���� �ܺο� ������ ���� ������ �ڶ��� ������, ������ ������¿� ü���� ����Ѵ�.
                //�׷��� �ܺο� ������ LandControl�� summonedprefab�� mytreeprefab�̴�. 
            }
        }

        
    }

    
    public void OutSideFarmSummonTree()
    {
        if (currentDate != gameManager.currentDay)
        {
            //��¥ ����ȭ�� �׻�.
            currentDate = gameManager.currentDay;

            if (transform.childCount == 0 && gameManager.currentMonth != 3)
            {   // �ڽ��� ���ٸ� 20% Ȯ���� 3�ܰ� ���� ����, �ܿ��� �ƴ�.
                int i = Random.Range(0, 100);
                if (i >= 80)
                {
                    // myTreePrefab �� �̹� unity���� ������ �Ϸ��ߴ�.
                    myTreePrefab.GetComponent<FieldTreeLand>().currentLevel = 2; // 0�ܰ�� ����.
                    Instantiate(myTreePrefab, this.transform.position, Quaternion.identity).transform.parent = this.transform;
                    //�ܺο� ������ �����տ� ������ �������� �̹� �����Ǿ� �ִ�.
                }
            }
        }
    }

    public void InSideFarmSummonTree()
    {
        //�ٴ��� Ȯ�������� �������� ����
        if (currentMonth != gameManager.currentMonth)
        {
            // ��¥ ����ȭ�� �׻�.
            currentMonth = gameManager.currentMonth;
            if (transform.childCount == 0)
            {
                // �������� �������� ��ȯ // �ΰ��� ���� 
                int R = Random.Range(0, 100);
                if (R >= 80)
                {
                    GameObject summonStick;
                    int i = Random.Range(0, 2);
                    if (i == 0) { summonStick = Resources.Load($"Prefabs/FieldTree/Stick{i + 1}") as GameObject; }
                    else { summonStick = Resources.Load($"Prefabs/FieldTree/Stick{i + 1}") as GameObject; }
                    Instantiate(summonStick, this.transform.position, Quaternion.identity).transform.parent = this.transform;
                    prefabPath = $"Prefabs/FieldTree/Stick{i + 1}";
                }
            }
        }
        //���� Ȯ�������� ���ѻ���
        else if (currentDate != gameManager.currentDay)
        {
            // ��¥ ����ȭ�� �׻�.
            currentDate = gameManager.currentDay;
            // �ڽ� ������Ʈ�� �ְ�, ������ �ܿ��� �ƴϸ�, �ڽĿ�����Ʈ�� "�ʵ�Ʈ�� ������Ʈ"��ũ��Ʈ �� �ִٸ�.
            if (transform.childCount != 0 && gameManager.currentMonth != 3 && transform.GetComponentInChildren<FieldTreeLand>() != null)
            {
                // �׷��� �� �ڽ� ������Ʈ�� ����ܰ谡 4(����)�̶��
                if (transform.GetComponentInChildren<FieldTreeLand>().currentLevel == 4)
                {
                    //Ȯ��������. 
                    int i = Random.Range(0, 100);
                    if (i >= 80)
                    {
                        // ��ó 8ĭ�߿� �ڽ��� ������, ������Ʈ�� TreeLand ��ũ��Ʈ�� ���� ������Ʈ�� �ϳ��� ��� ������ ��ȯ�Ѵ�.
                        // ��ó ������Ʈ �ҷ����� -> �� �� Ư�� ������Ʈ �����ָ� �ҷ����� -> �� �� �ڽ��� ���� �ָ� ã��
                        // �³׵�� �迭 ���� ���� �ϰ� �ϳ��� �ҷ�����
                        // Instantiate�� FieldTreeObject�� ���� �������� level 0���� ����� �ҷ����� ��ó ������Ʈ�� �ڽ����� �־��ֱ�
                    }
                }
            }
        }

    }
}