using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    private RightHandCaster.Spells currSpell;
    public GameObject fireWand;
    public GameObject sparkWand;
    public GameObject shineWand;

    public void startSpell(RightHandCaster.Spells spell)
    {
        Debug.Log($"Starting {spell}");
        currSpell = spell;
        if(spell == RightHandCaster.Spells.Fire)
        {
            Debug.Log("if statement");
            fireWand.SetActive(true);
        }
    }

    public void endSpell()
    {
        if (currSpell == RightHandCaster.Spells.Fire)
        {
            fireWand.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
