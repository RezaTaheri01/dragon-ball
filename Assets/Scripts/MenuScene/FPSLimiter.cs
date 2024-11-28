using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSLimiter : MonoBehaviour
{
    public int targetFPS = 30;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
    }
}

