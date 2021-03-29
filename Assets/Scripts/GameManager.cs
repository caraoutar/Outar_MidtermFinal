using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject popTextBG;

    //has the text component
    public GameObject popTextObj;

    public Text popTextComponent; 

    public float textTimeReset;

    float textTime;

    bool countdown = false;


    // Start is called before the first frame update
    void Start()
    {
        textTime = textTimeReset;
    }

    // Update is called once per frame
    void Update()
    {   //by default is false, only changes to true when text is to be displayed
        if (countdown) {
            //similar to room speed in gamemaker
            textTime -= Time.deltaTime;

            if(textTime <= 0 ) {
                //turn off everything!
                popTextBG.SetActive(false);
                popTextObj.SetActive(false);
                textTime = textTimeReset;
                countdown = false;
            }
        }
    }

    public void ShowPopText(string textToShow) {

        //sets grey background for text to active
        popTextBG.SetActive(true);
        //sets text object to active
        popTextObj.SetActive(true);
        //sets text in text component to the string textToShow
        popTextComponent.text = textToShow;
        //start countdown 
        countdown = true; 
    }
}
