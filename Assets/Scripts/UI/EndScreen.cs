using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : WindowBase
{
    [SerializeField] private Button nextBtn, replayBtn, menuGameBtn;
    private GridConfig gridConfig;
    private string menuScene = "Menu";
    protected LevelModel model;

    public void Show()
    {
        nextBtn.OnClick(() =>
        {
            CompleteLevel(model);
            WindowManager.Instance.CloseAll();
            GridHandler.Instance.RespawnGrid();
        });
        replayBtn.OnClick(() =>
        {
            GridHandler.Instance.RespawnGrid();
            WindowManager.Instance.CloseAll();
        });
        menuGameBtn.OnClick(() =>
        {
            WindowManager.Instance.CloseAll();
            SceneManager.LoadScene(menuScene);
        });
    }

    public void CompleteLevel(LevelModel model)
    {
        Time.timeScale = 1;
        var saves = GameSaves.Instance;
        var currentLastLevel = saves.LoadCurrentLevel("Level");
        saves.SaveLevel("Level", currentLastLevel + 1);
    }
}