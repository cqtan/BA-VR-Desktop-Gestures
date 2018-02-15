using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGestures : MonoBehaviour
{
  [SerializeField] private GameObject GestureManager;

  private GestureManager gestures;
  private bool isWaiting;

  void Start()
  {
    gestures = GestureManager.GetComponent<GestureManager>();
    isWaiting = false;
  }

  void Update()
  {
    if (isWaiting == false)
    {
      TestRightHandPinch();
      TestLeftShowPalm();
      TestGrabGesture();
      TestFingersExtended();
    }
  }

  private void TestRightHandPinch()
  {
    if (gestures.NumberOfHandsVisible == 1 &&
        gestures.RightIsFacingAway == true &&
        gestures.RightIndexPinch == true)
    {
      Debug.Log("1-handed + Right facing away + Right Index-Pinch --> Left-Click");
      StartCoroutine(DelayNextAction());
    }
    else if (gestures.NumberOfHandsVisible == 1 &&
             gestures.RightIsFacingAway == false &&
             gestures.RightPinkyPinch == true)
    {
      Debug.Log("1-handed + Right facing user + Right Pinky-Pinch --> Middle-Click");
      StartCoroutine(DelayNextAction());
    }
    else if (gestures.NumberOfHandsVisible == 1 &&
        gestures.RightIsFacingAway == false &&
        gestures.RightIndexPinch == true)
    {
      Debug.Log("1-handed + Right facing user + Right Index-Pinch --> Right-Click");
      StartCoroutine(DelayNextAction());
    }
  }

  private void TestGrabGesture()
  {
    if (gestures.NumberOfHandsVisible == 1)
    {
      if (gestures.RightGrab == true)
      {
        Debug.Log("1-handed + Right Grab");
        StartCoroutine(DelayNextAction());

      }
      else if (gestures.LeftGrab == true)
      {
        Debug.Log("1-handed + Left Grab");
        StartCoroutine(DelayNextAction());

      }
    }
    else if (gestures.NumberOfHandsVisible == 2 &&
             gestures.RightGrab == true &&
             gestures.LeftGrab == true)
    {
      Debug.Log("2-handed + Both Grab");
      StartCoroutine(DelayNextAction());
    }
  }

  private void TestFingersExtended()
  {
    if (gestures.NumberOfHandsVisible == 1)
    {
      if (gestures.LeftFingersExtended == true)
      {
        Debug.Log("1-handed + Left Fingers extended");
        StartCoroutine(DelayNextAction());
      }
      else if (gestures.RightFingersExtended == true)
      {
        Debug.Log("1-handed + Right Fingers extended");
        StartCoroutine(DelayNextAction());
      }
    }
    else if (gestures.NumberOfHandsVisible == 2 &&
             gestures.LeftFingersExtended == true &&
             gestures.RightFingersExtended == true)
    {
      Debug.Log("2-handed + All Fingers extended");
      StartCoroutine(DelayNextAction());
    }
  }

  private void TestLeftShowPalm()
  {
    if (gestures.NumberOfHandsVisible == 1 &&
        gestures.LeftIsFacingAway == false &&
        gestures.LeftFingersExtended == true)
    {
      Debug.Log("1-handed + Left facing away + Left Fingers extended --> Show Menu");
      StartCoroutine(DelayNextAction());
    }
  }

  IEnumerator DelayNextAction()
  {
    isWaiting = true;
    Debug.Log("...wait a sec...");
    yield return new WaitForSeconds(1);
    isWaiting = false;
  }
}
