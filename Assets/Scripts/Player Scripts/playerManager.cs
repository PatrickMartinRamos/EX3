using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public static playerManager Instance { get; private set; }
    [Header("Jetpack")]
    [SerializeField] private float jetpackChargeStrength = 100;//not implemented
    [SerializeField] private float _fuelDepleteRate  = .5f;
    [SerializeField] private float _fuelRefileRate  = 5f;
    [SerializeField] private float jetpackFuel = 100;
    [SerializeField] private float jetpackThrust = 10f;
    [SerializeField] private bool haveJetpack = false; //set to true for testing

    private float chargeMultiplier = 1f;

    [Header("Player")]
    [SerializeField] private float playerChargeStregth = 30;
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

    #region getters
    public float getJetpackCharge()
    {
        float s = chargeMultiplier * jetpackChargeStrength;
        return s;
    }

    public float getFuelDepleteRate()
    {
        return _fuelDepleteRate ;
    }

    public float getFuelRefileRate()
    {
        return _fuelRefileRate;
    }

    public float getFuel()
    {
        return jetpackFuel;
    }

    public float getPlayerChargeStrength()
    {
        float s = chargeMultiplier * playerChargeStregth;
        return s;
    }

    public float getJetpackThrustForce()
    {
        return jetpackThrust;
    }

    public bool getHaveJetpack()
    {
        return haveJetpack;
    }

    public bool getIsUsingJetpackStatus()
    {
        return isUsingJetpack; //if ginagamit ung jetpack reduce ung jetpack fuel
    }
    #endregion
}
