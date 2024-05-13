using System;

class SlimeData : MonsterData
{
    public string slimeName;
    string appears; // ��� ������ ������ �ΰ�? -> �̰Ϳ� ���� ���ȹ� ���� �ٲ�
    public string[] appearsArr;
    float colorR; //RGB����
    float colorG;
    float colorB;
    int gender; // �ϼ� 0�� ����, 1�� ����

    SlimeDB slimeSet;
   
    public SlimeData(string sceneName)  // �������� �����ϴ� ���� �̸��� ���� �������� �Ӽ��� �����ȴ�
                                        // ex) ���� n��, �ذ񵿱�, ������ ��, �� ����� ��, ���� ����� ��, ��
    {
        appears = sceneName;
        if (appears.Contains("Mine"))
        {
            appearsArr = appears.Split('_');
            switch (appearsArr[0])
            {
                case "NormalMine":
                    if (Convert.ToInt32(appearsArr[1]) < 40) // ���ڸ��� ���ڿ��� �Ѵ�
                    {
                        slimeName = "GreenSlime";
                    }
                    else if (Convert.ToInt32(appearsArr[1]) < 80)
                    {
                        slimeName = "BlueSlime";
                    }
                    else if (Convert.ToInt32(appearsArr[1]) < 120)
                    {
                        slimeName = "RedSlime";
                    }
                    return;
                case "DesertMine":
                    slimeName = "PurpleSlime";
                    return;
                case "StoneMine":
                    Random i = new Random();
                    int j = i.Next(1, 3);
                    if (j == 1)
                    {
                        slimeName = "DirtSlime";
                    }
                    else if (j == 2)
                    {
                        slimeName = "StoneSlime";
                    }
                    return;
            }
        }
        else if (appears.Contains("Forest")) // ���� �̸��� ����� ���� ���
                                             // ������ ĵ������ �ҷ��� GameManager�� ȣ�� �� �� ������ �Ǵ�
                                             // new gameManager = GameObject.Find("Canvas").GetComponent<GameManager>;
                                             // if (gameManager.season == "Spring") ...
        {
            //������ �̸��� ȣ��
        }

        SlimeSetting();
    }

    protected void SlimeSetting()
    {
        slimeSet = new SlimeDB(slimeName);
    }
}