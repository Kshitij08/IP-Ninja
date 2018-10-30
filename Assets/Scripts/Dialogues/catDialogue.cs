using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class catDialogue : MonoBehaviour {

    public Canvas catCanvas;
    public Text catText;
    public Image quizImage;
    public Image answerImage;
    public Canvas patentCanvas;

    public GameObject Video;
   

    public Button option1;
    public Button option2;
    public Button option3;
    public Button option4;
    public Button option5;
    public Button option6;
    public Button option7;
    public Button option8;

    public Button buttonToEnable1;
    public Button buttonToEnable2;

    public Text option1Text;
    public Text option2Text;
    public Text option3Text;
    public Text option4Text;
    public Text option5Text;
    public Text option6Text;
    public Text option7Text;
    public Text option8Text;


    public float Timer;

    public static int hasCollided = 0;

    Scene scene;
    string sceneName;

    // Use this for initialization
    void Start () {
        sceneName = scene.name;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Player.Timer);

        //------For Curiosity------
        if (Player.Timer >= 4 && hasCollided == 1)
        {
            if (Player.Timer >= 6)
            {
                Destroy(Video);
                patentCanvas.gameObject.SetActive(true);
                catCanvas.gameObject.SetActive(true);
                hasCollided = 0;
            }

        }

    }

    public void OnOption1Click()
    {      
        catText.text = "Do you want to know how to defeat those Ninjas??\nFor that you need to answer a simple question\nDo you want to proceed??";
        option1.gameObject.SetActive(false);
        option2.gameObject.SetActive(true);
        //option3.gameObject.SetActive(false);      

    }


    public void OnOption2Click()
    {
        catText.text = "How many colors are there??";
        quizImage.gameObject.SetActive(true);
        
        option3.gameObject.SetActive(true);
        option4.gameObject.SetActive(true);
        option2.gameObject.SetActive(false);

    }



    //Yes option
    public void OnOption3Click()
    {
        catText.text = "Wow you are vary smart!! Your answer is correct.\nLet me give you some weapons. \nYou can use them after going back.\nBest of luck for your journey!!";
        option3.gameObject.SetActive(false);
        option5.gameObject.SetActive(true);
        buttonToEnable1.gameObject.SetActive(true);
        buttonToEnable2.gameObject.SetActive(true);
        quizImage.gameObject.SetActive(true);

    }

    //No option
    public void OnOption4Click()
    {
        catText.text = "No, it's worng.\nDo you want Help??";
        //Handheld.Vibrate();
        option3.gameObject.SetActive(false);
        option4.gameObject.SetActive(true);
        option6.gameObject.SetActive(true);
        option7.gameObject.SetActive(true);
        
    }

    //After yes
    public void OnOption5Click()
    {
        catCanvas.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    //Asked for hint
    public void OnOption6Click()
    {
        //show the video
        //Handheld.PlayFullScreenMovie("i.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
        patentCanvas.gameObject.SetActive(false);
        catCanvas.gameObject.SetActive(false);
        


        Video.SetActive(true);
        Player.Timer = 0;
        


        answerImage.gameObject.SetActive(true);

        option6.gameObject.SetActive(false);
        option7.gameObject.SetActive(false);
        //option3.gameObject.SetActive(true);
        option4.gameObject.SetActive(false);
        option8.gameObject.SetActive(true);
    }

    public void OnOption7Click()
    {
        answerImage.gameObject.SetActive(false);
        option6.gameObject.SetActive(false);
        option7.gameObject.SetActive(false);
        option3.gameObject.SetActive(true);
        option4.gameObject.SetActive(true);

    }

    public void OnOption8Click()
    {
        answerImage.gameObject.SetActive(false);
        option8.gameObject.SetActive(false);
        option3.gameObject.SetActive(true);
        option4.gameObject.SetActive(true);
        catText.text = "How many colors are there??";

    }

    public void OnCloseClick()
    {
        buttonToEnable1.gameObject.SetActive(true);
        buttonToEnable2.gameObject.SetActive(true);
        catCanvas.gameObject.SetActive(false);

        if(sceneName == "Level4")
        {
            patentCanvas.gameObject.SetActive(true);
        }

        if (sceneName == "Level7")
        {
            patentCanvas.gameObject.SetActive(true);
        }

        //Destroy(gameObject);
    }

    public void OnPatentYes7()
    {
        GameControl.control.patent3 = 1;
        GameControl.control.coins -= 40;
        patentCanvas.gameObject.SetActive(false);
    }

    public void OnPatentNo7()
    {
        GameControl.control.patent3 = 0;
        patentCanvas.gameObject.SetActive(false);

    }

    public void OnPatentYes4()
    {
        GameControl.control.patent2 = 1;
        GameControl.control.coins -= 40;
        patentCanvas.gameObject.SetActive(false);
    }

    public void OnPatentNo4()
    {
        GameControl.control.patent2 = 0;
        patentCanvas.gameObject.SetActive(false);

    }

    //public void OnOption2Click()
    //{
    //    if (i == 0)
    //    {
    //        catText.text = "Yes indeed.\nBye";
    //        option1.gameObject.SetActive(false);
    //        option2.gameObject.SetActive(false);
    //        option3.gameObject.SetActive(false);
    //    }

    //    if(i == 1)
    //    {
    //        catText.text = "Get lost, you piece of shit!!";
    //        option1.gameObject.SetActive(false);
    //        option2.gameObject.SetActive(false);
    //        option3.gameObject.SetActive(false);
    //    }

    //}

    //public void OnOption3Click()
    //{
    //    if(i == 0)
    //    {

    //        catText.text = "Good Morning\nIsn't it the perfect time for hunting. ";
    //        option1Text.text = "Whaaat hunting??\nPlease don't eat me.";
    //        option2Text.text = "Jerk off you bastard.";
    //        option3Text.text = "Not if I kill you first.";

    //    }
    //    //Try adding new button and the turn it on or off depending on the situation
    //    if(i == 1)
    //    {
    //        catText.text = "Shit\nAaaah\nCurse you";
    //        option1.gameObject.SetActive(false);
    //        option2.gameObject.SetActive(false);
    //        option3.gameObject.SetActive(false);
    //    }
    //}


}
