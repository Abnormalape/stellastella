using UnityEngine;




class PlayCamera : MonoBehaviour
{   //PlayCamera는 CameraManager에서 정보들을 받아와 오기만 한다.
    public GameObject followObject; // CameraManager에서 따라다닐 오브젝트를 설정한다.
    CameraManager cameraManager;
    Vector2 cameraLimit_0;
    Vector2 cameraLimit_1;

    private void Awake()
    {
        cameraManager = FindFirstObjectByType<CameraManager>();
    }



    private void Update()
    {
        if (cameraManager.nowManagingCamera != GetComponent<Camera>())
        {   //현재 자신이 관리대상 카메라가 아니면
            if (GetComponent<Camera>().enabled == true)
            {
                GetComponent<Camera>().enabled = false;
            }
            return; // 종료한다.
        }


        if (cameraManager.nowManagingCamera == GetComponent<Camera>())
        {   //자신이 현재 관리대상 카메라이면. 
            GetComponent<Camera>().enabled = true;
            if (followObject == null)
            {   //따라다닐 오브젝트를 배정받지 않았다면.
                followObject = cameraManager.followObject; //자신이 따라다닐 오브젝트를 배정받는다.
            }
        }
        if (cameraLimit_0 != cameraManager.cameraLimit_0) // 카메라의 상한을 설정한다.
        {
            cameraLimit_0 = cameraManager.cameraLimit_0;
        }
        if (cameraLimit_1 != cameraManager.cameraLimit_1)
        {
            cameraLimit_1 = cameraManager.cameraLimit_1;
        }
        // 카메라의 위치는 따라다닐 오브젝트의 위치와 동일하다. 허나 카메라의 위치는 정해진 상한을 넘을 수 없다.
        if (followObject != null)
        {
            transform.position =
            new Vector3(CameraPositionX(followObject.transform.position.x), CameraPositionY(followObject.transform.position.y), -100f);
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






//Vector2 cameraLimit_0;
//Vector2 cameraLimit_1;
//[SerializeField] public nowLocation nowcamera;
//PlayerController pCon;
//GameManager gameManager;

//private void Awake()
//{
//    if(followObject == null)
//    {
//        followObject = GameObject.FindGameObjectWithTag("Player");
//    }
//    pCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
//    nowcamera = pCon.nowLocation;
//    gameManager = FindFirstObjectByType<GameManager>();
//}

//private void OnEnable()
//{
//    if (gameManager.needSubCam)
//    {
//        this.gameObject.SetActive(false);
//    }
//}

//private void Update()
//{d
//    nowcamera = pCon.nowLocation;
//    CameraLimit((int)nowcamera);
//    if (followObject != null)
//    {
//        this.transform.position =
//        new Vector3(CameraPositionX(followObject.transform.position.x), CameraPositionY(followObject.transform.position.y), -100f);
//    }
//}

//void CameraLimit(int i)
//{
//    switch (i)
//    {
//        case 0://농장.
//            cameraLimit_0 = new Vector2(-16, -10);
//            cameraLimit_1 = new Vector2(17, 10);
//            return;
//        case 1://마을.
//            cameraLimit_0 = new Vector2(38, -30);
//            cameraLimit_1 = new Vector2(100, 10);
//            return;
//        case 3: // 산.
//            cameraLimit_0 = new Vector2(-1300, -1400);
//            cameraLimit_1 = new Vector2(1500, 1900);
//            return;
//        case 4:
//            cameraLimit_0 = new Vector2(-56, -60);
//            cameraLimit_1 = new Vector2(2, -29);
//            return;
//        case 5://집안.
//            cameraLimit_0 = new Vector2(13, 14);
//            cameraLimit_1 = new Vector2(15, 19);
//            return;
//        case 6://잡화점.
//            cameraLimit_0 = new Vector2(63, 13);
//            cameraLimit_1 = new Vector2(65, 22);
//            return;
//        case 7:
//            cameraLimit_0 = new Vector2(100.5f , 17);
//            cameraLimit_1 = new Vector2(100.5f , 19);
//            return;
//    }
//}

//float xx;
//float CameraPositionX(float x)
//{

//    if (x < cameraLimit_0.x)
//    {
//        xx = cameraLimit_0.x;
//    }
//    else if (x > cameraLimit_1.x)
//    {
//        xx = cameraLimit_1.x;
//    }
//    else
//    {
//        xx = x;
//    }
//    return xx;
//}

//float yy;
//float CameraPositionY(float y)
//{
//    if (y < cameraLimit_0.y)
//    {
//        yy = cameraLimit_0.y;
//    }
//    else if (y > cameraLimit_1.y)
//    {
//        yy = cameraLimit_1.y;
//    }
//    else
//    {
//        yy = y;
//    }
//    return yy;
//}