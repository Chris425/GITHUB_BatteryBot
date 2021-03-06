﻿using UnityEngine;
using System.Collections;

public class MeleeFollowSP : MonoBehaviour
{
    private Animator anim;
    UnityEngine.AI.NavMeshAgent agent;
    //get location of character!
    public GameObject target;
    public float distanceX;
    public float distanceZ;
    public float distanceY;
    private float cooldown = 6.0f;
    private float cooldownTimer;
    bool shouldPlayAggroEffect = false;
    Quaternion aggroRot = new Quaternion(0.0f, 180.0f, 180.0f, 0.0f);

    public GameObject BiteSpecEffect1;
    public GameObject BiteSpecEffect2;
    public GameObject DeathSpecEffect;
    public GameObject BloodSpecEffect;
    public GameObject AggroSpecEffect;

    //LOOT
    public GameObject RedBattery;
    public GameObject GreenBattery;
    public GameObject Gear;

    public GameObject GunDrop;

    public static bool isAggroed;

    public int health = 2;
    public int vampireDamage = 20;
    public bool isSummoned = false;

    void OnEnable()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.Find("BatteryBot");

        anim = this.GetComponentInChildren<Animator>();
        anim.applyRootMotion = false;
        anim.SetBool("IsAggroed",false);

        if (isSummoned)
        {
            isAggroed = true;
            anim.SetBool("IsAggroed", true);
        }
        else
        {
            isAggroed = false;
        }
        
        shouldPlayAggroEffect = true;
    }

    public void OnCollisionEnter(Collision other)
    {
        //case when your player projectile hits the vampire
        if (other.gameObject.name.Contains("Shot"))
        {

            //make a special effect on death
            if (other.gameObject.name.Contains("PlayerShot"))
            {
                //Note that multishot has the same damage - you just shoot a bunch at the same time
                if (HeroControllerSP.isSuperCharged == true)
                {
                    health -= 2;
                    Instantiate(DeathSpecEffect, other.transform.position, this.transform.rotation);
                }
                else
                {
                    health -= 1;
                    Instantiate(DeathSpecEffect, other.transform.position, this.transform.rotation);
                }
            }
            else if (other.gameObject.name.Contains("GS_Shot"))
            {
                if (other.gameObject.name.Contains("FIRE"))
                {
                    health -= 4;
                    Instantiate(BloodSpecEffect, other.transform.position, this.transform.rotation);
                }
                else
                {
                    health -= 2;
                    Instantiate(BloodSpecEffect, other.transform.position, this.transform.rotation);
                }

            }
            else if (other.gameObject.name.Contains("Axe_Shot"))
            {
                if (other.gameObject.name.Contains("LIGHTNING"))
                {
                    health -= 2;
                    Instantiate(BloodSpecEffect, other.transform.position, this.transform.rotation);
                }
                else
                {
                    health -= 1;
                    Instantiate(BloodSpecEffect, other.transform.position, this.transform.rotation);
                }

            }
            //note that shield shot IS the ice special... shield normally shoots an axe shot (because reasons)
            else if (other.gameObject.name.Contains("Shield_Shot"))
            {
                health -= 2;
                Instantiate(DeathSpecEffect, other.transform.position, this.transform.rotation);
            }
            //Only spawn loot if it is a normal vampire, not a summoned one.

            if (health <= 0)
            {
                //vampire is dead
                Destroy(this.gameObject);

                if (!isSummoned)
                {


                    int randomNum = Random.Range(1, 14);
                    if (randomNum <= 2)
                    {
                        int posOffset = Random.Range(-4, 4);
                        int rotOffset1 = Random.Range(1, 180);
                        int rotOffset2 = Random.Range(1, 180);
                        Vector3 spawnPos = new Vector3(this.transform.position.x, this.transform.position.y + posOffset, this.transform.position.z);
                        Quaternion spawnRot = new Quaternion(this.transform.rotation.x + rotOffset1, this.transform.rotation.y + rotOffset2, this.transform.rotation.z + rotOffset1, this.transform.rotation.w + rotOffset2);
                        Instantiate(RedBattery, spawnPos, spawnRot);

                    }
                    else if (randomNum == 3 || randomNum == 4)
                    {
                        int posOffset = Random.Range(-4, 4);
                        int rotOffset1 = Random.Range(1, 180);
                        int rotOffset2 = Random.Range(1, 180);
                        Vector3 spawnPos = new Vector3(this.transform.position.x, this.transform.position.y + posOffset, this.transform.position.z);
                        Quaternion spawnRot = new Quaternion(this.transform.rotation.x + rotOffset1, this.transform.rotation.y + rotOffset2, this.transform.rotation.z + rotOffset1, this.transform.rotation.w + rotOffset2);
                        Instantiate(GreenBattery, spawnPos, spawnRot);

                    }
                    else if (randomNum == 5)
                    {
                        int posOffset = Random.Range(1, 3);
                        int rotOffset1 = Random.Range(1, 180);
                        int rotOffset2 = Random.Range(1, 180);
                        Vector3 spawnPos = new Vector3(this.transform.position.x, this.transform.position.y + posOffset, this.transform.position.z);
                        Quaternion spawnRot = new Quaternion(this.transform.rotation.x + rotOffset1, this.transform.rotation.y + rotOffset2, this.transform.rotation.z + rotOffset1, this.transform.rotation.w + rotOffset2);
                        Instantiate(GunDrop, spawnPos, spawnRot);

                    }
                    else if (randomNum == 6)
                    {
                        int posOffset = Random.Range(1, 3);
                        int rotOffset1 = Random.Range(1, 180);
                        int rotOffset2 = Random.Range(1, 180);
                        Vector3 spawnPos = new Vector3(this.transform.position.x, this.transform.position.y + posOffset, this.transform.position.z);
                        Quaternion spawnRot = new Quaternion(this.transform.rotation.x + rotOffset1, this.transform.rotation.y + rotOffset2, this.transform.rotation.z + rotOffset1, this.transform.rotation.w + rotOffset2);
                        Instantiate(Gear, spawnPos, spawnRot);

                    }
                }
            }



            //remove your player bullet as well - unless player is currently supercharged (>120% power)
            if (HeroControllerSP.isSuperCharged == false)
            {
                Destroy(other.gameObject);
            }

        }
        else if (other.gameObject.name.Contains("Iceberg"))            
        {
            Debug.Log("IT WORKS ");
        }
    }

    void Update()
    {
        distanceX = this.transform.position.x - target.transform.position.x;
        distanceZ = this.transform.position.z - target.transform.position.z;
        distanceY = this.transform.position.y - target.transform.position.y;

        if (!isSummoned)
        {
            checkAggro();
        }
        

        if (isAggroed)
        {
            moveToPlayer();
        }

        
    }

    private void checkAggro()
    {
        if ((distanceX > -10 && distanceX < 10) && (distanceZ > -10 && distanceZ < 10) && (distanceY > -3 && distanceY < 3) )
        {
            isAggroed = true;
            anim.SetBool("IsAggroed", true);
            agent.Resume();
            if (shouldPlayAggroEffect)
            {
                Instantiate(AggroSpecEffect, this.transform.position, aggroRot);
                shouldPlayAggroEffect = false;
            }
        }
        //if you have aggroed, then ran away, and you're too far he gives up
        else if ((distanceX < -45 || distanceX > 45) || (distanceZ < -45 || distanceZ > 45) || (distanceY < -10 || distanceY > 10) && !isSummoned)
        {
            isAggroed = false;
            anim.SetBool("IsAggroed", false);
            if (agent.isActiveAndEnabled)
            {
                shouldPlayAggroEffect = true;
                agent.Stop();
            }
        }
    }

    void moveToPlayer()
    {
        cooldownTimer -= 0.03f;

       
        //Debug.Log (distance);


        if ((distanceX > -1.2 && distanceX < 1.2) && (distanceZ > -1.2 && distanceZ < 1.2) && (distanceY > -1.2 && distanceY < 1.2) && cooldownTimer < 0.01f)
        {
            anim.SetTrigger("isAttacking");
            //we are in range. Start draining battery
            Debug.Log("Energy vampire is draining your battery!!!");
            if (HeroControllerSP.hasShield && HeroControllerSP.isSlot4)
            {
                HeroControllerSP.battery -= (vampireDamage - 5);
            }
            else
            {
                HeroControllerSP.battery -= vampireDamage;
            }
            
            cooldownTimer = cooldown;

            int randomNum = Random.Range(1, 3);
            switch (randomNum)
            {
                case 1:
                    Instantiate(BiteSpecEffect1, this.transform.position, this.transform.rotation);
                    break;
                case 2:
                    Instantiate(BiteSpecEffect2, this.transform.position, this.transform.rotation);
                    break;
            }

        }
        else
        {
            if (agent.isActiveAndEnabled)
            {
                agent.SetDestination(target.transform.position);
            }
            
        }

    }

}