using UnityEngine;
using System;

public class GridPosition : IEquatable<GridPosition> {
    public int Q { get; private set; }
    public int R { get; private set; }
    public int S { get; private set; }

    public GridPosition(int q, int r) {
        Q = q;
        R = r;
        S = -q - r;
    }

    public override bool Equals(object obj) {
        return obj is GridPosition position && Q == position.Q && R == position.R;
    }

    public bool Equals(GridPosition other) {
        return this == other;
    }

    public override int GetHashCode() {
        return HashCode.Combine(Q, R, S);
    }

    public override string ToString() {
        return "(" + Q + ", " + R + ", " + S + ")";
    }

    public static bool operator ==(GridPosition a, GridPosition b) {
        return a.Q ==  b.Q && a.R == b.R;
    }

    public static bool operator !=(GridPosition a, GridPosition b) {
        return !(a == b);
    }

    public static GridPosition operator +(GridPosition a, GridPosition b) {
        return new GridPosition(a.Q + b.Q, a.R + b.R);
    }

    public static GridPosition operator -(GridPosition a, GridPosition b) {
        return new GridPosition(a.Q - b.Q, a.R - b.R);
    }
}
