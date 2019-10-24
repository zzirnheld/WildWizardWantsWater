using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : Spell
{
    public SpellCasting caster;
    private FlammableController burningObjController;

    public override void Cast(SpellCasting caster)
    {
        Debug.Log("Casting a flame spell");
        this.caster = caster;
        RaycastHit hit = default(RaycastHit);
        if(caster.RaycastFromWandDefaultMask(out hit))
        {
            burningObjController = hit.transform.gameObject.GetComponent<FlammableController>();
            burningObjController?.LightOnFire(this);
        }

        Debug.Log($"Hit {hit.transform?.gameObject?.name}, burning controller ? {burningObjController}");
    }

    public override void End(SpellCasting caster)
    {
        burningObjController?.StopBurning();
    }
}
