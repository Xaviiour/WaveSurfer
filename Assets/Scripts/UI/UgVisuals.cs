using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UgVisuals : MonoBehaviour
{
    static GameObject mainVol;
    static GameObject lowHVol;

    public static bool switchedMainAndLowH = false; // If main and lowH have been switched
    // Start is called before the first frame update
    void Start()
    {
        mainVol = transform.GetChild(0).gameObject;
        lowHVol = transform.GetChild(1).gameObject;
    }

    public static void SwitchMainAndLowH() 
    {
        lowHVol.SetActive(mainVol.activeInHierarchy);
        mainVol.SetActive(!mainVol.activeInHierarchy);
        switchedMainAndLowH = !switchedMainAndLowH;
    }
}
