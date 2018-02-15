using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Hover.Core.Items.Types;

/*
 * If both hands are performing the grab gesture then change values of the monitor
 * depending on the chosen radio button (Size, Bend or Zoom).
 */
public class MonitorOptionsManager : MonoBehaviour
{
  [SerializeField] private GameObject radioMonitorSize;
  [SerializeField] private GameObject radioMonitorBend;
  [SerializeField] private GameObject radioMonitorZoom;

  [SerializeField] private GameObject grabManager;
  [SerializeField] private GameObject monitor;

  [SerializeField] private GameObject labelFeedback;

  [SerializeField] private float scaleMinValue;
  [SerializeField] private float scaleMaxValue;
  [SerializeField] private float scaleModifier;

  [SerializeField] private float bendMinValue;
  [SerializeField] private float bendMaxValue;
  [SerializeField] private float bendModifier;

  [SerializeField] private float zoomMinValue;
  [SerializeField] private float zoomMaxValue;
  [SerializeField] private float zoomModifier;

  private HoverItemDataRadio radioSize;
  private HoverItemDataRadio radioBend;
  private HoverItemDataRadio radioZoom;

  private uDesktopDuplication.Texture monitorTexture;
  private GrabGestureTwoHands grabGesture;
  private Transform monitorTransform;
  private float monitorOriginalSizeX;
  private float monitorLastSizeX;

  private float scaleSpeedX;
  private float scaleSpeedY;
  private float scaleMinX;
  private float scaleMaxX;
  private float scaleMinY;
  private float scaleMaxY;
  private float bendOriginalMinValue;
  private HoverItemDataText feedback;

  void Start()
  {
    feedback = labelFeedback.GetComponent<HoverItemDataText>();
    radioSize = radioMonitorSize.GetComponent<HoverItemDataRadio>();
    radioBend = radioMonitorBend.GetComponent<HoverItemDataRadio>();
    radioZoom = radioMonitorZoom.GetComponent<HoverItemDataRadio>();
    monitorTexture = monitor.GetComponent<uDesktopDuplication.Texture>();
    monitorTransform = monitor.GetComponent<Transform>();
    grabGesture = grabManager.GetComponent<GrabGestureTwoHands>();

    if (bendMinValue < 3.1f && bendMinValue > 70f)
      bendMinValue = 3.1f;
    if (bendMaxValue < 3.1f && bendMaxValue > 70f)
      bendMaxValue = 70f;

    bendOriginalMinValue = bendMinValue;

    monitorOriginalSizeX = monitorTransform.localScale.x;
    monitorLastSizeX = monitorOriginalSizeX;

    scaleMinX = scaleMinValue * monitorTransform.localScale.x;
    scaleMaxX = scaleMaxValue * monitorTransform.localScale.x;
    scaleMinY = scaleMinValue * monitorTransform.localScale.y;
    scaleMaxY = scaleMaxValue * monitorTransform.localScale.y;
    scaleSpeedX = monitorTransform.localScale.x * scaleModifier;
    scaleSpeedY = monitorTransform.localScale.y * scaleModifier;
  }

  void Update()
  {
    if (grabGesture.BothGrabsDetected == true)
    {
      if (radioSize.Value == true)
      {
        GrabScaleOption();
        feedback.Label = "2h-Grab: Scale";
      }
      else if (radioBend.Value == true)
      {
        GrabBendOption();
        feedback.Label = "2h-Grab: Bend";
      }
      else if (radioZoom.Value == true)
      {
        GrabZoomOption();
        feedback.Label = "2h-Grab: Zoom";
      }
    }
  }

  private void GrabScaleOption()
  {
    float scaleGrabSpeedX;
    float scaleGrabSpeedY;
    float currentScaleValueX;
    scaleGrabSpeedX = scaleSpeedX * grabGesture.DistanceDifference;
    scaleGrabSpeedY = scaleSpeedY * grabGesture.DistanceDifference;
    monitorTransform.localScale += new Vector3(scaleGrabSpeedX, scaleGrabSpeedY, 0);
    currentScaleValueX = monitorTransform.localScale.x;

    if (currentScaleValueX <= scaleMinX)
      monitorTransform.localScale = new Vector3(scaleMinX, scaleMinY, 1);
    else if (currentScaleValueX >= scaleMaxX)
      monitorTransform.localScale = new Vector3(scaleMaxX, scaleMaxY, 1);
  }

  private void GrabBendOption()
  {
    float bendNewValue;
    float currentBendRadius;
    bendNewValue = grabGesture.DistanceDifference;
    bendNewValue *= (bendModifier * 100);
    monitorTexture.radius += bendNewValue;
    currentBendRadius = monitorTexture.radius;

    adaptMinBend();

    if (currentBendRadius <= bendMinValue)
      monitorTexture.radius = bendMinValue;
    else if (currentBendRadius >= bendMaxValue)
      monitorTexture.radius = bendMaxValue;
  }

  private void GrabZoomOption()
  {
    float currentZoomValue;
    float newZoomValue;
    newZoomValue = -10 * grabGesture.DistanceDifference;
    newZoomValue *= zoomModifier;
    monitorTransform.position += new Vector3(0, 0, newZoomValue);
    currentZoomValue = monitor.transform.position.z;

    if (currentZoomValue <= zoomMinValue)
      monitorTransform.position = new Vector3(0, 0, zoomMinValue);
    else if (currentZoomValue >= zoomMaxValue)
      monitorTransform.position = new Vector3(0, 0, zoomMaxValue);
  }

  // Prevents Bend overlap when screen is scaled higher
  private void adaptMinBend()
  {
    float monitorCurrentSizeX = monitorTransform.localScale.x;
    if (monitorCurrentSizeX != monitorLastSizeX)
    {
      bendMinValue = bendOriginalMinValue * (monitorCurrentSizeX / monitorOriginalSizeX);
      monitorLastSizeX = monitorCurrentSizeX;
    }
  }
}
