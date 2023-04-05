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
        fadeImage.DOFade(0, 1).ChangeStartValue(1);
    }

    [YarnCommand("FreeWillFade")]
    public static IEnumerator FreeWillFade(float fadeTime)
    {
        instance.fadeImage.DOFade(1, fadeTime);
        yield return new WaitForSeconds(fadeTime);
        instance.fadeImage.DOFade(0, fadeTime);
        yield return new WaitForSeconds(fadeTime);
    }

}
