using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public class GameHandler : MonoBehaviour
{
    public GameObject buttonPrefab;
    public List<string> letters = new List<string>();
    public List<string> words = new List<string>();
    public char[] wordLetters;
    public Text[] displayLetters;
    public Vector3 buttonLocation;
    public GameObject letterContainer;
    public GameObject[] hungman;
    public GameObject wordDisplay;
    public int incorrectGuessIndex;

    //To go through the letters by using a foreach loop.
    private void Start()
    {
        ChooseWord();
        CreateKeyBoard();
        SpawnLetters();
        for (int i = 0; i < hungman.Length; i++) // For every index item in the hangman array, set them as inactive to begin with.
        {
            hungman[i].SetActive(false);
        }

        //ShowCorrectLetter();


        //guessesLeft = new GameObject[guessesLeft];    
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        { 
            DrawHangman();
            
        }

        
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
    {// Index = 0, if 0 is less than the length of the Hungman GO array, add one to the index. Which then executes the below, which will draw each piece one at a time. (Draws all pieces at once. So need to review)
     //    for(int i = 0; i < hungman.Length; i++)
     //    {// If hungman array is NOT active.
     //        if (!hungman[i].activeSelf)
     //        { // Set as active.
     //            hungman[i].SetActive(true);
     //        }
     //        return;
     //    }

        if (!hungman[incorrectGuessIndex].activeSelf)
        {
           hungman[incorrectGuessIndex].SetActive(true);
        }
        if (incorrectGuessIndex < 6)
        {
            incorrectGuessIndex++;

        }
    }


    //public void ShowCorrectLetter()
    //{

    //    foreach (char c in wordLetters)
    //    {

    //        if (wordLetters[c].Contains(letters[i]))
    //        {

    //        }
    //    }
    //}



}
