// ===============================
// AUTHOR     : Rafael Maio (rafael.maio@ua.pt)
// PURPOSE     : Static function to be accessed by any script.
// SPECIAL NOTES: X
// ===============================

using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticFunctions : MonoBehaviour
{
    /// <summary>
    /// Pinch threshold.
    /// </summary>
    private const float PinchThreshold = 0.7f;

    /// <summary>
    /// Find a transform child from an object.
    /// </summary>
    /// <param name="aParent">Transform from the parent game object.</param>
    /// <param name="aName">Child game object name.</param>
    /// <returns>The child transform.</returns>
    static public Transform FindChildByRecursion(Transform aParent, string aName)
    {
        if (aParent == null) return null;
        var result = aParent.Find(aName);
        if (result != null)
            return result;
        foreach (Transform child in aParent)
        {
            result = FindChildByRecursion(child, aName);
            if (result != null)
                return result;
        }
        return null;
    }

    /// <summary>
    /// Verifies if the hand is pinching.
    /// </summary>
    /// <param name="trackedHand">The hand being tracked.</param>
    /// <returns>If the hand is pinching.</returns>
    public static bool IsPinching(Handedness trackedHand)
    {
        return HandPoseUtils.CalculateIndexPinch(trackedHand) > PinchThreshold;
    }

    /// <summary>
    /// Maintain the transform when chaning the personal window.
    /// </summary>
    /// <param name="closed">The window being closed.</param>
    /// <param name="opened">The window being opened.</param>
    public static void maintainTransform(GameObject closed, GameObject opened)
    {
        opened.transform.position = closed.transform.position;
        opened.transform.rotation = closed.transform.rotation;
        opened.transform.localScale = closed.transform.localScale;
    }
}