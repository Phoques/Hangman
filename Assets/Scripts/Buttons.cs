using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Buttons : MonoBehaviour
{
    
    Button button;
    string letter;

    GameHandler gameHandler;

    private void Start()
    {
        gameHandler = FindObjectOfType<GameHandler>();
    }

    public void ButtonSetup(string _letter)
    {
        button = GetComponent<Button>();
        button.interactable = true;
        GetComponentInChildren<Text>().text = _letter;
        //letter = _letter;
        name = _letter;
        //button variable in this script, onclick (Like inspector onClick) Addlistener adds a method in ()
        //this is telling the method inside the () to listen out for the 'Onclick'
        button.onClick.AddListener(letterGuess);
    }

    public void letterGuess()
    {
        button.interactable = false;

        //if (isIncorrect)
        //{
        //gameHandler.DrawHangman();

        //}
        
    }



}
