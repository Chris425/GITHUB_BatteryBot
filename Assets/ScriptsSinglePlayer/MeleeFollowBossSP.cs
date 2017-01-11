﻿using UnityEngine;
using System.Collections;

public class MeleeFollowBossSP : MonoBehaviour
{
    private Animator anim;
    NavMeshAgent agent;
    //get location of character!
    public GameObject target;
    public float distanceX;
    public float distanceZ;
    public float distanceY;
    private float cooldown = 6.0f;
    private float cooldownTimer;
    private int bossHealth = 5;
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
    public GameObject SuperBattery;
    public GameObject Gear;
    public GameObject GoldenGear;

    public GameObject AxeDrop;
    public GameObject ShieldDrop;
    public GameObject GunDrop;
    public GameObject GreatswordDrop;

    public static bool isAggroed;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("BatteryBot");

        anim = this.GetComponentInChildren<Animator>();
        anim.applyRootMotion = false;
        anim.SetBool("IsAggroed", false);
        isAggroed = false;
        shouldPlayAggroEffect = true;
        Quaternion aggroRot = new Quaternion(0.0f, 180.0f, 180.0f, 0.0f);
    }

    public void OnCollisionEnter(Collision other)
    {
        //case when your player projectile hits the boss
        if (other.gameObject.name.Contains("Shot"))
        {
            isAggroed = true;
            if (other.gameObject.name.Contains("PlayerShot"))
            {
                //Note that multishot has the same damage - you just shoot a bunch at the same time
                if (HeroControllerSP.isSuperCharged == true)
                {
                    bossHealth -= 2;
                    Instantiate(DeathSpecEffect, other.transform.position, this.transform.rotation);
                }
                else
                {
                    bossHealth -= 1;
                    Instantiate(DeathSpecEffect, other.transform.position, this.transform.rotation);
                }
            }
            else if (other.gameObject.name.Contains("GS_Shot"))
            {
                if (other.gameObject.name.Contains("FIRE"))
                {
                    bossHealth -= 4;
                    Instantiate(BloodSpecEffect, other.transform.position, this.transform.rotation);
                }
                else
                {
                    bossHealth -= 2;
                    Instantiate(BloodSpecEffect, other.transform.position, this.transform.rotation);
                }

            }
            else if (other.gameObject.name.Contains("Axe_Shot"))
            {
                if (other.gameObject.name.Contains("LIGHTNING"))
                {
                    bossHealth -= 2;
                    Instantiate(BloodSpecEffect, other.transform.position, this.transform.rotation);
                }
                else
                {
                    bossHealth -= 1;
                    Instantiate(BloodSpecEffect, other.transform.position, this.transform.rotation);
                }

            }
            //note that shield shot IS the ice special... shield normally shoots an axe shot (because reasons)
            else if (other.gameObject.name.Contains("Shield_Shot"))
            {
                bossHealth -= 2;
                Instantiate(DeathSpecEffect, other.transform.position, this.transform.rotation);
            }


                //consume playershot
                Destroy(other.gameObject);
            if (bossHealth <= 0)
            {
                //kill the skeleton
                Destroy(this.gameObject);

                //possibly spawn some loot!
                //CDC add new special loot for boss monsters?


                int randomNum = Random.Range(1, 20);
                if (randomNum <= 3)
                {
                    int posOffset = Random.Range(1, 4);
                    int rotOffset1 = Random.Range(1, 180);
                    int rotOffset2 = Random.Range(1, 180);
                    Vector3 spawnPos = new Vector3(this.transform.position.x, this.transform.position.y + posOffset, this.transform.position.z);
                    Quaternion spawnRot = new Quaternion(this.transform.rotation.x + rotOffset1, this.transform.rotation.y + rotOffset2, this.transform.rotation.z + rotOffset1, this.transform.rotation.w + rotOffset2);
                    Instantiate(RedBattery, spawnPos, spawnRot);

                }
                else if (randomNum > 3 && randomNum <= 5)
                {
                    int posOffset = Random.Range(1, 4);
                    int rotOffset1 = Random.Range(1, 180);
                    int rotOffset2 = Random.Range(1, 180);
                    Vector3 spawnPos = new Vector3(this.transform.position.x, this.transform.position.y + posOffset, this.transform.position.z);
                    Quaternion spawnRot = new Quaternion(this.transform.rotation.x + rotOffset1, this.transform.rotation.y + rotOffset2, this.transform.rotation.z + rotOffset1, this.transform.rotation.w + rotOffset2);
                    Instantiate(GreenBattery, spawnPos, spawnRot);

                }
                else if (randomNum == 6)
                {
                    int posOffset = Random.Range(1, 4);
                    int rotOffset1 = Random.Range(1, 180);
                    int rotOffset2 = Random.Range(1, 180);
                    Vector3 spawnPos = new Vector3(this.transform.position.x, this.transform.position.y + posOffset, this.transform.position.z);
                    Quaternion spawnRot = new Quaternion(this.transform.rotation.x + rotOffset1, this.transform.rotation.y + rotOffset2, this.transform.rotation.z + rotOffset1, this.transform.rotation.w + rotOffset2);
                    Instantiate(SuperBattery, spawnPos, spawnRot);

                }
                else if (randomNum == 7)
                {
                    int posOffset = Random.Range(1, 3);
                    int rotOffset1 = Random.Range(1, 180);
                    int rotOffset2 = Random.Range(1, 180);
                    Vector3 spawnPos = new Vector3(this.transform.position.x, this.transform.position.y + posOffset, this.transform.position.z);
                    Quaternion spawnRot = new Quaternion(this.transform.rotation.x + rotOffset1, this.transform.rotation.y + rotOffset2, this.transform.rotation.z + rotOffset1, this.transform.rotation.w + rotOffset2);
                    Instantiate(GreatswordDrop, spawnPos, spawnRot);

                }
                else if (randomNum == 8)
                {
                    int posOffset = Random.Range(1, 3);
                    int rotOffset1 = Random.Range(1, 180);
                    int rotOffset2 = Random.Range(1, 180);
                    Vector3 spawnPos = new Vector3(this.transform.position.x, this.transform.position.y + posOffset, this.transform.position.z);
                    Quaternion spawnRot = new Quaternion(this.transform.rotation.x + rotOffset1, this.transform.rotation.y + rotOffset2, this.transform.rotation.z + rotOffset1, this.transform.rotation.w + rotOffset2);
                    Instantiate(AxeDrop, spawnPos, spawnRot);

                }
                else if (randomNum == 9)
                {
                    int posOffset = Random.Range(1, 3);
                    int rotOffset1 = Random.Range(1, 180);
                    int rotOffset2 = Random.Range(1, 180);
                    Vector3 spawnPos = new Vector3(this.transform.position.x, this.transform.position.y + posOffset, this.transform.position.z);
                    Quaternion spawnRot = new Quaternion(this.transform.rotation.x + rotOffset1, this.transform.rotation.y + rotOffset2, this.transform.rotation.z + rotOffset1, this.transform.rotation.w + rotOffset2);
                    Instantiate(ShieldDrop, spawnPos, spawnRot);

                }
                else if (randomNum == 10)
                {
                    int posOffset = Random.Range(1, 3);
                    int rotOffset1 = Random.Range(1, 180);
                    int rotOffset2 = Random.Range(1, 180);
                    Vector3 spawnPos = new Vector3(this.transform.position.x, this.transform.position.y + posOffset, this.transform.position.z);
                    Quaternion spawnRot = new Quaternion(this.transform.rotation.x + rotOffset1, this.transform.rotation.y + rotOffset2, this.transform.rotation.z + rotOffset1, this.transform.rotation.w + rotOffset2);
                    Instantiate(GunDrop, spawnPos, spawnRot);

                }
                else if (randomNum == 11)
                {
                    int posOffset = Random.Range(1, 3);
                    int rotOffset1 = Random.Range(1, 180);
                    int rotOffset2 = Random.Range(1, 180);
                    Vector3 spawnPos = new Vector3(this.transform.position.x, this.transform.position.y + posOffset, this.transform.position.z);
                    Quaternion spawnRot = new Quaternion(this.transform.rotation.x + rotOffset1, this.transform.rotation.y + rotOffset2, this.transform.rotation.z + rotOffset1, this.transform.rotation.w + rotOffset2);
                    Instantiate(GoldenGear, spawnPos, spawnRot);

                }

                else if (randomNum >= 12 && randomNum <= 15)
                {
                    int posOffset = Random.Range(1, 3);
                    int rotOffset1 = Random.Range(1, 180);
                    int rotOffset2 = Random.Range(1, 180);
                    Vector3 spawnPos = new Vector3(this.transform.position.x, this.transform.position.y + posOffset, this.transform.position.z);
                    Quaternion spawnRot = new Quaternion(this.transform.rotation.x + rotOffset1, this.transform.rotation.y + rotOffset2, this.transform.rotation.z + rotOffset1, this.transform.rotation.w + rotOffset2);
                    Instantiate(Gear, spawnPos, spawnRot);

                }

                Destroy(other.gameObject);
                
        }

        }
    }

    void Update()
    {
        distanceX = this.transform.position.x - target.transform.position.x;
        distanceZ = this.transform.position.z - target.transform.position.z;
        distanceY = this.transform.position.y - target.transform.position.y;

        
        checkAggro();

        if (isAggroed)
        {
            moveToPlayer();
        }

    }

    private void checkAggro()
    {
        if ((distanceX > -20 && distanceX < 20) && (distanceZ > -20 && distanceZ < 20) && (distanceY > -5 && distanceY < 5))
        {
            isAggroed = true;
            anim.SetBool("IsAggroed", true);
            if (shouldPlayAggroEffect)
            {
                Instantiate(AggroSpecEffect, this.transform.position, aggroRot);
                shouldPlayAggroEffect = false;
            }
           
        }
        //This monster doesn't give up - will chase until defeated
        
    }

    void moveToPlayer()
    {
        cooldownTimer -= 0.03f;

        //Debug.Log (distance);


        if ((distanceX > -2.7 && distanceX < 2.7) && (distanceZ > -2.7 && distanceZ < 2.7) && (distanceY > -3.7 && distanceY < 3.7) && cooldownTimer < 0.01f)
        {
            anim.SetTrigger("isAttacking");
            //we are in range. Start draining battery. Less if Player has shield!
            Debug.Log("Skeleton Attacks!");
            if (HeroControllerSP.hasShield && HeroControllerSP.isSlot4)
            {
                HeroControllerSP.battery -= 35;
            }
            else
            {
                HeroControllerSP.battery -= 50;
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