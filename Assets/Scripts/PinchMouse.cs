using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsMouseApi;
using Leap.Unity;
using Leap;

public class PinchMouse : MonoBehaviour
{
  public enum MouseType
  {
    UsePinchDetector = 1,
    UseDetectTipDistance,
  }
  public MouseType _MouseType;

  [SerializeField] private PinchDetector[] pinchDetectors;
  [SerializeField] private GameObject handModels;
  [SerializeField] private GameObject rightPalmLabel;

  private LeapServiceProvider provider;
  private HandRotation handRotation;
  private MouseApi mouseControl;
  private Vector thumb;
  private Vector index;
  private Vector pinky;
  private bool isWaiting;

  void Awake()
  {
    if (pinchDetectors.Length == 0)
      Debug.LogWarning("No pinch detectors were specified!");
  }

  void Start()
  {
    provider = FindObjectOfType<LeapServiceProvider>();
    mouseControl = new MouseApi();
    handRotation = handModels.GetComponent<HandRotation>();
  }

  void Update()
  {
    if (isWaiting == false)
    {
      if (_MouseType == MouseType.UseDetectTipDistance)
      {
        DetectTipDistance();
      }
      else if (_MouseType == MouseType.UsePinchDetector)
      {
        PinchDetector();
      }
    }
  }

  private void DetectTipDistance()
  {
    Frame currentFrame = provider.CurrentFrame;
    foreach (Hand hand in currentFrame.Hands)
    {
      if (hand.IsRight)
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

        if (handRotation.RightPalmUp != true)
        {
          rightPalmLabel.SetActive(false);
          if (thumb.DistanceTo(index) <= 0.02f)
          {
            Debug.Log("LEFT-CLICK");
            mouseControl.LeftClick();
            StartCoroutine(DelayNextClick());
          }
        }
        else
        {
          rightPalmLabel.SetActive(true);
          if (thumb.DistanceTo(index) <= 0.02f)
          {
            Debug.Log("RIGHT-CLICK");
            mouseControl.RightClick();
            StartCoroutine(DelayNextClick());
          }
          else if (thumb.DistanceTo(pinky) <= 0.02f)
          {
            Debug.Log("MIDDLE-CLICK");
            mouseControl.MiddleClick();
            StartCoroutine(DelayNextClick());
          }
        }

        //GrabDragDrop(hand);
      }
    }
  }

  // Prevents continuous clicking
  IEnumerator DelayNextClick()
  {
    isWaiting = true;
    yield return new WaitForSeconds(1);
    isWaiting = false;
  }

  private void GrabDragDrop(Hand hand)
  {
    Debug.Log("Hand grab angle: " + hand.GrabAngle);
  }
 
  private void PinchDetector()
  {
    for (int i = 0; i < pinchDetectors.Length; i++)
    {
      var detector = pinchDetectors[i];
      if (detector.DidStartHold)
      {
        if (handRotation.RightPalmUp != true)
        {
          //mouseControl.LeftClick();
          Debug.Log("(1) LEFT-CLICK");
        }
        else
        {
          //mouseControl.RightClick();
          Debug.Log("(2) RIGHT-CLICK");
        }
      }

      if (detector.DidRelease)
      {
        //mouseControl.LeftUp();
        Debug.Log("(3) LEFT-CLICK-UP");
      }

      if (detector.IsHolding)
      {
        if (handRotation.RightPalmUp != true)
        {
          //mouseControl.LeftDown();
          Debug.Log("(4) LEFT-CLICK-DOWN");
        }
      }
    }
  }
}
