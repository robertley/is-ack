using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{

    public GameObject dummyTextObject;
    public GameObject dummyTextDPSObject;

    private TextMesh textMesh;
    private TextMesh dpsTextMesh;

    private float damage = 0;
    private float dpsDamage = 0;

    private float lastCheckTime = 0;
    private float lastCheckDamage = 0;

    // Start is called before the first frame update
    void Start() {
        textMesh = dummyTextObject.GetComponent<TextMesh>();
        dpsTextMesh = dummyTextDPSObject.GetComponent<TextMesh>();

        InvokeRepeating("resetDpsDamage", 1f, 1f);  //1s delay, repeat every 1s

    }

    // Update is called once per frame
    void Update() {
        
        if ((Time.time - lastCheckTime) > 2f) {
            if (lastCheckDamage == damage) {
                resetDamage();
            }
            lastCheckDamage = damage;
            lastCheckTime = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        string tag = collision.gameObject.tag;

        if (tag == "Projectile") {
            float projectileDamage = collision.gameObject.GetComponent<Bullet>().damage;
            damage += projectileDamage;
            textMesh.text = damage.ToString();

            dpsDamage += projectileDamage;
        }
    }

   public void doPiercingDamage(float damage) {
        this.damage += damage;
        textMesh.text = this.damage.ToString();

        dpsDamage += damage;
    }

    private void resetDamage() {
        damage = 0;
        textMesh.text = "";
    }

    private void resetDpsDamage() {
        dpsTextMesh.text = "DPS: " + (dpsDamage * 1);
        dpsDamage = 0;
    }
}
