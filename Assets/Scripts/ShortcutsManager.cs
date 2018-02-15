using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hover.Core.Items.Managers;
using Hover.Core.Items.Types;
using WindowsInput;
using WindowsLaunchExeApi;

public class ShortcutsManager : MonoBehaviour
{
  [SerializeField] private GameObject selectOpenBrowser;
  [SerializeField] private GameObject selectTaskView;
  [SerializeField] private GameObject selectMinMaxWindows;

  private HoverItemSelectionState selectOpenBrowserState;
  private HoverItemSelectionState selectTaskViewState;
  private HoverItemSelectionState selectMinMaxWindowsState;
  private HoverItemDataSelector selectMinMaxWindowsLabel;
  private LaunchExecutable launchExe;
  private bool isMinimized;

  void Start()
  {
    selectOpenBrowserState = selectOpenBrowser.GetComponent<HoverItemSelectionState>();
    selectTaskViewState = selectTaskView.GetComponent<HoverItemSelectionState>();
    selectMinMaxWindowsState = selectMinMaxWindows.GetComponent<HoverItemSelectionState>();
    selectMinMaxWindowsLabel = selectMinMaxWindows.GetComponent<HoverItemDataSelector>();
    launchExe = new LaunchExecutable();
    isMinimized = false;
  }

  void Update()
  {
    if ((int)selectOpenBrowserState.SelectionProgress == 1)
    {
      launchExe.RunChromeWithLink("https://www.google.de/");
      StartCoroutine(DelayAction());
    }

    if ((int)selectTaskViewState.SelectionProgress == 1)
    {
      InputSimulator.SimulateModifiedKeyStroke(new[] { VirtualKeyCode.LMENU, VirtualKeyCode.LCONTROL }, VirtualKeyCode.TAB);
      StartCoroutine(DelayAction());
    }

    if ((int)selectMinMaxWindowsState.SelectionProgress == 1)
    {
      if (isMinimized == false)
      {
        isMinimized = true;
        selectMinMaxWindowsLabel.Label = "Maxmimize All";
        InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_M);
        //launchExe.RunTaskManager();
        StartCoroutine(DelayAction());
      }
      else
      {
        isMinimized = false;
        selectMinMaxWindowsLabel.Label = "Minimize All";
        InputSimulator.SimulateModifiedKeyStroke(new[] { VirtualKeyCode.LWIN, VirtualKeyCode.SHIFT }, VirtualKeyCode.VK_M);
        StartCoroutine(DelayAction());
      }
    }
  }

  IEnumerator DelayAction()
  {
    yield return new WaitForSeconds(1);
  }
}
