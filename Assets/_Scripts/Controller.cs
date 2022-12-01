using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.SocialPlatforms.Impl;
using DG.Tweening;

public class Controller : MonoBehaviour {
    private static Controller m_instance;
    private int m_currentLevel;

    [SerializeField] private Transform fruitLoadedPosition;
    [SerializeField] private List<LevelParameter> levels;
    public int CurrentLevel {
        set { m_currentLevel = value; }
        get { return m_currentLevel; }
    }
    [SerializeField] private Score m_score;
    public Score Score {
        set { m_score = value; }
        get { return m_score; }
    }
    [SerializeField] private Audio m_audio = new Audio();
    public Audio Audio {
        set { m_audio = value; }
        get { return m_audio; }
    }

    public static Controller Instance {
        get {
            if (m_instance == null) {
                var controller =
                Instantiate(Resources.Load("Prefabs/Controller")) as GameObject;
                m_instance = controller.GetComponent<Controller>();
            }
            return m_instance;
        }
    }
    private void Awake() {
        if (m_instance == null) {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            if (m_instance != this)
                Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Audio.SourceMusic = gameObject.AddComponent<AudioSource>();
        Audio.SourceRandomPitchSFX = gameObject.AddComponent<AudioSource>();
        Audio.SourceSFX = gameObject.AddComponent<AudioSource>();

        EventManager.OnFruitTapped += LoadFruit;

        // to do: get info from level settings
    }
    private void Start() {
        InitializeLevel();
        Audio.PlayMusic(true);
    }
    public void InitializeLevel() {
        
    }
    private void LoadFruit(GameObject fruitGameobject) {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(fruitGameobject.transform.DOJump(fruitLoadedPosition.position, 0.2f, 1, 1f));
    }
}
