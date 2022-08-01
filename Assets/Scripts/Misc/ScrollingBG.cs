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
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer childSprite;
    private bool fadeOut = false;

    public void SetAlpha(float alpha)
    {

        sprite.color = sprite.color - new Color(0, 0, 0, sprite.color.a - alpha);
        childSprite.color = sprite.color;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }

    private void Awake()
    {
        if (!sprite)
        {
            sprite = GetComponent<SpriteRenderer>();
            childSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        


        var bounds = sprite.bounds;
        
        if (bgWidth == 0.0f)
        {
            bgWidth = bounds.max.x - bounds.min.x;
            // bgWidth = transform.lossyScale.x;
        }

    }


    // Update is called once per frame
    private void Update()
    {
       

        if (fadeOut)
        {
            SetAlpha(sprite.color.a - Time.deltaTime);
            if (sprite.color.a <= 0)
            {
                fadeOut = false;
                SetAlpha(1);
            }
        }
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
