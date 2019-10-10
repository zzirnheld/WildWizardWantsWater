using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RightHandPalette : MonoBehaviour
{
    public GameObject Palette;
    public GameObject Right_Hand;
    #region steamvractions
    public SteamVR_Action_Boolean paletteaction;
    #endregion steamvractions
    bool isSummoned = false;

    void Start()
    {
        paletteaction.onStateDown += OnPaletteDown;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void OnPaletteDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!isSummoned)
        {
            Palette.SetActive(true);
            print("innert");
            //Palette.transform.localScale = new Vector3(0F, 0F, 0F);
            //double i = 0;
            //while(i < 8){
            //Palette.transform.localScale += new Vector3(1F, 0.125F, 1F);
            //i = i + 1;
            //}
            Vector3 wandDir = transform.TransformDirection(Vector3.forward);
            Palette.transform.position = Right_Hand.transform.position + (Right_Hand.transform.forward * 2);//transform.TransformPoint(wandDir);//Right_Hand.transform.position + wandDir;
                                                                                                            //Palette.transform.rotation =  Quaternion.LookRotation(wandDir);
            Palette.transform.LookAt(Right_Hand.transform);
            Palette.transform.rotation *= Quaternion.Euler(90, 0, 0);
            Palette.transform.rotation *= Quaternion.Euler(0, 180, 0);

            isSummoned = true;
        }
        else
        {
            Palette.SetActive(false);
            isSummoned = false;
        }
    }
}
