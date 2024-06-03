using UnityEngine;


class PlayerCamera : MonoBehaviour
{
    enum nowLocation
    {
        Farm,
        Town,
        Beach,
        Mountain,
        Forest,
        FarmHouse,
    }

    GameObject followObject;
    Vector2 cameraLimit_0;
    Vector2 cameraLimit_1;
    [SerializeField] nowLocation nowcamera;
    private void Awake()
    {
        followObject = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        CameraLimit((int)nowcamera);
        this.transform.position = 
        new Vector3(CameraPositionX(followObject.transform.position.x), CameraPositionY(followObject.transform.position.y), -100f);
    }

    void CameraLimit(int i)
    {
        switch (i)
        {
            case 0:
                cameraLimit_0 = new Vector2(-16,-10);
                cameraLimit_1 = new Vector2(17, 10);
                return;
            case 5:
                cameraLimit_0 = new Vector2(13, 14);
                cameraLimit_1 = new Vector2(15, 19);
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
