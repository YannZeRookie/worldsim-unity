using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayerPositionner : MonoBehaviour
{
    [SerializeField]
    RectTransform StartPosition;
    [SerializeField]
    float Distance;
    [SerializeField]
    float BlockDistance;
    [SerializeField]
    float ColumnSize;

    int column = 0;

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
    public Vector3 NextPosition(bool RollBackEnabled = true, float? customDistance = null)
    {
        Vector3 position = CurrentPosition;

        CurrentPosition += Vector3.down * ((customDistance == null)? Distance: customDistance.Value);

        if (!RollBackEnabled)
            StartPosition.localPosition = CurrentPosition;

        return position;
    }

    public List<Vector3> CreateColumns(int number, bool RollBackEnabled = true)
    {
        number += 1;

        var positions = new List<Vector3>();
        
        var current = NextPosition(RollBackEnabled, BlockDistance);

        Vector3 start = current + Vector3.left * (ColumnSize * number) / 2;
        Vector3 end = current + Vector3.right * (ColumnSize * number) / 2;

        for (int i = 1; i < number; i++)
        {
            Vector3 newPosition = Vector3.Lerp(start, end, (float)i / number);
            positions.Add(newPosition);
        }

        return positions;
    }

    public void RestartPosition()
    {
        CurrentPosition = StartPosition.localPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(StartPosition.position, Vector3.right * 20);
        Gizmos.DrawRay(StartPosition.position, Vector3.down * 20);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(StartPosition.position + Vector3.down * Distance, Vector3.right * 20);
        Gizmos.DrawRay(StartPosition.position + Vector3.right * ColumnSize, Vector3.down * 20);
    }
}
