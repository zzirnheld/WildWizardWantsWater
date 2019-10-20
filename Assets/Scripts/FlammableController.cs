using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableController : MonoBehaviour
{
    private float timeBurning;
    private bool burning = false;
    private FireSpell source;

    public void LightOnFire(FireSpell source)
    {
        burning = true;
        timeBurning = 0f;
        this.source = source;
    }

    public void StopBurning()
    {
        burning = false;
        source = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(burning) timeBurning += Time.deltaTime;
        if(timeBurning >= 3f)
        {
            //the object burns
            source.caster.CurrentSpellsCast.Remove(source);
            Destroy(gameObject);
        }
    }
}
