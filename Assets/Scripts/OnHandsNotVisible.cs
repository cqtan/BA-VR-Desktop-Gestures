using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsMouseApi;

public class OnHandsNotVisible : MonoBehaviour
{
  [SerializeField] private GameObject leftHand;
  [SerializeField] private GameObject rightHand;
  [SerializeField] private GameObject pinchMouse;
  [SerializeField] private GameObject menuItems;
  [SerializeField] private GameObject raycasterHead;

  private MouseApi mouseControl;
  private PinchGrabMouse pinchMouseButtons;

  void Start()
  {
    mouseControl = new MouseApi();
    pinchMouseButtons = pinchMouse.GetComponent<PinchGrabMouse>();
  }

  void Update()
  {
    if (leftHand.activeSelf == false)
    {
      menuItems.SetActive(true);
      raycasterHead.SetActive(true);
    }

    if (rightHand.activeSelf == false)
    {
      mouseControl.LeftUp();
      pinchMouseButtons.IsWaiting = false;
      raycasterHead.SetActive(true);
    }
  }
}
