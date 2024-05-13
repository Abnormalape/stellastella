using System;

class SlimeData : MonsterData
{
    public string slimeName;
    string appears; // 어디에 등장한 슬라임 인가? -> 이것에 따라 스탯및 색이 바뀜
    public string[] appearsArr;
    float colorR; //RGB색상
    float colorG;
    float colorB;
    int gender; // 암수 0이 암컷, 1이 수컷

    SlimeDB slimeSet;
   
    public SlimeData(string sceneName)  // 슬라임이 등장하는 씬의 이름에 따라서 슬라임의 속성이 결정된다
                                        // ex) 동굴 n층, 해골동굴, 슬라임 장, 봄 비밀의 숲, 가을 비밀의 숲, 등
    {
        appears = sceneName;
        if (appears.Contains("Mine"))
        {
            appearsArr = appears.Split('_');
            switch (appearsArr[0])
            {
                case "NormalMine":
                    if (Convert.ToInt32(appearsArr[1]) < 40) // 뒷자리는 숫자여야 한다
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
        else if (appears.Contains("Forest")) // 씬의 이름이 비밀의 숲일 경우
                                             // 씬에서 캔버스를 불러와 GameManager를 호출 한 후 계절을 판단
                                             // new gameManager = GameObject.Find("Canvas").GetComponent<GameManager>;
                                             // if (gameManager.season == "Spring") ...
        {
            //슬라임 이름을 호출
        }

        SlimeSetting();
    }

    protected void SlimeSetting()
    {
        slimeSet = new SlimeDB(slimeName);
    }
}