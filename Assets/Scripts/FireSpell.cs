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
        this.caster = caster;
        RaycastHit hit;
        if(caster.RaycastFromWand(out hit))
        {
            burningObjController = hit.transform.gameObject.GetComponent<FlammableController>();
            burningObjController?.LightOnFire(this);
        }
    }

    public override void End(SpellCasting caster)
    {
        burningObjController?.StopBurning();
    }
}
