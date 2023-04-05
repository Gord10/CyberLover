using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    [YarnCommand("EndGame")]
    public static IEnumerator EndGame()
    {
        float endGameFadeTime = 2;
        Fade.EndGameFade(endGameFadeTime);
        yield return new WaitForSeconds(endGameFadeTime);
        SceneManager.LoadScene("EndGame");
    }

    [YarnCommand("StartAbyss")]
    public static void StartAbyss()
    {
        TimeText.StartAbyss();
        //yield return new WaitForSeconds(5);
    }
}
