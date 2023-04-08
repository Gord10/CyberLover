using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public bool autoJumpToFreeWillNodeForDebug = false;

    public static string endingName = "<UNDEFINED>";
    public const int possibleEndingAmount = 3;
    
    private const string reachedEndingsPlayerPrefsKey = "reachedEndings";

    private static bool hasPlayerEverFinishedGame = false;
    private DialogueRunner dialogueRunner;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();

#if !UNITY_EDITOR
        autoJumpToFreeWillNodeForDebug = false;
#endif
        string startNode = (hasPlayerEverFinishedGame || autoJumpToFreeWillNodeForDebug) ? "GainingFreeWill" : "Start";
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
        AttemptAddEndingToReachedEndingsList(endingName);
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

    public static void AttemptAddEndingToReachedEndingsList(string _endingName)
    {
        string reachedEndings = PlayerPrefs.GetString(reachedEndingsPlayerPrefsKey, "");
        string[] reachedEndingsArray = reachedEndings.Split('\n');

        int i;
        for(i = 0; i < reachedEndingsArray.Length; i++)
        {
            if(string.Equals(reachedEndingsArray[i], _endingName))
            {
                return;
            }
        }

        reachedEndings += _endingName + "\n";
        PlayerPrefs.SetString(reachedEndingsPlayerPrefsKey, reachedEndings);
        PlayerPrefs.Save();
        print(reachedEndings);
    }

    public static int GetReachedEndingAmount()
    {
        string reachedEndings = PlayerPrefs.GetString(reachedEndingsPlayerPrefsKey, "");
        string[] reachedEndingsArray = reachedEndings.Split('\n');
        return reachedEndingsArray.Length -1;
    }
}
