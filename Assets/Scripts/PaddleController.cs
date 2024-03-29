﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField]
    float speed = 0.2f;

    int direction = 0;
    float prevPosY;

    Vector3 minScreenBounds, maxScreenBounds;
    
    [SerializeField]
    KeyCode moveUp = KeyCode.Q;
    [SerializeField]
    KeyCode moveDown = KeyCode.A;
    [SerializeField]
    AudioClip hit;

    [SerializeField]
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        minScreenBounds.y = minScreenBounds.y + rend.bounds.size.y/2;
        maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        maxScreenBounds.y = maxScreenBounds.y - rend.bounds.size.y/2;

        prevPosY = transform.position.y;
    }

    void FixedUpdate()
    {
        // TODO: Collecting input should happen in Update. Only the action should happen here.

        if(Input.GetKey(moveUp)) MoveUp();
        else if (Input.GetKey(moveDown)) MoveDown();


        if (prevPosY > transform.position.y) direction = -1;
        else if (prevPosY < transform.position.y) direction = 1;
        else direction = 0;
    }

    void LateUpdate()
    {
        prevPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other){
        this.PlaySound(hit);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        float adjust = 5 * direction;
        other.rigidbody.velocity = new Vector2(other.rigidbody.velocity.x, other.rigidbody.velocity.y + adjust);
    }

    void MoveUp()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y + speed, minScreenBounds.y, maxScreenBounds.y));
    }

    void MoveDown()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y - speed, minScreenBounds.y, maxScreenBounds.y));
    }

    void PlaySound(AudioClip soundClip)
    {
        audioSource.clip = soundClip;
        audioSource.Play();
    }
}
