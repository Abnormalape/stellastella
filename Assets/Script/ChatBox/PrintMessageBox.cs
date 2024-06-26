using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PrintMessageBox : MonoBehaviour // 대화 가능한 npc에게 삽입되는 class
{
    GameObject ChatBox; // canvas까지 통째로 prefab화 할것.
    [SerializeField] Sprite Portrait; // 없거나 있거나. 있다면, 직접 넣을것.

    [SerializeField] TextAsset myDialogue;
    private List<Dictionary<string, string>> data;

    private void Awake()
    {
        string csvContent = myDialogue.text;
        data = ParseCsv(csvContent);

        FirstMessageSetting();
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "RightClick" && collision.transform.parent.tag == "Player")
        {
            SetAndOpenMessageBox(collision);
        }
    }

    //데이터를 받아서 메세지 박스의 구성 요소를 만들고, 메세지 박스를 여는 메서드.
    private void SetAndOpenMessageBox(Collider2D collision)
    {
        GameObject MessageInstance = Instantiate(ChatBox, Vector3.zero, Quaternion.identity, this.transform);
        collision.transform.GetComponentInParent<PlayerController>().Conversation(true);

        //초상화 설정 및 대화문의 크기 위치 설정.
        if (Portrait == null) { MessageInstance.transform.Find("ChatBoxPortrait").gameObject.SetActive(false); }
        else { MessageInstance.transform.Find("ChatBoxPortrait").GetComponent<Image>().sprite = Portrait; }



    }

    private void FirstMessageSetting() // 조건에 맞게 메세지를 출력한다.
    {
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

            if (entry["Level"] == "-1") //호감도와 상관 없거나 Todo: 필요 호감도가 현재 호감도보다 낮다면
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

        int R = 0;
        if (mainIdIndex.Count > 1) //조건을 만족하는 MainID가 2개 이상인 경우.
        {
            R = Random.Range(0, mainIdIndex.Count);
        }

        Debug.Log(data[R]["Dialogue"]);
    }
}