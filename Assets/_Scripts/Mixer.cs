using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

enum MixerState { Opened, Closed, IsOpening, IsClosing } 
public class Mixer : MonoBehaviour
{
    [SerializeField] private GameObject blenderCover;
    [SerializeField] private float openTimeLenght = 1.5f;
    [SerializeField] private float animationTimeInSeconds = .25f;
    private MixerState state;
    private float timeOpened;
    private Sequence sequence;
    private void Awake() {
        state = MixerState.Closed;
        timeOpened = 0f;
        sequence = DOTween.Sequence();
    }
    public void OpenMixer() {
        if (Equals(state, MixerState.Closed) || Equals(state, MixerState.IsClosing)) {
            StartCoroutine(OpenMixerInner());
        } else if (Equals(state, MixerState.Opened)) {
            timeOpened = 0f;
        }
    }
    private IEnumerator OpenMixerInner() {
        state = MixerState.IsOpening;
        sequence.Append(blenderCover.transform.DORotate(new Vector3(0, 0, -90), animationTimeInSeconds, RotateMode.LocalAxisAdd))
            .Join(blenderCover.transform.DOLocalMove(new Vector3(.06f, .37f, .05f), animationTimeInSeconds));

        yield return new WaitForSeconds(animationTimeInSeconds);
        state = MixerState.Opened;
    }
    private IEnumerator CloseMixerCupInner() {
        state = MixerState.IsClosing;

        sequence.Append(blenderCover.transform.DORotate(new Vector3(0, 0, 90), animationTimeInSeconds, RotateMode.LocalAxisAdd))
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
