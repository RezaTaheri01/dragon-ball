using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSLimiter : MonoBehaviour
{
    public int targetFPS = 30;
    public TextMeshProUGUI FPSText;
    private float currentFPS;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
        InvokeRepeating("GetFPS", 1, 1);
    }

    void GetFPS(){
        currentFPS = (int) (1f / Time.unscaledDeltaTime);
        FPSText.text = currentFPS.ToString();
    }
}

