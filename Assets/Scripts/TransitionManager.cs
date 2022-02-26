using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{


    public void ToMenu()
    {
        SceneManager.LoadScene("Menu_Scene", LoadSceneMode.Single);
    }
    public void ToPlatformer()
    {
        SceneManager.LoadScene("BBBTest", LoadSceneMode.Single);
    }

    public void ToSpace()
    {
        SceneManager.LoadScene("space", LoadSceneMode.Single);
    }

    public void ToTanks()
    {
        SceneManager.LoadScene("Tank_Test", LoadSceneMode.Single);
    }

    public void ToCredit()
    {
        SceneManager.LoadScene("Tank_Test", LoadSceneMode.Single);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}