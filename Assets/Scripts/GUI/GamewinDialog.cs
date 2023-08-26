using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamewinDialog : Dialog
{
    public Text namePlayer;

    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (namePlayer && GUIManager.Ins)
        {
            namePlayer.text = GUIManager.Ins.GetNamePlayerWin();
        }
    }

    public void Replay()
    {
        SceneManager.sceneLoaded += OnSceneLoadedEvent;
        SceneController.Ins.LoadCurrentScene();
    }

    private void OnSceneLoadedEvent(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoadedEvent;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
