using UnityEngine;
using System.Collections;
using Leap;

public class wavehello : MonoBehaviour {

	public HandController wavinghands;
	public GameObject statuscube;
	Animator anim;
	GameObject userEyes;
	GameObject speechbubble;


	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator>();
		userEyes = GameObject.Find("CenterEyeAnchor");
		speechbubble = GameObject.Find("speechbubble");

	}
	
	// Update is called once per frame
	void Update () {

		HandModel[] userHands = wavinghands.GetAllPhysicsHands();

		if (userHands.Length > 0){

			foreach (HandModel models in userHands){

				if (models.GetPalmPosition().y >= userEyes.transform.position.y - 0.03f){
						anim.SetTrigger("Wave");
						statuscube.GetComponent<Renderer>().material.color = Color.blue;
					} else {
						statuscube.GetComponent<Renderer>().material.color = Color.red;
						anim.SetTrigger("Idle");
				}
			}
		} else {
			statuscube.GetComponent<Renderer>().material.color = Color.red;
			anim.SetTrigger("Idle");
		}


		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")){

			changeMenuDisplay(speechbubble, 0);
		}else {

			changeMenuDisplay(speechbubble, 1);
		}
	
	}


	void changeMenuDisplay(GameObject menu, float alphavalue){
		
		Transform tempcanvas = menu.transform;
		
		if (tempcanvas != null){
			CanvasGroup[] cg;
			cg = tempcanvas.gameObject.GetComponents<CanvasGroup>();
			if (cg != null){
				foreach (CanvasGroup cgs in cg) {
					cgs.alpha = alphavalue;
				}
			}
		}
	}

}
