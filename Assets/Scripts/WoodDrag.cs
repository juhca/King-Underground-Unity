using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDrag : MonoBehaviour {


    bool didItPlay = false;
    private void OnCollisionStay(Collision col)
    {
        if(!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().volume = 1;
            GetComponent<AudioSource>().Play();
        }
    }


    /*
    void Update()
    {


        
        if (GetComponent<Rigidbody>().velocity.magnitude >= 0.1 && !GetComponent<AudioSource>().isPlaying && !didItPlay)
        {
            GetComponent<AudioSource>().Play();
            didItPlay = true;
        }
        
    }*/
}
