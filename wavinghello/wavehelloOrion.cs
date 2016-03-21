using UnityEngine; 
using System.Collections; 
using Leap; 

 
// This script is for the waving hello project using Orion.  
// http://rifty-business.blogspot.com/
// Copyright: Karen Bryla (karen@karenbryla.com) 
 
 
public class wavehelloOrion : MonoBehaviour
{


    LeapProvider provider;
 	Animator anim; 
 	GameObject speechbubble; 
 	GameObject centerEyeAnchor; 
 
 
 
 
 	// Use this for initialization 
 	void Start()
    {

              provider = FindObjectOfType<LeapProvider>() as LeapProvider;
              anim = GetComponent<Animator>();
    
              speechbubble = GameObject.Find("speechbubble");
              centerEyeAnchor = GameObject.Find("CenterEyeAnchor");
          }

    // Update is called once per frame 
    void Update()
    {

        // Get the current frame. 
        Frame frame = provider.CurrentFrame;


        // The frame conatins all the hands found. To simplify things a bit, the script only looks at the
        //  right hand to see if it is waving. (That way, the script does not need to account for one hand up 
        // and one hand down). The hands Y value is then compared to the Y value of the CenterEyeAncho.
        // The CenterEyeAnchor object is located at the center  of the user's eyes. If the Y value of 
        // the hand is greater than y value of the centerEyeAnchor, the hand is raised 
        // above the eyes. If the hand is above the eyes, set the trigger to wave and display the speech bubble.  


        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsRight)
            {

                if (hand.PalmPosition.y >= centerEyeAnchor.transform.position.y - 0.03f)
                {
                    anim.SetTrigger("Wave");
                    changeMenuDisplay(speechbubble, 1);
                    Debug.Log("Hand above");

                }
                else
                {
                    anim.SetTrigger("Idle");
                    changeMenuDisplay(speechbubble, 0);
                    Debug.Log("Hand below");
                }
            }
        }
    }
            
    
 
 
 
 
 	void changeMenuDisplay(GameObject menu, float alphavalue)
    {
        
      Transform tempcanvas = menu.transform;
        
      if (tempcanvas != null)
        {
                      CanvasGroup[] cg;
                      cg = tempcanvas.gameObject.GetComponents<CanvasGroup>();
                      if (cg != null)
            {
                              foreach (CanvasGroup cgs in cg)
                {
                                      cgs.alpha = alphavalue;
                                  }
                          }
                  }
          } 
 
 
 }

