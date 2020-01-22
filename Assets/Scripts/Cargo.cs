using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    public float Weight;
    public Size Size;
    public Vector3 Dimensions;
}

public enum Size
{
    Small = 0, Medium = 1, Large = 2, XLarge = 3
}