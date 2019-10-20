using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SpellCasting : MonoBehaviour
{
    private RightHandCaster.Spells? currSpellHeld;

    public List<Spell> CurrentSpellsCast;
    
    public GameObject[] WandParticles = new GameObject[3];
    public GameObject CameraRig;

    #region steamvractions
    public SteamVR_Action_Boolean TriggerHeld;
    public SteamVR_Input_Sources InputSrcs = SteamVR_Input_Sources.Any;
    #endregion steamvractions

    void Awake()
    {
        TriggerHeld.AddOnStateUpListener(CastSpell, InputSrcs);
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
        WandParticles[(int) spell].SetActive(true);
    }

    /// <summary>
    /// Called when the trigger is released. Launches the spell
    /// </summary>
    /// <param name="fromAction"></param>
    /// <param name="fromSource"></param>
    public void CastSpell(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!currSpellHeld.HasValue)
        {
            ResetWand();
            return;
        }

        Spell toCast = Spell.CreateSpell(currSpellHeld.Value);
        CurrentSpellsCast.Add(toCast);
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
            wandParticle?.SetActive(false);
        }
        currSpellHeld = null;
    }

    /// <summary>
    /// Ends all spells that haven't yet already ended
    /// </summary>
    public void EndAllSpells()
    {
        ResetWand();
    }

    public bool RaycastFromWand(out RaycastHit hit)
    {
        Vector3 wandDir = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, wandDir * 100F);
        if (Physics.Raycast(transform.position, wandDir, out hit, Mathf.Infinity))
        {
            return true;
        }

        return false;
    }

    public bool RaycastFromWandWithMask(int mask, out RaycastHit hit)
    {
        Vector3 wandDir = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, wandDir * 100F);
        if (Physics.Raycast(transform.position, wandDir, out hit, Mathf.Infinity, mask))
        {
            return true;
        }

        return false;
    }
}
