using UnityEditor.SearchService;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    // �� �̸� ��������
    SlimeMovement SlimeMovement;
    SlimeData SlimeData;
    private void Awake()
    {
        SlimeData = new SlimeData("NormalMine_1"); // �� �̸� �ֱ�
    }
    private void Update()
    {
        
    }
}
