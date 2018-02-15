using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;

public class HandRotation : MonoBehaviour
{
  public bool LeftPalmUp { get; private set; }
  public bool RightPalmUp { get; private set; }

  private LeapServiceProvider provider;
  public Vector leftPalmNormal;
  public Vector rightPalmNormal;

  void Start()
  {
    provider = FindObjectOfType<LeapServiceProvider>();
    LeftPalmUp = false;
    RightPalmUp = false;
  }

  void Update()
  {
    Frame currentFrame = provider.CurrentFrame;
    foreach (Hand hand in currentFrame.Hands)
    {
      if (hand.IsLeft)
      {
        leftPalmNormal = hand.PalmNormal;
        if (hand.PalmNormal.z < 0.2f)
          LeftPalmUp = true;
        else
          LeftPalmUp = false;
      }
      else if (hand.IsRight)
      {
        rightPalmNormal = hand.PalmNormal;
        if (hand.PalmNormal.z < 0.2f)
          RightPalmUp = true;
        else
          RightPalmUp = false;
      }
    }
  }
}
