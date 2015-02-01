using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class conversation : MonoBehaviour {

	public Transform questionholder;
	Text question;
	CanvasGroup cg;
	private  float startTime;
	private float currentTime;
	private float waitTime;
	bool isVisible;


	// Use this for initialization
	void Start () {
		cg = GetComponent<CanvasGroup>();
		question = questionholder.GetComponent<Text>();
		question.text = "Nod if you can read this.";
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		// A line of diaog should be visible for a few seconds before an answer is accepted. 
		// Calculate the time the dialog has been visible.
		currentTime = Time.time;
		waitTime = currentTime - startTime;

		// Check to see if the dialog is visible by checking the Alpha property of the canvas group.
		if (cg.alpha == 0){
			isVisible = false;
			startTime = currentTime;
		}else {
			isVisible = true;
		}

		// Allow the dialog to start over by pressing the letter C.
		bool resetKey = Input.GetKey(KeyCode.C);
		if (resetKey){
			question.text = "Nod if you can read this.";
		}
	}


	public void TriggerYes(){

		// If the TriggerYes message has been sent from RiftGesture, check to see if the dialog is visible
		// and been visible for more than 2 seconds. Then update dialog.
		if (isVisible && (waitTime > 2)){
			if (question.text == "Nod if you can read this."){
				question.text = "Are you normally telepathic?";
			}else if (question.text == "Are you normally telepathic?"){
				question.text = "Great! Join my team?";
			}else if (question.text == "Great! Join my team?"){
				question.text = "Welcome aboard.";
			}else if (question.text == "Still learning then?"){
				question.text = "Great! Join my team?";
			}
			// restart the dialog clock
			startTime = currentTime;

		}

	}

	public void TriggerNo(){

		// If the TriggerNo message has been sent from RiftGesture, check to see if the dialog is visible
		// and has been visible for more than 2 seconds. Then update dialog.

		if (isVisible && (waitTime > 1)){
			if (question.text == "Are you normally telepathic?"){
				question.text = "Still learning then?";
			}else if (question.text == "Still learning then?"){
				question.text = "I'm keeping my eye on you.";
			}else if (question.text == "Great! Join my team?"){
				question.text = "We are enemies now.";
			}
			// restart the dialog clock
			startTime = currentTime;
		}

	}
}



