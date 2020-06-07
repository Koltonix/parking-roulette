//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;
using ParkingRoulette.Boards;

namespace ParkingRoulette.Pathing
{
    public class Vehicle : MonoBehaviour
    {
        [Header("Colour")]
        [SerializeField]
        private Color32 carColour;

        [Header("Pathing Attributes")]
        public List<PathPoint> path = new List<PathPoint>();
        [HideInInspector]
        public Tile previousTile = null;
        [Space]

        [Header("Points")]
        [SerializeField]
        private float originalSpawnHeight = 2.0f;
        [SerializeField]
        private float heightIncrease = 2.0f;
        public GameObject pathIconPrefab;

        [Header("Lines")]
        public Color32 lineColour;
        public GameObject linePrefab;
        [HideInInspector]
        public LineRenderer line;

        private void Start()
        {
            line = Instantiate(linePrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
            line.name = (this.name + " PATH");

            carColour = Random.ColorHSV(0, 1, 1, 1, 1, 1);
            this.GetComponent<Renderer>().material.color = carColour;

            UpdateLine();
            EnablePath(false);
        }

        public void AddPathPoint(Tile tile)
        {
            path.Add(new PathPoint(tile, CreatePoint(tile)));
            previousTile = tile;

            UpdateLine();
        }

        public void RemovePathsUntil(Tile tile)
        {
            for (int i = path.Count - 1; i >= 0; i--)
            {
                if (path[i].tile == tile)
                {
                    previousTile = path[i].tile;
                    break;
                }

                Destroy(path[i].point);
                path.RemoveAt(i);
            }

            if (path.Count == 0)
                previousTile = BoardManager.Instance.WorldToTile(this.transform.position);

            UpdateLine();
        }

        public void RemovePathsUntilUpper(Tile tile)
        {
            for (int i = 0; i < path.Count; i++)
            {
                if (path[i].tile == tile)
                {
                    if (path.Count - 2 > 1)
                        previousTile = path[i - 1].tile;

                    DestroyPath(i);
                    path.RemoveRange(i, path.Count - i);
                    break;
                }
            }

            if (path.Count == 0)
                previousTile = BoardManager.Instance.WorldToTile(this.transform.position);

            UpdateLine();
        }

        private void DestroyPath(int index)
        {
            for (int i = index; i < path.Count; i++)
            {
                Destroy(path[i].point);
            }
        }

        private void UpdateLine()
        {
            List<Vector3> positions = new List<Vector3>();
            positions.Add(this.transform.position);

            foreach (PathPoint point in path)
                positions.Add(point.point.transform.position);

            line.positionCount = positions.Count;
            line.SetPositions(positions.ToArray());
        }

        public void EnablePath(bool isEnabled)
        {
            for (int i = 0; i < path.Count; i++)
                path[i].point.GetComponent<MeshRenderer>().enabled = isEnabled;

            line.enabled = isEnabled;
        }

        private GameObject CreatePoint(Tile tile)
        {
            Vector3 position = tile.transform.position;
            position.y = GetYRelativeToPaths(tile);
            
            GameObject point = Instantiate(pathIconPrefab, position, pathIconPrefab.transform.rotation, line.transform);
            point.GetComponent<Renderer>().material.color = carColour;

            return point;
        }

        private float GetYRelativeToPaths(Tile checkTile)
        {
            float height = originalSpawnHeight;
            for (int i = 0; i < path.Count; i++)
            {
                if (path[i].tile == checkTile)
                    height += heightIncrease;
            }

            return height;
        }
    }
}
