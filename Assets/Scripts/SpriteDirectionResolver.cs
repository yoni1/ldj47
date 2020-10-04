using System;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteDirectionResolver
{

    public enum Direction
    {
        W,
        NW,
        N,
        NE,
        E,
        SE,
        S,
        SW
    }

    public static readonly Dictionary<Direction, Quaternion> DIRECTION_ROTATIONS
    = new Dictionary<Direction, Quaternion> {
        {Direction.W, Quaternion.Euler(0f, 0f, 0f)},
        {Direction.NW, Quaternion.Euler(0f, 0f, 45f)},
        {Direction.N, Quaternion.Euler(0f, 0f, 90f)},
        {Direction.NE, Quaternion.Euler(0f, 0f, 135f)},
        {Direction.E, Quaternion.Euler(0f, 0f, 180f)},
        {Direction.SE, Quaternion.Euler(0f, 0f, 225f)},
        {Direction.S, Quaternion.Euler(0f, 0f, 270f)},
        {Direction.SW, Quaternion.Euler(0f, 0f, 315f)}
        };

    public static readonly float THRESHOLD_VALUE = 22.5f;

    public static Direction ResolveDirection(float angle)
    {
        foreach (KeyValuePair<Direction, Quaternion> dirToData in DIRECTION_ROTATIONS)
        {
            if (Mathf.Abs(dirToData.Value.eulerAngles.z - angle) <= THRESHOLD_VALUE)
                return dirToData.Key;
        }

        return Direction.W;
    }

    public static Direction ResolveDirection(Quaternion rotation)
    {

        foreach (KeyValuePair<Direction, Quaternion> dirToData in DIRECTION_ROTATIONS)
        {
            if (Quaternion.Angle(rotation, dirToData.Value) < THRESHOLD_VALUE)
                return dirToData.Key;
        }

        return Direction.W;
    }
}