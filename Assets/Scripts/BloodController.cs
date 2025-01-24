using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BloodController : MonoBehaviour
{
    public float bloodSpeed; 
    public float waveAmplitude = 0.5f;  
    public float waveFrequency = 1f;   
    

    private float targetY = -2.5f;
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] modifiedVertices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        modifiedVertices = mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        RiseWater();
        AnimateWaves();
    }

    void AnimateWaves()
    {
        float time = Time.time * waveFrequency;
        float topPosition = transform.GetChild(0).position.y;

        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];
            Vector3 vert = transform.TransformPoint(vertex);

            if (topPosition - vert.y < 3.5f)
            {
                vertex.y = Mathf.Sin(time + vertex.x * 2f + vertex.z * 2f) * waveAmplitude;
            }

            modifiedVertices[i] = vertex;
        }

        mesh.vertices = modifiedVertices;

        mesh.RecalculateNormals();

    }

    void RiseWater() {
        if (Mathf.Abs(transform.position.y - targetY) > 0.05f)
        {
            Debug.Log(transform.position.y);
            float newY = Mathf.Lerp(transform.position.y, targetY, bloodSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
