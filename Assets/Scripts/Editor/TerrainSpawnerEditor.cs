using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TerrainSpawnerEditor : EditorWindow
{
    private Terrain terrain;
    private GameObject objectToSpawn;
    private int objsPerAxis = 100;

    [MenuItem("Tools/TerrainSpawner")]
    public static void ShowWindow()
    {
        GetWindow<TerrainSpawnerEditor>();
    }
    
    private void OnGUI()
    {
        objsPerAxis = EditorGUILayout.IntField("Objects per axis", objsPerAxis);
        terrain = (Terrain)EditorGUILayout.ObjectField("Terrain", terrain, typeof(Terrain), true);
        objectToSpawn = (GameObject)EditorGUILayout.ObjectField("Object To Spawn", objectToSpawn, typeof(GameObject), false);

        if (GUILayout.Button("Place"))
            Place();
    }

    private void Place()
    {
        var stepX = terrain.terrainData.size.x / objsPerAxis;
        var stepZ = terrain.terrainData.size.z / objsPerAxis;

        for (var x = 0f; x < terrain.terrainData.size.x; x += stepX)
        for (var z = 0f; z < terrain.terrainData.size.z; z += stepZ)
        {
            var pos = new Vector3(x, 0f, z);
            pos.y = terrain.SampleHeight(pos);

            var normal = terrain.terrainData.GetInterpolatedNormal(x, z);

            Instantiate(objectToSpawn, pos, objectToSpawn.transform.rotation);
        }
    }
}
