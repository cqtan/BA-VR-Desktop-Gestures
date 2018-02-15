using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleHoverCursors : MonoBehaviour
{
  public GameObject LeapRightHand;
  public GameObject LeapLeftHand;
  public GameObject HoverRightHand;
  public GameObject HoverLeftHand;

  // Use this for initialization
  void Start()
  {}

  // Update is called once per frame
  void Update()
  {
    if (LeapRightHand.activeSelf == true)
      HoverRightHand.SetActive(true);
    else
      HoverRightHand.SetActive(false);

    if (LeapLeftHand.activeSelf == true)
      HoverLeftHand.SetActive(true);
    else
      HoverLeftHand.SetActive(false);
  }
}
