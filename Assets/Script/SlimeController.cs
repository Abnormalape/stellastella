using UnityEditor.SearchService;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    // 씬 이름 가져오기
    SlimeMovement SlimeMovement;
    SlimeData SlimeData;
    private void Awake()
    {
        SlimeData = new SlimeData("NormalMine_1"); // 씬 이름 넣기
    }
    private void Update()
    {
        
    }
}
