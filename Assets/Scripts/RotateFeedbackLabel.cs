using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFeedbackLabel : MonoBehaviour
{
  [SerializeField] private GameObject GestureManager;
  private GestureManager gesture;
  private bool isFlipped;

  void Start()
  {
    gesture = GestureManager.GetComponent<GestureManager>();
    isFlipped = false;
  }

  void Update()
  {
    if (isFlipped == false)
    {
      if (gesture.RightIsFacingAway == false)
        this.transform.localEulerAngles = new Vector3(270, 90f, 0f);
      else
        this.transform.localEulerAngles = new Vector3(90, 90f, 0f);
    }
  }
}
