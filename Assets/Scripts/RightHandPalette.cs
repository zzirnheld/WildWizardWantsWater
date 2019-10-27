using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RightHandPalette : MonoBehaviour
{
    public GameObject Palette;
    public GameObject Right_Hand;

    public RightHandCaster rightHandCaster;
    #region steamvractions
    public SteamVR_Action_Boolean paletteaction;
    #endregion steamvractions
    bool isSummoned = false;

    void Start()
    {
        paletteaction.onStateDown += OnPaletteDown;
    }

    public void OnPaletteDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        rightHandCaster.spellCasting.EndAllSpells();
        if (!isSummoned)
        {
            SummonPalette();
        }
        else
        {
            UnsummonPalette();
        }
    }

    public void SummonPalette()
    {
        Palette.transform.parent = null;
        Palette.SetActive(true);
        Vector3 wandDir = transform.TransformDirection(Vector3.forward);
        Palette.transform.position = Right_Hand.transform.position + (Right_Hand.transform.forward * 1);//transform.TransformPoint(wandDir);//Right_Hand.transform.position + wandDir;
                                                                                                        //Palette.transform.rotation =  Quaternion.LookRotation(wandDir);
        Palette.transform.LookAt(Right_Hand.transform);
        Palette.transform.rotation *= Quaternion.Euler(90, 0, 0);
        Palette.transform.rotation *= Quaternion.Euler(0, 180, 0);

        isSummoned = true;
    }

    public void UnsummonPalette()
    {
        Palette.transform.parent = transform;
        rightHandCaster.ClearLinesSet();
        Palette.SetActive(false);
        isSummoned = false;
    }
}
