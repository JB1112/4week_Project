using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyController : MonoBehaviour
{
    public PlayerInputs playerInputs { get; private set; }
    public PlayerInputs.PlayerActions playerActions { get; private set; }

    public GameObject TabPanel;

    private void Awake()
    {
        playerInputs = new PlayerInputs();
        playerActions = playerInputs.Player;

        playerActions.Inventory.started += OpenTab;
    }
    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }

    private void OpenTab(InputAction.CallbackContext context)
    {
        if (TabPanel != null)
        {
            TabPanel.SetActive(!TabPanel.activeSelf);
        }
    }
}
