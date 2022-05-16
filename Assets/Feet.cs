using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    public bool phase;
    Vector3 m_PrevPos;
    float m_DistanceTravelled = 0;
    float accumTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_PrevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //am i moving at all
        m_DistanceTravelled += (transform.position - m_PrevPos).magnitude;
        if (m_DistanceTravelled > 0.0f)
        {

            accumTime += Time.deltaTime * 5;
            float ypos;
            if (phase)
                ypos = Mathf.Sin(accumTime);
            else
                ypos = -Mathf.Sin(accumTime);

            Vector3 pos = transform.localPosition;
            pos.y = ypos;
            transform.localPosition = pos;

            m_DistanceTravelled = 0.0f;
        }
        else 
        {

            Vector3 pos = transform.localPosition;
            pos.y = -1;
            transform.localPosition = pos;
            accumTime = 0;

        }

        m_PrevPos = transform.position;


    }

    //FMOD Studio variables
    //The FMOD Studio Event path.
    //This script is designed for use with an event that has a game parameter for each of the surface variables, but it will still compile and run if they are not present.

    public string m_EventPath = "";

    //Surface variables
    //Range: 0.0f - 1.0f
    //These values represent the amount of each type of surface found when raycasting to the ground.
    //They are exposed to the UI (public) only to make it easy to see the values as the player moves through the scene.
    public float m_Street;
    public float m_Sidewalk;
    public float m_Grass;

    //Step variables
    //These variables are used to control when the player executes a footstep.
    //This is very basic, and simply executes a footstep based on distance travelled.
    //Ideally, in this case, footsteps would be triggered based on the headbob script. Or if there was an animated player model it could be triggered from the animation system.
    //You could also add variation based on speed travelled, and whether the player is running or walking. 
    public float m_StepDistance = 2.0f;

    //Debug variables
    //If m_Debug is true, this script will:
    // - Draw a debug line to represent the ray that was cast into the ground.
    // - Draw the triangle of the mesh that was hit by the ray that was cast into the ground.
    // - Log the surface values to the console.
    // - Log to the console when an expected game parameter is not found in the FMOD Studio event.
    public bool m_Debug;
         
    private void OnTriggerEnter(Collider other)
    {

        
        //Defaults
        m_Sidewalk = 0.0f;
        m_Grass = 0.0f;
        m_Street = 0.0f;

        
        
        Debug.Log("HIT " + other.name);
        if (other.tag == "Street")
        {
            m_Sidewalk = 0.0f;
            m_Grass = 0.0f;
            m_Street = 1.0f;

        }
        if (other.tag == "Grass")
        {
            m_Sidewalk = 0.0f;
            m_Grass = 1.0f;
            m_Street = 0.0f;
        }
        if (other.tag == "Sidewalk")
        {
            m_Sidewalk = 1.0f;
            m_Grass = 0.0f;
            m_Street = 0.0f;

        }
        else
        {
            Debug.Log("no ground");
        }
        

        if (m_Debug)
            Debug.Log("Street: " + m_Street + " Grass: " + m_Grass + " Sidewalk: " + m_Sidewalk);

        if (m_EventPath != null)
        {
            FMOD.Studio.EventInstance e = FMODUnity.RuntimeManager.CreateInstance(m_EventPath);
            e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

            SetParameter(e, "Street", m_Street);
            SetParameter(e, "Grass", m_Grass);
            SetParameter(e, "Sidewalk", m_Sidewalk);

            e.start();
            e.release();//Release each event instance immediately, there are fire and forget, one-shot instances. 
        }
    }

    void SetParameter(FMOD.Studio.EventInstance e, string name, float value)
    {


        e.setParameterByName(name, value);

    }
}
