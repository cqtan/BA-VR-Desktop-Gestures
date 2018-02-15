using UnityEngine;
using System.Collections;
using WindowsMouseApi;

public class RaycastTest : MonoBehaviour
{
  public Transform from;
  public Transform to;
  public bool enableMouseControl;

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

    if (Input.GetKeyDown(KeyCode.M))
    {
      enableMouseControl = !enableMouseControl;
      //Debug.Log("xxx MouseControl is enabled? = " + enableMouseControl);
    }

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


        if (enableMouseControl)
        {
          mouseControl.MoveTo(result.desktopCoord.x, result.desktopCoord.y);
          //Debug.Log("---Moved mouse to: " + result.desktopCoord.x + ", " + result.desktopCoord.y);
        }
        
      }
    }
  }
}
