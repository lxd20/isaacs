﻿using HoloToolkit;
using UnityEngine.VR.WSA.Input;
using UnityEngine;

/// <summary>
/// HandsManager keeps track of when a hand is detected.
/// </summary>
public class HandsManager : Singleton<HandsManager>
{

    /// <summary>
    /// Tracks the hand detected state.
    /// </summary>
    public bool HandDetected
    {
        get;
        private set;
    }

    // Keeps track of the GameObject that the hand is interacting with.
    public GameObject FocusedGameObject { get; private set; }

    void Awake()
    {

        InteractionManager.SourceDetected += InteractionManager_SourceDetected;
        InteractionManager.SourceLost += InteractionManager_SourceLost;

        /* TODO: DEVELOPER CODE ALONG 2.a */

        // 2.a: Register for SourceManager.SourcePressed event.
        InteractionManager.SourcePressed += InteractionManager_SourcePressed;

        // 2.a: Register for SourceManager.SourceReleased event.
        InteractionManager.SourceReleased += InteractionManager_SourceReleased;

        // 2.a: Initialize FocusedGameObject as null.
        FocusedGameObject = null;
    }

   

    private void InteractionManager_SourceDetected(InteractionSourceState hand)
    {
        HandDetected = true;
    }

    private void InteractionManager_SourceLost(InteractionSourceState hand)
    {
        HandDetected = false;

        // 2.a: Reset FocusedGameObject.
        ResetFocusedGameObject();
    }

    private void InteractionManager_SourcePressed(InteractionSourceState hand)
    {
        if (InteractibleManager.Instance.FocusedGameObject != null)
        {
   
            // 2.a: Cache InteractibleManager's FocusedGameObject in FocusedGameObject.
            FocusedGameObject = InteractibleManager.Instance.FocusedGameObject;
        }
    }

    private void InteractionManager_SourceReleased(InteractionSourceState hand)
    {
        // 2.a: Reset FocusedGameObject.
        ResetFocusedGameObject();
    }

    private void ResetFocusedGameObject()
    {
        // 2.a: Set FocusedGameObject to be null.
        FocusedGameObject = null;

        // 2.a: On GestureManager call ResetGestureRecognizers
        // to complete any currently active gestures.
        GestureManager.Instance.ResetGestureRecognizers();
    }

    void OnDestroy()
    {
        InteractionManager.SourceDetected -= InteractionManager_SourceDetected;
        InteractionManager.SourceLost -= InteractionManager_SourceLost;

        // 2.a: Unregister the SourceManager.SourceReleased event.
        InteractionManager.SourceReleased -= InteractionManager_SourceReleased;

        // 2.a: Unregister for SourceManager.SourcePressed event.
        InteractionManager.SourcePressed -= InteractionManager_SourcePressed;
    }
}