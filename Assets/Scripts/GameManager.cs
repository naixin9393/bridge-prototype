using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
