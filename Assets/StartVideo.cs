using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVideo : MonoBehaviour
{
    public GameObject videoObject;
    private void OnTriggerEnter(Collider other)
    {
        videoObject.SetActive(true);
    }

}
