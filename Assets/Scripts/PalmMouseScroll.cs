using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsMouseApi;

public class PalmMouseScroll : MonoBehaviour
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
    if (gesture.NumberOfHandsVisible == 1)
    {
      if (UseLeftHand == true)
      {
        if (gesture.LeftIsVisible == true &&
            gesture.LeftIsFacingAway == true)
        {
          currentPos = gesture.LeftHand.PalmPosition.y;
          difference = currentPos - lastPos;
          if (gesture.LeftIsNearPerpendicular == true &&
              gesture.LeftFingersExtended == true)
            mouseControl.ScrollWheel(difference * ScrollSpeed);
          lastPos = currentPos;
        }
      }
      else if (UseLeftHand == false)
      {
        if (gesture.RightIsVisible == true &&
            gesture.RightIsFacingAway == true)
        {
          currentPos = gesture.RightHand.PalmPosition.y;
          difference = currentPos - lastPos;
          if (gesture.RightIsNearPerpendicular == true &&
              gesture.RightFingersExtended == true)
            mouseControl.ScrollWheel(difference * ScrollSpeed);
          lastPos = currentPos;
        }
      }
    }
  }
}
