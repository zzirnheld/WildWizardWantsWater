using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSpell : Spell
{
    public const int TeleportableMask = 1 << 10;

    public override void Cast(SpellCasting caster)
    {
        Debug.Log("teleportspell being cast");
        //raycast to the floor
        RaycastHit hit;
        if(caster.RaycastFromWandDefaultMask(out hit))
        {   
            if (hit.transform?.gameObject?.GetComponent<TeleportableController>() == null) { Debug.Log("no teleport controller"); return; }

            Debug.Log($"moving camera to {hit.point}");
            caster.CameraRig.transform.position = hit.point;
        }
        caster.CurrentSpellsCast.Remove(this);
    }

    public override void End(SpellCasting caster)
    {
        
    }
}
