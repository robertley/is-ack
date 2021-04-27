using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    GameObject player;

    public float damage;
    public bool piercing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Is not Trigger if not piercing
    private void OnCollisionEnter2D(Collision2D other) {
        if (!piercing) {
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
    }

    // Is Trigger if piercing
    private void OnTriggerEnter2D(Collider2D collision) {
        string tag = collision.gameObject.tag;

        Debug.Log(tag);

        if (tag == "Wall") {
            Destroy(gameObject);
            return;
        }

        if (tag == "Enemy") {
            collision.gameObject.GetComponent<EnemyController>().doPiercingDamage(damage);
        }

        if (tag == "Dummy") {
            collision.gameObject.GetComponent<DummyController>().doPiercingDamage(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPiercing() {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

}
