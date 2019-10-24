using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RightHandCaster : MonoBehaviour
{
    public GameObject LR;
    private int drawIteration = 0;
    public GameObject Beam;
    public LineRenderer LRenderer;
    public SpellCasting spellCasting;
    public RightHandPalette RHPalette;

    #region steamvractions
    public SteamVR_Action_Boolean TriggerHeld;
    #endregion steamvractions

    public enum Spells
    {
        Fire = 0,
        Levitate = 1,
        Teleport = 2
    }

    public const int starMask = 1 << 9;
    // RIP Start

    public GameObject[] Stars;

    private int lastStarHit = -1;
    private GameObject lastStarHitObj = null;
    private HashSet<Pair> lines;
    private PairComparer pairComparer;

    private Dictionary<HashSet<Pair>, Spells> spellsDictionary;

    void Awake()
    {
        LRenderer = LR.GetComponent<LineRenderer>();
        LRenderer.positionCount = 0;

        pairComparer = new PairComparer();

        lines = new HashSet<Pair>(pairComparer);
        spellsDictionary = new Dictionary<HashSet<Pair>, Spells>();

        HashSet<Pair> fireSet = new HashSet<Pair>(pairComparer);
        fireSet.Add(new Pair(3, 0));
        fireSet.Add(new Pair(0, 1));
        fireSet.Add(new Pair(1, 2));
        fireSet.Add(new Pair(2, 5));
        spellsDictionary.Add(fireSet, Spells.Fire);

        HashSet<Pair> teleportSet = new HashSet<Pair>(pairComparer);
        teleportSet.Add(new Pair(6, 3));
        teleportSet.Add(new Pair(3, 1));
        teleportSet.Add(new Pair(1, 5));
        teleportSet.Add(new Pair(5, 8));
        spellsDictionary.Add(teleportSet, Spells.Teleport);

        HashSet<Pair> levitateSet = new HashSet<Pair>(pairComparer);
        levitateSet.Add(new Pair(1, 3));
        levitateSet.Add(new Pair(3, 4));
        levitateSet.Add(new Pair(4, 5));
        levitateSet.Add(new Pair(5, 1));
        spellsDictionary.Add(levitateSet, Spells.Levitate);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 wandDir = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, wandDir*100F);
        RaycastHit starHit;
        if (TriggerHeld.state && spellCasting.CurrSpellHeld == null)
        {
            Beam.SetActive(true);
        }
        else
        {
            Beam.SetActive(false);        //REACTIVATE
        }
        if (Physics.Raycast(transform.position, wandDir, out starHit, Mathf.Infinity, starMask))
        {
            //Debug.Log($"hit {starHit.collider.gameObject.name}");
            //hitting star
            if (TriggerHeld.state)
            {
                HitStar(starHit.collider.gameObject, StarNumberFromObject(starHit.collider.gameObject));
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

    private void MakeLineToPosition(Vector3 position)
    {
        LRenderer.positionCount++;
        LRenderer.SetPosition(LRenderer.positionCount - 1, position + transform.TransformDirection(Vector3.back * 0.01f * (LRenderer.positionCount + 10)));
    }

    private void HitStar(GameObject starHit, int starNum)
    {
        if (lastStarHit != -1 && lastStarHit != starNum)
        {
            Debug.Log($"Adding line between {lastStarHit} and {starNum}");
            if (lastStarHitObj != null) {
                if (LRenderer.positionCount == 0)
                {
                    MakeLineToPosition(lastStarHitObj.transform.position);
                }
                MakeLineToPosition(starHit.transform.position);
            }
            lines.Add(new Pair(lastStarHit, starNum));

            if (CastSpellIfValid()) return;
        }

        lastStarHit = starNum;
        lastStarHitObj = starHit;
    }

    private Spells? CheckSetAsSpell()
    {
        foreach(KeyValuePair<HashSet<Pair>, Spells> pair in spellsDictionary)
        {
            Debug.Log($"Comparing set {SetToString(lines)} and {SetToString(pair.Key)}, set equals says {lines.SetEquals(pair.Key)}");
            if (lines.SetEquals(pair.Key))
            {
                return pair.Value;
            }
        }

        return null;
    }

    public void ClearLinesSet()
    {
        lines.Clear();
        lastStarHit = -1;
        lastStarHitObj = null;
        LRenderer.positionCount = 0;
        spellCasting.ResetWand();
    }

    //returns whether or not a spell was cast
    public bool CastSpellIfValid()
    {
        Spells? spell = CheckSetAsSpell();
        if (spell.HasValue)
        {
            RHPalette.UnsummonPalette();
            spellCasting.StartSpell(spell.Value);
        }

        return spell.HasValue;
    }


    private string SetToString(HashSet<Pair> set)
    {
        string str = "";
        foreach(Pair p in set)
        {
            str += p.ToString() + "; ";
        }
        return str;
    }
}
