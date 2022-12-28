using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

enum MixerState { Opened, Closed, IsOpening, IsClosing, IsMixing } 
public class Mixer : MonoBehaviour
{
    [SerializeField] private GameObject blenderCover;
    [SerializeField] private GameObject cup;
    [SerializeField] private float openTimeLenght = 1.5f;
    [SerializeField] private float animationTimeInSeconds = .1f;
    private MixerState state;
    private float timeOpened;
    private Vector3 cupInitPosition;
    private Quaternion cupInitRotation;
    private void Awake() {
        state = MixerState.Closed;
        timeOpened = 0f;
        cupInitPosition = blenderCover.transform.position;
        cupInitRotation = blenderCover.transform.rotation;
    }
    public void OpenMixer() {
        if (Equals(state, MixerState.Closed) || Equals(state, MixerState.IsClosing)) {
            StartCoroutine(OpenMixerInner());
        } else if (Equals(state, MixerState.Opened)) {
            timeOpened = 0f;
        }
    }
    public void MoveMixer() {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(cup.transform.DORotate(GetRandomFlick() * 200, animationTimeInSeconds))
            //.Append(cup.transform.DORotate(GetRandomFlick() * 200, animationTimeInSeconds))
            .Append(cup.transform.DORotate(new Vector3(0, 0, 0), animationTimeInSeconds));
    }
    public float MixMixer() {
        state = MixerState.IsMixing;
        Sequence sequenceCup = DOTween.Sequence();
        sequenceCup.Append(blenderCover.transform.DORotate(new Vector3(0, 0, 90), animationTimeInSeconds, RotateMode.LocalAxisAdd))
            .Join(blenderCover.transform.DOMove(cupInitPosition, animationTimeInSeconds));

        Sequence sequence = DOTween.Sequence();
        sequence.Append(cup.transform.DORotate(GetRandomFlick() * 400, animationTimeInSeconds / 2f))
            .Append(cup.transform.DORotate(GetRandomFlick() * 400, animationTimeInSeconds / 2f))
            .Append(cup.transform.DORotate(GetRandomFlick() * 400, animationTimeInSeconds / 2f))
            .Append(cup.transform.DORotate(GetRandomFlick() * 400, animationTimeInSeconds / 2f))
            .Append(cup.transform.DORotate(GetRandomFlick() * 400, animationTimeInSeconds / 2f))
            .Append(cup.transform.DORotate(GetRandomFlick() * 400, animationTimeInSeconds / 2f))
            .Append(cup.transform.DORotate(new Vector3(0, 0, 0), animationTimeInSeconds));

        StartCoroutine(MixMixerInner());
        return animationTimeInSeconds * 4;
    }
    IEnumerator MixMixerInner() {
        yield return new WaitForSeconds(animationTimeInSeconds * 4);
        blenderCover.transform.rotation = cupInitRotation;
        state = MixerState.Closed;
    }
    private IEnumerator OpenMixerInner() {
        state = MixerState.IsOpening;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(blenderCover.transform.DORotate(new Vector3(0, 0, -90), animationTimeInSeconds, RotateMode.LocalAxisAdd))
            .Join(blenderCover.transform.DOLocalMove(new Vector3(.06f, .37f, .05f), animationTimeInSeconds));

        yield return new WaitForSeconds(animationTimeInSeconds);
        state = MixerState.Opened;
    }
    private IEnumerator CloseMixerCupInner() {
        state = MixerState.IsClosing;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(blenderCover.transform.DORotate(new Vector3(0, 0, 90), animationTimeInSeconds, RotateMode.WorldAxisAdd))
            .Join(blenderCover.transform.DOMove(cupInitPosition, animationTimeInSeconds));

        yield return new WaitForSeconds(animationTimeInSeconds);
        blenderCover.transform.rotation = cupInitRotation;
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
    private Vector3 GetRandomFlick() {
        return new Vector3(Random.Range(-.05f, .05f), Random.Range(-.05f, .05f), Random.Range(-.05f, .05f));
    }
}
