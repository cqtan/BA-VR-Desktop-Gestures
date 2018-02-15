using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRayCaster : MonoBehaviour
{
  public GameObject rayCastHead;
  public GameObject rayCastRight;

  private bool rayCastFromHead;

  // Use this for initialization
  void Start()
  {
    rayCastFromHead = false;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.H))
    {
      rayCastHead.SetActive(!rayCastFromHead);
      rayCastRight.SetActive(rayCastFromHead);
      rayCastFromHead = !rayCastFromHead;
      Debug.Log("QQQ RayCastFromHead is enabled? = " + rayCastFromHead);
    }
  }
}
