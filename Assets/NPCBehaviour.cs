using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public enum STATE
    {
        idle,
        hungry,
        overwatered,
        approach
    };

    public STATE state;

    public NPCNavigation navtree;
    public float Food = 1.0f;
    public float Pee = 0.0f;
    public Transform destinationObject;

    public float hungerpoint = 0.3f;
    public float peepoint = 0.5f;
    void Start()
    {
        transform.GetComponent<NavMeshAgent>().SetDestination(destinationObject.position);
    }

    // Update is called once per frame
    void Update()
    {
        Food -= Time.deltaTime * 0.05f;
        Pee += Time.deltaTime * 0.03f;
        
        if (Food < hungerpoint)
        {
            state = STATE.hungry;
            Food = 1.0f;                    
        }                    
        else if (Pee > peepoint)
        {
            state = STATE.overwatered;
            Pee = 0.0f;
        }                    
        else
        {
            state = STATE.idle;
            return;
        }
         

    }
}
