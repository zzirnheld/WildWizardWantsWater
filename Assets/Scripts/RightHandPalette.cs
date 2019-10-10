using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RightHandPalette : MonoBehaviour
{
    public GameObject Palette;
    public GameObject Right_Hand;
    #region steamvractions
    public SteamVR_Action_Boolean PaletteTwo;
    #endregion steamvractions
    bool isSummoned = false;
    // Update is called once per frame
    void Update()
    {
        if (PaletteTwo.state == false)
        {
            if (!isSummoned)
            {
                Palette.transform.localScale = new Vector3(0F, 0F, 0F);
                //double i = 0;
                //while(i < 8){
                    //Palette.transform.localScale += new Vector3(1F, 0.125F, 1F);
                    //i = i + 1;
                //}
                Vector3 wandDir = transform.TransformDirection(Vector3.forward);
                Palette.transform.position = Right_Hand.transform.position+(Right_Hand.transform.forward*2);//transform.TransformPoint(wandDir);//Right_Hand.transform.position + wandDir;
                //Palette.transform.rotation =  Quaternion.LookRotation(wandDir);
                Palette.transform.LookAt(Right_Hand.transform);
                Palette.transform.rotation *= Quaternion.Euler(90, 0, 0);
                isSummoned = true;
            }
            else
            {

                isSummoned = false;
            }
        }
    }
}
