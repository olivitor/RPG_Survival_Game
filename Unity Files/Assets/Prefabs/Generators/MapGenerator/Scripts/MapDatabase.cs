using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDatabase : MonoBehaviour
{
    public List<TileData> tiles = new List<TileData>();

    public void StoreData(Vector3Int _tilePos, TileBase _tileRef)
    {
        tiles.Add(new TileData(_tilePos, _tileRef));
    }
}

public class TileData 
{
    public Vector3Int tilePosition;
    public TileBase tileReference;

    public TileData(Vector3Int _tilePos, TileBase _tileRef)
    {
        tilePosition = _tilePos;
        tileReference = _tileRef;
    }
}
