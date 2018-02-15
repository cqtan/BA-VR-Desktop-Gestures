using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmShowMenu : MonoBehaviour
{
  [SerializeField] private GameObject GestureManager;
  [SerializeField] private GameObject menu;
  [SerializeField] private GameObject raycasterHead;
  [SerializeField] private bool onPalmShowing;
  private GestureManager gesture;

  void Start()
  {
    gesture = GestureManager.GetComponent<GestureManager>();
  }

  void Update()
  {
    if (gesture.LeftIsVisible == true && 
        gesture.LeftFingersExtended == true)
    {
      if (onPalmShowing == true)
      {
        if (gesture.LeftIsFacingAway == false)
        {
          menu.SetActive(true);
          raycasterHead.SetActive(false);
        }
        else
          menu.SetActive(false);
      }
      else
      {
        if (gesture.LeftIsFacingAway == true)
        {
          menu.SetActive(true);
          raycasterHead.SetActive(false);
        }
        else
          menu.SetActive(false);
      }
    }
    else
    {
      menu.SetActive(false);
      raycasterHead.SetActive(true);
    }
  }
}
