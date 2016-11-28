using UnityEngine;
using System.Collections;

public class VideoSettings : MonoBehaviour {

    public void changeResolution(int value)
    {
        switch(value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            case 2:
                Screen.SetResolution(640, 480, Screen.fullScreen);
                break;
            case 3:
                Screen.SetResolution(480, 360, Screen.fullScreen);
                break;
        }
    }

    public void setFullscreen(bool enabled)
    {
        Screen.fullScreen = enabled;
    }
}
