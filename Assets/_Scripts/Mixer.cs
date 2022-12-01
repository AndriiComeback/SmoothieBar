using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

enum MixerState { Opened, Closed, InProgress } 
public class Mixer : MonoBehaviour
{
    [SerializeField] private GameObject blenderCover;
    [SerializeField] private float openTimeLenght = 1.5f;
    [SerializeField] private float animationTimeInSeconds = .25f;
    private MixerState state;
    private float timeOpened;
    private void Awake() {
        state = MixerState.Closed;
        timeOpened = 0f;
    }
    public void OpenMixer() {
        if (Equals(state, MixerState.Closed)) {
            StartCoroutine(OpenMixerInner());
        } else if (Equals(state, MixerState.Opened)) {
            timeOpened = 0f;
        }
    }
    private IEnumerator OpenMixerInner() {
        state = MixerState.InProgress;

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(blenderCover.transform.DORotate(new Vector3(0, 0, -90), animationTimeInSeconds, RotateMode.LocalAxisAdd))
            .Join(blenderCover.transform.DOLocalMove(new Vector3(.06f, .35f, 0), animationTimeInSeconds));

        yield return new WaitForSeconds(animationTimeInSeconds);
        state = MixerState.Opened;
    }
    private IEnumerator CloseMixerCupInner() {
        state = MixerState.InProgress;

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(blenderCover.transform.DORotate(new Vector3(0, 0, 90), animationTimeInSeconds, RotateMode.LocalAxisAdd))
            .Join(blenderCover.transform.DOLocalMove(new Vector3(0f, .25f, 0), animationTimeInSeconds));

        yield return new WaitForSeconds(animationTimeInSeconds);
        state = MixerState.Closed;
    }
    void Update() {
        if (Equals(state, MixerState.Opened)) {
            timeOpened += Time.deltaTime;
            if (timeOpened > openTimeLenght) {
                timeOpened = 0f;
                StartCoroutine(CloseMixerCupInner());
            }
        }
    }
}
