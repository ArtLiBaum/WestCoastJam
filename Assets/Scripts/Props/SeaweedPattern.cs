using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaweedPattern : MonoBehaviour
{
   [SerializeField] private Animation seaweedPath;
   [SerializeField] private Animator pathAnimator;
   public void Play()
   {
      pathAnimator.Play(seaweedPath.name);
   }
}
