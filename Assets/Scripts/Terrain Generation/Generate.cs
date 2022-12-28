using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generate : MonoBehaviour
{
    public TileBase grassTile;

    public int[] size = { 50, 50 };

    // Start is called before the first frame update
    void Start()
    {
        var tilemap = GetComponent<Tilemap>();

        var positions = new Vector3Int[size[0]*size[1]];
        var tiles = new TileBase[positions.Length];

        for (var index = 0; index < positions.Length; index++)
        {
            positions[index] = new Vector3Int(index % size[0], index / size[1], 0);
            tiles[index] = grassTile;
        }
        
        tilemap.SetTiles(positions, tiles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
