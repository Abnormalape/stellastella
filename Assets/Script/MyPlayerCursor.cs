using UnityEngine;
using UnityEngine.UI;

class MyPlayerCursor : MonoBehaviour
{
    public int itemID;
    public int itemCounts;
    public int itemGrade;
    public bool itemOnHand = false;
    Camera mainCamera;

    bool buildingGrid;

    GameObject buildGrid;
    GameObject gridImage;

    public string instBuildingName;

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

    private void Awake()
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
}

