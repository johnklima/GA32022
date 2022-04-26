using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTree : MonoBehaviour
{
    public Transform dialog;
    public Button ButtonA;
    public Button ButtonZ;
    

    // Start is called before the first frame update
    void Start()
    {
      
            //transform.GetChild(0).gameObject.SetActive(true);

            //get the first child in the tree
            dialog = transform.GetChild(0);
  

    }

    // Update is called once per frame
    void Update()
    {
        if (dialog.childCount == 0)
        {
            ButtonA.gameObject.SetActive(false);
            ButtonZ.gameObject.SetActive(false);
        }
        if (dialog.childCount == 1)
        {
            ButtonA.gameObject.SetActive(true);
            ButtonZ.gameObject.SetActive(false);
        }
        if (dialog.childCount == 2)
        {
            ButtonA.gameObject.SetActive(true);
            ButtonZ.gameObject.SetActive(true);
        }

    }
    public void onAclick()
    {
        if (dialog.childCount > 0)
        {
            Debug.Log("A Click");
            dialog.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void onZclick()
    {
        if (dialog.childCount > 1)
        {
            Debug.Log("Z Click");
            dialog.GetChild(1).gameObject.SetActive(true);
        }
    }
 
}
