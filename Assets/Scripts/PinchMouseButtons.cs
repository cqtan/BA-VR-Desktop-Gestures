using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsMouseApi;


public class PinchMouseButtons : MonoBehaviour
{
  [SerializeField] private GameObject GestureManager;
  [SerializeField] private GameObject PalmLabel;
  [SerializeField] private bool useLeftHand;

  public bool IsWaiting { get; set; }

  private GestureManager gesture;
  private MouseApi mouseControl;
  private bool wasHolding;

  void Start()
  {
    gesture = GestureManager.GetComponent<GestureManager>();
    mouseControl = new MouseApi();
    wasHolding = false;
  }

  void Update()
  {
    if (IsWaiting == false)
    {
      if (useLeftHand == true)
      {
        if (gesture.LeftIsVisible)
        {
          if (gesture.LeftIsFacingAway == true)
          {
            PalmLabel.SetActive(false);
            if (gesture.LeftIndexPinch == true)
            {
              if (wasHolding == false)
              {
                Debug.Log("LEFT-CLICK-DOWN");
                mouseControl.LeftDown();
                wasHolding = true;
              }
            }
            else
            {
              Debug.Log("LEFT-CLICK-UP");
              mouseControl.LeftUp();
              wasHolding = false;
            }
          }
          else if (gesture.LeftIsFacingAway == false)
          {
            if (wasHolding == true)
            {
              Debug.Log("LEFT-CLICK-UP");
              mouseControl.LeftUp();
              wasHolding = false;
            }
            PalmLabel.SetActive(true);
            if (gesture.LeftIndexPinch == true)
            {
              Debug.Log("RIGHT-CLICK");
              mouseControl.RightClick();
              StartCoroutine(DelayNextClick(1.0f));
            }
            else if (gesture.LeftPinkyPinch == true)
            {
              Debug.Log("MIDDLE-CLICK");
              mouseControl.MiddleClick();
              StartCoroutine(DelayNextClick(1.0f));
            }
          }
        }
      }
      else
      {
        if (gesture.RightIsVisible)
        {
          if (gesture.RightIsFacingAway == true)
          {
            PalmLabel.SetActive(false);
            if (gesture.RightIndexPinch == true)
            {
              if (wasHolding == false)
              {
                Debug.Log("LEFT-CLICK-DOWN");
                mouseControl.LeftDown();
                wasHolding = true;
              }
            }
            else
            {
              if (wasHolding == true)
              {
                Debug.Log("LEFT-CLICK-UP");
                mouseControl.LeftUp();
                wasHolding = false;
              }
            }
          }
          else if (gesture.RightIsFacingAway == false)
          {
            if (wasHolding == true)
            {
              Debug.Log("LEFT-CLICK-UP");
              mouseControl.LeftUp();
              wasHolding = false;
            }

            PalmLabel.SetActive(true);
            if (gesture.RightIndexPinch == true)
            {
              Debug.Log("RIGHT-CLICK");
              mouseControl.RightClick();
              StartCoroutine(DelayNextClick(1.0f));
            }
            else if (gesture.RightPinkyPinch == true)
            {
              Debug.Log("MIDDLE-CLICK");
              mouseControl.MiddleClick();
              StartCoroutine(DelayNextClick(1.0f));
            }
          }
        }
      }
    }

  }

  IEnumerator DelayNextClick(float seconds)
  {
    IsWaiting = true;
    yield return new WaitForSeconds(seconds);
    IsWaiting = false;
  }
}
