using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    [YarnCommand("EndGame")]
    public static IEnumerator OpenGameEndScene()
    {
        float endGameFadeTime = 2;
        Fade.EndGameFade(endGameFadeTime);
        yield return new WaitForSeconds(endGameFadeTime);
        SceneManager.LoadScene("EndGame");
    }
}
