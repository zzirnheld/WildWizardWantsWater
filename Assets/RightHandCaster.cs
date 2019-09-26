using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandCaster : MonoBehaviour
{
    public const int starMask = 1 << 9;
    // RIP Start

    // Update is called once per frame
    void Update()
    {
        Vector3 wandDir = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, wandDir*100F);
        RaycastHit starHit;
        if (Physics.Raycast(transform.position, wandDir, out starHit, Mathf.Infinity, starMask))
        {
            Debug.Log(starHit.collider.gameObject);
                              //hitting star
        }
    }

}
