using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fillen : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Img;
    public UnitStats profile;
    private float percent;
    private int cur;
    public void Bar(int value)
    {
        cur = value;
        percent = (float)1 - (float)cur / 100;
        Img.fillAmount = percent;
    }
    public float curpercent
    {
        get { return percent; }
    }
    public int curint
    {
        get { return cur; }

    }
    void Start()
    {
        // change the variable 
        Bar(profile.health = 50);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Bar(profile.health);
    }
}
