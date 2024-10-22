using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Tools;

public class GameView : MonoBehaviour
{
    [SerializeField] private TMP_Text levelTxt;
    private int levelIdx;

    private void Start()
    {
        LevelModel model = new LevelModel(levelIdx);
        levelTxt.text = "Level: " + model.levelIdx.ToString();
        WindowManager.Instance.CloseAll();
    }
}
