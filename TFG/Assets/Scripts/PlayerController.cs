//
// Copyright 2019 JotaDev
// Author: Juan Camilo Mayor Taborda
// Created: 18/09/2019
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Parameters
    //  ----------------PRIVATE------------------

    private Animator my_anim;
    private Rigidbody my_rb;

    private float v;
    private float h;


    //  ----------------PUBLIC------------------

    public Transform my_tr;
    public float movement_speed;
    public float jump_speed;
    public float current_speed;
    public float rb_speed;
    public float direction_damp_time;
    public float medium_speed;
    public float max_speed;
    public float rotation_speed;
    public float max_tilt;
    public bool grounded;
    public Material mat;
    public bool standing;
    public float glow_speed;
    public float falling_speed;
    public Vector3 rb_velocity;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        my_anim = GetComponent<Animator>();
        my_rb = GetComponent<Rigidbody>();
        my_tr = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if(my_anim)
        {
            AnimatorControl();
        }



    }

    void FixedUpdate()
    {
        v = Input.GetAxis("Vertical");

        if(v == 0 && my_rb.velocity.z < medium_speed)
        {
            my_rb.velocity += (my_tr.forward * movement_speed) * Time.deltaTime;
        }
        //else
        //{
        //    my_rb.velocity = new Vector3(my_rb.velocity.x, my_rb.velocity.y, max_speed);
        //}
        if(v > 0 && my_rb.velocity.z < max_speed)
        {
            
            my_rb.velocity += (my_tr.forward * movement_speed) * Time.deltaTime;
        }

        my_tr.position += my_tr.right * h * rotation_speed * Time.deltaTime;


        my_anim.SetFloat("Speed", v);
        rb_speed = my_rb.velocity.z;

        if(Input.GetKey(KeyCode.S))
        {
            my_anim.SetTrigger("Slide");
        }

        if(Input.GetKey(KeyCode.Space) && grounded)
        {
            my_rb.AddForce(my_tr.up * jump_speed, ForceMode.Impulse);
        }

        if(my_rb.velocity.y <= 0 && !grounded)
        {
            my_rb.velocity -= my_tr.up * Time.deltaTime * falling_speed;
        }
        rb_velocity = my_rb.velocity;
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Glow")
        {
            Debug.LogError("Hey");
            mat = col.transform.GetComponent<Renderer>().material;
            StartCoroutine("Glow");

        }
        //if (col.transform.tag == "Glow")
        //{
        //    //Debug.LogError(collision.transform.GetComponent<MeshRenderer>().material);
        //    standing = false;
        //}
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag == "Glow")
    //    {
    //        Debug.LogError("Hey");
    //        mat = collision.transform.GetComponent<Renderer>().material;
    //        StartCoroutine("Glow");
    //    }
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.transform.tag == "Glow")
    //    {
    //        standing = false;
    //    }
    //}
    IEnumerator Glow()
    {
        standing = true;

        while (standing)
        {
            mat.SetFloat("Vector1_C5C79D8E", 2.5f /*mat.GetFloat("Vector1_C5C79D8E") + Time.deltaTime * glow_speed*/);
            yield return new WaitForEndOfFrame();
        }


        yield return null;
    }



    /// <summary>
    /// Controls the animator updating the parameters.
    /// </summary>
    public void AnimatorControl()
    {

        current_speed = new Vector2(h, v).sqrMagnitude;




    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Ground")
        {
            grounded = true;
            my_anim.SetBool("Grounded", grounded);

        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.transform.tag == "Ground")
        {
            grounded = false;

            my_anim.SetBool("Grounded", grounded);

        }
    }
}
