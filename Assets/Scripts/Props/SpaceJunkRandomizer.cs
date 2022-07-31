using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class SpaceJunkRandomizer : MonoBehaviour
{
    [SerializeField] List<Sprite> junkSprites = new List<Sprite>();
    private void Awake()
    {
        var loadedSprites = Resources.LoadAll("SpaceJunk", typeof(Sprite));

        foreach (var obj in loadedSprites)
        {
            junkSprites.Add((Sprite)obj);
        }
    }

    public Sprite RandomJunkSprite()
    {
        return junkSprites[Random.Range(0, junkSprites.Count)];
    }
}
