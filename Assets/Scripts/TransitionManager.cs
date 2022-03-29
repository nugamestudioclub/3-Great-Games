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
        GameMemory.Instance.Load(GameId.Platformer);
        SceneManager.LoadScene("Platformer_Scene", LoadSceneMode.Single);
    }

    public void ToSpace()
    {
        GameMemory.Instance.Load(GameId.SpaceShooter);
        SceneManager.LoadScene("Space_Scene", LoadSceneMode.Single);
    }

    public void ToTanks()
    {
        GameMemory.Instance.Load(GameId.Tanks);
        SceneManager.LoadScene("Tank_Scene", LoadSceneMode.Single);
    }

    public void ToCredit()
    {
        SceneManager.LoadScene("Credits_Scene", LoadSceneMode.Single);
    }

    public void ToTankEnd()
    {
        SceneManager.LoadScene("Tank_Ending", LoadSceneMode.Single);
    }

    public void ToSpaceEnd()
    {
        SceneManager.LoadScene("Space_Ending", LoadSceneMode.Single);
    }

    public void ToPlatformerEnd()
    {
        SceneManager.LoadScene("Platformer_Ending", LoadSceneMode.Single);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}