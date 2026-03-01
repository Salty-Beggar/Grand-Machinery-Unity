using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSManager : Manager
{
    private int _targetFPS = 60;

    public void SetupFPS() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = _targetFPS;
    }

    public override void Update_Event()
    {
        if (Application.targetFrameRate != _targetFPS) Application.targetFrameRate = _targetFPS;
    }
}
