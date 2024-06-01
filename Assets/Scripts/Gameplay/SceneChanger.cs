using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public const string GAME_SCENE = "Game";
    public const string MENU_SCENE = "Menu";
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartGameButton()
    {
        ChangeScene(GAME_SCENE);
    }
    public void MenuButton()
    {
        ChangeScene(MENU_SCENE);
    }
}
