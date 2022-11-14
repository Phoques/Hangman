using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Buttons : MonoBehaviour
{

    //component button but empty so we can pass through our Button set up and Letter guess behaviour
    Button button;

    //this behaviour uses the keyboard letter to assign that value to the button event
    public void ButtonSetup(string _letter)
    {
        //get this buttons component button
        button = GetComponent<Button>();
        //make sure the button can be pressed by the player
        button.interactable = true;
        //change the keyboard button text display to display the letter it is
        GetComponentInChildren<Text>().text = _letter;
        //letter = _letter;
        name = _letter;
        //button variable in this script, onclick (Like inspector onClick) Addlistener adds a method in ()
        //this is telling the method inside the () to listen out for the 'Onclick'
        button.onClick.AddListener(LetterGuess);
    }

    public void LetterGuess()
    {
        //when pressed by the player turn off the interaction so it cant be pressed again
        button.interactable = false;
        //converting button name (which is a letter on the keyboard) from string to char and assigning it to the variable 'letter'
        char letter = Char.Parse(button.name);
        Debug.Log(letter);
        //sent the button letter that is now data type char to Game handler script, to the function 'LetterGuess' and then passing that char, into the function.
        GameHandler.gameHandlerInstance.LetterGuess(letter); // not only passes in the 'letter' variable we have set here, but also calls the 'LetterGuess' function in gamehandler.
    }



}
