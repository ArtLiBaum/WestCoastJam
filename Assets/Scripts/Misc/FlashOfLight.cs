using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOfLight : MonoBehaviour
{
    // Start is called before the first frame update

    public static void FlashLight()
    {
        instance.flashing = true;
        instance.flashstate = 0;
}

    [SerializeField] float flashspeed = 1;
    private bool flashing;
    private int flashstate = 0;
    static FlashOfLight instance;
    private UnityEngine.UI.Image image;
    void Start()
    {
        instance = this;
        image = GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flashing == true)
        {
            if (flashstate == 0)
            {
                image.color = image.color + new Color(0,0,0, Time.deltaTime * flashspeed);
                if (image.color.a >= 1)
                {
                    flashstate = 1;
                    return;
                }
            }
            else if (flashstate == 1)
            {
                image.color = image.color - new Color(0, 0, 0, Time.deltaTime * flashspeed);
                if (image.color.a <= 0)
                {
                    flashing = false;
                    return;
                }
            }
        }
    }
}
