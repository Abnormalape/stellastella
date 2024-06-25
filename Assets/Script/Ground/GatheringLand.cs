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
    
    [SerializeField] public Region region; // 지역 내에서는 한정된 gathering이 생성된다.
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
    private void Awake() // 게임 시작할 때 초기화
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
        //리전에 존재하는 채집물을 수를 확인해서 일정수(n) 이하면 0~3 개의 nowGathering중의 하나를 생성한다.
        //생성후의 채집물의 수는 일정수(n) 이하여야 한다.
        //갯수를 관리 해야 하니 gamemanager에서 관리하는 것이 좋다.
        //달이 바뀌면 가지고 있던 자식들을 전부 제거한다.
        //자식 오브젝트는 우클릭으로 채집하고.
        //곡괭이 등과 같은 도구로 죄클릭시 파괴된다.
        //자식 오브젝트의 script 이름은 GatheringObject이다.
        //소환하고 프리팹경로를 저장한다.

        int i = Random.Range(0, nowGathering.Length);

        GameObject childObject = Instantiate(gatheringPrefab, this.transform.position, Quaternion.identity, this.transform);
        childObject.GetComponent<GatheringObject>().itemID = nowGathering[i];
        gatherID = nowGathering[i];
    }
}
