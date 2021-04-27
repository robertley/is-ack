using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUiScript : MonoBehaviour
{

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();

        updateHealthBar(6, 6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealthBar(int health, int maxHealth) {
        string healthString = health > 0 ? "<color=red>" : "";
        bool doneRedCheck = false;
        for (int i = 0; i < maxHealth; i++) {
            if (i % 2 == 0) {
                healthString += " <";
            } else {
                healthString += "3";
            }
            if (!doneRedCheck && health > 0 && (i + 2 > health || i + 1 == maxHealth)) {
                healthString += "</color>";
                doneRedCheck = true;
            }
        }

        text.text = healthString;
    }
}
