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
}
