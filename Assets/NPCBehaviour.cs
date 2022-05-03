using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    // Start is called before the first frame update



    public NPCNavigation navtree;
    public float Food = 1.0f;
    public float Pee = 0.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Food -= Time.deltaTime * 0.05f;
        Pee += Time.deltaTime * 0.03f;
        
    }
}
