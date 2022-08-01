using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static float LevelSpeed { get; set; } = -3f;
    public static float LevelTime => -LevelSpeed * Time.deltaTime;
    public static int TotalPoints;
    public static int TotalHits;
    public static bool isPlaying = true;


    public static float CoasterTimeFraction = 0;

    public static bool IsAscending = false;
    [SerializeField] [Range(-5, -1)] private float startSpeed = -3f;
    [SerializeField] private float dragModifier = 0.1f;
    [SerializeField] private float CoasterSpeedGoal = -4.5f;
    [SerializeField] private float CoasterTimeGoal = 5;
    [SerializeField] private float AscensionTimeGoal = 10;
    [SerializeField] private float AscensionSpeed = -2;
    private float AscensionTimer = -1;
    private float CoasterTimer = 0;
    
    private PropGenerator _propGenerator;
    private AudioClip _asenscionNoise;

    [Header("UI Hookups")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;

    [SerializeField] private ScrollingBG stars;
    [Header("Other Objects")]
    private static GameManager instance;

    [Header("Last-minute hacks")]
    [SerializeField] private GameObject BG1;
    [SerializeField] private GameObject BG2;
    [SerializeField] private GameObject BG3;

    private static AudioSource _source;
    
    enum LevelBenchmarks
    {
        EasyLevel = 10,
        MedLevel = 15,
        HardLevel = 20,
    }



    private void Awake()
    {
        if (instance)
            Destroy(this);
        else
        {
            instance = this;
        }

        _source = GetComponent<AudioSource>();
        _asenscionNoise = Resources.Load("SFX/assention") as AudioClip;
    }

    private void Start()
    {
        _propGenerator = FindObjectOfType<PropGenerator>();
        LevelSpeed = startSpeed;
        CoasterTimer = 0;
        CoasterTimeGoal = (int)LevelBenchmarks.EasyLevel;
        TotalHits = 0;
        TotalPoints = 0;
        isPlaying = true;

        BG1.active = true;
        BG2.active = false;
        BG3.active = false;
    }

    public static void AdjustSpeed(float amount, float time)
    {
        instance.StartCoroutine(SpeedAdjust(amount, time));
    }
    
    private static IEnumerator SpeedAdjust(float amount, float time)
    {
        var modLeft = amount;
        while (modLeft > 0)
        {
            var dt = Time.deltaTime / time * amount;
            LevelSpeed -= Mathf.Min(dt, modLeft);
            modLeft -= dt;
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            FlashOfLight.FlashLight();
        }
        stars.SetAlpha(CoasterTimeFraction);
        print("" + LevelSpeed + "   " + CoasterTimeFraction);
        if (LevelSpeed < -1)
        {
            //drag rises exponentially with the speed
            float drag = Mathf.Abs(LevelSpeed) * dragModifier * Time.fixedDeltaTime;
            LevelSpeed += drag;
        }
        else
        {
            LevelSpeed += Time.fixedDeltaTime * dragModifier;
        }

        //print(drag);
        if (LevelSpeed > -0.01)
        {
            //Game Over
            LevelSpeed = 0;
            gameOverScreen.SetActive(true);
            isPlaying = false;
        }

        if ((LevelSpeed <= CoasterSpeedGoal))
        {
            CoasterTimer += Time.fixedDeltaTime;
            CoasterTimeFraction = CoasterTimer / CoasterTimeGoal;
        }

        if (CoasterTimeFraction >= 1)
        {
            IsAscending = true;
            CoasterTimeFraction = 0;
            if (_propGenerator.CurrentState != PropGenerator.SpawnState.RandomHard)
            {
                FlashOfLight.FlashLight();
            }
            
            // foreach(var obj in Multitag.FindGameObjectsWithTag("Prop"))
            // {
            //     obj.SetActive(false);
            // }
            //

            for (int i = 0; i < _propGenerator.transform.childCount; i++)
            {
                Destroy(_propGenerator.transform.GetChild(i).gameObject);
            }
            _propGenerator.gameObject.SetActive(false);
            switch (_propGenerator.CurrentState)
            {
                case PropGenerator.SpawnState.RandomEasy:
                    _propGenerator.SetState(PropGenerator.SpawnState.RandomMed);
                    if (TotalHits <= 10)
                        ++TotalPoints;
                    CoasterTimer = 0;
                    CoasterTimeGoal = (int)LevelBenchmarks.MedLevel;

                    break;
                case PropGenerator.SpawnState.RandomMed:
                _propGenerator.SetState(PropGenerator.SpawnState.RandomHard);
                   if (TotalHits <= 20)
                        ++TotalPoints;
                    CoasterTimer = 0;
                    CoasterTimeGoal = (int)LevelBenchmarks.HardLevel;
                    break;
                case PropGenerator.SpawnState.RandomHard:
                    //if win then activate winScreen
                    if (TotalHits <= 30)
                        ++TotalPoints;
                    CoasterTimer = 0;
                    isPlaying = false;
                    winScreen.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        print("" + AscensionTimer + " | " + IsAscending);
        if (IsAscending)
        {
            LevelSpeed = AscensionSpeed;
            stars.SetAlpha(1);
            CoasterTimer = 0;
            AscensionTimer += Time.fixedDeltaTime;
            if(!_source.isPlaying)
                PlayHitSound(_asenscionNoise);
            
            if (AscensionTimer >= AscensionTimeGoal)
            {
                stars.FadeOut();
                AscensionTimer = 0;
                IsAscending = false;
                switch(_propGenerator.CurrentState)
                {
                    case PropGenerator.SpawnState.RandomMed:
                        BG1.active = false;
                        BG2.active = true;
                        BG3.active = false;
                        break;

                    case PropGenerator.SpawnState.RandomHard:
                        BG1.active = false;
                        BG2.active = false;
                        BG3.active = true;
                        break;
                }
                FlashOfLight.FlashLight();
                _propGenerator.gameObject.SetActive(true);
            }
        }

    }

    public void RestartLevel()
    {
        LevelSpeed = -1f;
        _propGenerator.SetState(PropGenerator.SpawnState.RandomEasy);
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void PlayHitSound(AudioClip clip)
    {
        _source.pitch = Random.Range(0.9f, 1.1f);
        _source.PlayOneShot(clip);
    }

}
