using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public static playerManager Instance { get; private set; }
    [Header("Jetpack")]
    [SerializeField] private float _fuelDepleteRate;
    [SerializeField] private float _fuelRefileRate;
    [SerializeField] private float jetpackFuel;
    [SerializeField] private float jetpackThrust;

    private float chargeMultiplier = 1f;

    [Header("Player")]
    [SerializeField] private float playerDashStregth = 30;
    private bool isUsingJetpack = false;

    #region
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
           // DontDestroyOnLoad(gameObject);
           // Debug.Log("PlayerManager instance set");

        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region getters
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
        float s = chargeMultiplier * playerDashStregth;
        return s;
    }

    public float getJetpackThrustForce()
    {
        return jetpackThrust;
    }

    public bool getIsUsingJetpackStatus()
    {
        return isUsingJetpack; //if ginagamit ung jetpack reduce ung jetpack fuel
    }
    #endregion
}
