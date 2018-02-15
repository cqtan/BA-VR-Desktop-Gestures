using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemsManager : MonoBehaviour
{
  public enum MenuType
  {
    MainMenu = 1,
    MonitorMenu,
    Shortcuts,
    Guide
  }
  public MenuType _MenuType { get; set; }

  [SerializeField] private MenuType MenuStartingPoint;
  [SerializeField] private GameObject mainMenu;
  [SerializeField] private GameObject monitorMenu;
  [SerializeField] private GameObject shortcuts;
  [SerializeField] private GameObject guide;

  private int MouseOptionsSelected;
  private MenuType _LastMenuType;

  void Start()
  {
    _MenuType = MenuStartingPoint;
  }

  void Update()
  {
    if (_MenuType != _LastMenuType)
    {
      _LastMenuType = _MenuType;
      switch (_MenuType)
      {
        case MenuType.MainMenu:
          mainMenu.SetActive(true);
          monitorMenu.SetActive(false);
          shortcuts.SetActive(false);
          guide.SetActive(false);
          Debug.Log("[1] Showing Main Menu");
          break;
        case MenuType.MonitorMenu:
          mainMenu.SetActive(false);
          monitorMenu.SetActive(true);
          shortcuts.SetActive(false);
          guide.SetActive(false);
          Debug.Log("[2] Showing Monitor Menu");
          break;
        case MenuType.Shortcuts:
          mainMenu.SetActive(false);
          monitorMenu.SetActive(false);
          shortcuts.SetActive(true);
          guide.SetActive(false);
          Debug.Log("[3] Showing Shortcuts");
          break;
        case MenuType.Guide:
          mainMenu.SetActive(false);
          monitorMenu.SetActive(false);
          shortcuts.SetActive(false);
          guide.SetActive(true);
          Debug.Log("[4] Showing Guide");
          break;
      }
    }
  }
}
