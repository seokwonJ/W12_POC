using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HireCharacter : MonoBehaviour // 상점 시작 전 동료 한 명 고용
{
    [SerializeField] private GameObject hireCanvas;
    [SerializeField] private ShopHireItem[] characterOptions;

    private void Start()
    {
        if (Managers.PlayerControl.Characters.Count >= 4) // 동료가 다 차면 고용하지 않음 (해고나 동료 최대치 변경이 있다면 이 부분 수정 필요)
        {
            Destroy(hireCanvas);
            return;
        }

        float rate = 5f / transform.parent.localScale.x;

        for (int i = 0; i < characterOptions.Length; i++)
        {
            characterOptions[i].CharacterOptionIdx = Random.Range(0, Managers.Asset.Characters.Length);
        }
    }
}
