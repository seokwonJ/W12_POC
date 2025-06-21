using TMPro;
using UnityEngine;

public class ShopArtifactCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI artifactTxt;
    public int ArtifactIdx => artifactIdx;
    private int artifactIdx;

    private void Start()
    {
        if (Managers.Artifact.IsFullArtifact) Destroy(gameObject);

        do
        {
            artifactIdx = Random.Range(0, Managers.Artifact.ArtifactLists.Count);
        }
        while (Managers.Artifact.ArtifactLists[artifactIdx].isPurchased); // �������� ���� ������ ä���

        artifactTxt.text = Managers.Artifact.ArtifactLists[artifactIdx].explain;
    }

    private void OnEnable()
    {
        if (Managers.Artifact.IsFullArtifact) Destroy(gameObject);

        do
        {
            artifactIdx = Random.Range(0, Managers.Artifact.ArtifactLists.Count);
        }
        while (Managers.Artifact.ArtifactLists[artifactIdx].isPurchased); // �������� ���� ������ ä���

        artifactTxt.text = Managers.Artifact.ArtifactLists[artifactIdx].explain;
    }
}