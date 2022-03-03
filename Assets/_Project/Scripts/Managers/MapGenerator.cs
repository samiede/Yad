using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class MapGenerator : MonoBehaviour
    {

        public GameTile[,] mapTiles;
        
        [SerializeField] private GameObject fieldPrefab;
        [SerializeField] private Vector2Int widthHeight;
        private Transform _transform;

        private void Awake()
        {
            int x = Mathf.Max(1, widthHeight.x % 2 == 0 ? widthHeight.x : widthHeight.x - 1);
            int y = Mathf.Max(1, widthHeight.y % 2 == 0 ? widthHeight.y : widthHeight.y - 1);
            mapTiles = new GameTile[x, y];

            _transform = transform;
            
        }


        public void GenerateMap()
        {
            _GenerateMap();
        }



        private void _GenerateMap()
        {
            float xSize = fieldPrefab.transform.localScale.x;
            float zSize = fieldPrefab.transform.localScale.z;
            int xNum = Mathf.Max(1, mapTiles.GetLength(0) / 2);
            int yNum = Mathf.Max(1, mapTiles.GetLength(1) / 2);
            int xIndex = 0;
            int yIndex = 0;
            
            for (int y = -yNum; y < yNum; y++)
            {
                for (int x = -xNum; x < xNum; x++)
                {
                    Vector3 pos = new Vector3(x * xSize, -fieldPrefab.transform.localScale.y / 2, y * zSize);
                    GameObject tile =  Instantiate(fieldPrefab, pos, Quaternion.identity, _transform);
                    mapTiles[xIndex, yIndex] = tile.GetComponent<GameTile>(); 
                    xIndex++;
                }

                xIndex = 0;
                yIndex++;
            }
            
        }


    }
}
