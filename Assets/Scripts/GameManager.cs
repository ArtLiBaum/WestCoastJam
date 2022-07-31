using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static float LevelSpeed { get; set; } = -3f;
    public static float LevelTime => -LevelSpeed * Time.deltaTime;
    public static int TotalPoints;
    public static int TotalHits;
    public static bool isPlaying = true;


    public static float CoasterTimeFraction = 0;
    [SerializeField] [Range(-5,-1)] private float startSpeed = -3f;
    [SerializeField] private float dragModifier = 0.1f;       
    [SerializeField] private float CoasterSpeedGoal = -5;
    [SerializeField] private float CoasterTimeGoal = -5;

    private float CoasterTimer = 0;

    private PropGenerator _propGenerator;


    [Header("UI Hookups")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;

    private static GameManager instance;

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

    }

    public void RestartLevel()
    {
        LevelSpeed = -1f;
        _propGenerator.SetState(PropGenerator.SpawnState.RandomEasy);
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
