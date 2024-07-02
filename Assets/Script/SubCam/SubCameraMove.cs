using UnityEngine;

class SubCameraMove : MonoBehaviour
{
    GameObject DownCol;
    GameObject UpCol;
    GameObject LeftCol;
    GameObject RightCol;
    GameManager manager;
    [SerializeField] int cameraPlace;

    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        DownCol = transform.Find("Down").gameObject;
        UpCol = transform.Find("Up").gameObject;
        LeftCol = transform.Find("Left").gameObject;
        RightCol = transform.Find("Right").gameObject;

        manager = FindFirstObjectByType<GameManager>();

        if (manager.needSubCam)
        {
            Camera.main.GetComponent<PlayerCamera>().followObject = this.gameObject;
        }
        else
        {
            this.gameObject.SetActive(false);
        }

        CameraLimit(0);
    }

    private void Update()
    {
        if (DownCol.GetComponent<SubCamChild>().IsMouseOn == true)
        {
            transform.position += Vector3.down * Time.deltaTime * 3;
        }
        if (UpCol.GetComponent<SubCamChild>().IsMouseOn == true)
        {
            transform.position += Vector3.up * Time.deltaTime * 3;
        }
        if (LeftCol.GetComponent<SubCamChild>().IsMouseOn == true)
        {
            transform.position += Vector3.left * Time.deltaTime * 3;
        }
        if (RightCol.GetComponent<SubCamChild>().IsMouseOn == true)
        {
            transform.position += Vector3.right * Time.deltaTime * 3;
        }

        transform.position = new Vector3(CameraPositionX(transform.position.x), CameraPositionY(transform.position.y), -100f);
    }

    Vector2 cameraLimit_0;
    Vector2 cameraLimit_1;
    public void CameraLimit(int i)
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

    float xx;
    float CameraPositionX(float x)
    {

        if (x < cameraLimit_0.x)
        {
            xx = cameraLimit_0.x;
        }
        else if (x > cameraLimit_1.x)
        {
            xx = cameraLimit_1.x;
        }
        else
        {
            xx = x;
        }
        return xx;
    }

    float yy;
    float CameraPositionY(float y)
    {
        if (y < cameraLimit_0.y)
        {
            yy = cameraLimit_0.y;
        }
        else if (y > cameraLimit_1.y)
        {
            yy = cameraLimit_1.y;
        }
        else
        {
            yy = y;
        }
        return yy;
    }
}