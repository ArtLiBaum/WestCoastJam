using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ScrollingBG : MonoBehaviour
{
    private float scrollSpeed = -10.0f;
    
    [Tooltip("The width of this object. Set to 0 to auto-calculate.")]
    [SerializeField] private float bgWidth = 0.0f;
    [SerializeField] private float modifier = 10f;
    private float movedDist = 0.0f;
    
    private void Awake()
    {
        var bounds = GetComponent<SpriteRenderer>().bounds;
        
        if (bgWidth == 0.0f)
        {
            bgWidth = bounds.max.x - bounds.min.x;
            // bgWidth = transform.lossyScale.x;
        }

    }

    // Update is called once per frame
    private void Update()
    {
        
        scrollSpeed = GameManager.LevelSpeed * modifier;
        var moveAmt = scrollSpeed * Time.deltaTime;
        movedDist += Mathf.Abs(moveAmt);
        transform.position += Vector3.right * moveAmt;

        if (movedDist > bgWidth)
        {
            movedDist -= bgWidth;
            transform.position -= Vector3.right * bgWidth * Mathf.Sign(scrollSpeed);
        }
    }
}
