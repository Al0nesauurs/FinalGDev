using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSliderManager : MonoBehaviour {

    float ReloadTime = 0;
    bool Reloading = false;
    public Slider myslider;
    void Start()
    {
        myslider= GetComponent<Slider>();
    }




    void Update()
    {
       // myslider.value = PlayerController.healthbar;
    }


}
