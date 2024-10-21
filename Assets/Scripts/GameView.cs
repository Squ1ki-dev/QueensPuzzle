using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameView : MonoBehaviour
{
    [SerializeField] private TMP_Text levelTxt;
    private int levelIdx;

    void Start()
    {
        LevelModel model = new LevelModel(levelIdx);
        levelTxt.text = "Level: " + model.levelIdx.ToString();
    }
}
