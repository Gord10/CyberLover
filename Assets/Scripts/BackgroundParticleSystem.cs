using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParticleSystem : MonoBehaviour
{
    public float speedOnFastForward = 50;
    public float acceleration = 30;

    private ParticleSystem particleSystem;
    private float normalSpeed = 0;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        normalSpeed = particleSystem.playbackSpeed;
    }

    public void OnFastForward()
    {
        StartCoroutine(ChangePlaybackSpeed(speedOnFastForward));
    }

    private IEnumerator ChangePlaybackSpeed(float targetSpeed)
    {
        while (particleSystem.playbackSpeed != targetSpeed)
        {
            particleSystem.playbackSpeed = Mathf.MoveTowards(particleSystem.playbackSpeed, targetSpeed, Time.deltaTime * acceleration);
            yield return new WaitForEndOfFrame();
            //print(particleSystem.playbackSpeed);
        }
    }

    public void TurnBackToNormalSpeed()
    {
        StartCoroutine(ChangePlaybackSpeed(normalSpeed));
    }

    public void MultiplyPlaybackSpeed(float coFactor)
    {
        particleSystem.playbackSpeed *= coFactor;
    }

}
