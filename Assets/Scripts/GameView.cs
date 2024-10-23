using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Tools;

public class GameView : MonoBehaviour
{
    public static GameView Instance { get; private set;}
    [SerializeField] private TMP_Text levelTxt;
    protected LevelModel model;

    private void Awake() => Instance = this;

    private void Start()
    {
        WindowManager.Instance.CloseAll();
        var saves = GameSaves.Instance;
        levelTxt.text = "Level: " + saves.LoadCurrentLevel("Level");
    }

    private void Update()
    {
        levelTxt.text = "Level: " + GameSaves.Instance.LoadCurrentLevel("Level");
    }
}
