using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class gateScript : MonoBehaviour
{
    [SerializeField] private Transform leftGate;
    [SerializeField] private Transform rightGate;
    [SerializeField] private Transform orangeGate;

    [SerializeField] private float leftMoveAmount;
    [SerializeField] private float righttMoveAmount;
    [SerializeField] private float upwardMoveAmount;

    public void openGreenGate()
    {
        Debug.Log("Gate opened: " + gameObject.name);
        //gameObject.SetActive(false);

        leftGate.DOLocalMoveX(leftMoveAmount, 3f).SetEase(Ease.InCubic);
        rightGate.DOLocalMoveX(righttMoveAmount, 3f).SetEase(Ease.InCubic);
    }

    public void openOrangeGatre()
    {
        Debug.Log("Orange Gate opened: " + gameObject.name);

        // Shrink the orange gate upward by reducing its Y scale
        orangeGate.DOScaleY(0, upwardMoveAmount).SetEase(Ease.InCubic);
    }


}
