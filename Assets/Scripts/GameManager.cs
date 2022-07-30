using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float LevelSpeed { get; set; } = -1f;
    public static float LevelTime => -LevelSpeed * Time.deltaTime;

    [SerializeField] private float LevelSpeedDecay = 0.1f;       
    [SerializeField] private float CoasterSpeedGoal = -5;
    [SerializeField] private float CoasterTimeGoal = -5;

    private float CoasterTimer = 0;


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
        print(LevelSpeed);
        LevelSpeed += LevelSpeedDecay * Time.deltaTime;
        if (LevelSpeed > 0)
        {
            LevelSpeed = 0;
        }

        if (LevelSpeed <= CoasterSpeedGoal)
        {
            CoasterTimer += Time.deltaTime;
        }
    }
}
