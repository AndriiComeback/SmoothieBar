using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Hud : MonoBehaviour {
    private static Hud m_instance;
    public static Hud Instance {
        get {
            return m_instance;
        }
    }
    [SerializeField] private TMP_Text[] m_scoreValue;
    [SerializeField] private Slider m_musicSlider;
    [SerializeField] private Slider m_soundSlider;
    [SerializeField] private CanvasGroup m_LevelCompletedWindow;
    private GraphicRaycaster m_raycaster;

    public void UpdateScoreValue(int value) {
        for (int i = 0; i < m_scoreValue.Length; i++) {
            m_scoreValue[i].text = value.ToString();
        }
    }
    private void Awake() {
        m_instance = this;
        m_raycaster = gameObject.GetComponent<GraphicRaycaster>();
    }
    public void ShowWindow(CanvasGroup window) {
        window.alpha = 1f;
        window.blocksRaycasts = true;
        window.interactable = true;
    }
    public void HideWindow(CanvasGroup window) {
        window.alpha = 0f;
        window.blocksRaycasts = false;
        window.interactable = false;
    }
    public void Quit() {
        Application.Quit();
    }
    public void SetMusicVolume(float volume) {
        Controller.Instance.Audio.MusicVolume = volume;
    }
    public void SetSoundVolume(float volume) {
        Controller.Instance.Audio.SfxVolume = volume;
    }
    public void UpdateOptions() {
        m_musicSlider.value = Controller.Instance.Audio.MusicVolume;
        m_soundSlider.value = Controller.Instance.Audio.SfxVolume;
    }
    public void Next() {
        Controller.Instance.InitializeLevel();
    }
    public void PlayPreviewSound() {
        Controller.Instance.Audio.PlaySound("Drop");
    }
}
