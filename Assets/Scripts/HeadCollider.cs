using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollider : MonoBehaviour
{
    public GameObject water;
    public GameObject head;

    BoxCollider waterCol;
    BoxCollider headCol;

    // Start is called before the first frame update
    void Start()
    {
        waterCol = water.GetComponent<BoxCollider>();
        headCol = head.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waterCol.bounds.Intersects(headCol.bounds))
        {
            print("WINWINWINWINWIN");//WIN CONDITION, NEXT LEVEL
        }
    }
}
