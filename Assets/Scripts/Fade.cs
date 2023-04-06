using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Yarn.Unity;

public class Fade : MonoBehaviour
{
    private Image fadeImage;
    private static Fade instance;

    private void Awake()
    {
        instance = this;

        fadeImage = GetComponent<Image>();
        SetAlpha(1);
        fadeImage.DOFade(0, 1);
    }

    [YarnCommand("FreeWillFade")]
    public static IEnumerator FreeWillFade(float fadeTime)
    {
        instance.fadeImage.DOFade(1, fadeTime);
        yield return new WaitForSeconds(fadeTime);
        instance.fadeImage.DOFade(0, fadeTime);
        yield return new WaitForSeconds(fadeTime);
    }

    public static void EndGameFade(float fadeTime)
    {
        instance.fadeImage.color = Color.black;
        instance.SetAlpha(0);
        instance.fadeImage.DOFade(1, fadeTime);
    }

    public void SetAlpha(float newAlpha)
    {
        Color color = fadeImage.color;
        color.a = newAlpha;
        fadeImage.color = color;
    }
}
