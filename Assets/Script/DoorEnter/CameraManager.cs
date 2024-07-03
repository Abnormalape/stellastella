using UnityEngine;
public enum nowLocation
{
    Farm,
    Town,
    Beach,
    Mountain,
    Forest,
    FarmHouse,
    GroceryStore,
    RobinHouse,
}
class CameraManager : MonoBehaviour
{   //즉 카메라가 비추는 대상이나 장소들을 바꾸고 싶다면.
    //followObject, nowCamera, nowManagingCamera를 재 설정한다.
    public GameObject followObject;
    public Vector2 cameraLimit_0 { get; private set; }
    public Vector2 cameraLimit_1 { get; private set; }

    private nowLocation tNowLocation;
    [SerializeField] public nowLocation nowcamera 
    { 
        get { return tNowLocation; }
        set 
        { 
            tNowLocation = value;
            CameraLimit((int)value);
        }
    }

    GameManager gameManager;
    public Camera nowManagingCamera; // 사용할 카메라이다.
    public bool needCubCam;

    //기본적으로는 플레이어를 따라다니는 카메라를 사용하며, 카메라의 상한을 nowcamera에 의존한다.
    //특수한 상황이 되면 플레이어를 따라다니느 카메라를 비활성화 하며, 카메라의 상한은 nowcamera에 의존한다.

    private void Awake()
    {
        if (nowManagingCamera == null) // 관리할 카메라를 설정한다(기본 : 메인카메라.).
        {
            nowManagingCamera = Camera.main;
        }
        if (followObject == null) // 기본적으로 플레이어를 따라다닌다.
        {
            followObject = GameObject.FindGameObjectWithTag("Player");
        }
        nowcamera = nowLocation.FarmHouse; // 최초의 카메라 위치는 농장건물이다.
        gameManager = FindFirstObjectByType<GameManager>(); // 게임매니저는 gamemanager이다.
        CameraLimit((int)nowcamera); // 카메라의 상한을 설정한다.
        //==>a 해당 정보들을 관리할 카메라에 전송한다. 관리할 카메라는 PlayCamera라는 component를 가진다.
    }

    private void Update()
    {
        if (nowManagingCamera == null)
        {
            nowManagingCamera = Camera.main;
        }
    }
    void CameraLimit(int i)
    {
        switch (i)
        {
            case 0://농장.
                cameraLimit_0 = new Vector2(-16, -10);
                cameraLimit_1 = new Vector2(17, 10);
                return;
            case 1://마을.
                cameraLimit_0 = new Vector2(38, -30);
                cameraLimit_1 = new Vector2(100, 10);
                return;
            case 3: // 산.
                cameraLimit_0 = new Vector2(-1300, -1400);
                cameraLimit_1 = new Vector2(1500, 1900);
                return;
            case 4:
                cameraLimit_0 = new Vector2(-56, -60);
                cameraLimit_1 = new Vector2(2, -29);
                return;
            case 5://집안.
                cameraLimit_0 = new Vector2(13, 14);
                cameraLimit_1 = new Vector2(15, 19);
                return;
            case 6://잡화점.
                cameraLimit_0 = new Vector2(63, 13);
                cameraLimit_1 = new Vector2(65, 22);
                return;
            case 7:
                cameraLimit_0 = new Vector2(100.5f, 17);
                cameraLimit_1 = new Vector2(100.5f, 19);
                return;
        }
    }
}