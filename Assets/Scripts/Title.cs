using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class Title : MonoBehaviour
{
    private DialogueRunner dialogueRunner;

    private void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        StartCoroutine(RunDialogueRunner("Title"));

        /*
        TimeText timeText = FindObjectOfType<TimeText>();
        timeText.StartCoroutine(timeText.IncreaseMinutesInfinitely(0.01f));*/
    }

    private IEnumerator RunDialogueRunner(string startNode)
    {
        while (!dialogueRunner.NodeExists(startNode))
        {
            yield return new WaitForEndOfFrame();
        }

        dialogueRunner.StartDialogue(startNode);
    }

    [YarnCommand("LoadGame")]
    public static void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
