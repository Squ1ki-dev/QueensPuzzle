using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using Tools.PlayerPrefs;
using Tools.Reactive;

public class GameSaves : Singleton<GameSaves>
{
    public int LoadCurrentLevel(string key) => PlayerPrefsPro.Get<int>(key);
    public void SaveLevel(string key, int value) => PlayerPrefsPro.Set(key, value);
    readonly public AutoSaverList<LevelModel> levelModels = new AutoSaverList<LevelModel>(nameof(levelModels));
}