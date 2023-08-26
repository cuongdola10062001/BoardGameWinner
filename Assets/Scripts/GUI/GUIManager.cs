using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager>
{
    public PauseDialog pauseDialog;
    public GamewinDialog gamewinDialog;

    string namePlayerWin = "";

    public void SetNamePlayerWin(string txt)
    {
        namePlayerWin = txt;
    }

    public string GetNamePlayerWin()
    {
        return namePlayerWin;
    }

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        base.Start();
    }
}
