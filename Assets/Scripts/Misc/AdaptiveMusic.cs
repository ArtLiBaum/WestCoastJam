using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveMusic : MonoBehaviour
{
    [SerializeField] private List<AudioSource> layers = new List<AudioSource>();

    [SerializeField] private List<Vector2> layerFadePoints = new List<Vector2>();

    private void Update()
    {
        var levelSpeed = Mathf.Abs(GameManager.LevelSpeed);
        for (var i = 0; i < layers.Count; ++i)
        {
            if (levelSpeed < layerFadePoints[i].x)
            {
                layers[i].volume = 0;
            }
            else if (levelSpeed > layerFadePoints[i].y)
            {
                layers[i].volume = 1;
            }
            else
            {
                layers[i].volume = Mathf.Lerp(
                    0,
                    1,
                    (levelSpeed - layerFadePoints[i].x)
                        / (layerFadePoints[i].y - layerFadePoints[i].x));
            }
        }
    }
}
