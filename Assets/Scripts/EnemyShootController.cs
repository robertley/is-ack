using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootController : MonoBehaviour {
    public GameObject bullet;
    private GameObject player;

    public Transform enemyTip;
    public Transform projectileParent;

    private PlayerController playerController;
    private float fireRate;
    private float projectileSpeed;
    private int shootCounter = 0;
    private bool shooting = true;
    private EnemyController enemyController;


    private void Start() {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        enemyController = gameObject.GetComponent<EnemyController>();
        fireRate = enemyController.fireRate;
        projectileSpeed = enemyController.projectileSpeed;
    }

    void Update() {

        Vector2 shootDirection = player.GetComponent<Transform>().position - transform.position;
        float shootAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        enemyTip.rotation = Quaternion.Euler(0f, 0f, shootAngle - 90f);

    }
    private void FixedUpdate() {

        if (enemyController.getAlive()) {
            float fireRateShootVal = 60 / fireRate;

            if (shootCounter < fireRateShootVal) {
                shootCounter++;
            }

            if ((shootCounter >= fireRateShootVal) && shooting) {
                createProjectile();
                shootCounter = 0;
            }
        }

    }

    private void createProjectile() {

        GameObject newBullet = Instantiate(bullet, projectileParent);
        newBullet.GetComponent<Bullet>().damage = enemyController.damage;

        Rigidbody2D rb2d = newBullet.GetComponent<Rigidbody2D>();
        rb2d.velocity = createProjectileVelocity();

        player = GameObject.Find("Player");
        Physics2D.IgnoreCollision(newBullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
    }


    private Vector2 createProjectileVelocity() {
        return enemyTip.up * projectileSpeed;
    }
}
