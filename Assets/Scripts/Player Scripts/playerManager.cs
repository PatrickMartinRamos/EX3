using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public static playerManager Instance { get; private set; }
    [Header("Jetpack")]
    [SerializeField] private float jetpackCharge = 100;
    [SerializeField] private float jetpackFuel = 100;
    [SerializeField] private float jetpackThrust;
    private float chargeSpeed;
    private bool haveJetpack = false;

    [Range(0.1f, 1)]
    private float chargeMultiplier;

    [Header("Player")]
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float playerChargeStregth = 30;
    private bool onComputer = false;
    private bool isUsingJetpack = false;

    #region
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
           // Debug.Log("PlayerManager instance set");

        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public float getJetpackCharge()
    {
        float s = chargeMultiplier * jetpackCharge;
        return s;
    }

    public float getPlayerChargeStrength()
    {
        float s = chargeMultiplier * playerChargeStregth;
        return s;
    }

    public float getMoveSpeed()
    {
        return playerMoveSpeed;
    }

    public bool getHaveJetpack()
    {
        return haveJetpack;
    }

    public bool getOnComputerStatus()
    {
        return onComputer;
    }

    public bool getIsUsingJetpackStatus()
    {
        return isUsingJetpack; //if ginagamit ung jetpack reduce ung jetpack fuel
    }
    
    public float getJetpackChargeSpeed()
    {
        return chargeSpeed; //if hindi ginagamit ung jetpack refill fuel, can only be charge if gronded
    }
}
