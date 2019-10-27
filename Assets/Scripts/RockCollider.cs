using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockCollider : MonoBehaviour
{
    public GameObject rock;
    public GameObject plate;
    public GameObject water;

    BoxCollider rockCol;
    BoxCollider plateCol;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        rockCol = rock.GetComponent<BoxCollider>();
        plateCol = plate.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rockCol.bounds.Intersects(plateCol.bounds))
        {
            water.SetActive(true);
        }
    }
}
