using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PrintMessageBox : MonoBehaviour // 대화 가능한 npc에게 삽입되는 class
{
    [SerializeField] GameObject MessageBox; // canvas까지 통째로 prefab화 할것.
    [SerializeField] Sprite Portrait; // 없거나 있거나. 있다면, 직접 넣을것.

    [SerializeField] TextAsset myDialogue;
    private List<Dictionary<string, string>> data;

    private GameObject[] SelectionButtons;
    ConversationChoiceManager conversationChoiceManager;

    private void Awake()
    {
        string csvContent = myDialogue.text;
        data = ParseCsv(csvContent);
        conversationChoiceManager = FindFirstObjectByType<ConversationChoiceManager>();
    }

    private void CallButtons(GameObject messageCanvas)
    {
        int buttonCounts = messageCanvas.GetComponentsInChildren<MessageBoxInteraction>().Length;
        MessageBoxInteraction[] mBI = messageCanvas.GetComponentsInChildren<MessageBoxInteraction>();

        Debug.Log(buttonCounts);

        SelectionButtons = new GameObject[buttonCounts];
        for (int i = 0; i < buttonCounts; i++)
        {
            SelectionButtons[i] = mBI[i].gameObject;
            SelectionButtons[i].SetActive(false);
        }
    }

    List<Dictionary<string, string>> ParseCsv(string csvContent)
    {
        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();

        // 줄 단위로 분리.
        string[] lines = csvContent.Split('\n');

        // 첫 열을 헤더로 사용.
        if (lines.Length <= 1) { return data; } // 데이터가 없을 경우 빈 리스트를 반환.

        // 첫 열을 쉼표로 나누어 헤더로 저장.
        string[] headers = lines[0].Split(',');

        // 각 줄마다 데이터를 쪼개고 저장.
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue; // 빈 줄은 건너뜁니다.

            // 쉼표로 필드를 구분. 큰따옴표 안의 쉼표는 무시.
            string[] fields = Regex.Split(lines[i], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            Dictionary<string, string> entry = new Dictionary<string, string>();

            for (int j = 0; j < headers.Length; j++)
            {
                // 키는 헤더 이름, 값은 해당 필드의 값.
                // 각 값의 큰따옴표를 제거하고, 공백을 제거.
                if (j == fields.Length)
                {
                    break;
                }
                entry[headers[j].Trim()] = fields[j].Trim('"').Trim();
            }

            data.Add(entry);
        }
        return data;
    }

    PlayerController pCon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "RightClick" && collision.transform.parent.tag == "Player")
        {
            pCon = collision.GetComponentInParent<PlayerController>();
            pCon.Conversation(true);

            FirstMessageSetting(); // 첫 메세지를 셋팅하는 단계. (judge);
            OpenMessageBox(); //메세지 창을 여는 단계.
            MessageBoxSizeLocation(); //메세지 창의 구성요소를 셋팅하는 단계.
        }
    }

    //데이터를 받아서 메세지 박스의 구성 요소를 만들고, 메세지 박스를 여는 메서드.
    GameObject MessageInstance;
    private void OpenMessageBox()
    {
        MessageInstance = Instantiate(MessageBox, Vector3.zero, Quaternion.identity, this.transform);
        CallButtons(MessageInstance);
    }

    private void MessageBoxSizeLocation(bool nowChoice = false)
    {
        //선택지 상태에선 선택에 따라 다른 결과를 보여야 한다.
        if (nowChoice == true)
        {   //선택지 상태일때 : 초상화 없음.
            MessageInstance.transform.Find("ChatBoxPortrait").gameObject.SetActive(false);
            //선택지의 갯수에 따라 대화 상자의 크기 변경.
            Vector3 instV3;

            instV3 = MessageInstance.transform.Find("ChatBoxTextBackGround").transform.localPosition;
            instV3 = new Vector3(0, instV3.y, instV3.z);
            MessageInstance.transform.Find("ChatBoxTextBackGround").transform.localPosition = instV3; // 위치 설정.

            instV3 = MessageInstance.transform.Find("ChatBoxText").transform.localPosition;
            instV3 = new Vector3(0, instV3.y, instV3.z);
            MessageInstance.transform.Find("ChatBoxText").transform.localPosition = instV3; // 위치 설정.

            //선택지 갯수 판정.
            if (choiceIndex.Length >= 4) // 선택지의 갯수가 4 이상이라면.
            {   //선택지를 length만큼 활성화 하고 활성화 된 선택지의 위치를 설정하라
                //선택지의 갯수 만큼 배경의 높이를 조정한다.
                MessageInstance.transform.Find("ChatBoxBackGround").GetComponent<RectTransform>().sizeDelta
                    = new Vector3(2800, 700 + 50 + (choiceIndex.Length - 4) * 150, 0); //큰 배경.
                MessageInstance.transform.Find("ChatBoxTextBackGround").GetComponent<RectTransform>().sizeDelta 
                    = new Vector3(2700, 600 + 50 + (choiceIndex.Length - 4) * 150, 0); //작은배경

                int height = -480 + ((choiceIndex.Length - 4) * 150 + 50);

                for (int i = 0; i < choiceIndex.Length; i++)
                {
                    SelectionButtons[i].SetActive(true); // 버튼을 활성화하고.
                    SelectionButtons[i].GetComponent<RectTransform>().sizeDelta = new Vector3(2650, 125, 0);
                    SelectionButtons[i].transform.localPosition = new Vector3(0, height - (150 * i), 0);
                    SelectionButtons[i].GetComponentInChildren<Text>().text = data[choiceIndex[i]]["Choices"];

                    

                }
            }
            else // 선택지 갯수가 2~3이라면.
            {   //선택지를 length만큼 만들고 선택지의 위치를 설정하라
                //배경의 높이를 고정 값으로 한다.

                for (int i = 0; i < choiceIndex.Length; i++)
                {
                    SelectionButtons[i].SetActive(true); // 버튼을 활성화하고.
                    SelectionButtons[i].GetComponent<RectTransform>().sizeDelta = new Vector3(2650, 125 ,0);
                    SelectionButtons[i].transform.localPosition = new Vector3(0, -480 - (150 * i), 0);
                    SelectionButtons[i].GetComponentInChildren<Text>().text = data[choiceIndex[i]]["Choices"];
                }
                MessageInstance.transform.Find("ChatBoxBackGround").GetComponent<RectTransform>().sizeDelta 
                    = new Vector3(2800, 700, 0); //큰 배경.
                //MessageInstance.transform.Find("ChatBoxTextBackGround").GetComponent<RectTransform>().sizeDelta 
                //    = new Vector3(2800, 700, 0); //작은배경
            }
            //위에서 이미지 제작과 텍스트 배치를 끝냈으니, 이젠 각 버튼별로 이벤트를 등록한다.
            for (int i = 0; i < choiceIndex.Length; i++)
            {
                if (data[choiceIndex[i]]["ChoiceEvent"] == "GoTo")
                {   //event가 goto인 경우 해당인덱스의 goto값중 하나를 받아 해당 값에 일치하는 메세지 박스를 출력한다.
                    
                }
                else
                {   //이외의 경우 해당 인덱스의 event와 같은 이름의 함수를 찾는다.

                }
            }
        }
        else
        {   //그 외: 초상화 있음.
            MessageInstance.transform.Find("ChatBoxPortrait").GetComponent<Image>().sprite = Portrait;
            //대화 상자의 크기 변경 없음.
            MessageInstance.transform.Find("ChatBoxText").GetComponent<Text>().text = data[SelectedIndex]["Dialogue"];

            JudgeNextMessage();//선택지가 없는 경우 바로 다음 문장을 체크한다.
        }

    }

    int SelectedIndex;
    private void FirstMessageSetting() // 조건에 맞는 Index를 선택한다.
    {
        //Set Text Start.
        List<int> mainIdIndex = new List<int>();
        int dataIndex = 0;
        foreach (var entry in data)
        {
            int trueCount = 0; //이값이 일정 수준이 된다면 해당 MainID저장.

            if (entry["MainID"] == null || entry["MainID"] == "") //메인 아이디가 없다면
            {
                dataIndex++;
                continue;
            }

            if (entry["Level"] == "1") //호감도와 상관 없거나 Todo: 필요 호감도가 현재 호감도보다 낮다면
            {
                trueCount++;
            }
            if (entry["Weather"] == "-1")
            {
                trueCount++;
            }
            if (entry["Season"] == "-1")
            {
                trueCount++;
            }
            if (entry["Time"] == "-1")
            {
                trueCount++;
            }
            if (entry["Place"] == "-1")
            {
                trueCount++;
            }
            if (entry["Date"] == "-1")
            {
                trueCount++;
            }
            if (entry["Year"] == "-1")
            {
                trueCount++;
            }
            if (entry["WeekNum"] == "-1")
            {
                trueCount++;
            }

            if (trueCount == 8) // 모든 조건이 참일때 현재 i (=dataindex)를 저장한다.
            {
                mainIdIndex.Add(dataIndex);
            }
            dataIndex++;
        }

        if (mainIdIndex.Count > 1) //조건을 만족하는 MainID가 2개 이상인 경우.
        {
            Debug.Log("2개 이상의 MainID");
            int R = Random.Range(0, mainIdIndex.Count);

            SelectedIndex = mainIdIndex[R];
        }
        else
        {
            SelectedIndex = mainIdIndex[0];
        }

        Debug.Log(data[SelectedIndex]["Dialogue"]);
        //=================Main Dia Print==============//

        //==============Ready For Next=================//
        JudgeNextMessage();
    }
    private void JudgeNextMessage() // 현재 문장을 데이터를 확인하는 메소드.
    {
        if (data[SelectedIndex]["End"] == "Y") //현재 대화문의 종료 대화문이라면.
        {   //이후 좌클릭을 받았을때 대화창을 닫는다.
            Debug.Log("종료되는 문장.");
            StartCoroutine(EndConversation());
        }
        else
        {
            Debug.Log("종료되지 않는 문장.");

            if (data[SelectedIndex]["ChoiceYN"] == "Y") // 선택지가 있는 문장일 경우
            {
                Debug.Log("선택지가 있는 문장.");
                StartCoroutine(ChoiceMessageSetting());
            }
            else if (data[SelectedIndex]["ChoiceYN"] == "N") // 선택지가 없는 문장일 경우
            {   //좌클릭을 받았을때 다음 대화문을 출력한다.
                Debug.Log("다음 문장 출력 대기.");
                StartCoroutine(NextMessageSetting());
            }
        }
    }

    //bool messageLoad = false;
    private IEnumerator EndConversation()
    {
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        while (Input.GetMouseButton(0))
        {
            yield return null;
        }

        Debug.Log("EndConversation");
        Destroy(MessageInstance);
        ////메세지 로드가 완료되지 않았다면.
        //if (messageLoad == false)
        //{
        //    //메세리 로드를 완료하고.
        //    Debug.Log("Messaged Text Loaded");
        //    messageLoad = true;
        //    //좌클릭 입력대기.
        //    while (!Input.GetMouseButton(0))
        //    {
        //        yield return null;
        //    }
        //    Debug.Log("EndConversation");
        //    Destroy(MessageInstance);
        //}
        ////메세지 로드가 완료되었다면.
        //else if (messageLoad == true)
        //{
        //    Debug.Log("EndConversation");
        //    Destroy(MessageInstance);
        //}
    }
    private IEnumerator NextMessageSetting()
    {
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        while (Input.GetMouseButton(0))
        {
            yield return null;
        }
        string[] GoSubIDstr = data[SelectedIndex]["GoTo"].Split(',');
        int[] GoSubID = new int[GoSubIDstr.Length];


        for (int i = 0; i < GoSubIDstr.Length; i++)
        {
            GoSubID[i] = Convert.ToInt32(GoSubIDstr[i]);
        }

        int randomSubID = Random.Range(0, GoSubID.Length);
        int selectedSubID = GoSubID[randomSubID];

        Debug.Log($"선택된 서브 아이디 : {selectedSubID}");

        int GotoIndex = -1;

        for (int i = 0; i < data.Count; i++)
        {
            if (data[i]["SubID"] == "")
            {
                continue;
            }

            if (data[i]["SubID"] == selectedSubID.ToString())
            {
                GotoIndex = i;
                break;
            }
        }
        SelectedIndex = GotoIndex;

        //선택지가 없는 문장인데 종료문장이 아니라면 무조건! 다음 문장이 있어야한다.
        if (SelectedIndex < 0) { Debug.LogError("다음 문장이 없습니다."); Debug.Log(SelectedIndex); yield break; }
        Debug.Log(data[SelectedIndex]["Dialogue"]);

        CloseAndOpenMessageBox();
    }

    int[] choiceIndex; //선택지들의 index.


    int choicerepeated;
    private IEnumerator ChoiceMessageSetting()
    {   //앞선 문장이 선택지를 가진 문장일때.
        choicerepeated++;
        Debug.Log("choicerepeated : " + choicerepeated); //Todo:
        while (!Input.GetMouseButton(0))
        {   //클릭을 기다렸다가.
            yield return null;
        }
        while (Input.GetMouseButton(0))
        {
            yield return null;
        }

        //선택지들을 불러와서 저장하고.
        int choiceIndexCount = Convert.ToInt32(data[SelectedIndex]["ChoiceIndex"]);
        choiceIndex = new int[choiceIndexCount];
        for (int i = 0; i < choiceIndexCount; i++)
        {
            choiceIndex[i] = SelectedIndex + i + 1;
        }

        //각 선택지에 event나 goto를 부여한다.

        //현재있는 대화창을 닫고, 새로 열어서, 구성품을 설정한다.
        CloseAndOpenMessageBox(true);
    }

    private void CloseAndOpenMessageBox(bool conbool = false) // 기본적으로 false가 들어가나 true가 필요할 때가 있다.
    {   
        CloseMessageBox();
        OpenMessageBox();
        MessageBoxSizeLocation(conbool); // 받아온 데이터로 그림을 그리고.
    }

    private void CloseMessageBox()
    {
        Destroy(MessageInstance);
    }
}