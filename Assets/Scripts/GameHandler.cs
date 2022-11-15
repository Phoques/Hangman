using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public class GameHandler : MonoBehaviour
{

    #region Singleton
    public static GameHandler gameHandlerInstance;
    public GameHandler GameHandlerInstance
    {
        set
        {
            if (gameHandlerInstance == null)
            {
                gameHandlerInstance = this;
            }
        }
    }
    #endregion
    #region Words and Guessing
    //list of all words player can guess from in the game, which is populated in inspector.
    public List<string> words = new List<string>();
    //char array of the word that was chosen, split into the indidividual letters of the word.
    public char[] wordLetters;
    #endregion
    #region Keyboard Display
    //list of alphabet letters
    public List<string> letters = new List<string>();
    //spawn position for the keyboard
    public Vector3 buttonLocation;
    //container to parent the keyboard to
    public GameObject letterContainer;
    #endregion
    #region Word Display
    //text component array of the word display
    public Text[] displayLetters;
    //parent object for the word display
    public GameObject wordDisplay;
    #endregion
    //prefab for the keyboard and the word guess display
    public GameObject buttonPrefab;
    //hangman art array
    public GameObject[] hungman;
    //incorrect guess marker
    public int incorrectGuessIndex;
    public GameObject losePanel;
    public GameObject winPanel;

    //Set the instance to this script so that the button script can easily talk to the only gamehandler in the scene
    private void Awake()
    {
        GameHandlerInstance = gameHandlerInstance;
    }
    
    private void Start()
    {
        //when the game starts we need to choose a word, then create the players keyboard and display the word that the player is going to guess
        ChooseWord();
        CreateKeyBoard();
        SpawnLetters();
        //turn off the hang man display for the start of the round
        for (int i = 0; i < hungman.Length; i++) // For every index item in the hangman array, set them as inactive to begin with.
        {
            hungman[i].SetActive(false);
        }
        //Initialise win & lose panel as false on startup.
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        
    }
    void CreateKeyBoard() // This function only handles the keyboard.
    {
        //Button Array is being reset to the number of letters in the list.
        for (int i = 0; i < letters.Count; i++)
        {// Spawn new gameobject, from a prefab, at location, with no rotation, and set parent; (In this case then shows as a grid due to how the 'Letters' GO is setup.
            GameObject clone = Instantiate(buttonPrefab, buttonLocation, Quaternion.identity, letterContainer.transform);
            //clone is GO just made, gets a reference to the script Buttons, to then run the function 'ButtonSetup' and passing in the parameter.
            clone.GetComponent<Buttons>().ButtonSetup(letters[i]);
        }
    }
    public void ChooseWord() // This function chooses the word, and converts to a char array (Single letters instead of entire word)
    {//String type to variable = words (LIST) random range between 0, (First index count) and words.count which is maximum index count in LIST.
        string word = words[Random.Range(0, words.Count)];
        // wordletters (Is a char array) now = words LIST converted to a CHAR array, which makes each button a single letter.
        wordLetters = word.ToCharArray();
    }
    void SpawnLetters()
    {// displayLetters is a text array (That isnt previously populated, now = a new array which passes in the 'wordLetters' CHAR array, which is populated by 'ChooseWord()'
        //at the length of the array, which should change depending on the length of the word.
        displayLetters = new Text[wordLetters.Length];
        //if 0 (index?) is less than the length of the wordletters list, add the next index number until it reaches the length.
        for (int i = 0; i < wordLetters.Length; i++)
        {// Spawn new gameobject, from a prefab, at location of wordDisplay (UI canvas), with no rotation, and set parent (Which then adopts the grid layout);
            GameObject clone = Instantiate(buttonPrefab, wordDisplay.transform.position, Quaternion.identity, wordDisplay.transform);
            //clone is the GO just made, GetcomponentinChildren refers to the newly created GO clone buttons, and the children component is the text box.
            //This is now being asked to display the text as an underscore (Though commenting the below out does not show the word)
            clone.GetComponentInChildren<Text>().text = "_";
            //displayLetters text array, was converted to a Char Array, which is populated by the 'ChooseWord()' which is now assigned to the buttons to display?
            displayLetters[i] = clone.GetComponentInChildren<Text>();
        }

    }
    public void DrawHangman()
    {
        //hangman display is set to inactive in start
        if (!hungman[incorrectGuessIndex].activeSelf)
        {
            //when an incorrect guess is triggered, draw the next hangman element
            hungman[incorrectGuessIndex].SetActive(true);
        }
        //if we are not at the end of our guesses (<= 6 as we need it to tip over to 7 to trigger the lose panel below)
        if (incorrectGuessIndex <= 6)
        {
            //increment the guess index by 1
            incorrectGuessIndex++;

        }
        //If we are at the end of our guesses
        if (incorrectGuessIndex >= 7)
        {
            Debug.Log("You LOSE");
            //Wait half a second before triggering the 'LosePanel' Function.
            Invoke("LosePanel", 0.5f);
        }

    }

    void LosePanel()
    {//If GO Panel is not null
        if(losePanel != null)
        { // Set Panel as active
            losePanel.SetActive(true);
        }
    }

    void WinPanel()
    {//If GO Panel is not null
        if (winPanel != null)
        {// Set Panel as active
            winPanel.SetActive(true);
        }
    }

   

    //this letter is the letter that we guessed by pressing a button, it is sent to this scripts instance from the button listener event
    public void LetterGuess(char guessedLetter)
    {
        //temp bool to see if we got a letter correct
        bool correctGuess = false;
        //for every character in side of our character array called wordLetters
        foreach (char c in wordLetters)
        {
            //if the letter in wordLetters is the same as our guessed letter
            if (c == guessedLetter)
            {
                //set the temp bool to true
                correctGuess = true;
                Debug.Log("Got Letter Yay" + ": " + guessedLetter);
            }
        }
        //if we have correct letters guessed
        if (correctGuess)
        {
            //loop through every element of display letters buttons
            for (int i = 0; i < displayLetters.Length; i++)
            {
                //change the letter display if the current elements letter is our guessed letter
                if (guessedLetter == wordLetters[i])
                {//display letters array of buttons, has a text child, and that child now = the passed in guessed letter and is converted to a string. Which changes the '_' to the letter
                    displayLetters[i].text = guessedLetter.ToString();
                }
            }
        }
        //if bool isnt true here then there were no correct letters
        if (!correctGuess)
        {
            DrawHangman();
        }
        //temp bool to see if all letters have been guessed
        bool notAllLettersGuessed = false;
        //check to see if all letters are guessed
        for (int i = 0; i < displayLetters.Length; i++)
        {
            //when looping and checking if out display letters are set to _
            if (displayLetters[i].text == "_")
            {
                //then indicate we havent finished guessing and need more words
                notAllLettersGuessed = true;
                Debug.Log("more Guessed needed");
                return;
            }
        }
        //if none of our display letters were _ then we have guessed all letters and the word is complete
        if (!notAllLettersGuessed)
        {
            Debug.Log("YAY Word GUESSED!");
            Invoke("WinPanel", 0.5f);
        }
    }


    
}
