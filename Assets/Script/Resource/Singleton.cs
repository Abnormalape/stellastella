using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindFirstObjectByType<T>();

                if (instance == null )
                {
                    Debug.LogError($"Singleton 오류: 씬에 {typeof(T)}가 없습니다.");
                }
            }
            return instance;
        }
    }
}
