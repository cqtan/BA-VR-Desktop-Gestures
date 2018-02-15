using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hover.Core.Items.Types
{
  public class ToggleMouseControl : MonoBehaviour
  {
    public GameObject rayCastHead;
    public GameObject rayCastRight;


    private bool useHandAsMouse;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      useHandAsMouse = this.GetComponent<HoverItemDataCheckbox>().Value;

      if (useHandAsMouse)
      {
        rayCastRight.SetActive(true);
        rayCastHead.SetActive(false);
      }
      else
      {
        rayCastRight.SetActive(false);
        rayCastHead.SetActive(true);
      }
    }
  }

}