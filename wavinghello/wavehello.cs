using UnityEngine;
using System.Collections;
using Leap;

public class wavehello : MonoBehaviour {

	public HandController wavinghands;
	Animator anim;
	GameObject centerEyeAnchor;
	GameObject speechbubble;


	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator>();
		centerEyeAnchor = GameObject.Find("CenterEyeAnchor");
		speechbubble = GameObject.Find("speechbubble");

	}
	
	// Update is called once per frame
	void Update () {

		// Find all the physic handsmodels in the scene.
		HandModel[] userHands = wavinghands.GetAllPhysicsHands();

		// For all hands found, compare the y position of the hand with the y position of the 
		// CenterEyeAnchor(userEyes). If above, set the trigger to wave and display the speech bubble. 
		if (userHands.Length > 0){

			foreach (HandModel models in userHands){

				if (models.GetPalmPosition().y >= centerEyeAnchor.transform.position.y - 0.03f){
						anim.SetTrigger("Wave");
						changeMenuDisplay(speechbubble, 1);
					} else {
						anim.SetTrigger("Idle");
						changeMenuDisplay(speechbubble, 0);
				}
			}

		// If no hands found, set trigger to Idle and hide the speech bubble.
		} else {
			anim.SetTrigger("Idle");
			changeMenuDisplay(speechbubble, 0);
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
