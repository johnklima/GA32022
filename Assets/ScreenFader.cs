using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    // Start is called before the first frame update
    Image image;
    float timer = -1;
    public bool fadeIntoScene = true;
    public bool fadeOutOfScene = false;

    void Start()
    {
        image = transform.GetComponent<Image>();        
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

       
    }

    // Update is called once per frame
    void Update()
    {

        if (fadeIntoScene)
        {
            image.color = new Color(image.color.r,
                                    image.color.g,
                                    image.color.b,
                                    image.color.a - Time.deltaTime);

            if( image.color.a <= 0 )
            {
                fadeIntoScene = false;
            }
        }

        if (fadeOutOfScene)
        {
            image.color = new Color(image.color.r,
                                    image.color.g,
                                    image.color.b,
                                    image.color.a + Time.deltaTime);

            if (image.color.a >= 1)
            {
                fadeOutOfScene = false;
            }
        }

    }
}
