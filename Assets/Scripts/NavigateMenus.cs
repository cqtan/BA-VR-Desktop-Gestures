using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hover.Core.Items.Managers;

public class NavigateMenus : MonoBehaviour
{
  public MenuItemsManager.MenuType MenuDestination;

  private MenuItemsManager menuController;
  private HoverItemSelectionState selection;
  private int selected;

  void Start()
  {
    menuController = GameObject.Find("MenuManager").GetComponent<MenuItemsManager>();
    selection = this.GetComponent<HoverItemSelectionState>();
  }

  void Update()
  {
    selected = (int)selection.SelectionProgress;
    if (selected == 1)
    {
      menuController._MenuType = MenuDestination;
    }
  }
}
