using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RightHandCaster : MonoBehaviour
{
    public GameObject Beam;

    #region steamvractions
    public SteamVR_Action_Boolean TriggerHeld;
    #endregion steamvractions

    public enum Spells
    {
        Fire
    }

    public const int starMask = 1 << 9;
    // RIP Start

    public GameObject[] Stars;

    private int lastStarHit = -1;
    private HashSet<Pair> lines;

    private Dictionary<HashSet<Pair>, Spells> spellsDictionary;

    void Awake()
    {
        lines = new HashSet<Pair>();
        spellsDictionary = new Dictionary<HashSet<Pair>, Spells>();
        spellsDictionary.Add(new HashSet<Pair>
        {
            new Pair(6, 3),
            new Pair(3, 1),
            new Pair(1, 5),
            new Pair(5, 8)
        }, Spells.Fire);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 wandDir = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, wandDir*100F);
        RaycastHit starHit;
        if (TriggerHeld.state)
        {
            Beam.SetActive(true);
        }
        else
        {
            Beam.SetActive(false);
        }
        if (Physics.Raycast(transform.position, wandDir, out starHit, Mathf.Infinity, starMask))
        {
            Debug.Log(starHit.collider.gameObject);
            //hitting star
            if (TriggerHeld.state)
            {
                HitStar(StarNumberFromObject(starHit.collider.gameObject));
            }
        }
    }

    private int StarNumberFromObject(GameObject obj)
    {
        for(int i = 0; i < Stars.Length; i++)
        {
            if (obj == Stars[i]) return i;
        }

        return -1;
    }

    private void HitStar(int starNum)
    {
        if(lastStarHit != -1 && lastStarHit != starNum)
        {
            Debug.Log($"Adding line between {lastStarHit} and {starNum}");
            lines.Add(new Pair(lastStarHit, starNum));
            CastSpellIfValid();
        }

        lastStarHit = starNum;
    }

    private Spells? CheckSetAsSpell()
    {
        foreach(KeyValuePair<HashSet<Pair>, Spells> pair in spellsDictionary)
        {
            Debug.Log($"Comparing set {lines} and {pair.Key}");
            if (lines.SetEquals(pair.Key))
            {
                return pair.Value;
            }
        }

        return null;
    }

    private void ClearLinesSet()
    {
        lines.Clear();
    }

    //returns whether or not a spell was cast
    public bool CastSpellIfValid()
    {
        Spells? spell = CheckSetAsSpell();
        if (spell != null)
        {
            lastStarHit = -1;
            Debug.Log("CASTING " + spell);
        }

        ClearLinesSet();
        return spell != null;
    }

}
