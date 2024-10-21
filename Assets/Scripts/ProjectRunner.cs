using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class ProjectRunner : MonoBehaviour
{
    private void Start()
    {
        WindowManager.Instance.Show<MainMenuScreen>().Show();
    }
}
