using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim : MonoBehaviour {
    public Animator anim;
    public bool onDamage;
    public bool onDie;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    void Update()
    {
        if (onDamage)
        {
            anim.SetTrigger("onDamage");
            onDamage = false;
        }
        if (onDie)
        {
            anim.SetTrigger("onDied");
            onDie = false;
        }
    }
	
	// Update is called once per frame
	void OnGUI () {
		if(GUI.Button(new Rect(20, 20, 120, 50), "Damage"))
        {
            onDamage = true;
        }
        if (GUI.Button(new Rect(20, 80, 120, 50), "Die"))
        {
            onDie = true;
        }
    }
}
