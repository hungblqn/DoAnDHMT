using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float projectileSpeed = 50;
    private AudioSource audioSource;
    public AudioClip windHitSound;
    void Start()
    {
        StartCoroutine(AutoDestroy());
        audioSource = GameObject.Find("AS1").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
    }
    IEnumerator AutoDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(windHitSound);
            other.GetComponent<Target>().health -= 50;
        }
    }
}
