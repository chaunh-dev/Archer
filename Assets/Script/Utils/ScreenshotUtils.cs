using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenshotUtils : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!Directory.Exists("Screenshots"))
            {
                Directory.CreateDirectory("Screenshots");
            }
            string fileName = "Screenshots/" + Screen.width.ToString() + "x" + Screen.height.ToString() + "_" + Time.frameCount;
            ScreenCapture.CaptureScreenshot(fileName + ".png");
        }

#endif
    }
}
