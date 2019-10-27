using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeleteRevealWater : MonoBehaviour
{
    public GameObject WaterToReveal;
    
    void OnDestroy()
    {
        WaterToReveal.SetActive(true);
    }
}
