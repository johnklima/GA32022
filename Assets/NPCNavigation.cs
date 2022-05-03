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

    //
    public void Awake()
    {
        agent.SetDestination(transform.position);
    }
    // Start is called before the first frame update
    public void initiate()
    {        
        agent.SetDestination(transform.position);
        TextBox.text = text;
        Debug.Log(transform.name);

    }
    // Update is called once per frame
    private void Update()
    {

        Transform child;
        //when we arrive at our destination, we activate our chosen child
        //in the simple case,there is only one child.
        if (agent.remainingDistance < 1.0f)
        {
            //do i have a children?
           if(transform.childCount > 1)
           {
                //choose which path to follow
                
                if(behaviour.Food < 0.3f)
                {
                    child = transform.GetChild(0);
                    
                    //this is all working fine, as I treverse the tree first time
                    child.gameObject.SetActive(true);

                    child.GetComponent<NPCNavigation>().initiate();

                    behaviour.Food = 1.0f;

                }
                else if(behaviour.Pee > 0.7f)
                {
                    child = transform.GetChild(1);                    
                    //this is all working fine, as I treverse the tree first time
                    child.gameObject.SetActive(true);
                    child.GetComponent<NPCNavigation>().initiate();

                    behaviour.Pee = 0.0f;
                }
                else
                { 
                    //do nothing
                
                }

            }
            else if (transform.childCount > 0)
            {
                //YES
                //set active should do the job, but somehow the tree traversal
                //is calling this all down the tree, so we must first set the new destination
                //before activating the node.
                child = transform.GetChild(0);
                //this is all working fine, as I treverse the tree first time
                child.gameObject.SetActive(true);
                child.GetComponent<NPCNavigation>().initiate();

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
