using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GameController : MonoBehaviour {
    private static GameController m_instance;
    private int currentLevel;

    [SerializeField] private Mixer mixer;
    [SerializeField] private Transform fruitLoadedPosition;
    [SerializeField] private List<LevelParameter> levels;
    [SerializeField] private List<Transform> spawnPoints;
    private List<Color> colors = new List<Color>();
    [SerializeField] private Audio m_audio = new Audio();
    [SerializeField] private const string fruitHalvesName = "Halves";
    [SerializeField] private GameObject smoothie;
    [SerializeField] private Material smoothieMaterial;
    public Audio Audio {
        set { m_audio = value; }
        get { return m_audio; }
    }

    public static GameController Instance {
        get {
            if (m_instance == null) {
                var controller =
                Instantiate(Resources.Load("Prefabs/GameController")) as GameObject;
                m_instance = controller.GetComponent<GameController>();
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
    }
    private void Start() {
        EventManager.OnFruitTapped += LoadFruit;
        EventManager.OnMixButtonTapped += Mix;

        if (levels == null) {
            Debug.LogError("Controller levels is null!");
        }
        if (spawnPoints == null) {
            Debug.LogError("Controller spawn points is null!");
        }
        foreach (LevelParameter level in levels) {
            if (level.fruits.Count > spawnPoints.Count) {
                Debug.LogError("There is a level with fruit count exceeding spawn points count!");
            }
        }
        currentLevel = 0;
        InitializeLevel();
        //Audio.PlayMusic(true);

    }
    public void InitializeLevel() {
        smoothie.SetActive(false);
        ObjectPooler.Instance.ResetPool();

        colors = new List<Color>();
        int i = 0;

        foreach (var fruit in levels[currentLevel].fruits) {
            ObjectPooler.Instance.SpawnFromPool(fruit.name, spawnPoints[i].position, Quaternion.Euler(15, 70, 0));
            i++;
        }
        CustomerController.Instance.CallNewCustomer(levels[currentLevel].targetColor);
    }
    public void InitializeNextLevel() {
        currentLevel++;
        if (currentLevel >= levels.Count) {
            currentLevel = 0;
        }
        InitializeLevel();
    }
    private void LoadFruit(GameObject fruitGameobject) {
        StartCoroutine(LoadFruitInner(fruitGameobject));
    }
    IEnumerator LoadFruitInner(GameObject fruitGameobject) {
        float jumpTime = 1f;
        mixer.OpenMixer();
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(fruitGameobject.transform.DOJump(fruitLoadedPosition.position + GetRandomFlick(), 0.2f, 1, jumpTime));
        mySequence.Join(fruitGameobject.transform.DORotate(new Vector3(90, 0, 0) + GetRandomFlick() * 25, jumpTime));
        yield return new WaitForSeconds(jumpTime);

        GameObject half1 = ObjectPooler.Instance.SpawnFromPool($"{fruitGameobject.tag}{fruitHalvesName}", 
            fruitGameobject.transform.position, Quaternion.Euler(0, 0, 0));
        GameObject half2 = ObjectPooler.Instance.SpawnFromPool($"{fruitGameobject.tag}{fruitHalvesName}", 
            fruitGameobject.transform.position, Quaternion.Euler(0, 180, 0));

        if (half1 != null) {
            fruitGameobject.SetActive(false);
        }

        int i = 0;
        foreach (Transform spawnPoint in spawnPoints) {
            int layerMask = 1 << LayerMask.NameToLayer("Mixable");
            Collider[] hitColliders = Physics.OverlapBox(spawnPoint.position, new Vector3(.1f, .1f, .1f), 
                spawnPoint.rotation, layerMask);

            if (hitColliders.Length == 0) {
                if (i < spawnPoints.Count && i < levels[currentLevel].fruits.Count) {
                    if (fruitGameobject.CompareTag(levels[currentLevel].fruits[i].name)) {
                        ObjectPooler.Instance.SpawnFromPool(fruitGameobject.tag, spawnPoints[i].position, Quaternion.Euler(15, 70, 0));
                        colors.Add(levels[currentLevel].fruits[i].color);
                    }
                }
            }
            i++;
        }

        yield return new WaitForSeconds(.25f);
        mixer.MoveMixer();
    }
    private void Mix() {
        if (colors == null || colors.Count == 0) {
            //Debug.Log("Add fruits to mix!");
            return;
        }

        (float result, Color resultColor) = ColorMixerHelper.GetConformityDegreeInPercent(colors, levels[currentLevel].targetColor);
        smoothieMaterial.color = resultColor;
        StartCoroutine(MixCourutine((int)result));
        
    }
    IEnumerator MixCourutine(int result) {
        float time = mixer.MixMixer();
        yield return new WaitForSeconds(time);
        ObjectPooler.Instance.ResetPool("Customer");
        smoothie.SetActive(true);
        Hud.Instance.ShowLevelResults(result, result > levels[currentLevel].targetPercent);
    }
    void OnDrawGizmosSelected() {
        foreach (Transform spawnPoint in spawnPoints) {
            Gizmos.color = Color.yellow;
            //Gizmos.DrawSphere(spawnPoint.position, .1f);
        }
    }

    private Vector3 GetRandomFlick() {
        return new Vector3(Random.Range(-.05f, .05f), Random.Range(-.05f, .05f), Random.Range(-.05f, .05f));
    }
}
