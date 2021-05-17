using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayerPositionner : MonoBehaviour
{
    [SerializeField]
    RectTransform StartPosition;
    [SerializeField]
    float Distance;
    Vector3 CurrentPosition;

    private void Start()
    {
        RestartPosition();
    }

    /// <summary>
    ///     Return a valid new position to display a new line of infos for the CellInfosDisplayer
    /// </summary>
    /// <param name="RollBackEnabled">If false, set the returned position as always occupied, and will never
    ///                               return this position or any prior to it, even after a restart</param>
    /// <returns>A new position on the CellInfos panel</returns>
    public Vector3 GetPosition(bool RollBackEnabled = true)
    {
        Vector3 position = CurrentPosition;

        CurrentPosition += Vector3.down * Distance;

        if (!RollBackEnabled)
            StartPosition.localPosition = CurrentPosition;

        return position;
    }

    public void RestartPosition()
    {
        CurrentPosition = StartPosition.localPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(StartPosition.position, Vector3.right * 30);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(StartPosition.position + Vector3.down * Distance, Vector3.right * 30);
    }
}
