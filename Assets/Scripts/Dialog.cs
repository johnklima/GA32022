using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialog : MonoBehaviour
{
    public string text = "default text";
    public Text textView;
    public DialogTree dialogTree;
    public string AbuttonText = "A button";
    public string ZbuttonText = "Z button";
    public Text Abutton;
    public Text Zbutton;

    private void Awake()
    {
        Debug.Log(" COMPUTER SAYS: " + text);
        textView.text = text;
        dialogTree.dialog = transform;

        Abutton.text = AbuttonText;
        Zbutton.text = ZbuttonText;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
