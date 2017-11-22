using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Rigidbody2D body;
	public float MovSpeed;
	public PlayerAttack Swing;
	public PlayerAttack Thrust;
    public AudioClip SwingSound;
    public AudioClip ThrustSound;
    private AudioSource Source;


	// Use this for initialization
	void Start () 
	{
		body = GetComponent<Rigidbody2D> ();
        Source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");
		body.velocity = new Vector2 (horizontal * MovSpeed, vertical * MovSpeed);
		Attack ();
	}
	void Attack()
	{
		if (Input.GetMouseButton (0) == true) 
		{
            Source.clip = SwingSound;
            Source.Play();
			Swing.gameObject.SetActive (true);
			StartCoroutine (DisableObject (Swing.gameObject,.1f));
			return;
		}
		if (Input.GetMouseButton (1) == true)
		{
            Source.clip = ThrustSound;
            Source.Play();
			Thrust.gameObject.SetActive (true);
			StartCoroutine (DisableObject (Thrust.gameObject,.1f));
			return;
		} 
	}

	IEnumerator DisableObject(GameObject obj, float time)
	{
		yield return new WaitForSeconds(time);
		obj.SetActive (false);
	}
}
