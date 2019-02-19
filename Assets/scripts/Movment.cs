using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movment : MonoBehaviour {

    public int Player;
    public bool Is_Shooting;
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
    public float distanceX = 1f;
    public Transform holdpoint;
    public float throwpower = 15f;
    public LayerMask notgrabbed;
    
    // Charcter Animator
    public Animator animator;

    // Keys to controllers
    public string jumpKey = "Jump";
    public string movekey = "MoveHorizontal";
    public string crouchkey = "Crouch";
    public string Grabbkey = "Grabbing";
    public string ShootingKey = "Fire";

    private Gun equipped;
   
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
        IsShooting();
    }

   
    void Movefunction()
    {
        rbody.velocity = new Vector2(Input.GetAxisRaw(movekey + Player) * movespeed, rbody.velocity.y);
        

    }
    void Jumpfunction()
    {
        if (Input.GetButtonDown(jumpKey + Player))
        {
            
            if (groundcheck.isgrounded > 0)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, Jumpspeed);

            }
        }
        
    }
    void Crouchfunction()
    {
        if (Input.GetButtonDown(crouchkey + Player))
        {
            Crouch = true;
           
        }
        if (Input.GetButtonUp(crouchkey + Player))
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
    
    }
    void CrouchGlidefunction()
    {
        if (Input.GetButtonDown(crouchkey + Player))
        {
            Crouch = true;
        }
        if (Input.GetButtonUp(crouchkey))
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
        if (Input.GetButtonDown(crouchkey))
        {
            if (gameObject.GetComponent<SpriteRenderer>().sprite == Deafult)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Crouchsprite;

            }

        }
        if (Input.GetButtonUp(crouchkey))
        {
            if (gameObject.GetComponent<SpriteRenderer>().sprite == Crouchsprite)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Deafult;
            }
        }
        if (Input.GetButtonDown(crouchkey))
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
        if (Input.GetButtonUp(crouchkey))
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
        // Raycast X
        if (Input.GetButtonDown(Grabbkey + Player))
        {
            Debug.Log(string.Format("clicking"));
            if (!grabbed)
            {
                Physics2D.queriesStartInColliders = false;

                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distanceX);

                if (hit.collider != null && hit.collider.tag == "grabbable")
                {
                    grabbed = true;
                    hit.collider.GetComponent<colliderONOF>().move = false;
                    equipped = hit.collider.GetComponent<Gun>();
                    equipped.Shooting = true;
                }

            }
            else if (!Physics2D.OverlapPoint(holdpoint.position, notgrabbed))
            {
                grabbed = false;
                hit.collider.GetComponent<colliderONOF>().move = true;
                equipped = null;

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

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distanceX);
    }
   void IsShooting()
   {
        if(Input.GetButtonDown(ShootingKey + Player))
        {
            equipped.Shoot();
        }
       
   }
}

