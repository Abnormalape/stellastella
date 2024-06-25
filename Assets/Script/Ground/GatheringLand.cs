using UnityEngine;
using Random = UnityEngine.Random;
public enum Region
{
    Mountain,
    Forest,
    Beach,
}
public class GatheringLand : MonoBehaviour
{
    
    [SerializeField] GameManager gameManager;
    
    [SerializeField] public Region region; // ���� �������� ������ gathering�� �����ȴ�.
    [SerializeField] int[] SpringGathering;
    [SerializeField] int[] SummerGathering;
    [SerializeField] int[] FallGathering;
    [SerializeField] int[] WinterGathering;
    int[] nowGathering;
    public int gatherID;

    [SerializeField] GameObject gatheringPrefab;

    LandControl LandControl;

    [SerializeField] int currentDate;
    [SerializeField] int currentMonth;
    private void Awake() // ���� ������ �� �ʱ�ȭ
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentDate = gameManager.currentDay;
        currentMonth = gameManager.currentMonth;

        landControl = GetComponent<LandControl>();

        landControl.OnAValueUpdated += HandleAValueUpdated;
        landControl.OnBValueUpdated += HandleBValueUpdated;
    }
    private void Start()
    {

    }
    private void Update()
    {

    }



    LandControl landControl;
    bool monthChanged;
    bool dayChanged;

    void HandleAValueUpdated(bool newValue)
    {
        monthChanged = newValue;
        MakeGathering();
    }

    void HandleBValueUpdated(bool newValue)
    {
        dayChanged = newValue;
    }
    

    GameObject summonedGathering;
    public string prefabPath = "Prefabs/GatherLand/GatheringObject";
    
    private void MakeGathering()
    {
        if(monthChanged)
        {
            RemoveChildObject();
            RemakeGatheringPrefabs();
            monthChanged = false;
        }
    }

    private void RemakeGatheringPrefabs()
    {
        switch (gameManager.currentSeason) //season = month - 1;
        {
            case 0:
                nowGathering = SpringGathering;
                break;
            case 1:
                nowGathering = SummerGathering;
                break;
            case 2:
                nowGathering = FallGathering;
                break;
            case 3:
                nowGathering = WinterGathering;
                break;
        }
    }

    private void RemoveChildObject()
    {
        if (transform.childCount != 0 && GetComponentInChildren<GatheringObject>())
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    GatheringLand[] otherGathering;
    public void SummonGathering()
    {
        //������ �����ϴ� ä������ ���� Ȯ���ؼ� ������(n) ���ϸ� 0~3 ���� nowGathering���� �ϳ��� �����Ѵ�.
        //�������� ä������ ���� ������(n) ���Ͽ��� �Ѵ�.
        //������ ���� �ؾ� �ϴ� gamemanager���� �����ϴ� ���� ����.
        //���� �ٲ�� ������ �ִ� �ڽĵ��� ���� �����Ѵ�.
        //�ڽ� ������Ʈ�� ��Ŭ������ ä���ϰ�.
        //��� ��� ���� ������ ��Ŭ���� �ı��ȴ�.
        //�ڽ� ������Ʈ�� script �̸��� GatheringObject�̴�.
        //��ȯ�ϰ� �����հ�θ� �����Ѵ�.

        int i = Random.Range(0, nowGathering.Length);

        GameObject childObject = Instantiate(gatheringPrefab, this.transform.position, Quaternion.identity, this.transform);
        childObject.GetComponent<GatheringObject>().itemID = nowGathering[i];
        gatherID = nowGathering[i];
    }
}
