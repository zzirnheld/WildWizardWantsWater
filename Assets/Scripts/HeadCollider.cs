
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadCollider : MonoBehaviour
{
    public static int LevelCount = 0;

    public bool WaterFlowing;

    public GameObject light1;
    public GameObject light2;

    public Animator light1ani;
    public Animator light2ani;
    public AudioSource slurp;
    public AudioClip slurpsound;
    public GameObject water;
    public GameObject head;
    public GameObject victorywater1 = null;
    public GameObject victorywater2 = null;
    public GameObject victorywater3 = null;
    bool slurpPlayed = false;

    BoxCollider waterCol;
    BoxCollider victorywater1Col;
    BoxCollider victorywater2Col;
    BoxCollider victorywater3Col;
    BoxCollider headCol;
    
    void Awake()
    {
        Debug.Log("im wide awake");
        //slurp = head.GetComponent<AudioSource>();
        light1ani = light1.GetComponent<Animator>();
        light2ani = light2.GetComponent<Animator>();
        waterCol = water.GetComponent<BoxCollider>();
        headCol = head.GetComponent<BoxCollider>();
        slurpPlayed = false;

        if (victorywater1 == null) return;
        victorywater1Col = victorywater1?.GetComponent<BoxCollider>();
        victorywater2Col = victorywater2?.GetComponent<BoxCollider>();
        victorywater3Col = victorywater3?.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waterCol.bounds.Intersects(headCol.bounds))
        {
            //Debug.Log("intersectlol");
            if (!slurpPlayed)
            {
                //AudioSource audio = head.GetComponent<AudioSource>();
                slurp.PlayOneShot(slurpsound, 1F);
                StartCoroutine(winCon());
            }
        }

        if (victorywater1 == null) return;
        if (victorywater1Col.bounds.Intersects(headCol.bounds) || victorywater1Col.bounds.Intersects(headCol.bounds) || victorywater1Col.bounds.Intersects(headCol.bounds))
        {
            Application.Quit();
        }
    }
    IEnumerator winCon()
    {
        slurpPlayed = true;
        //slurp.PlayOneShot(slurp.clip, 1F);
        //print(slurp.clip);
        //slurp.Play();
        light1ani.SetTrigger("fadeOut");
        light2ani.SetTrigger("fadeOut");
        print("WINWINWINWINWIN");//WIN CONDITION, NEXT LEVEL

        yield return new WaitForSeconds(4.5F);
        LevelCount++;
        SceneManager.LoadScene(LevelCount);
    }
}
