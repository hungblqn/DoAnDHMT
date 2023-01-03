using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    public Camera fpsCamera;
    public Transform laserOrigin;
    public Text ammoText;
    public AudioClip laserSound;
    public AudioClip noAmmoSound;

    private int ammo = 100;
    private int ammoStorage = 1000;
    private int ammoLimit;
    private float damage = 100;
    private float fireRate = 0.1f;
    private float gunRange = 50f;
    private float laserDuration = 0.05f;
    private LineRenderer laserLine;
    private Animator handAnimator;
    private AudioSource audioSource;

    private float timeStamp = 0;
    private void Start()
    {
        ammoLimit = ammo;
        ammoText.text = ammo + "/" + ammoStorage;

        laserLine = GetComponent<LineRenderer>();
        handAnimator = GetComponent<Animator>();
        audioSource = GameObject.Find("AS1").GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && canShoot())
        {
            Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && canReload())
        {
            audioSource.PlayOneShot(noAmmoSound);
        }
        if (Input.GetKeyDown(KeyCode.R) && canReload())
        {
            Reload();
        }
    }
    private void Reload()
    {
        int ammoNeed;
        ammoNeed = ammoLimit - ammo;
        ammo = ammoLimit;
        ammoStorage -= ammoNeed;

        if (ammoStorage < 0)
        {
            ammo += ammoStorage;
            ammoStorage = 0;
        }
        ammoText.text = ammo + "/" + ammoStorage;
    }
    private bool canReload()
    {
        if (ammo < ammoLimit && ammo >= 0 && ammoStorage > 0) return true;
        else return false;
    }
    private void Shoot()
    {
        handAnimator.Play("SHOOT");
        audioSource.PlayOneShot(laserSound);
        ammo--;
        ammoText.text = ammo + "/" + ammoStorage;
        timeStamp = Time.time;
        laserLine.SetPosition(0, laserOrigin.position);
        Vector3 rayOrigin = fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if(Physics.Raycast(rayOrigin,fpsCamera.transform.forward,out hit, gunRange))
        {
            laserLine.SetPosition(1, hit.point);
            //damage
            Target target = hit.transform.GetComponent<Target>();
            if(target != null) target.TakeDamage(damage);
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (fpsCamera.transform.forward * gunRange));
        } 
        StartCoroutine(ShootLaser());
    }
    private bool canShoot()
    {
        if (ammo > 0 && Time.time - timeStamp > fireRate)
        {
            return true;
        }
        else return false;
    }
    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}
