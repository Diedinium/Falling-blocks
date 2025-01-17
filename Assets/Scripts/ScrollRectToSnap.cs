﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectToSnap : MonoBehaviour {

    //public Variables
    public RectTransform panel; //Holds scroll panel
    public Button[] bttn;
    public RectTransform center; //Center to compare the distance for each button

    //public text containers.
    public GameObject standardRedDescription;
    public GameObject vibrantBlueDescription;
    public GameObject neonGreenDescription;
    public GameObject perfectPurpleDescription;
    public GameObject goldenGoldDescription;

    //public text containers for "unlock description" text
    public GameObject vibrantBlueUnlocked;
    public GameObject neonGreenUnlocked;
    public GameObject perfectPurpleUnlocked;
    public GameObject goldenGoldUnlocked;

    //Private variables
    private float[] distance; //stores each buttons distance to the center.
    private bool dragging = false; //will be true as panel is dragged.
    private int bttnDistance; //Stores distance between the buttons.
    private int minButtonNum; //To hold the number of buttom with smallest distance to center.

    void Start()
    {
        //Get length of buttons and store in variable
        int bttnLength = bttn.Length;
        distance = new float[bttnLength];

        //Get distance between buttons
        bttnDistance = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    void Update()
    {
        //for each button, calculate it's distance compared to the center game object.
        for (int i = 0; i < bttn.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - bttn[i].transform.position.x);
        }

        float minDistance = Mathf.Min(distance); //Min distance to center.

        //Find and store the array number of the button with the smallest distance to the center.
        for (int a = 0; a < bttn.Length; a++)
        {
            if (minDistance == distance[a])
            {
                minButtonNum = a;
            }
        }

        //Controls which buttons are unlocked/usable
        UnlockControl();

        //When user stopps dragging, lerps to nearest button.
        if (!dragging)
        {
            LerpToBttn(minButtonNum * -bttnDistance);
        }


    }

    //Calculates a lerp value so that scrollRect smoothly moves to center position.
    void LerpToBttn(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPosition;
    }

    //Controls which buttons are unlocked.
    void UnlockControl()
    {
        //shows description if nearest button is first button
        if (minButtonNum == 0)
        {
            standardRedDescription.SetActive(true);
        }
        else
        {
            standardRedDescription.SetActive(false);
        }

        //Controls which description is visible and if button is active based on playerprefs values
        if (minButtonNum == 1 && PlayerPrefs.GetInt("TotalPlayerDeaths") >= 100)
        {
            vibrantBlueDescription.SetActive(true);
            vibrantBlueUnlocked.SetActive(false);

        }
        else if (minButtonNum == 1 && PlayerPrefs.GetInt("TotalPlayerDeaths") < 100)
        {
            vibrantBlueDescription.SetActive(false);
            vibrantBlueUnlocked.SetActive(true);
        }
        else
        {
            vibrantBlueDescription.SetActive(false);
            vibrantBlueUnlocked.SetActive(false);
        }

        //Controls which description is visible and if button is active based on playerprefs values
        if (minButtonNum == 2 && PlayerPrefs.GetInt("TotalBlocksDodged") >= 5000)
        {
            neonGreenDescription.SetActive(true);
            neonGreenUnlocked.SetActive(false);

        }
        else if (minButtonNum == 2 && PlayerPrefs.GetInt("TotalBlocksDodged") < 5000)
        {
            neonGreenDescription.SetActive(false);
            neonGreenUnlocked.SetActive(true);
        }
        else
        {
            neonGreenDescription.SetActive(false);
            neonGreenUnlocked.SetActive(false);
        }

        //Controls which description is visible and if button is active based on playerprefs values
        if (minButtonNum == 3 && PlayerPrefs.GetInt("TotalTimeSurvived") >= 1800)
        {
            perfectPurpleDescription.SetActive(true);
            perfectPurpleUnlocked.SetActive(false);

        }
        else if (minButtonNum == 3 && PlayerPrefs.GetInt("TotalTimeSurvived") < 1800)
        {
            perfectPurpleDescription.SetActive(false);
            perfectPurpleUnlocked.SetActive(true);
        }
        else
        {
            perfectPurpleDescription.SetActive(false);
            perfectPurpleUnlocked.SetActive(false);
        }

        //Controls which description is visible and if button is active based on playerprefs values
        if ((minButtonNum == 4) && PlayerPrefs.GetInt("TotalTimeSurvived") >= 3600 && PlayerPrefs.GetInt("HighScore") >= 80)
        {
            goldenGoldDescription.SetActive(true);
            goldenGoldUnlocked.SetActive(false);

        }
        else if ((minButtonNum == 4) && (PlayerPrefs.GetInt("TotalTimeSurvived") <= 3599 || PlayerPrefs.GetInt("HighScore") <= 79))
        {
            goldenGoldDescription.SetActive(false);
            goldenGoldUnlocked.SetActive(true);
        }
        else
        {
            goldenGoldDescription.SetActive(false);
            goldenGoldUnlocked.SetActive(false);
        }
    }

    //Sets the dragging value when scrollRect is being dragged.
    public void StartDrag()
    {
        dragging = true;
    }

    //Sets the dragging value when the scrollRect is not being dragged.
    public void EndDrag()
    {
        dragging = false;
    }
}
