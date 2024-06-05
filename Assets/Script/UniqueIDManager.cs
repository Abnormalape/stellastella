using UnityEngine;

public class UniqueIDManager : MonoBehaviour
{
    private static UniqueIDManager uniqueIDManager; //정적.

    private string uniqueID; // 고유ID

    // 외부에서 인스턴스에 접근하는 정적 메서드
    public static UniqueIDManager UNIQUEIDManager
    {
        get
        {
            // 인스턴스가 없으면 새로 생성
            if (uniqueIDManager == null)
            {
                uniqueIDManager = new GameObject("UniqueIDManager").AddComponent<UniqueIDManager>();
                DontDestroyOnLoad(uniqueIDManager.gameObject);
            }
            return uniqueIDManager;
        }
    }

    private void Awake()
    {
        uniqueID = this.GetHashCode().ToString();
    }
    public string UniqueID()
    {
        return uniqueID;
    }
}
