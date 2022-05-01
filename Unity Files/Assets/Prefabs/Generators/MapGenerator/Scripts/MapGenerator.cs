using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [Header("General Settings")]
    public int seed;

    [Header("Chunk Settings")]
    private Vector2 chunkSize;
    [SerializeField] private float perlinSize;

    [Header("Tile Settings")]
    [SerializeField] private Tilemap ground;
    [SerializeField] private TileBase groundTile;

    public Vector2 centerSpacement;
    private MapRenderer mapRenderer;

    private void Awake()
    {
        centerSpacement.x = chunkSize.x % 2 == 0 ? 0 : .5f;
        centerSpacement.y = chunkSize.y % 2 == 0 ? 0 : .5f;
        mapRenderer = this.GetComponent<MapRenderer>();
        chunkSize = mapRenderer.chunkSize;
    }

    public void DrawChunk(Vector2 _centerPos)
    {
        Vector2 _startPos = new Vector2 (_centerPos.x - ((chunkSize.x / 2) - centerSpacement.x),
            _centerPos.y - ((chunkSize.y / 2) - centerSpacement.y));
        
        //Generate tiles per perlin position
        for (var _y = (int)_startPos.y; _y < (int)_startPos.y + chunkSize.y; _y++)
        {
            for (var _x = (int)_startPos.x; _x < (int)_startPos.x + chunkSize.x; _x++)
            {
                float _perlin = Mathf.PerlinNoise((_x + seed) * perlinSize, (_y + seed) * perlinSize);
                if (_perlin > .5f) ground.SetTile(new Vector3Int(_x, _y), groundTile);
            }
        }
    }

    public void ResetChunks()
    {
        ground.ClearAllTiles();
    }
}
