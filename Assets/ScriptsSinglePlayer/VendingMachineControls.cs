using UnityEngine;
using System.Collections;

public class VendingMachineControls : MonoBehaviour {
    public int whichControllerNumberAmI;

    public AudioClip switchHit;
    public AudioClip errorSound;
    private AudioSource source;
    public GameObject spawnLoc;
    public GameObject SE_Explosion;

    public GameObject slot1Item;
    public GameObject slot2Item;
    public GameObject slot3Item;
    public GameObject slot4Item;
    public GameObject slot5Item;


    void Awake()
    {
        source = GetComponent<AudioSource>();

    }

    // Use this for initialization
    void Start () {
	
	}

    public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.name.Contains("PlayerShot") || other.gameObject.name.Contains("InteractShot"))
        {

            if (whichControllerNumberAmI == 1)
            {
                if (HeroControllerSP.Gears >= 2)
                {
                    source.PlayOneShot(switchHit, 1.0f);
                    HeroControllerSP.Gears -= 2;
                    Instantiate(slot1Item, spawnLoc.transform.position, spawnLoc.transform.rotation);
                    Instantiate(SE_Explosion, spawnLoc.transform.position, spawnLoc.transform.rotation);
                }
                else
                {
                    source.PlayOneShot(errorSound, 1.0f);
                }
            }

            else if (whichControllerNumberAmI == 2)
            {
                
                if (HeroControllerSP.Gears >= 2)
                {
                    source.PlayOneShot(switchHit, 1.0f);
                    HeroControllerSP.Gears -= 2;
                    Instantiate(slot2Item, spawnLoc.transform.position, spawnLoc.transform.rotation);
                    Instantiate(SE_Explosion, spawnLoc.transform.position, spawnLoc.transform.rotation);
                }
                else
                {
                    source.PlayOneShot(errorSound, 1.0f);
                }
                
            }
            else if (whichControllerNumberAmI == 3)
            {
                
                if (HeroControllerSP.Gears >= 5)
                {
                    source.PlayOneShot(switchHit, 1.0f);
                    HeroControllerSP.Gears -= 5;
                    Instantiate(slot3Item, spawnLoc.transform.position, spawnLoc.transform.rotation);
                    Instantiate(SE_Explosion, spawnLoc.transform.position, spawnLoc.transform.rotation);
                }
                else
                {
                    source.PlayOneShot(errorSound, 1.0f);
                }
                
            }

            else if (whichControllerNumberAmI == 4)
            {                
                if (HeroControllerSP.Gears >= 3)
                {
                    source.PlayOneShot(switchHit, 1.0f);
                    HeroControllerSP.Gears -= 3;
                    Instantiate(slot4Item, spawnLoc.transform.position, spawnLoc.transform.rotation);
                    Instantiate(SE_Explosion, spawnLoc.transform.position, spawnLoc.transform.rotation);
                }
                else
                {
                    source.PlayOneShot(errorSound, 1.0f);
                }
                
            }
            else if (whichControllerNumberAmI == 5)
            {
                
                if (HeroControllerSP.Gears >= 1)
                {
                    source.PlayOneShot(switchHit, 1.0f);
                    HeroControllerSP.Gears -= 1;
                    Instantiate(slot5Item, spawnLoc.transform.position, spawnLoc.transform.rotation);
                    Instantiate(SE_Explosion, spawnLoc.transform.position, spawnLoc.transform.rotation);
                }
                else
                {
                    source.PlayOneShot(errorSound, 1.0f);
                }
                
            }

        }
    }

                // Update is called once per frame
                void Update ()
                {

                }
            }
