using UnityEngine;
using UnityEngine.SceneManagement;

public class OnExitLevel : MonoBehaviour
{
    public string sceneName;
    public bool isNextScene = true;

    [SerializeField]
    public SceneInfo sceneInfo;

    void OnTriggerEnter2D(Collider2D player)
    {
        sceneInfo.isNextScene = isNextScene;
        SceneManager.LoadScene(sceneName);
    }
}
