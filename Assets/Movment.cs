using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movment : MonoBehaviour {
    // Vareabel till Movefunction
    public static float movespeed = 6f;
    // vareabel till Jumpfunction
    public static float Jumpspeed = 15f;
    // Vareabler till crouch animation och crouch collider2D
    public Sprite Deafult;
    public Sprite Crouchsprite;
    public Collider2D disablecollider2D;
    // Vareabel till GRounchecker scriptet
    public Groundchecker groundcheck;
    // Hämtar Rigidbody
    private Rigidbody2D rbody;
    // Vareabel till Crouchfunction
    public bool Crouch;
    // Vareabel till crouchslidingfunction
    public  bool sliding = false;
    public float slidingtimer = 0f;
    public float maxslidetime = 0.5f;
    // Vareabeler till grabb scriptet
    public bool grabbed;
    RaycastHit2D hit;
    public float distance = 2f;
    public Transform holdpoint;
    public float throwpower = 15f;
    public LayerMask notgrabbed;

    void Start () {
       
        rbody = GetComponent<Rigidbody2D>(); 
        gameObject.GetComponent<SpriteRenderer>().sprite = Deafult;
        Crouch = false;
    }
    void Update()
    {
        Movefunction();
        Jumpfunction();
        Crouchfunction();
        CrouchGlidefunction();
        weaponpickup();
    }

   
    void Movefunction()
    {
        rbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movespeed,
            rbody.velocity.y);

    }
    void Jumpfunction()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (Groundchecker.isgrounded > 0)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, Jumpspeed);

            }
        }
    }
    void Crouchfunction()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Crouch = true;
            movespeed = 0f;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Crouch = false;
            movespeed = 6f;
        }

        if (Crouch == true)
        {

            disablecollider2D.enabled = false;


        }
        if (Crouch == false)
        {
           
            disablecollider2D.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (gameObject.GetComponent<SpriteRenderer>().sprite == Deafult)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Crouchsprite;

            }

        } 
        if (Input.GetKeyUp(KeyCode.S))      
        {
            if (gameObject.GetComponent<SpriteRenderer>().sprite == Crouchsprite)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Deafult;
            }
        }
    }
    void CrouchGlidefunction()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Crouch = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Crouch = false;
        }

        if (Crouch == true)
        {

            disablecollider2D.enabled = false;


        }
        if (Crouch == false)
        {

            disablecollider2D.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (gameObject.GetComponent<SpriteRenderer>().sprite == Deafult)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Crouchsprite;

            }

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (gameObject.GetComponent<SpriteRenderer>().sprite == Crouchsprite)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Deafult;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
       {
            slidingtimer = 0f;
            sliding = true;
            Movment.movespeed = 4.5f;
       }
       if(sliding == true)
       {
            slidingtimer += Time.deltaTime;

            if (slidingtimer > maxslidetime)
            {
                sliding = false;
                Movment.movespeed = 0f;
            }
       }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Movment.movespeed = 6f;
            sliding = false;

            if (sliding == false)
            {
                Movment.movespeed = 6f;
            }
        }
        
    }
    void weaponpickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (!grabbed)
            {
                Physics2D.queriesStartInColliders = false;

                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

                if (hit.collider != null && hit.collider.tag == "grabbable")
                {
                    grabbed = true;
                    hit.collider.GetComponent<colliderONOF>().move = false;
                }

            }
            else if (!Physics2D.OverlapPoint(holdpoint.position, notgrabbed))
            {
                grabbed = false;
                hit.collider.GetComponent<colliderONOF>().move = true;
                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {

                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0) * throwpower;
                }
            }
            
        }
        if (grabbed)
            hit.collider.gameObject.transform.position = holdpoint.position;
        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}

