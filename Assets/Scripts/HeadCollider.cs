using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollider : MonoBehaviour
{
    public GameObject light1;
    public GameObject light2;

    public Animator light1ani;
    public Animator light2ani;
    AudioSource slurp;
    public GameObject water;
    public GameObject head;
    bool slurpPlayed = false;

    BoxCollider waterCol;
    BoxCollider headCol;

    // Start is called before the first frame update
    void Start()
    {
        slurp = head.GetComponent<AudioSource>();
        light1ani = light1.GetComponent<Animator>();
        light2ani = light2.GetComponent<Animator>();
        waterCol = water.GetComponent<BoxCollider>();
        headCol = head.GetComponent<BoxCollider>();
        slurpPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (waterCol.bounds.Intersects(headCol.bounds))
        {
            if (!slurpPlayed) {
                slurpPlayed = true;
                slurp.PlayOneShot(slurp.clip, 1F);
                light1ani.SetTrigger("fadeOut");
                light2ani.SetTrigger("fadeOut");
                print("WINWINWINWINWIN");//WIN CONDITION, NEXT LEVEL
            }
        }
    }
}
