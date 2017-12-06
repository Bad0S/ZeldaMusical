using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour {
	public GameObject healItem;
	public int counterHeal;// compte les combos ici
	public static bool counterReset;// remet le compteur de combos à 0
	private float life = 1f;


	// Use this for initialization
	void Start () {
		
	}

	public void Heal( float lifeToGain)// la fonction pour soigner
	{
		life -= lifeToGain;
		print (life); 
	}
		
	// Update is called once per frame
	void Update () {
		counterHeal = combo.counter;//appelle dans combo
		if (counterHeal >= 5){
			GameObject clone = (GameObject)Instantiate (healItem, transform.position, transform.rotation);// créé les objets de soin
			counterReset = true;
		}
		if (counterHeal == 0){
			counterReset = false;
		}
	}
}
