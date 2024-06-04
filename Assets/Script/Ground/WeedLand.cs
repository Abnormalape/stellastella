using UnityEngine;
public class WeedLand : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] weedPrefabs;
    LandControl LandControl;

    [SerializeField] bool atFarm;

    [SerializeField] int currentDate;
    [SerializeField] int currentMonth;
    private void Awake() // ���� ������ �� �ʱ�ȭ
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentDate = gameManager.currentDay;
        currentMonth = gameManager.currentMonth;

        //LandControl = GetComponent<LandControl>();
    }

    private void Update()
    {
        if (atFarm == false)
        {
            OutSideFarm();
        }
        else if (atFarm == true)
        {
            InSideFarm();
        }

        //���� summonWeed�� �ʱ�ȭ ���� ���� ���� �̹Ƿ� LandControl���� summonWeed�� �����ϸ� �ȴ�.

        //if(summonWeed != null){
        //LandControl.SummonedPrefab(summonWeed);}
    }

    GameObject summonWeed;
    public string prefabPath;
    public void OutSideFarm()
    {
        if (currentDate != gameManager.currentDay)
        {
            //��¥ ����ȭ�� �׻�.
            currentDate = gameManager.currentDay;
            currentMonth = gameManager.currentMonth;
            if (transform.childCount == 0 && gameManager.currentMonth != 3)
            {   // ���� �ٱ����� ������ ���ʻ��� 60%
                int R = Random.Range(0, 100);
                if (R >= 40)
                {
                    int i = Random.Range(0, 2);
                    if (i == 0)
                    {
                        summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i + 1}") as GameObject;
                    }
                    else
                    {
                        summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i + 1}") as GameObject;
                    }
                    Instantiate(summonWeed, this.transform.position, Quaternion.identity).transform.parent = this.transform;
                    prefabPath = $"Prefabs/Weedprefabs/Weed{i + 1}";
                }
            }
        }
    }

    public void InSideFarm()
    {
        if (currentMonth != gameManager.currentMonth)
        {   // �޿��ѹ�
            // ��¥ ����ȭ�� �׻�.
            currentMonth = gameManager.currentMonth;
            currentDate = gameManager.currentDay;
            if (transform.childCount == 0)
            {
                int R = Random.Range(0, 100);
                if (R >= 80)
                {
                    GameObject summonWeed;
                    int i = Random.Range(0, 2);
                    if (i == 0)
                    {
                        summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i + 1}") as GameObject;
                        prefabPath = $"Prefabs/Weedprefabs/Weed{i + 1}";
                    }
                    else
                    {
                        summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i + 1}") as GameObject;
                        prefabPath = $"Prefabs/Weedprefabs/Weed{i + 1}";
                    }
                    Instantiate(summonWeed, this.transform.position, Quaternion.identity).transform.parent = this.transform;
                }
                return;
            }
        }
        //else if (currentDate !=  gameManager.currentDay)
        //{   // ������
        //    currentDate = gameManager.currentDay;
        //    if(transform.childCount == 0)
        //    {
        //        int R = Random.Range(0, 100);
        //        if (R >= 90)
        //        {
        //            GameObject summonWeed;
        //            int i = Random.Range(0, 2);
        //            if (i == 0) { summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i + 1}") as GameObject; }
        //            else { summonWeed = Resources.Load($"Prefabs/Weedprefabs/Weed{i + 1}") as GameObject; }
        //            Instantiate(summonWeed, this.transform.position, Quaternion.identity).transform.parent = this.transform;
        //        }
        //    }
        //}
    }



    //public LandData GetLandData()
    //{
    //    LandData data = new LandData();
    //    data.prefabPath = prefabPath;

    //    return data;
    //}
}
