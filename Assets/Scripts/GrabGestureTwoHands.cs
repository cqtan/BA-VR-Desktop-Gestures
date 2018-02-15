using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsMouseApi;
using Leap;

/*
 * Checks if both hands are performing the grab gesture and calculate
 * the difference of the distance of both hands.
 */
public class GrabGestureTwoHands : MonoBehaviour
{
  [SerializeField] private GameObject GestureManager;
  [SerializeField] private float maxDistanceModifier; // 2f

  public float DistanceDifference { get; private set; }
  public bool BothGrabsDetected { get; private set; }

  private GestureManager gesture;
  private float maxDistance = 1.0f;
  private float distanceRange;
  private float distanceScaled;
  private float lastDistance;
  private float currentDistance;
  private Vector trackedHandLeft;
  private Vector trackedHandRight;

  void Start()
  {
    gesture = GestureManager.GetComponent<GestureManager>();
    distanceRange = maxDistance * maxDistanceModifier;
    lastDistance = 0.0f;
  }
  void Update()
  {
    // Calculate difference of distance of both hands
    if (gesture.NumberOfHandsVisible == 2 &&
      gesture.LeftGrab == true &&
      gesture.RightGrab == true)
    {
      BothGrabsDetected = true;

      trackedHandLeft = gesture.LeftHand.PalmPosition;
      trackedHandRight = gesture.RightHand.PalmPosition;

      //Debug.Log("Both Grabs Detected!");
      var distance = trackedHandLeft.DistanceTo(trackedHandRight);
      distanceScaled = (distance / distanceRange);
      currentDistance = distanceScaled;

      // prevent value jumps
      if (lastDistance == 0.0f)
        lastDistance = currentDistance;

      DistanceDifference = (currentDistance - lastDistance);
      //Debug.Log("Manager/ Distance Difference: " +  DistanceDifference);
      lastDistance = currentDistance;
    }
    else
    {
      BothGrabsDetected = false;
      lastDistance = 0.0f;
    }

  }
}


