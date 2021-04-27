using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100;
    public int damage = 1;
    public float moveSpeed = 1f;
    public float projectileSpeed = 10f;
    public float fireRate = 2f;

    public Sprite deathSprite;

    private Rigidbody2D rb;
    private Vector2 movement;
    private PlayerController playerController;
    private GameObject player;
    private bool alive = true;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update() {
        Vector3 direction = player.GetComponent<Transform>().position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate() {
        if (alive) {
            calculateMovement(movement);
        }
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Projectile") {
            health -= playerController.damage;
            if (health < 0) {
                death();
            }
        }
    }

    public void doPiercingDamage(float damage) {
        health -= damage;
        if (health < 0) {
            death();
        }
    }

    private void death() {
        alive = false;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = deathSprite;
        spriteRenderer.sortingLayerName = "OnFloor";
        Destroy(gameObject.GetComponent<BoxCollider2D>());
    }

    private void calculateMovement(Vector2 direction) {
        rb.MovePosition((Vector2) transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    public bool getAlive() {
        return alive;
    }
}
