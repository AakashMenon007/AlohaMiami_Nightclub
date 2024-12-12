using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class CurvedScreen : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float curveStrength = 0.0f; // Adjust this value to curve the screen
    public Slider curveSlider; // Assign a slider for dynamic control (optional)

    private RawImage rawImage;
    private RectTransform rectTransform;
    private Mesh mesh;
    private Vector3[] originalVertices;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();

        // Generate a mesh for the Raw Image
        CreateMesh();
    }

    void Update()
    {
        // Update the curve based on slider value, if a slider is assigned
        if (curveSlider != null)
        {
            curveStrength = curveSlider.value;
        }

        ApplyCurve();
    }

    void CreateMesh()
    {
        // Create a quad mesh for the Raw Image
        mesh = new Mesh();

        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        // Define the vertices of a quad
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-width / 2, -height / 2, 0), // Bottom-left
            new Vector3(width / 2, -height / 2, 0),  // Bottom-right
            new Vector3(-width / 2, height / 2, 0),  // Top-left
            new Vector3(width / 2, height / 2, 0),   // Top-right
        };

        originalVertices = vertices;

        // Define the triangles
        int[] triangles = new int[]
        {
            0, 2, 1, // Bottom-left triangle
            2, 3, 1  // Top-right triangle
        };

        // Define UVs for the texture
        Vector2[] uvs = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };

        // Assign to mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        // Assign the mesh to the Raw Image
        rawImage.canvasRenderer.SetMesh(mesh);
    }

    void ApplyCurve()
    {
        if (mesh == null || originalVertices == null) return;

        Vector3[] vertices = new Vector3[originalVertices.Length];
        originalVertices.CopyTo(vertices, 0);

        float curveAmount = curveStrength * rectTransform.rect.width;

        // Apply the curve effect by modifying vertex positions
        for (int i = 0; i < vertices.Length; i++)
        {
            float x = vertices[i].x;
            float z = Mathf.Pow(x / rectTransform.rect.width, 2) * curveAmount;
            vertices[i].z = z;
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        rawImage.canvasRenderer.SetMesh(mesh);
    }
}
