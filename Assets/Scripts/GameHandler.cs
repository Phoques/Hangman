using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


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
    

    //To go through the letters by using a foreach loop.
    private void Start()
    {
        ChooseWord();
        CreateKeyBoard();
        SpawnLetters();
        for (int i = 0; i < hungman.Length; i++)
        {
            hungman[i].SetActive(false);
        }

        //guessesLeft = new GameObject[guessesLeft];    
    }
    void CreateKeyBoard()
    {
        //Button Array is being reset to the number of letters in the list.
        for (int i = 0; i < letters.Count; i++)
        {// Spawn new gameobject, from a prefab, at location, with no rotation, and set parent;
            GameObject clone = Instantiate(buttonPrefab, buttonLocation, Quaternion.identity, letterContainer.transform);
            //clone is GO just made, gets a reference to the script Buttons, to then run the function 'ButtonSetup' and passing in the parameter.
            clone.GetComponent<Buttons>().ButtonSetup(letters[i]);
        }
    }
    public void ChooseWord()
    {
        string word = words[Random.Range(0, words.Count)];
        wordLetters = word.ToCharArray();
    }
    void SpawnLetters()
    {
        displayLetters = new  Text[wordLetters.Length];
        for (int i = 0; i < wordLetters.Length; i++)
        {// Spawn new gameobject, from a prefab, at location, with no rotation, and set parent;
            GameObject clone = Instantiate(buttonPrefab, wordDisplay.transform.position, Quaternion.identity, wordDisplay.transform);
            //clone is GO just made, gets a reference to the script Buttons, to then run the function 'ButtonSetup' and passing in the parameter.
            clone.GetComponentInChildren<Text>().text = "_";
            displayLetters[i] =  clone.GetComponentInChildren<Text>();
        }

    }
    public void DrawHangman()
    {
        for(int i = 0; i < hungman.Length; i++)
        {
            if (!hungman[i].activeSelf)
            {
                hungman[i].SetActive(true);
                return;
            }
        }
    }

    //public void LetterChoice(Button butt)
    //{
    //    Debug.Log("AHHH");
    //  butt. GetComponent<Button>().interactable = false;
    //}

}
