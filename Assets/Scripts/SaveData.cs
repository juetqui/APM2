using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData
{
    public int life;
    public string heroName;
    public float[] myCol;
    public MagicPower myPower;
    public Vector3 position;
}

public enum MagicPower { Fire, Aqua, Terra }