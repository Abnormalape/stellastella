using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class BuildingWindow : MonoBehaviour
{
    //이노마가 판매할 모든 건축물의 데이터를 리스트에 저장.
    public List<Dictionary<string, string>> buildingData;
    public TextAsset buildingList;

    private PlayerController tPCon;
    public PlayerController pCon { get { return tPCon; } set { tPCon = value; pInven = pCon.GetComponent<PlayerInventroy>(); WhenDataIported(); } }
    public PlayerInventroy pInven;

    private void WhenDataIported()
    {
        buildingData = new ParseCsvFile().ParseCsv(buildingList.text);
        CanBuildList();
        AddMethodToButton();
        ShowPage();
    }

    List<int> canBuildIndex = new List<int>();//리스트의 번호.
    //플레이어의 현재 조건에 맞는 건축물들만 보여줄 리스트에 추가.
    private void CanBuildList()
    {
        for (int i = 0; i < buildingData.Count; i++)
        {   //Todo:
            int canBuild = 0;
            if (buildingData[i]["PlayerFarmOption"] == "" || FindBoolByStringAtClass(pCon, buildingData[i]["PlayerFarmOption"])) // PlayerControlOption 과 같은 이름의 bool값을 pCon에서 찾아서 
            {
                canBuild++;
            }
            if (buildingData[i]["Option2"] == "" || FindBoolByStringAtClass(pCon, buildingData[i]["Option2"]))
            {
                canBuild++;
            }
            if (buildingData[i]["Option3"] == "" || FindBoolByStringAtClass(pCon, buildingData[i]["Option3"]))
            {
                canBuild++;
            }
            if (buildingData[i]["Option4"] == "" || FindBoolByStringAtClass(pCon, buildingData[i]["Option4"]))
            {
                canBuild++;
            }
            if (canBuild == 4)
            {
                canBuildIndex.Add(i);
            }
        }

        for (int i = 0;i < canBuildIndex.Count; i++)
        {
            Debug.Log(buildingData[canBuildIndex[i]]["BuildingName"]); //선택된 건물들의 이름을 출력
        }
    }

    private bool FindBoolByStringAtClass(PlayerController playerController, string headerName)
    {
        PropertyInfo targetBool = typeof(PlayerController).GetProperty(headerName, BindingFlags.Public|BindingFlags.Instance);

        if (targetBool != null && targetBool.PropertyType == typeof(bool))
        {
            return (bool)targetBool.GetValue(playerController, null); //playercontroller의 instance의 headername과 일치하는 bool형 매개변수
            //의 값을 return.
        }
        else { return false; }
    }
    //이노마의 버튼들. 1.다음페이지 2.이전페이지 3.닫기 4.건설 => 각각 맞는 버튼에 할당.
    int currentPage = 0;
    private GameObject nextButton;
    private GameObject prevButton;
    private GameObject closeButton;
    private GameObject buildButton;
    private void AddMethodToButton()
    {
        nextButton = transform.Find("NextButton").gameObject;
        nextButton.GetComponent<Button>().onClick.AddListener(NextPage);
        prevButton = transform.Find("PrevButton").gameObject;
        prevButton.GetComponent<Button>().onClick.AddListener(PrevPage);
        closeButton = transform.Find("CloseWindow").gameObject;
        closeButton.GetComponent<Button>().onClick.AddListener(CloseWindow);
        buildButton = transform.Find("BuildButton").gameObject;
        buildButton.GetComponent<Button>().onClick.AddListener(BuildButton);
    }

    private void ShowPage() //Todo: currentpage에 맞게 페이지를 출력
    {   //페이지를 출력.
        if (currentPage == canBuildIndex.Count - 1)
        {nextButton.SetActive(false);}
        else { nextButton.SetActive(true); }
        if (currentPage == 0)
        {prevButton.SetActive(false);}
        else { prevButton.SetActive(true); }

        Debug.Log(buildingData[canBuildIndex[currentPage]]["BuildingName"]);
    }

    private void NextPage()
    {
        if (currentPage < canBuildIndex.Count - 1)
        {
            currentPage++;
            ShowPage();
        }
    }
    private void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage();
        }
    }
    private void CloseWindow()
    {
        pCon.Conversation(false);
        Destroy(this.gameObject);
    }
    private void BuildButton()
    {
        Debug.Log("BuildButton");
    }
    //플레이어의 인벤토리와 현재 보여주고 있는 건축물의 요구 자원을 비교해서 요구량에 비해 모자란 자원의 글자를 붉은색으로 설정.

    //버튼을 누르면 다른 건축물을 출력 보여줌.

    //esc나 취소 버튼 누르면 페이드와 함께 거래창 종료.

    //건설 버튼 누르면 페이드와 함께 농장 씬으로 변경.
}