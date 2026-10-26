using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Player.score = 0;
        SceneManager.LoadScene("Scenes/Level1");
    }
}
