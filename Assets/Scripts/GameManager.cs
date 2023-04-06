using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static string endingName = "<UNDEFINED>";

    private static bool hasPlayerEverFinishedGame = false;
    private DialogueRunner dialogueRunner;


    private void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();

        string startNode = (hasPlayerEverFinishedGame) ? "GainingFreeWill" : "Start";
        StartCoroutine(RunDialogueRunner(startNode));
    }

    private IEnumerator RunDialogueRunner(string startNode)
    {
        while(!dialogueRunner.NodeExists(startNode))
        {
            print("dsds");
            yield return new WaitForEndOfFrame();
        }

        dialogueRunner.StartDialogue(startNode);
    }

    [YarnCommand("EndGame")]
    public static IEnumerator EndGame(string _endingName)
    {
        endingName = _endingName.ToUpper();
        float endGameFadeTime = 2;
        Fade.EndGameFade(endGameFadeTime);
        yield return new WaitForSeconds(endGameFadeTime);
        hasPlayerEverFinishedGame = true;
        SceneManager.LoadScene("EndGame");
    }

    [YarnCommand("StartAbyss")]
    public static void StartAbyss()
    {
        TimeText.StartAbyss();
        //yield return new WaitForSeconds(5);
    }
}
