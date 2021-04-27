using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootController : MonoBehaviour
{
    public GameObject bullet;
    private GameObject player;

    public Transform playerTip;

    private PlayerController playerController;

    private float fireRate = 10;
    private int shootCounter = 0;

    public float projectileSpeed = 10;

    private bool shooting = false;

    private void Start() {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        updateValueShootVariables();
    }

    void Update()
    {
        
        Vector2 shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float shootAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        playerTip.rotation = Quaternion.Euler(0f, 0f, shootAngle - 90f);

        if (Input.GetMouseButtonDown(0)) {
            shooting = true;
        }
        if (Input.GetMouseButtonUp(0)) {
            shooting = false;
        }
    }
    private void FixedUpdate() {

        float fireRateShootVal = 60 / fireRate;

        if (shootCounter < fireRateShootVal) {
            shootCounter++;
        }

        if ((shootCounter >= fireRateShootVal) && shooting) {
            createProjectile();
            shootCounter = 0;
        }

    }

    private void createProjectile() {

        GameObject newBullet = Instantiate(bullet, transform);

        //float projectileSizeScaleDivisor = playerController.piercingProjectiles ? 50 : 10;
        //float projectileSizeScale = (1 + (playerController.damage / projectileSizeScaleDivisor)) / 2;
        //newBullet.transform.localScale = new Vector3(projectileSizeScale, projectileSizeScale, 0);

        newBullet.transform.localScale = new Vector3(playerController.projectileSize, playerController.projectileSize, 0);

        Bullet bulletController = newBullet.GetComponent<Bullet>();
        bulletController.damage = playerController.damage;
        if (playerController.piercingProjectiles) {
            bulletController.setPiercing();
        }

        Rigidbody2D rb2d = newBullet.GetComponent<Rigidbody2D>();
        rb2d.velocity = createProjectileVelocity();

        player = GameObject.Find("Player");
        Physics2D.IgnoreCollision(newBullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
    }

    private Vector2 createProjectileVelocity() {
        return playerTip.up * projectileSpeed;
    }

    private Vector2 createProjectileVelocityAlt() {
        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection-transform.position;

        return new Vector2(shootDirection.x * projectileSpeed, shootDirection.y * projectileSpeed);
    }

    public void updateValueShootVariables() {
        projectileSpeed = playerController.projectileSpeed;
        fireRate = playerController.rateOfFire;
        shootCounter = 0;
    }
}
