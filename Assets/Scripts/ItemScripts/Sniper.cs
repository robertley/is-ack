using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Item {
    // Start is called before the first frame update
    void Start() {
        this.maxHealth = 0;
        this.health = 0;
        this.projectileSpeed = 75;
        this.rateOfFire = -100;
        this.damage = 150;
        this.movementSpeed = 0;
        this.text = "Sniper";
        this.subText = "+++ Damage" + "\n" + "+++ Projectile Speed" + "\n" + "--- ROF";

        this.makePiercing = true;
    }

    // Update is called once per frame
    void Update() {

    }
}
