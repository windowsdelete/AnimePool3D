using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBall : MonoBehaviour
{
    public AudioClip aClip;
    public AudioSource aSour;

    void OnTriggerEnter(Collider other) {
    	if(other.tag == "Player" || other.tag == "Respawn")
    		aSour.PlayOneShot(aClip);
    		}
    	}
