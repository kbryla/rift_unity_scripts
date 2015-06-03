using UnityEngine;
using System.Collections;
using Leap;

// This script is for the waving hello project in the blog post: 
// http://rifty-business.blogspot.com/2015/06/unity-leap-raising-your-hand-to-get.html
// Copyright: Karen Bryla (karen@karenbryla.com)

public class wavehello : MonoBehaviour {

	public HandController wavinghands;
	Animator anim;
	GameObject speechbubble;
	GameObject centerEyeAnchor;


	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator>();
		speechbubble = GameObject.Find("speechbubble");
		centerEyeAnchor = GameObject.Find("CenterEyeAnchor");
	}
	
	// Update is called once per frame
	void Update () {

		// Find all the physic handsmodels in the scene.
		HandModel[] userHands = wavinghands.GetAllPhysicsHands();

		// For all hands found, GetPalmPosition returns the postion of the hand relative to the
		// HandController. The HandController is located at 0,0,0 relative to it's parent
		// obect the CenterEyeAnchor. The CenterEyeAnchor object is located at the center 
		// of the user's eyes. If the Y value of the hand is greater than y value of the centerEyeAnchor, the hand is raised
		// above the eyes. If the hand is above the eyes, set the trigger to wave and display the speech bubble. 
		if (userHands.Length > 0){

			foreach (HandModel models in userHands){

				if (models.GetPalmPosition().y >=  centerEyeAnchor.transform.position.y -0.03f){
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
