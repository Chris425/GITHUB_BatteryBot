using UnityEngine;
using System.Collections;

//to be placed on all ice blocks that you wish to be destroyable

public class IceBlockDestructionSP : MonoBehaviour {

    public GameObject SE_IceBreak;
    public int numHitsToBreak = 1;
    private int counter = 0;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnCollisionEnter(Collision other)
    {
        //so enemy projectiles could break it too... that is okay
        if (other.gameObject.name.Contains("Shot"))
        {
            counter += 1;
            if (counter ==numHitsToBreak)
            {
                //play an effect
                Instantiate(SE_IceBreak, this.transform.position, this.transform.rotation);

                //consume the bullet
                Destroy(other.gameObject);
                //destroy this piece of ice
                Destroy(this.gameObject);
            }
            
            
        }
    }
    
}
