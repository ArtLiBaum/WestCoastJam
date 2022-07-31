using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static float LevelSpeed { get; set; } = -3f;
    public static float LevelTime => -LevelSpeed * Time.deltaTime;
    public static float CoasterTimeFraction = 0;

    [SerializeField] [Range(-5, -1)] private float startSpeed = -3f;
    [SerializeField] private float dragModifier = 0.1f;
    [SerializeField] private float CoasterSpeedGoal = -4.5f;
    [SerializeField] private float CoasterTimeGoal = 5;

    private float CoasterTimer = 0;

    private PropGenerator _propGenerator;


    [Header("UI Hookups")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;

    private GameManager instance;

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
                    CoasterTimer = 0;
                    CoasterTimeGoal = (int)LevelBenchmarks.MedLevel;
                    break;
                case PropGenerator.SpawnState.RandomMed:

                    _propGenerator.SetState(PropGenerator.SpawnState.RandomHard);
                    CoasterTimer = 0;
                    CoasterTimeGoal = (int)LevelBenchmarks.HardLevel;

                    break;
                case PropGenerator.SpawnState.RandomHard:
                    //if win then activate winScreen
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
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
