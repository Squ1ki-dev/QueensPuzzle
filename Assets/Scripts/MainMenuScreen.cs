using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScreen : WindowBase
{
    [SerializeField] private Button  playBtn;
    public void Show()
    {
        SubscribeBtn(playBtn, 1);

        void SubscribeBtn(Button btn, int index)
        {
            btn.OnClick(() => SceneManager.LoadScene(index));
        }
    }
}
