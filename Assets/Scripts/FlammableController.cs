using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableController : MonoBehaviour
{
    private float timeBurning;
    private bool burning = false;
    private FireSpell source;

    public GameObject BurningPrefab;

    private GameObject fireParticle;

    public void LightOnFire(FireSpell source)
    {
        burning = true;
        timeBurning = 0f;
        this.source = source;
        fireParticle = Instantiate(BurningPrefab, transform.position, transform.rotation, transform);
        fireParticle.transform.localScale = new Vector3(3, 3, 3);
    }

    public void StopBurning()
    {
        burning = false;
        source = null;
        fireParticle.SetActive(false);
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
