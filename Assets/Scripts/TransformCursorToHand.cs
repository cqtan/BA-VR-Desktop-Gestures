using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCursorToHand : MonoBehaviour
{
  public GameObject cursor;

  private Vector3 currentPosition;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    currentPosition = this.transform.position;
    cursor.transform.position = currentPosition;
  }
}
