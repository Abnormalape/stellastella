using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ParseCsvFile
{
    public ParseCsvFile()
    {
        
    }
    public List<Dictionary<string, string>> ParseCsv(string csvContent)
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
}