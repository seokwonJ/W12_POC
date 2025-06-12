using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HireCharacter : MonoBehaviour // ���� ���� �� ���� �� �� ���
{
    [SerializeField] private GameObject hireCanvas;
    [SerializeField] private ShopHireItem[] characterOptions;

    private void Start()
    {
        if (Managers.PlayerControl.Characters.Count >= 4) // ���ᰡ �� ���� ������� ���� (�ذ� ���� �ִ�ġ ������ �ִٸ� �� �κ� ���� �ʿ�)
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
