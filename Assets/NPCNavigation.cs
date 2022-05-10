using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class NPCNavigation : MonoBehaviour
{
    public NPCBehaviour behaviour;
    public NavMeshAgent agent;
    public Transform returnPoint;
    public Text TextBox;
    public string text;

    public float waitTime = 3.0f;
    public float waitTimer = -1;
    
   
    // Update is called once per frame
    private void Update()
    {
        //if the node is not the npcs destination node I have nothing to do
        //get out (exit the function aka "return")
        if (transform != behaviour.destinationObject)
            return;  

        Transform child;
        float dist = agent.remainingDistance;                       

        //when we arrive at our destination, we activate our chosen child
        //in the simple case,there is only one child.
        if (dist < 1.0f)        
        {
            
            //set the wait timer and get out
            if(waitTimer < 0)
            {
                waitTimer = Time.time;
                return;
            }
            //get out if the time has not expire
            if (Time.time - waitTimer < waitTime)
            {
                return;
            }

            //otherwise, process the node  

            //do i have a children?
            if (transform.childCount > 1) // I have two (or more) children
           {
                //choose which path to follow.
                int which = 0;
                if (behaviour.state == NPCBehaviour.STATE.hungry)
                    which = 0;
                else if (behaviour.state == NPCBehaviour.STATE.overwatered)
                    which = 1;
                else
                    return;

                child = transform.GetChild(which);
                child.gameObject.SetActive(true);
                child.GetComponent<NPCNavigation>().initiate();
           }
           else if (transform.childCount > 0)  //one child, just go there
           {              
                
                child = transform.GetChild(0);
                child.gameObject.SetActive(true);
                child.GetComponent<NPCNavigation>().initiate();
               
           }
           else  // no children, go to the return node
           {
                //turn off everyone else from the return node on down recursively
                returnPoint.GetComponent<NPCNavigation>().resetPoint();
                
                //enable the return point, so it can do its thing
                returnPoint.gameObject.SetActive(true);
                returnPoint.GetComponent<NPCNavigation>().initiate();


            }
        }
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

    public void initiate()
    {
        waitTimer = -1;
        behaviour.destinationObject = transform;
        agent.SetDestination(transform.position);
        TextBox.text = text;
       
    }

}
