using TMPro;
using UnityEngine;

public class ShopArtifactItm : ShopItem
{
    [SerializeField] private TextMeshProUGUI artifactTxt;
    private int artifactIdx;

    private void Start()
    {
        if (Managers.Artifact.IsFullArtifact) Destroy(gameObject); // ���̻� ���� ��Ƽ��Ʈ�� ���ٸ� �ı�

        do
        {
            artifactIdx = Random.Range(0, (int)EArtifacts.Length);
        }
        while (Managers.Artifact.Artifacts[artifactIdx]);

        Managers.Artifact.Artifacts[(int)artifactIdx] = true;
        artifactTxt.text = ((EArtifacts)artifactIdx).ToString() + "\n100��";
    }

    public override void BuyItem()
    {
        if (Managers.Status.Gold < 100) return;

        Managers.Status.Gold -= 100;

        Destroy(gameObject);
    }
}
