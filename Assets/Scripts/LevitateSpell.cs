using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitateSpell : Spell
{
    public const int LevitateableMask = 1 << 11;

    private GameObject levitating;

    public override void Cast(SpellCasting caster)
    {
        //raycast to the floor
        RaycastHit hit;
        if (caster.RaycastFromWandWithMask(LevitateableMask, out hit))
        {
            Debug.Log($"Levitating {hit.transform.gameObject.name}");
            levitating = hit.transform.gameObject;
            hit.transform.parent = caster.gameObject.transform;
        }
        else
        {
            Debug.Log("Failed to levitate anything");
        }
    }

    public override void End(SpellCasting caster)
    {
        if(levitating != null)
            levitating.transform.parent = null;
    }
}
