using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 3;
    public float damage = 1;
    public float projectileSpeed = 10;
    public float rateOfFire = 10;
    public float projectileSize = 1;
    public int maxHealth = 6;

    public int health = 0;
    Rigidbody2D rb2d;

    public GameObject lifeUI;
    public GameObject itemTextUI;
    public GameObject itemSubTextUI;
    private LifeUiScript lifeUiScript;
    private PlayerAnimator playerAnimator;
//    private bool showingItemText = false;
    private bool invinsible = false;
    private bool isBouncing = false;

    public bool piercingProjectiles = false;

    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;

        lifeUiScript = lifeUI.GetComponent<LifeUiScript>();
        playerAnimator = GetComponent<PlayerAnimator>();

        health = maxHealth;
    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate() {
        calculateMovement();
    }

    private void calculateMovement() {
        float xAxis = 0;
        float yAxis = 0;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            xAxis += movementSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            xAxis -= movementSpeed;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            yAxis += movementSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            yAxis -= movementSpeed;
        }
        if (xAxis != 0f && yAxis != 0f) {
            xAxis /= 1.5f;
            yAxis /= 1.5f;
        }
        if (!isBouncing) rb2d.velocity = new Vector2(xAxis, yAxis);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        string tag = other.gameObject.tag;

        if (tag == "Item") {
            consumeItem(other.gameObject);
            return;
        }

        if (!invinsible) {
            doDamageCollision(other);
        }

    }

    private void doDamageCollision(Collision2D other) {
        string tag = other.gameObject.tag;
        if (tag == "EnemyProjectile" || tag == "Enemy") {
            int damageAmt;
            if (tag == "Enemy") {
                damageAmt = other.gameObject.GetComponent<EnemyController>().damage;
                bouncePlayerBack(other.gameObject.GetComponent<Transform>());
            }
            else {
                damageAmt = (int) other.gameObject.GetComponent<Bullet>().damage;
            }

            health -= damageAmt;
            lifeUiScript.updateHealthBar(health, maxHealth);
            if (health == 0) {
                death();
                return;
            }

            StartCoroutine(invinsibleCoroutine());
        }
    }

    private void bouncePlayerBack(Transform enemyTransform) {
        isBouncing = true;
        Vector3 bounceDirection = enemyTransform.position - transform.position;

        rb2d.AddForce(bounceDirection * -200);
        Invoke("stopBounce", 0.3f);
    }

    private void death() {
        //TEMP
        // Destroy(gameObject);
        Debug.Log("You died");
    }

    private void stopBounce() {
        isBouncing = false;
    }

    private void consumeItem(GameObject itemGO) {
        Debug.Log("consume item");
        Item item = itemGO.GetComponent<Item>();

        movementSpeed += item.movementSpeed;
        health += item.health;
        maxHealth += item.maxHealth;
        rateOfFire += item.rateOfFire;
        rateOfFire = Mathf.Max(1, rateOfFire);
        projectileSpeed += item.projectileSpeed;
        projectileSize += item.projectileSize;
        damage += item.damage;
        piercingProjectiles = !piercingProjectiles ? item.makePiercing : true;

        updateControllers();

        if (item.text != null) {
            TextMeshProUGUI text = itemTextUI.GetComponent<TextMeshProUGUI>();
            text.text = item.text;
        }

        if (item.subText != null) {
            TextMeshProUGUI subText = itemSubTextUI.GetComponent<TextMeshProUGUI>();
            subText.text = item.subText;
        }
        // TODO text bug when collecting more than one item fast
        Invoke("clearItemText", 3f);
        Destroy(itemGO);
    }

    private void clearItemText() {
        TextMeshProUGUI text = itemTextUI.GetComponent<TextMeshProUGUI>();
        text.text = "";
        TextMeshProUGUI subText = itemSubTextUI.GetComponent<TextMeshProUGUI>();
        subText.text = "";
    }

    private void updateControllers() {
        lifeUiScript.updateHealthBar(health, maxHealth);
        gameObject.GetComponent<ShootController>().updateValueShootVariables();
    }

    IEnumerator invinsibleCoroutine() {
        invinsible = true;
        playerAnimator.toggleInvinsible(true);
        yield return new WaitForSeconds(2);
        invinsible = false;
        playerAnimator.toggleInvinsible(false);
    }
}
