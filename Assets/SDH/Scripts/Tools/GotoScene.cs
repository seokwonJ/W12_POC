using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene : MonoBehaviour
{
    public void GotoStringScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
