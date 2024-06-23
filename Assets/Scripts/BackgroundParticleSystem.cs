using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class BackgroundParticleSystem : MonoBehaviour
{
    public float speedOnFastForward = 50;
    public float acceleration = 30;

    private ParticleSystem particleSystem;
    private float normalSpeed = 0;

    private static BackgroundParticleSystem instance;
    private Coroutine ChangePlaybackSpeedCoRoutine;


    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        normalSpeed = particleSystem.playbackSpeed;
        instance = this;
    }

    public void OnFastForward()
    {
        if(ChangePlaybackSpeedCoRoutine != null)
        {
            StopCoroutine(ChangePlaybackSpeedCoRoutine);
        }

        ChangePlaybackSpeedCoRoutine = StartCoroutine(ChangePlaybackSpeed(speedOnFastForward));
    }

    private IEnumerator ChangePlaybackSpeed(float targetSpeed)
    {
        print("Target speed: " + targetSpeed);
        while (particleSystem && particleSystem.playbackSpeed != targetSpeed)
        {
            particleSystem.playbackSpeed = Mathf.MoveTowards(particleSystem.playbackSpeed, targetSpeed, Time.deltaTime * acceleration);
            yield return new WaitForEndOfFrame();
            //print(particleSystem.playbackSpeed + " Target speed: " + targetSpeed);
        }
    }

    public void TurnBackToNormalSpeed()
    {
        print("Normal speed " + normalSpeed);

        if(ChangePlaybackSpeedCoRoutine != null)
        {
            StopCoroutine(ChangePlaybackSpeedCoRoutine);
        }

        ChangePlaybackSpeedCoRoutine = StartCoroutine(ChangePlaybackSpeed(normalSpeed));
    }

    public void MultiplyPlaybackSpeed(float coFactor)
    {
        particleSystem.playbackSpeed *= coFactor;
    }

    private void OnDestroy()
    {
        if(ChangePlaybackSpeedCoRoutine != null)
        {
            StopCoroutine(ChangePlaybackSpeedCoRoutine);
        }
    }


    [YarnCommand("HideBackgroundParticles")]
    public static void Hide()
    {
        instance.gameObject.SetActive(false);
    }

}
