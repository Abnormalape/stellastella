using UnityEngine;
using Random = UnityEngine.Random;
class PlayerFishingManager : MonoBehaviour
{   //게임 매니져랑 연계해서 날씨 계절 시간에 맞게 물고기ID를 발행해 PlayerFishingMinigame에 전달하는 역할
    GameManager gameManager;
    private void Awake()
    {   //gamemanager는 고유개체
        //playerfsingmanager는 플레이어 부착개체
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    int fishlistnumber = 0;
    public int GiveFishtoPlayerFishMiniGame()
    {
        MakeFishList();
        VoteFish();
        Debug.Log(votedFishID);
        return votedFishID;
    }

    int[] fishList;
    int fishListLength = 0;
    void MakeFishList()
    {
        //조건에 맞는 물고기의 리스트를 뽑아냄.

        //날씨, 계절 시간을 판단해 그에 맞는 물고기를 출력한다.
        //물고기DB의 모든 정보를 훑어서 현재 날씨, 계절, 시간, 장소에 맞는 애들만 배열에 추가한다.
        fishlistnumber = 0;
        fishListLength = 0;

        for (int i = 1; i < 11; i++)
        {
            FishDB fishDB = new FishDB(i);
            if (fishDB.weather[gameManager.weather] == true && //날씨
                fishDB.season[gameManager.currentSeason] == true && //계절
                fishDB.place[0] == true && //장소 (임시장소 = 강)
                fishDB.time[0] <= gameManager.currentHour && fishDB.time[1] > gameManager.currentHour) // 시간
            {
                fishListLength++;
            }
        }
        fishList = new int[fishListLength];

        int j = 0;
        for (int i = 0; i < 10; i++)
        {
            FishDB fishDB = new FishDB(i + 1);
            if (fishDB.weather[gameManager.weather] == true && fishDB.season[gameManager.currentSeason] == true && fishDB.place[0] == true &&
                fishDB.time[0] <= gameManager.currentHour && fishDB.time[1] > gameManager.currentHour) // 장소는 임시로 강으로 설정
            {
                fishList[j] = i + 1;
                Debug.Log($"{fishDB.fishName},{j}번째자리에 등록 성공!");
                j++;
            }
            else
            {
                Debug.Log($"{fishDB.fishName},등록 실패!");
            }
        }
        
        if(fishListLength > 0)
        {
            
            for(int i = 0; i < fishList.Length; i++)
            {
                FishDB asdf = new FishDB(fishList[i]);
                Debug.Log($"{i}번째 물고기의 이름은 : {asdf.fishName}");
            }
        }
    }
    int totalFishPercentage = 0;
    int[] eachFishPercentage;
    int votedFishID;
    void VoteFish()
    {   //뽑아진 리스트를 통해 물고기를 결정.
        eachFishPercentage = new int[fishList.Length];
        for (int i = 0; i < fishList.Length; i++)
        {
            FishDB fishDB = new FishDB(fishList[i]);
            totalFishPercentage = totalFishPercentage + fishDB.fishPercentage;
            eachFishPercentage[i] = totalFishPercentage;
        }

        int R = Random.Range(0, totalFishPercentage);
        for (int i = 0; i < fishList.Length; i++)
        {
            if (R < eachFishPercentage[i])
            {
                votedFishID = fishList[i];
                return;
            }
        }
    }
}