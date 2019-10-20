using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell
{
    public static Spell CreateSpell(RightHandCaster.Spells spell)
    {
        switch (spell)
        {
            case RightHandCaster.Spells.Fire:
                return null;
                break;
            case RightHandCaster.Spells.Levitate:
                return null;
                break;
            case RightHandCaster.Spells.Teleport:
                return new TeleportSpell();
                break;
            default:
                return null;
                break;
        }
    }

    public abstract void Cast(SpellCasting caster);

    public abstract void End(SpellCasting caster);
}
