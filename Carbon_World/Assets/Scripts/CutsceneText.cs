using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneText : MonoBehaviour
{
    public PlayableDirector timeline;
    public Text textObject;

    // Dialogue array
    public string[] dialouge;

    public int sceneBuildIndex;

    // Index to keep track of current dialogue
    private int index;

    // typing speed
    public float wordSpeed;

    private void Start() {
        timeline.Play();
    }

    public void beginCutscene() {
        zeroText();
    }

    public void endCutscene() {
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }

    // Method to reset the dialogue
    public void zeroText()
    {
        textObject.text = "";
        index = 0;
        StartCoroutine(Typing());
    }

    // Coroutine to type out the dialogue
    IEnumerator Typing()
    {
        foreach (char letter in dialouge[index].ToCharArray())
        {
            textObject.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    // Method to go to the next line of dialogue
    public void NextLine()
    {

        // If there are more dialogues, go to the next one
        if (index < dialouge.Length - 1)
        {
            index++;
            textObject.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            // Otherwise, reset the dialogue
            zeroText();
        }
    }
}
