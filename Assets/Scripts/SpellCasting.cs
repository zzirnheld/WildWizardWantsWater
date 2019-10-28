using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SpellCasting : MonoBehaviour
{
    public ParticleSystem[] OnCastParticles = new ParticleSystem[3];

    private RightHandCaster.Spells? currSpellHeld;

    public RightHandCaster.Spells? CurrSpellHeld { get { return currSpellHeld; } }

    public List<Spell> CurrentSpellsCast;
    
    public GameObject[] WandParticles = new GameObject[3];
    public GameObject CameraRig;

    public const int InteractableMask = 1 << 11;

    #region steamvractions
    public SteamVR_Action_Boolean TriggerHeld;
    public SteamVR_Input_Sources InputSrcs = SteamVR_Input_Sources.Any;
    #endregion steamvractions

    void Awake()
    {
        TriggerHeld.AddOnStateDownListener(CastSpell, InputSrcs);
        CurrentSpellsCast = new List<Spell>();
    }

    /// <summary>
    /// Set the spell being hold in the wand, including setting the particles
    /// </summary>
    /// <param name="spell"></param>
    public void StartSpell(RightHandCaster.Spells spell)
    {
        Debug.Log($"Starting {spell}");
        currSpellHeld = spell;
        WandParticles[(int) spell]?.SetActive(true);
    }

    /// <summary>
    /// Called when the trigger is released. Launches the spell
    /// </summary>
    /// <param name="fromAction"></param>
    /// <param name="fromSource"></param>
    public void CastSpell(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log($"Casting {currSpellHeld}");
        if (!currSpellHeld.HasValue)
        {
            ResetWand();
            return;
        }

        Spell toCast = Spell.CreateSpell(currSpellHeld.Value);
        CurrentSpellsCast.Add(toCast);
        OnCastParticles[(int)currSpellHeld.Value].Play();
        toCast.Cast(this);
        ResetWand();
    }

    /// <summary>
    /// Hides particles, cancels any spell that would have been cast
    /// </summary>
    public void ResetWand()
    {
        foreach (GameObject wandParticle in WandParticles)
        {
            if (wandParticle == null) continue;
            
            wandParticle.SetActive(false);
        }
        currSpellHeld = null;
    }

    /// <summary>
    /// Ends all spells that haven't yet already ended
    /// </summary>
    public void EndAllSpells()
    {
        ResetWand();
        foreach(Spell s in CurrentSpellsCast)
        {
            s.End(this);
        }
        CurrentSpellsCast.Clear();
    }

    public bool RaycastFromWandDefaultMask(out RaycastHit hit)
    {
        return RaycastFromWandWithMask(InteractableMask, out hit);
    }

    public bool RaycastFromWandWithMask(int mask, out RaycastHit hit)
    {
        Vector3 wandDir = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, wandDir * 100F);
        return Physics.Raycast(transform.position, wandDir, out hit, Mathf.Infinity, mask);
    }
}
