using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hover.Core.Items.Managers;
using Hover.Core.Items.Types;
using WindowsInput;

public class DataManipulationManager : MonoBehaviour
{
  [SerializeField] private GameObject selectCopy;
  [SerializeField] private GameObject selectPaste;
  [SerializeField] private GameObject selectCut;
  [SerializeField] private GameObject selectUndo;
  [SerializeField] private GameObject selectRedo;
  [SerializeField] private GameObject labelDataManipulation;

  private HoverItemSelectionState selectCopyState;
  private HoverItemSelectionState selectPasteState;
  private HoverItemSelectionState selectCutState;
  private HoverItemSelectionState selectUndoState;
  private HoverItemSelectionState selectRedoState;
  private HoverItemDataText labelDataManipulationText;
  private string labelHeader;

  void Start()
  {
    selectCopyState = selectCopy.GetComponent<HoverItemSelectionState>();
    selectPasteState = selectPaste.GetComponent<HoverItemSelectionState>();
    selectCutState = selectCut.GetComponent<HoverItemSelectionState>();
    selectUndoState = selectUndo.GetComponent<HoverItemSelectionState>();
    selectRedoState = selectRedo.GetComponent<HoverItemSelectionState>();
    labelDataManipulationText = labelDataManipulation.GetComponent<HoverItemDataText>();
    labelHeader = "Last Action: ";
  }

  void Update()
  {
    if ((int)selectCopyState.SelectionProgress == 1)
    {
      InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LCONTROL, VirtualKeyCode.VK_C);
      labelDataManipulationText.Label = labelHeader + "Copy";
      Debug.Log("--> COPY");
    }

    if ((int)selectPasteState.SelectionProgress == 1)
    {
      InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LCONTROL, VirtualKeyCode.VK_V);
      labelDataManipulationText.Label = labelHeader + "Paste";
      Debug.Log("--> PASTE");
    }

    if ((int)selectCutState.SelectionProgress == 1)
    {
      InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LCONTROL, VirtualKeyCode.VK_X);
      labelDataManipulationText.Label = labelHeader + "Cut";
      Debug.Log("--> CUT");
    }

    if ((int)selectUndoState.SelectionProgress == 1)
    {
      InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LCONTROL, VirtualKeyCode.VK_Z);
      labelDataManipulationText.Label = labelHeader + "Undo";
      Debug.Log("--> UNDO");
    }

    if ((int)selectRedoState.SelectionProgress == 1)
    {
      InputSimulator.SimulateModifiedKeyStroke(new[] { VirtualKeyCode.LCONTROL, VirtualKeyCode.SHIFT }, VirtualKeyCode.VK_Z);
      labelDataManipulationText.Label = labelHeader + "Redo";
      Debug.Log("--> REDO");
    }
  }
}
