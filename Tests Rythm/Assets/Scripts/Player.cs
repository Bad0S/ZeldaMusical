﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Rigidbody2D body;
	public float MovSpeed;
	public PlayerAttack Swing;
	public PlayerAttack AttaqueBase;
    public AudioClip BaseAttackSound;
    public AudioClip HalfCircleAttackSound;
    public AudioClip DashAttackSound;
    private AudioSource Source;
    private float PlayerRot;
    public float DashSpeed;
    public bool isDashing;
    public AudioSource[] audioSource;


	// Use this for initialization
	void Start () 
	{
		body = GetComponent<Rigidbody2D> ();
        audioSource = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        body.transform.rotation =Quaternion.Euler (0,0,PlayerRot);
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");
		body.velocity = new Vector2 (horizontal * MovSpeed, vertical * MovSpeed);
        if (body.velocity.x > 0) { PlayerRot = 270; }
        if (body.velocity.x < 0) { PlayerRot = 90; }
        if (body.velocity.y > 0) { PlayerRot = 0; }
        if (body.velocity.y < 0) { PlayerRot = 180; }
        Attack ();
        if ((body.velocity.x != 0) || (body.velocity.y != 0))
        {
            foreach (AudioSource source in audioSource)
            {
                if (source.priority == 128)
                {
                    source.mute = false;
                }
            }
        }
        else
        {
            foreach (AudioSource source in audioSource)
            {
                if (source.priority == 128)
                {
                    source.mute = true;
                }
            }
        }
	}
	void Attack()
	{
		if (Input.GetButton ("Fire1") == true) 
		{
            foreach (AudioSource source in audioSource)
            {
                if (source.priority == 129)
                {
                    source.clip = BaseAttackSound;
                    source.Play();
                }
            }
            AttaqueBase.gameObject.SetActive(true);
            StartCoroutine(DisableObject(AttaqueBase.gameObject, .1f));
            return;
        }
        if (Input.GetButtonDown ("Fire2") == true)
        {
            foreach (AudioSource source in audioSource)
            {
                if (source.priority == 129)
                {
                    source.clip = DashAttackSound;
                    source.Play();
                }
            }
            transform.Translate(Vector3.up * Time.deltaTime * DashSpeed);
            isDashing = true;
        }
        if (Input.GetButtonUp ("Fire2") == true) { isDashing = false; }
		if (Input.GetButton ("Fire3") == true)
		{
            foreach (AudioSource source in audioSource)
            {
                if (source.priority == 129)
                {
                    source.clip = HalfCircleAttackSound;
                    source.Play();
                    for (float fl = -1.0f; fl < 1.0f;fl ++)
                    {
                        source.panStereo = fl;
                    }
                }
            }
            Swing.gameObject.SetActive(true);
            StartCoroutine(DisableObject(Swing.gameObject, .1f));
            return;
        }
        if (Input.GetButtonUp ("Fire3") == true)
        {
            foreach (AudioSource source in audioSource)
            {
                if (source.priority == 129)
                {
                    source.panStereo = 0.0f;
                }
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="Enemy" && isDashing == true)
        {
            Destroy(other.gameObject);
        }
    }
    IEnumerator DisableObject(GameObject obj, float time)
	{
		yield return new WaitForSeconds(time);
		obj.SetActive (false);
	}
}
