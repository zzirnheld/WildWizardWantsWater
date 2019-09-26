using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RightHandPalette : MonoBehaviour
{
    #region steamvractions
    public SteamVR_Action_Boolean PaletteAction;
    #endregion steamvractions
    // Update is called once per frame
    void Update()
    {
        if (PaletteAction.state)
        {
            
        }
    }
}
