using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Deckbuilder
{
    public class MapGenerator : MonoBehaviour
    {
        // TODO make into SO
        public static GameTile[,] MapTiles;
        
        [SerializeField] private GameObject fieldPrefab;
        [SerializeField] private Vector2Int widthHeight;
        private Transform _transform;
        
        [SerializeField] private InteractableDictVariable allInteractables;
        [SerializeField] private InteractableDictVariable enemyInteractables;

        [SerializeField] private GameObjectVariable currentInteractable;

        [SerializeField] private GameObject golemPrefab;
        
        private void Awake()
        {
            int x = Mathf.Max(1, widthHeight.x % 2 == 0 ? widthHeight.x : widthHeight.x - 1);
            int y = Mathf.Max(1, widthHeight.y % 2 == 0 ? widthHeight.y : widthHeight.y - 1);
            MapTiles = new GameTile[x, y];

            _transform = transform;
            
        }


        public void GenerateMap()
        {
            _GenerateMap();
            
            // Put a golem on the field

            int x = Random.Range(0, widthHeight.x);
            int y = Random.Range(0, widthHeight.y);
            GameTile tile = MapTiles[x, y];

            GameObject go = Instantiate(golemPrefab, tile.SpawnPoint.position, Quaternion.Euler(0, 180, 0));
            go.transform.localScale = Vector3.one;
            IInteractable golem = go.GetComponent<IInteractable>();
            
            allInteractables.Add(go.GetInstanceID(), golem);
            enemyInteractables.Add(go.GetInstanceID(), golem);


        }

        public static int[] WorldPosToGrid(Vector3 pos)
        {
            // Vector3 localScale = fieldPrefab.transform.localScale;
            float xSize = 1;
            float zSize = 1;
            int xNum = Mathf.Max(1, MapTiles.GetLength(0) / 2);
            int zNum = Mathf.Max(1, MapTiles.GetLength(1) / 2);
            int xLoc =  (int) Math.Floor(pos.x / xSize) + xNum;
            int zLoc =  (int) Math.Floor(pos.z / zSize) + zNum;
            return new int[2] { xLoc, zLoc };
            
        }

        public static int ManhattanDistance(Vector2Int A, Vector2Int B)
        {
            return Mathf.Abs(A.x - B.x) + Mathf.Abs(A.y - B.y);
        }
        
        
        public void HighlightTileWithInteractable()
        {
        
            UnhighlightTiles();

            if (currentInteractable.Value)
            {
                int[] indices = WorldPosToGrid(currentInteractable.Value.transform.position);
                Vector2Int pos = new Vector2Int(indices[0], indices[1]);

                IInteractable interactable = allInteractables.Get(currentInteractable.Value.GetInstanceID());
                HighlightWithManhattanDistance(pos, (int) interactable.RemainingMovement);

            }

        }
        
        public void HighlightWithManhattanDistance(Vector2Int pos, int distance) {
            for (int x = 0; x < MapTiles.GetLength(0); x++)
            {
                for (int y = 0; y < MapTiles.GetLength(1); y++)
                {
                    int manDist = ManhattanDistance(pos, new Vector2Int(x, y));
                    if (manDist <= distance && manDist > 0)
                    {
                        MapTiles[x, y].Highlight();
                    }
                }
            }
        }

        public void UnhighlightTiles()
        {
            foreach (GameTile tile in MapTiles)
            {
                tile.Unhighlight();
            }
        }

        
        private void _GenerateMap()
        {
            float xSize = fieldPrefab.transform.localScale.x;
            float zSize = fieldPrefab.transform.localScale.z;
            int xNum = Mathf.Max(1, MapTiles.GetLength(0) / 2);
            int yNum = Mathf.Max(1, MapTiles.GetLength(1) / 2);
            int xIndex = 0;
            int yIndex = 0;
            
            for (int y = -yNum; y < yNum; y++)
            {
                for (int x = -xNum; x < xNum; x++)
                {
                    Vector3 pos = new Vector3(x * xSize, -fieldPrefab.transform.localScale.y / 2, y * zSize);
                    GameObject tile =  Instantiate(fieldPrefab, pos, Quaternion.identity, _transform);
                    MapTiles[xIndex, yIndex] = tile.GetComponent<GameTile>(); 
                    xIndex++;
                }

                xIndex = 0;
                yIndex++;
            }
            
        }


    }
}
