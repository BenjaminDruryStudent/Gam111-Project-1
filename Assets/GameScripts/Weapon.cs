using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public float weaponDamage = 80;
    Rigidbody Rb;
    public GameObject Owner;

    void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other)
    {
        BodyPartCON hitPart = other.gameObject.GetComponent<BodyPartCON>();
        if (other != null)
        {
            if (hitPart.owner != Owner)
            {
                hitPart.bodyPartHealth -= HitDamage();
                print(HitDamage());
                if (hitPart.bodyPartHealth <= 0)
                {
                    hitPart.BreakJoint(Rb.velocity);
                }
            }
        }
    }

    public float HitDamage()
    {        
        return weaponDamage * Rb.velocity.magnitude;
    }   	
}
