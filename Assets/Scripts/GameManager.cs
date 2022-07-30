using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   private static float levelSpeed = 1f;
   public static float LevelSpeed => levelSpeed;

   private GameManager instance;

   private void Awake()
   {
      if(instance)
         Destroy(this);
      else
      {
         instance = this;
      }
   }


   public static void ChangeSpeed(float delta)
   {
      levelSpeed += delta;
   }
}
