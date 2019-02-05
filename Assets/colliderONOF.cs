using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderONOF : MonoBehaviour {
    public Collider2D dissablecollider2d;
    public Rigidbody2D rbody2;
    public bool move;

    void Start()
    {
        dissablecollider2d.enabled = (false);
    }
    void Update () {

		if(move == true)
        {
            dissablecollider2d.enabled = (true);
            rbody2.bodyType = RigidbodyType2D.Dynamic;
        }
	}
}
