using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell
{
    public static Spell CreateSpell(RightHandCaster.Spells spell)
    {
        switch (spell)
        {
            case RightHandCaster.Spells.Teleport:
                return new TeleportSpell();
        }

        Debug.Log($"No current spell ready for spell enum {spell}");
        return null;
    }

    public abstract void Cast(SpellCasting caster);

    public abstract void End(SpellCasting caster);
}
