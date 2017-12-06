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
	private float dashTimer = 2f;
	private Animator anim;
	private SpriteRenderer renderer;


	// Use this for initialization
	void Start () 
	{
		body = GetComponent<Rigidbody2D> ();
        audioSource = GetComponents<AudioSource> ();
		anim = GetComponent<Animator> ();
		renderer = GetComponent <SpriteRenderer> ();
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
		dashTimer += Time.deltaTime;
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
					source.clip = HalfCircleAttackSound;
					source.Play ();
					anim.SetTrigger("soundPan");
				}
			}
			Swing.gameObject.SetActive(true);
			StartCoroutine(DisableObject(Swing.gameObject, .1f));
			return;
		}
		if (Input.GetButtonUp ("Fire2") == true)
		{
			foreach (AudioSource source in audioSource)
			{
				if (source.priority == 129)
				{
					source.panStereo = 0.0f;
				}
			}
		}

		if (Input.GetButtonDown ("Fire3") == true && dashTimer >= 2f)
        {
            foreach (AudioSource source in audioSource)
            {
                if (source.priority == 129)
                {
                    source.clip = DashAttackSound;
                    source.Play();
                }
            }
			renderer.color = Color.yellow;
			transform.Translate(Vector3.up * Time.deltaTime * DashSpeed);
            isDashing = true;
			dashTimer = 0f;
        }
		if (Input.GetButtonUp ("Fire3") == true) {isDashing = false;}
		if (dashTimer >= 2f) { renderer.color = Color.white; }
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
