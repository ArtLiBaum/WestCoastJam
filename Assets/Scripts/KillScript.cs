using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class KillScript : MonoBehaviour
{
  [SerializeField] private List<AudioClip> crunchClips;
  private AudioSource _source;

  private void Awake()
  {
    transform.rotation = Quaternion.Euler(0, 0, Random.Range(-45, 45));
    _source = GetComponent<AudioSource>();
    _source.pitch = Random.Range(0.9f, 1.1f);
    _source.PlayOneShot(crunchClips[Random.Range(0, crunchClips.Count)]);
  }

  public void Kill()
  {
    Destroy(gameObject);
  }
}
