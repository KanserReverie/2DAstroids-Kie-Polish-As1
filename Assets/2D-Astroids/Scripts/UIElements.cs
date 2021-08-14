using UnityEngine;
using UnityEngine.SceneManagement;

public class UIElements : MonoBehaviour
{
    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}
