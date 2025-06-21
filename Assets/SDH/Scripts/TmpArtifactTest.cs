
using Unity.VisualScripting;
using UnityEngine;

public class TmpArtifactTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ((ArtifactTemplate<PlayerMove>)Managers.Artifact.ArtifactLists[1]).Subscribe();
        ((ArtifactTemplate<PlayerMove>)Managers.Artifact.ArtifactLists[1]).effect.Invoke(null);
    }
}
