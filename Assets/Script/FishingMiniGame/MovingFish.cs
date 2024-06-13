using UnityEngine;
using Random = UnityEngine.Random;
class MovingFish : MonoBehaviour
{
    FishDB fishDB;
    public int fishID;
    float fishdifficulty;
    

    float timepassed;

    float fishPosition(float i)
    {
        float ii = i;
        if(ii > 4.3f)
        {
            ii = 4.3f;
            return ii;
        }
        else if(ii< -4.44f)
        {
            ii = -4.44f;
            return ii;
        }
        else { return ii; }
    }

    int M;
    bool setfish = false;
    private void Update()
    {
        if (setfish == false && fishID != 0) {
            timepassed = 0;
            fishID = GetComponentInParent<FishingMiniGame>().fishID;
            fishDB = new FishDB(fishID);
            
            fishdifficulty = fishDB.fishDifficulty;
            fishUp();
            float NM = Random.Range(50f / fishdifficulty, 100f / fishdifficulty);
            Invoke("nextMoveReady", NM);
            //동작 1회 실행을 위해 조건을 끈다
            movementReady = false;

            setfish = true;
        }

        timepassed = timepassed + Time.deltaTime;
        if (movementReady)
        {   //동작이 준비 되었을때 동작을 준다 : 상승,하강,정지.
            M = Random.Range(0, 3);

            //다음 동작은 1초 ~ 2초 내에 실행된다.
            float NM = Random.Range(50f / fishdifficulty, 100f/fishdifficulty);

            Invoke("nextMoveReady", NM);
            
            //동작 1회 실행을 위해 조건을 끈다
            movementReady = false;
        }

        if (M == 0)
        {
            fishUp();
        }
        else if (M == 1)
        {
            fishDown();
        }
        else if (M == 2)
        {
            fishStay();
        }

        this.transform.localPosition = new Vector2(this.transform.localPosition.x, fishPosition(this.transform.localPosition.y));
    }

    private void fishUp()
    {   //물고기 상승 : 난이도가 높을수록 빠르게 상승.
        transform.localPosition = (Vector2)transform.localPosition + (Vector2.up * fishdifficulty /10f) * Time.deltaTime;
        
    }
    private void fishDown()
    {   //물고기 하강 : 난이도가 높을수록 빠르게 하강.
        transform.localPosition = (Vector2)transform.localPosition + (Vector2.down * fishdifficulty /10f) * Time.deltaTime;
        
    }
    private void fishStay()
    {   //물고기 정지 : 
        transform.localPosition = (Vector2)transform.localPosition;
        
    }

    bool movementReady = false;
    void nextMoveReady(){movementReady = true;}
}