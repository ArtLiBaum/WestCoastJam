using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static float LevelSpeed { get; set; } = -1f;
    public static float LevelTime => -LevelSpeed * Time.deltaTime;

    [SerializeField] private float dragModifier = 0.1f;       
    [SerializeField] private float CoasterSpeedGoal = -5;
    [SerializeField] private float CoasterTimeGoal = -5;

    private float CoasterTimer = 0;


    [Header("UI Hookups")] [SerializeField]
    private GameObject gameOverScreen; 

    private GameManager instance;


    private void Update()
    {
        //TODO Slow the player over time

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

    private void FixedUpdate()
    {
        //print(LevelSpeed);
        //drag rises exponentially with the speed
        float drag = LevelSpeed * LevelSpeed * dragModifier * Time.deltaTime;
        LevelSpeed += drag;
        print(drag);
        if (LevelSpeed > -0.01)
        {
            //Game Over
            LevelSpeed = 0;
            gameOverScreen.SetActive(true);
            
        }

        if (LevelSpeed <= CoasterSpeedGoal)
        {
            CoasterTimer += Time.deltaTime;
        }
    }

    public void RestartLevel()
    {
        LevelSpeed = -1f;
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
