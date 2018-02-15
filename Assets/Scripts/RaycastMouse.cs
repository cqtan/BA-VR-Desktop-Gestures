using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsMouseApi;


public class RaycastMouse : MonoBehaviour
{
  public Transform from;
  public Transform to;

  private MouseApi mouseControl;
  private uDesktopDuplication.Texture monitorRes;
  private LineRenderer laserLine;

  void Start()
  {
    monitorRes = FindObjectOfType<uDesktopDuplication.Texture>();
    mouseControl = new MouseApi();
    mouseControl.setScreenBounds((float)monitorRes.monitor.width, (float)monitorRes.monitor.height);
    laserLine = GetComponent<LineRenderer>();
  }

  void Update()
  {
    if (!from || !to) return;

    Debug.DrawLine(from.position, to.position, Color.red);
    laserLine.SetPosition(0, from.position);

    foreach (var uddTexture in GameObject.FindObjectsOfType<uDesktopDuplication.Texture>())
    {
      var result = uddTexture.RayCast(from.position, to.position - from.position);
      if (result.hit)
      {
        Debug.DrawLine(result.position, result.position + result.normal, Color.yellow);
        //Debug.Log("COORD: " + result.coords + ", DESKTOP: " + result.desktopCoord);
        laserLine.SetPosition(1, to.position);
      
        mouseControl.MoveTo(result.desktopCoord.x, result.desktopCoord.y);
        //Debug.Log("---Moved mouse to: " + result.desktopCoord.x + ", " + result.desktopCoord.y);
      }
    }
  }
}
