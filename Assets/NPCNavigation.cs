using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavigation : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform returnPoint;

    // Start is called before the first frame update

    // Update is called once per frame
    private void Update()
    {
        //when we arrive at our destination, we activate our chosen child
        //in the simple case,there is only one child.
        if (agent.remainingDistance < 1.0f)
        {
            //do i have a child?
            if (transform.childCount > 0)
            {
                //YES
                //set active should do the job, but somehow the tree traversal
                //is calling this all down the tree, so we must first set the new destination
                //before activating the node.
                agent.SetDestination(transform.GetChild(0).position);

                //this is all working fine, as I treverse the tree first time
                transform.GetChild(0).gameObject.SetActive(true);
                
            }
            else
            {
                //NO! in which case i better have a return node to loop on.
                
                //here I want to set the first destination, which i am calling
                //the return point, and i want to turn myself off as I have no children.
                agent.SetDestination(returnPoint.position);
                
                //turn off everyone else from the return node on down recursively
                returnPoint.GetComponent<NPCNavigation>().resetPoint();

                //turn myself off just to be sure.
                gameObject.SetActive(false);

                //enable the return point, so it can do its thing
                returnPoint.gameObject.SetActive(true);


            }
        }
    }
    private void Awake()
    {
        //on awake, or enabled, we set the nav agent to go to our point in space.
        agent.SetDestination(transform.position);
    }
    
    public void resetPoint()
    {
        //find my first child and disable it, which through recursion disables
        //its children, which disable their children. This is called a "Depth First Search"
        if(transform.childCount > 0)        
            transform.GetChild(0).GetComponent<NPCNavigation>().resetPoint();
        
        //disable recursively, exiting the recursion at me.
        transform.gameObject.SetActive(false);
    }

}
