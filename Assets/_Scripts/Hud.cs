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
    [SerializeField] private TMP_Text m_resultValue;
    [SerializeField] private TMP_Text m_victoryValue;
    [SerializeField] private Slider m_musicSlider;
    [SerializeField] private Slider m_soundSlider;
    [SerializeField] private CanvasGroup m_LevelCompletedWindow;
    private GraphicRaycaster m_raycaster;

    public void ShowLevelResults(int value, bool isVictory) {
        ShowWindow(m_LevelCompletedWindow);
        m_resultValue.text = $"{value.ToString()}%";
        m_victoryValue.text = isVictory ? "Level complete!": "Try again! (dev mode: next level enabled)";
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
        GameController.Instance.Audio.MusicVolume = volume;
    }
    public void SetSoundVolume(float volume) {
        GameController.Instance.Audio.SfxVolume = volume;
    }
    public void UpdateOptions() {
        m_musicSlider.value = GameController.Instance.Audio.MusicVolume;
        m_soundSlider.value = GameController.Instance.Audio.SfxVolume;
    }
    public void Next() {
        GameController.Instance.InitializeLevel();
    }
    public void PlayPreviewSound() {
        GameController.Instance.Audio.PlaySound("Drop");
    }
}
