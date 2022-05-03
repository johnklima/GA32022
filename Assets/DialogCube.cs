using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogCube : MonoBehaviour
{

    public string theText;
    public Text textView;
    public Transform objectToEnable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Enter" + transform.name);
        textView.text = theText;
        textView.gameObject.SetActive(true);

        if(objectToEnable)
            objectToEnable.gameObject.SetActive(true);

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Collider Exit " + transform.name);
        textView.text = "";
        textView.gameObject.SetActive(false); 
    }
}
