﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class EmoteConstraint : MonoBehaviour
{
    public Transform originalBone;
    public Transform emoteBone;
    Vector3 originalPosition;
    Quaternion originalRotation;
    public bool constraintActive = false;
    public bool revertTransform;
    bool firstTime = true;
    bool hasEverActivatedConstraints = false;
    public bool onlyY = false;
    void Update()
    {
        if (constraintActive)
        {
            if (onlyY)
            {
                originalBone.position = new Vector3(originalBone.position.x, emoteBone.position.y, originalBone.position.z);
            }
            else
            {
                originalBone.position = emoteBone.position;
                originalBone.rotation = emoteBone.rotation;
            }
        }
    }
    public void ActivateConstraints()
    {
        if (!constraintActive)
        {
            originalPosition = originalBone.localPosition;
            originalRotation = originalBone.localRotation;
            hasEverActivatedConstraints = true;
            constraintActive = true;
            onlyY = false;
        }
    }
    public void DeactivateConstraints()
    {
        constraintActive = false;
        if (firstTime || !revertTransform || !hasEverActivatedConstraints)
        {
            firstTime = false;
        }
        else
        {
            originalBone.localPosition = originalPosition;
            originalBone.localRotation = originalRotation;
        }
    }
    internal void AddSource(ref Transform originalBone, ref Transform emoteBone)
    {
        this.originalBone = originalBone;
        this.emoteBone = emoteBone;
    }
    internal void AddSource(Transform originalBone, Transform emoteBone)
    {
        this.originalBone = originalBone;
        this.emoteBone = emoteBone;
    }
}



//PLAYING MORE THAN ONE EMOTE WITHOUT GOING TO "none" FIRST CAUSES THE ARMS TO BREAK, LOOK INTO IT?