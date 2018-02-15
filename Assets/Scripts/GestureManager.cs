using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;

/*
 * Checks how many hands are visible and which gestures are performed 
 */
public class GestureManager : MonoBehaviour
{
  [SerializeField] private float pinchDistanceLeftClick;
  [SerializeField] private float pinchDistanceMiddleClick;

  public Hand LeftHand { get; private set; }
  public Hand RightHand { get; private set; }
  public bool LeftIsVisible{ get; private set; }
  public bool RightIsVisible { get; private set; }

  public int NumberOfHandsVisible { get; private set; }

  public bool LeftIsFacingAway { get; private set; }
  public bool RightIsFacingAway { get; private set; }

  public bool LeftIsNearPerpendicular { get; private set; }
  public bool RightIsNearPerpendicular { get; private set; }

  public bool LeftIndexPinch { get; private set; }
  public bool LeftPinkyPinch { get; private set; }
  public bool RightIndexPinch { get; private set; }
  public bool RightPinkyPinch { get; private set; }

  public bool LeftGrab { get; private set; }
  public bool RightGrab { get; private set; }

  public bool LeftFingersExtended { get; private set; }
  public bool RightFingersExtended { get; private set; }

  private LeapServiceProvider provider;
  private Vector thumb;
  private Vector index;
  private Vector pinky;

  void Start()
  {
    provider = FindObjectOfType<LeapServiceProvider>();
    NumberOfHandsVisible = 0;
    LeftIsFacingAway = false;
    RightIsFacingAway = false;
    LeftIndexPinch = false;
    LeftPinkyPinch = false;
    RightIndexPinch = false;
    RightPinkyPinch = false;
    LeftGrab = false;
    RightGrab = false;
    LeftFingersExtended = false;
    RightFingersExtended = false;
  }

  void Update()
  {
    Frame currentFrame = provider.CurrentFrame;
    CountVisibleHands(currentFrame);

    foreach (Hand hand in currentFrame.Hands)
    {
      setHands(hand);
      CheckPalmDirection(hand);
      CheckNearPerpendicular(hand);
      CheckPinchGesture(hand);
      CheckGrabGesture(hand);
      CheckFingersExtended(hand);
    }
  }

  // Handle for each hand and check visibility
  private void setHands(Hand hand)
  {
    if (hand.IsLeft)
    {
      LeftHand = hand;
      LeftIsVisible = true;
    }
    else if (hand.IsRight)
    {
      RightHand = hand;
      RightIsVisible = true;
    }
    else
    {
      LeftIsVisible = false;
      RightIsVisible = false;
    }
  }

  // Number of visible hands
  private void CountVisibleHands(Frame currentFrame)
  {
    if (currentFrame.Hands.Count == 1)
      NumberOfHandsVisible = 1;
    else if (currentFrame.Hands.Count == 2)
      NumberOfHandsVisible = 2;
    else
      NumberOfHandsVisible = 0;
  }

  // is facing away or towards user?
  private void CheckPalmDirection(Hand hand)
  {
    if (hand.IsLeft)
    {
      if (hand.PalmNormal.z < 0.2f)
        LeftIsFacingAway = false;
      else
        LeftIsFacingAway = true;
    }
    else if (hand.IsRight)
    {
      if (hand.PalmNormal.z < 0.2f)
        RightIsFacingAway = false;
      else
        RightIsFacingAway = true;
    }
  }

  // is almost perpendicular to floor
  private void CheckNearPerpendicular(Hand hand, float triggerRange = 0.15f)
  {
    if (hand.IsLeft)
    {
      if ( -triggerRange < hand.PalmNormal.y && hand.PalmNormal.y < triggerRange)
        LeftIsNearPerpendicular = true;
      else
        LeftIsNearPerpendicular = false;
    }
    else if (hand.IsRight)
    {
      if (-triggerRange < hand.PalmNormal.y && hand.PalmNormal.y < triggerRange)
        RightIsNearPerpendicular = true;
      else
        RightIsNearPerpendicular = false;
    }
  }

  // is pinching?
  private void CheckPinchGesture(Hand hand)
  {
    if (hand.IsLeft)
    {
      foreach (Finger finger in hand.Fingers)
      {
        if (finger.Type == Finger.FingerType.TYPE_THUMB)
          thumb = finger.TipPosition;
        else if (finger.Type == Finger.FingerType.TYPE_INDEX)
          index = finger.TipPosition;
        else if (finger.Type == Finger.FingerType.TYPE_PINKY)
          pinky = finger.TipPosition;
      }

      pinchDistanceLeftClick = thumb.DistanceTo(index);
      pinchDistanceMiddleClick = thumb.DistanceTo(pinky);
      if (thumb.DistanceTo(index) <= 0.03f)
        LeftIndexPinch = true;
      else
        LeftIndexPinch = false;

      if (thumb.DistanceTo(pinky) <= 0.02f)
        LeftPinkyPinch = true;
      else
        LeftPinkyPinch = false;
    }
    else if (hand.IsRight)
    {
      foreach (Finger finger in hand.Fingers)
      {
        if (finger.Type == Finger.FingerType.TYPE_THUMB)
          thumb = finger.TipPosition;
        else if (finger.Type == Finger.FingerType.TYPE_INDEX)
          index = finger.TipPosition;
        else if (finger.Type == Finger.FingerType.TYPE_PINKY)
          pinky = finger.TipPosition;
      }

      pinchDistanceLeftClick = thumb.DistanceTo(index);
      pinchDistanceMiddleClick = thumb.DistanceTo(pinky);

      if (thumb.DistanceTo(index) <= 0.03f)
        RightIndexPinch = true;
      else
        RightIndexPinch = false;

      if (thumb.DistanceTo(pinky) <= 0.02f)
        RightPinkyPinch = true;
      else
        RightPinkyPinch = false;
    }
  }

  // is grabbing?
  private void CheckGrabGesture(Hand hand)
  {
    if (hand.IsLeft && hand.GrabAngle > 2.8f)
    {
      LeftGrab = true;
    }
    else if (hand.IsRight && hand.GrabAngle > 2.8f)
    {
      RightGrab = true;
    }
    else
    {
      RightGrab = false;
      LeftGrab = false;
    }
  }

  // are fingers extended?
  private void CheckFingersExtended(Hand hand)
  {
    int extendedFingers = 0;

    if (hand.IsLeft)
    {
      for (int f = 0; f < hand.Fingers.Count; f++)
      {
        Finger digit = hand.Fingers[f];
        if (digit.IsExtended)
        {
          extendedFingers++;
          if (extendedFingers >= 5)
            LeftFingersExtended = true;
        }
        else
          LeftFingersExtended = false;
      }
    }
    else if (hand.IsRight)
    {
      for (int f = 0; f < hand.Fingers.Count; f++)
      {
        Finger digit = hand.Fingers[f];
        if (digit.IsExtended)
        {
          extendedFingers++;
          if (extendedFingers >= 5)
            RightFingersExtended = true;
        }
        else
          RightFingersExtended = false;
      }
    }
  }

}
