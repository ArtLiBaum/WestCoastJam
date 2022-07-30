using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static float LevelSpeed { get; set; } = -1f;

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
}
