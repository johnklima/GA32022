using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelTrigger : MonoBehaviour
{

    public string scene = "Additive0";
    public ScreenFader fader;
    public bool isFading = false;
    public GameObject destroyGroup;
    public bool additive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isFading == true && fader.fadeOutOfScene == false)
        {
            if(additive)
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            else
                SceneManager.LoadScene(scene);

            if (destroyGroup)
                Destroy(destroyGroup);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(fader)
        {
            fader.fadeOutOfScene = true;
            isFading = true;
        }
        

    }
}
