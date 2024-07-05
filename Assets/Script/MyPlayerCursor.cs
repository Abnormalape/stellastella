using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class MyPlayerCursor : MonoBehaviour
{
    public int itemID;
    public int itemCounts;
    public int itemGrade;
    public bool itemOnHand = false;
    Camera mainCamera;
    //================================
    bool buildingGrid;
    GameObject buildGrid;
    GameObject gridImage;
    public string instBuildingName;
    GameManager gameManager;
    //================================
    private string tAnimalName;
    public string animalName
    {
        get { return tAnimalName; }
        set { tAnimalName = value;
            if (value != "") { myCollider.enabled = true; StartCoroutine(HoveringAnimal()); }
        }
    }


    public GameObject player;
    public void showBuildingGrid(bool input, int length, int height)
    {
        buildingGrid = input;

        GameObject grid = Instantiate(buildGrid, transform);
        GameObject firstGrid = null;

        for (int i = 0; i < length * height; i++)
        {
            if(i == 0)
            {
                firstGrid = Instantiate(gridImage, grid.transform) as GameObject;
            }
            else
            {
                Instantiate(gridImage, grid.transform);
            }
        }

        grid.GetComponent<GridLayoutGroup>().cellSize = Vector2.one;
        grid.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.GetComponent<GridLayoutGroup>().constraintCount = length;
        grid.GetComponent<BuildCursorControl>().buildingName = instBuildingName;
        instBuildingName = null;
        firstGrid.GetComponent<BuildCursorElement>().firstElement = true;
    }

    CircleCollider2D myCollider;
    private void Awake()
    {
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.enabled = false;
        gameManager = FindFirstObjectByType<GameManager>();

        if (Camera.main != null)
        {
            mainCamera = Camera.main;
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("SubCam"))
            {
                mainCamera = GameObject.FindGameObjectWithTag("SubCam").GetComponent<Camera>();
            }
        }

        if (buildGrid == null)
        {
            buildGrid = Resources.Load("Prefabs/BuildCanvas/BuildGrid") as GameObject;
        }
        if (gridImage == null)
        {
            gridImage = Resources.Load("Prefabs/BuildCanvas/GridImage") as GameObject;
        }
    }
    private void Update()
    {
        if (Camera.main != null)
        {
            mainCamera = Camera.main;
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("SubCam"))
            {
                mainCamera = GameObject.FindGameObjectWithTag("SubCam").GetComponent<Camera>();
            }
        }

        if (buildingGrid)
        {

        }

        this.transform.position = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y, this.transform.position.z);
    }

    private IEnumerator HoveringAnimal()
    {
        while(animalName != "")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                animalName = "";
                Invoke("GotoOriginScene",0.5f);
                yield break;
            }
            ShootRayWithAnimal();
            yield return null;
        }
    }

    GameObject touchedBuildLandObject;
    GameObject savedBuldingObject;
    private void ShootRayWithAnimal() // mouse enter와 exit을 관장하는...
    {
        bool foundCore = false;

        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] touches = Physics2D.OverlapPointAll(mousePosition);

        for (int ix = 0; ix < touches.Length; ix++)
        {
            if (touches[ix].GetComponent<BuildLandObject>())
            {
                if (touches[ix].GetComponent<BuildLandObject>().buildCore)
                {
                    touchedBuildLandObject = touches[ix].gameObject;
                    foundCore = true;
                    break;
                }
            }
        }

        if(!foundCore) { touchedBuildLandObject = null; }

        if(savedBuldingObject == null && touchedBuildLandObject == null)
        {

        }
        else if (savedBuldingObject != null && touchedBuildLandObject == null)
        {
            savedBuldingObject.GetComponent<BuildLandObject>().ResetColler();
        }
        else if (savedBuldingObject == null && touchedBuildLandObject != null)
        {
            touchedBuildLandObject.GetComponent<BuildLandObject>().JudgeAndColor(this);
        }
        else if (savedBuldingObject != null && touchedBuildLandObject !=null && savedBuldingObject == touchedBuildLandObject)
        {

        }
        else if (savedBuldingObject != null && touchedBuildLandObject != null && savedBuldingObject != touchedBuildLandObject)
        {
            savedBuldingObject.GetComponent<BuildLandObject>().ResetColler();
            touchedBuildLandObject.GetComponent<BuildLandObject>().JudgeAndColor(this);
        }

        if (touchedBuildLandObject == null) savedBuldingObject = null;
        else if (touchedBuildLandObject != null) savedBuldingObject = touchedBuildLandObject;
    }

    void GotoOriginScene()
    {
        //cameraManager.nowcamera = nowLocation.ManiHouse;
        player.SetActive(true);
        player.GetComponent<PlayerController>().Conversation(false);
        gameManager.currentSceneName = "InsideHouse";
        gameManager.needSubCam = false;
    }
}

