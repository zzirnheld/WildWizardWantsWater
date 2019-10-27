using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadCollider : MonoBehaviour
{
    public static int LevelCount = 0;

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
    
    void Awake()
    {
        Debug.Log("im wide awake");
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
            Debug.Log("intersectlol");
            if (!slurpPlayed) {
                slurpPlayed = true;
                slurp.PlayOneShot(slurp.clip, 1F);
                light1ani.SetTrigger("fadeOut");
                light2ani.SetTrigger("fadeOut");
                print("WINWINWINWINWIN");//WIN CONDITION, NEXT LEVEL
                LevelCount++;
                SceneManager.LoadScene(LevelCount);
            }
        }
    }
}
