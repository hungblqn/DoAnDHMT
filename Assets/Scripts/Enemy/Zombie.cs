using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = System.Random;

public class Zombie : MonoBehaviour
{
    private PlayerController playerController;

    private GameObject player;
    private NavMeshAgent enemy;
    private Animator enemyAnimator;
    public LayerMask playerLayerMask;

    public Slider healthBar;

    public GameObject healthDrop;
    public GameObject gemDrop;

    private float attackCooldown = 1.0f;

    private float timeStamp;

    private float maxHealth;

    private Vector3 savePos;
    void Start()
    {
        savePos = transform.position;
        timeStamp = Time.time;
        maxHealth = gameObject.GetComponent<Target>().health;
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.Find("FPS Player");
        enemyAnimator = GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = gameObject.GetComponent<Target>().health / maxHealth;
        if(!isNearPlayer()) enemy.SetDestination(player.transform.position);
        if (isNearPlayer() && canAttack())
        {
            Attack();
        }
    }
    private void OnDestroy()
    {
        Drop();
    }
    
    private void Drop()
    {
        Random generator = new Random();
        const int probabilityWindow = 30;
        int randomChance = generator.Next(0, 100);

        if (randomChance < 10)
        {
            Instantiate(healthDrop, transform.position + new Vector3(1,1,0),transform.rotation);
        }
        if(randomChance < probabilityWindow)
        {
            Instantiate(gemDrop, transform.position + new Vector3(0, 1, 0), transform.rotation);
        }
    }
    private void Attack()
    {
        timeStamp = Time.time;
        enemyAnimator.Play("ATTACK_002");
        playerController.characterHealth -= 10;
    }
    private bool canAttack()
    {
        if (Time.time - timeStamp > attackCooldown) return true;
        else return false;
    }

    bool isNearPlayer()
    {
        return Physics.CheckSphere(transform.position, 3, playerLayerMask);
    }
}
