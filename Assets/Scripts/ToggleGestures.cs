using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hover.Core.Items.Types;

public class ToggleGestures : MonoBehaviour
{
  [SerializeField] private GameObject grabGestureTwoHands;
  [SerializeField] private GameObject pinchMouse;
  [SerializeField] private GameObject palmShowMenu;
  [SerializeField] private GameObject palmMouseScroll;
  [SerializeField] private GameObject grabMouseScroll;
  [SerializeField] private GameObject palmShowDataManipulation;

  private GestureManager gesture;

  void Start()
  {
    gesture = this.GetComponent<GestureManager>();
  }

  void Update()
  {
    if (gesture.NumberOfHandsVisible == 1)
    {
      if (gesture.RightIsVisible == true)
      {
        pinchMouse.SetActive(true);
        //palmMouseScroll.SetActive(true);
        grabMouseScroll.SetActive(true);
      }

      if (gesture.LeftIsVisible == true)
      {
        palmShowMenu.SetActive(true);
        grabMouseScroll.SetActive(true);
        palmShowDataManipulation.SetActive(true);
      }
    }
    else if (gesture.NumberOfHandsVisible == 2)
    {
      pinchMouse.SetActive(false);
      //palmMouseScroll.SetActive(false);
      grabMouseScroll.SetActive(true);
      grabGestureTwoHands.SetActive(true);
      palmShowMenu.SetActive(true);
      palmShowDataManipulation.SetActive(true);
    }
    else {
      grabGestureTwoHands.SetActive(false);
      pinchMouse.SetActive(false);
      palmShowMenu.SetActive(false);
      palmShowDataManipulation.SetActive(false);
      //palmMouseScroll.SetActive(false);
    }
  }
}
