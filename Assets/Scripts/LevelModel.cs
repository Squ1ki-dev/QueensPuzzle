using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelModel
{
    public bool isWon;
    public int levelIdx;
    
    public LevelModel(int levelIdx)
    {
        this.levelIdx = levelIdx;
    }
}
