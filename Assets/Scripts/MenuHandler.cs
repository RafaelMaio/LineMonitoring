// ===============================
// AUTHOR     : Rafael Maio (rafael.maio@ua.pt)
// PURPOSE     : Handles the navegation between menus
// SPECIAL NOTES: X
// ===============================

using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    /// <summary>
    /// Main menu window.
    /// </summary>
    public GameObject mainMenu;

    /// <summary>
    /// Configuration window.
    /// </summary>
    public GameObject configurationMenu;

    /// <summary>
    /// Visualization window.
    /// </summary>
    public GameObject lineMonitoringMenu;

    /// <summary>
    /// The current menu opened.
    /// </summary>
    private GameObject currentMenu;

    /// <summary>
    /// All menus to navigate (main, configuration and visualization).
    /// </summary>
    private List<GameObject> allMenus = new List<GameObject>();

    /// <summary>
    /// Manager game object.
    /// </summary>
    public GameObject manager;

    /// <summary>
    /// Dialog warning for saving the configuration.
    /// </summary>
    public GameObject saveConfDialog;

    /// <summary>
    /// Button to filter visualization.
    /// </summary>
    public GameObject filterButton;

    /// <summary>
    /// Button to cancel the filtering menu.
    /// </summary>
    public GameObject cancelButton;

    /// <summary>
    /// Collection with the possible filter buttons.
    /// </summary>
    public GameObject filterCollection;

    /// <summary>
    /// Unity Start function.
    /// </summary>
    private void Start()
    {
        currentMenu = mainMenu;
        allMenus.Add(mainMenu);
        allMenus.Add(configurationMenu);
        allMenus.Add(lineMonitoringMenu);
    }

    /// <summary>
    /// Enable menus when the left hand palm is in view.
    /// </summary>
    public void enableCurrentMenu()
    {
        currentMenu.SetActive(true);
    }

    /// <summary>
    /// Disable menus when the left hand palm is out of view.
    /// </summary>
    public void disableMenus()
    {
        foreach (var menu in allMenus)
        {
            menu.SetActive(false);
        }
    }

    /// <summary>
    /// Change menu when buttons are clicked.
    /// </summary>
    /// <param name="menu">Menu to chage.</param>
    public void changeMenu(string menu)
    {
        if (menu.Equals("main"))
        {
            currentMenu = mainMenu; 
            if (manager.GetComponent<ConfigurationHandller>().enabled)
            {
                if (!manager.GetComponent<ConfigurationHandller>().getSavedStatus())
                {
                    Dialog saveDialog = Dialog.Open(saveConfDialog, DialogButtonType.Yes | DialogButtonType.No, "Configuration warning!", "The configuration is not saved. Do you wish to save the configuraion?", true);
                    if (saveDialog != null)
                    {
                        saveDialog.OnClosed += OnClosedSaveDialogEvent;
                    }
                }
                else
                {
                    manager.GetComponent<ConfigurationHandller>().enabled = false;
                }

                if (manager.GetComponent<ConfigurationHandller>().IsInvoking())
                {
                    manager.GetComponent<ConfigurationHandller>().CancelInvoke();
                }
            }
            else
            {
                manager.GetComponent<LineMonitoringHandler>().enabled = false;
                if (manager.GetComponent<LineMonitoringHandler>().IsInvoking())
                {
                    manager.GetComponent<LineMonitoringHandler>().CancelInvoke();
                }
            }
        }
        else if (menu.Equals("configuration"))
        {
            currentMenu = configurationMenu;
        }
        else if (menu.Equals("lineMonitoring"))
        {
            currentMenu = lineMonitoringMenu;
        }
        disableMenus();
        enableCurrentMenu();
    }

    /// <summary>
    /// Close the dialog warning for saving configuration.
    /// </summary>
    /// <param name="obj">Yes or No (to save the configuration).</param>
    private void OnClosedSaveDialogEvent(DialogResult obj)
    {
        if (obj.Result == DialogButtonType.Yes)
        {
            manager.GetComponent<ConfigurationHandller>().saveConfiguration();
        }
        manager.GetComponent<ConfigurationHandller>().enabled = false;
    }

    /// <summary>
    /// Open the filtering collection.
    /// </summary>
    public void FilterPressed()
    {
        filterButton.SetActive(false);
        cancelButton.SetActive(true);
        filterCollection.SetActive(true);
    }

    /// <summary>
    /// Close the filtering collection.
    /// </summary>
    public void CancelFilterPressed()
    {
        filterButton.SetActive(true);
        cancelButton.SetActive(false);
        filterCollection.SetActive(false);
    }
}