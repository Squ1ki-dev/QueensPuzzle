using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : AnimatedWindowBase
{
    [SerializeField] private Button nextBtn, replayBtn, menuGameBtn;
    private string menuScene = "Menu";
    public void Show(LevelModel model)
    {
        //if(model == null) return;
        
        nextBtn.SetActive(model.isWon);
        nextBtn.OnClick(() =>
        {
            ++model.levelIdx;
            GridCreation.Instance.RespawnGrid();
        });
        replayBtn.OnClick(() =>
        {
            GridCreation.Instance.RespawnGrid();
        });
        menuGameBtn.OnClick(() =>
        {
            WindowManager.Instance.CloseAll();
            SceneManager.LoadScene(menuScene);
        });
    }
}