using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : Item
{
    // Start is called before the first frame update
    void Start()
    {
        this.maxHealth = 2;
        this.health = 2;
        this.damage = 0; //2;
        this.movementSpeed = 5;
        this.projectileSize = 0; //.5f;
        this.rateOfFire = .5f;
        this.text = "Bowling Ball";
        this.subText = "+ Max Health" + "\n" + "+ Speed" + "\n" + "+ ROF";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
