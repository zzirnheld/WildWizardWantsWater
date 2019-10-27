using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSpell : Spell
{
    public const int TeleportableMask = 1 << 10;

    public override void Cast(SpellCasting caster)
    {
        //raycast to the floor
        RaycastHit hit;
        if(caster.RaycastFromWandWithMask(TeleportableMask, out hit))
        {
            caster.CameraRig.transform.position = hit.point;
        }
        caster.CurrentSpellsCast.Remove(this);
    }

    public override void End(SpellCasting caster)
    {
        
    }
}
