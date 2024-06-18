using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFullScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
    }
}
