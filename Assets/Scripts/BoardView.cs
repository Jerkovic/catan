﻿using System.Text;
using UnityEngine;
using Catan;
using Managers;
using TMPro;

public class BoardView : MonoBehaviour
{
    [Header("Prefabs")] 
    public GameObject hexPrefab;
    public GameObject chitPrefab;
    public GameObject cornerPlaceholderPrefab;
    public GameObject roadPlaceholderPrefab;
    public GameObject portPrefab;

    [Header("HexTile Materials")] 
    [SerializeField] private Material mountainMaterial;
    [SerializeField] private Material pastureMaterial;
    [SerializeField] private Material hillMaterial;
    [SerializeField] private Material fieldMaterial;
    [SerializeField] private Material forestMaterial;
    [SerializeField] private Material desertMaterial;
    [SerializeField] private Material seaMaterial;
    

    private void Start()
    {
        Debug.Log("Render game board...");
        var game = GameManager.Instance.GetGame();
        var board = game.GetBoard();

        foreach (var tile in board.GetTiles()) InitHexTile(tile); // 19

        foreach (var corner in board.GetCorners()) InitCorner(corner); // 54 

        foreach (var edge in board.GetEdges()) InitEdge(edge); // 72
    }

    private void InitHexTile(HexTile tile)
    {
        var hexGo = Instantiate(hexPrefab, tile.ToWorldCoordinates(), Quaternion.identity);
        hexGo.name = tile.GetHashCode().ToString();
        var meshRenderer = hexGo.GetComponentInChildren<MeshRenderer>();

        if (tile.GetTileType() == TileTypeEnum.MOUNTAIN) meshRenderer.material = mountainMaterial;

        if (tile.GetTileType() == TileTypeEnum.PASTURE) meshRenderer.material = pastureMaterial;

        if (tile.GetTileType() == TileTypeEnum.HILL) meshRenderer.material = hillMaterial;

        if (tile.GetTileType() == TileTypeEnum.FIELD) meshRenderer.material = fieldMaterial;

        if (tile.GetTileType() == TileTypeEnum.FOREST) meshRenderer.material = forestMaterial;

        if (tile.GetTileType() == TileTypeEnum.DESERT) meshRenderer.material = desertMaterial;

        if (tile.GetTileType() == TileTypeEnum.SEA) meshRenderer.material = seaMaterial;

        hexGo.tag = "Hexagon";
        hexGo.transform.SetParent(transform);
        hexGo.isStatic = true;

        // create chit
        if (tile.GetTileType() != TileTypeEnum.SEA && tile.GetTileType() != TileTypeEnum.DESERT)
        {
            var chit = Instantiate(chitPrefab, hexGo.transform, false);
            var textComponent = chit.GetComponentInChildren<TMP_Text>();
            var sb = new StringBuilder();
            sb.AppendLine(tile.GetChit().ToString());
            sb.AppendLine(tile.GetOddsDots());
            textComponent.text = sb.ToString();
            if (tile.IsRedChit()) textComponent.color = Color.red;
        }
        
        // Harbors
        var board = GameManager.Instance.GetGame().GetBoard();
        
        if (tile.GetTileType() == TileTypeEnum.SEA && board.HasTilePort(tile))
        {
            var chit = Instantiate(portPrefab, hexGo.transform, false);
            var textComponent = chit.GetComponentInChildren<TMP_Text>();
            var sb = new StringBuilder();
            sb.AppendLine("?");
            textComponent.text = sb.ToString();
        }
    }

    private void InitEdge(Edge edge)
    {
        var corner1 = edge.GetLeftCorner();
        var corner2 = edge.GetRightCorner();
        var pos1 = GetCornerWorldCoordinates(corner1);
        var pos2 = GetCornerWorldCoordinates(corner2);
        var rotation = Quaternion.LookRotation(pos1 - pos2, Vector3.forward);
        var centerX = (pos1.x + pos2.x) / 2;
        var centerZ = (pos1.z + pos2.z) / 2;
        var pos = new Vector3(centerX, .07f, centerZ);
        var edgeObj = Instantiate(roadPlaceholderPrefab, pos, rotation);
        edgeObj.name = edge.GetHashCode().ToString();
        edgeObj.tag = "Edge";
        edgeObj.transform.SetParent(transform);
        edgeObj.isStatic = true;
    }

    private Vector3 GetHexTileWordCoordinates(int hashCode)
    {
        var tile = GameManager.Instance.GetGame().GetBoard().GetTileByHashCode(hashCode);
        return tile.ToWorldCoordinates();
    }

    private Vector3 GetCornerWorldCoordinates(Corner corner)
    {
        var t1 = GetHexTileWordCoordinates(corner.hex1);
        var t2 = GetHexTileWordCoordinates(corner.hex2);
        var t3 = GetHexTileWordCoordinates(corner.hex3);
        var centerX = (t1.x + t2.x + t3.x) / 3;
        var centerZ = (t1.z + t2.z + t3.z) / 3;
        return new Vector3(centerX, 0, centerZ);
    }

    private void InitCorner(Corner corner)
    {
        var cornerObj = Instantiate(cornerPlaceholderPrefab, GetCornerWorldCoordinates(corner), Quaternion.identity);
        cornerObj.name = corner.GetHashCode().ToString();
        cornerObj.tag = "Corner";
        cornerObj.transform.SetParent(transform);
        cornerObj.isStatic = true;

        if (corner.IsPort())
        {
            var meshRenderer = cornerObj.GetComponentInChildren<MeshRenderer>();
            meshRenderer.enabled = true;
        }
    }
}