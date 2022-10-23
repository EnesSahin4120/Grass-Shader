using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FloorMesh : MonoBehaviour
{
    [SerializeField] private float _size = 1;
    [SerializeField] private int _frameCount = 16;

    private List<Vector3> _vertices = new List<Vector3>();
    private List<Vector3> _normals = new List<Vector3>();
    private List<Vector2> _uvs = new List<Vector2>();
    private List<int> _triangles = new List<int>(); 

    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
    }

    void Start()
    {
        CreateMesh();
        UpdateMesh();
    }

    private void CreateMesh()
    {
        float frame = _size / (float)_frameCount;
        for (int i = 0; i < _frameCount + 1; ++i)
        {
            for (int j = 0; j < _frameCount + 1; ++j)
            {
                _vertices.Add(new Vector3(-_size * 0.5f + (i * frame), 0, -_size * 0.5f + (j * frame)));
                _normals.Add(Vector3.up);
                _uvs.Add(new Vector2(i / (float)_frameCount, j / (float)_frameCount));
            }
        }

        //var triangles = new List<int>();
        int vertexCount = _frameCount + 1;
        for (int i = 0; i < vertexCount * vertexCount - vertexCount; ++i)
        {
            if ((i + 1) % vertexCount == 0)
                continue;

            _triangles.Add(i + 1 + vertexCount);
            _triangles.Add(i + vertexCount);
            _triangles.Add(i);
            _triangles.Add(i);
            _triangles.Add(i + 1);
            _triangles.Add(i + 1 + vertexCount);

        }
    }

    private void UpdateMesh()
    {
        _meshFilter.mesh.SetVertices(_vertices);
        _meshFilter.mesh.SetNormals(_normals);
        _meshFilter.mesh.SetUVs(0, _uvs);
        _meshFilter.mesh.SetTriangles(_triangles, 0);
        _meshCollider.sharedMesh = _meshFilter.mesh;
    }
}