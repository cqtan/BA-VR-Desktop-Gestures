using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;

/*
 * Changes (v0.14.3)
 * - Show menu of last nested level
 * - Remove separation of 2D and 3D mode
 * - 
 */
public class ShowMenu : MonoBehaviour
{
  [SerializeField] private GameObject menu;
  [SerializeField] private HandRotation handModels;
  [SerializeField] private GameObject raycasterHead;

  private HandRotation handRotation;
  private Controller controller;
  private LeapServiceProvider provider;
  private int extendedFingers;
  private int maxFingers = 5;

  void Start()
  {
    extendedFingers = 0;
    provider = FindObjectOfType<LeapServiceProvider>();
    handRotation = handModels.GetComponent<HandRotation>();
  }

  void Update()
  {
    // show menu and turn off mouse-head control
    if (handRotation.LeftPalmUp)
    {
      CountExtendedFingers();
      if (extendedFingers == maxFingers)
      {
        menu.SetActive(true);
        raycasterHead.SetActive(false);
      }
      else
      {
        menu.SetActive(false);
        raycasterHead.SetActive(true);
      }
    }
    else
    {
      extendedFingers = 0;
      menu.SetActive(false);
      raycasterHead.SetActive(true);
    }
  }

  /*
   * Counts the number of extended fingers of the left hand.
   * If atleast one finger is not extended, then dont do anything
   */
  void CountExtendedFingers() {
    Frame currentFrame = provider.CurrentFrame;
    if (currentFrame.Hands.Count > 0)
    {
      List<Hand> hands = currentFrame.Hands;
      foreach (var hand in hands)
      {
        if (hand.IsLeft)
        {
          for (int f = 0; f < hand.Fingers.Count; f++)
          {
            Finger digit = hand.Fingers[f];
            if (digit.IsExtended)
            {
              extendedFingers++;
              if (extendedFingers >= maxFingers)
                extendedFingers = maxFingers;
            }
            else
              extendedFingers = 0;
          }
        }
      }
    }
  }

}