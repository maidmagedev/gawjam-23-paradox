using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class StagePlatformTag : ScriptableObject
{

    public TileBase[] tiles;

    public int platformID;
}
