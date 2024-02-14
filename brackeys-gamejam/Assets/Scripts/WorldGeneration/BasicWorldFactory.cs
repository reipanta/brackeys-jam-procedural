using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Range = System.Range;

namespace WorldGeneration
{
    public class BasicWorldFactory
    {
        // Creating non-initialized variables of a needed type
        // We need Vector3 and int arrays to store vertices and triangles of the meshes
        internal Mesh Mesh;
        internal Vector3[] Vertices;
        internal int[] Triangles;

        // Here goes width and height of the terrain
        // (remember that Unity uses y to measure altitude
        // while Unreal uses z for the same purposes)
        internal int xSize;
        internal int zSize;

        // This function generates the terrain mesh
        internal void CreateShape()
        {
            // Initializing Vertices to the basic x,y size
            // and adding 1 (since triangle has two dots on each side)
            Vertices = new Vector3[(xSize + 1) * (zSize + 1)];

            for (int i = 0, z = 0; z <= zSize; z++) // Double for loop to cover the whole square area - rows and columns
            {
                for (int x = 0; x <= xSize; x++)
                {
                    // We are getting random values in a small range
                    // to make terrain look more random each generation cycle.
                    float xRand = Random.Range(0.1f, 0.25f);
                    float zRand = Random.Range(0.1f, 0.25f);

                    // Plus, we're multiplying the random values
                    // by Perlin Noise values to create even more randomness
                    float y = Mathf.PerlinNoise(x * xRand, z * zRand) * 1.5f;

                    // This initializes the current i-Vertex point with x, y, z positions in space.
                    // Then we add i + i to our manual counter and go into next cycle of the loop
                    Vertices[i] = new Vector3(x, y, z);
                    i++;
                }
            }

            // 6 is a magic number meaning a triangle consists of 3 lines,
            // and a quad mesh out of two triangles (3 * 2 = 6)
            Triangles = new int[xSize * zSize * 6];

            // These are counters for vertices and for triangles
            int vert = 0;
            int tris = 0;

            for (int z = 0; z < zSize; z++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    // This is for generating ONE plane mesh
                    Triangles[tris + 0] = vert + 0;
                    Triangles[tris + 1] = vert + xSize + 1;
                    Triangles[tris + 2] = vert + 1;
                    Triangles[tris + 3] = vert + 1;
                    Triangles[tris + 4] = vert + xSize + 1;
                    Triangles[tris + 5] = vert + xSize + 2;

                    // We increase the counter...
                    vert++;
                    // ...and shift to the side to generate the next 6-sided quad mesh
                    tris += 6;
                }

                // Shifting the column after the row has been filled
                vert++;
            }
        }

        // This function ties the mesh available as component on WorldBuilderController on the scene
        internal void UpdateMesh()
        {
            Mesh.Clear();
            Mesh.vertices = Vertices;
            Mesh.triangles = Triangles;

            // This function call is needed to make adequate shading
            Mesh.RecalculateNormals();
        }
        
        // The constructor is needed for a better availability in singleton
        public BasicWorldFactory(Mesh mesh, int xSize, int zSize)
        {
            Mesh = mesh;
            this.xSize = xSize;
            this.zSize = zSize;
        }
    }
}