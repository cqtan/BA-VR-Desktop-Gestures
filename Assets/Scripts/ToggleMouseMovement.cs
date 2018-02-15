using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hover.Core.Items.Types
{
  public class ToggleMouseMovement : MonoBehaviour
  {
    public GameObject RaycasterHead;
    public GameObject RaycasterRightHand;

    private HoverItemDataRadio radioRightHandControl;
    private HoverItemDataRadio radioHeadControl;


    // Use this for initialization
    void Start()
    {
      radioRightHandControl = GameObject.Find("RadioRightHandAsMouse").GetComponent<HoverItemDataRadio>();
      radioHeadControl = GameObject.Find("RadioHeadAsMouse").GetComponent<HoverItemDataRadio>();
    }

    // Update is called once per frame
    void Update()
    {
      if (radioRightHandControl.Value == true)
      {
        RaycasterRightHand.SetActive(true);
        radioHeadControl.Value = false;
        RaycasterHead.SetActive(false);
      }
      else
      {
        RaycasterRightHand.SetActive(false);
        radioRightHandControl.Value = false;
        RaycasterHead.SetActive(true);
      }
    }
  }
}
