using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Animator anim;
    public Camera fpsCam;
    public GameObject wind;
    private AudioSource audioSource;
    public AudioClip slashSound;
    public float fireRate;

    private float timeStamp;
    void Start()
    {
        timeStamp = Time.time;
        anim = GetComponent<Animator>();
        audioSource = GameObject.Find("AS1").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && canSlash())
        {
            audioSource.PlayOneShot(slashSound);
            anim.Play("Armature|SLASH");
            SpawnWind();
            timeStamp = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.Play("Armature|Inspect");
        }
    }
    bool canSlash()
    {
        if (Time.time - timeStamp > fireRate) return true;
        else return false;
    }
    private void SpawnWind()
    {
        Instantiate(wind, transform.position, fpsCam.transform.rotation * Quaternion.Euler(0,0,-45));
    }
}
