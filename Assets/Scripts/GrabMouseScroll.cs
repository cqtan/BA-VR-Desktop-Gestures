using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsMouseApi;

public class GrabMouseScroll : MonoBehaviour
{
  [SerializeField] private GameObject GestureManager;
  [SerializeField] private float ScrollSpeed;
  [SerializeField] private bool NaturalScroll;
  [SerializeField] private bool UseLeftHand;

  [SerializeField] private float currentPos;

  private GestureManager gesture;
  private MouseApi mouseControl;
  private float lastPos;
  private float difference;

  void Start()
  {
    gesture = GestureManager.GetComponent<GestureManager>();
    mouseControl = new MouseApi();
    if (NaturalScroll)
      ScrollSpeed *= -1;
  }

  void Update()
  {
    if (UseLeftHand == true && gesture.LeftIsVisible == true)
    {
      currentPos = gesture.LeftHand.PalmPosition.y;
      difference = currentPos - lastPos;
      if (gesture.LeftIsFacingAway == true &&
          gesture.LeftGrab == true)
        mouseControl.ScrollWheel(difference * ScrollSpeed);
      lastPos = currentPos;
    }
    else if (UseLeftHand == false)
    {
      currentPos = gesture.RightHand.PalmPosition.y;
      difference = currentPos - lastPos;
      if (gesture.RightIsFacingAway == true &&
          gesture.RightGrab == true)
        mouseControl.ScrollWheel(difference * ScrollSpeed);
      lastPos = currentPos;
    }
  }
}
