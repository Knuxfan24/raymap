﻿using OpenSpace.Loader;
using System;
using System.Linq;
using UnityEngine;

namespace OpenSpace.ROM {
	public class GeometricElementTriangles : ROMStruct {
		public Reference<VisualMaterial> visualMaterial;
		public Reference<GeometricElementTrianglesData> triangles;
		public Reference<VertexArray> vertices;
		public ushort sz_triangles;
		public ushort num_vertices;
		public ushort unk0;
		public ushort sz_vertices;
		public ushort unk2;

		protected override void ReadInternal(Reader reader) {
			visualMaterial = new Reference<VisualMaterial>(reader, true);
			if (Settings.s.platform == Settings.Platform.N64) {
				MapLoader.Loader.print("Triangles: " + Pointer.Current(reader));
				triangles = new Reference<GeometricElementTrianglesData>(reader);
				vertices = new Reference<VertexArray>(reader);
				sz_triangles = reader.ReadUInt16();
				sz_vertices = reader.ReadUInt16();
				unk2 = reader.ReadUInt16();

				vertices.Resolve(reader, v => v.length = sz_vertices);
			} else if (Settings.s.platform == Settings.Platform.DS) {
				triangles = new Reference<GeometricElementTrianglesData>(reader);
				sz_triangles = reader.ReadUInt16();
			} else if (Settings.s.platform == Settings.Platform._3DS) {
				triangles = new Reference<GeometricElementTrianglesData>(reader);
				sz_triangles = reader.ReadUInt16();
				num_vertices = reader.ReadUInt16();
			}
			triangles.Resolve(reader, (t) => { t.length = sz_triangles; t.num_vertices = num_vertices; });

		}

		public GameObject GetGameObject(GeometricObject.Type type, GeometricObject go) {
			GameObject gao = new GameObject("Element @ " + Offset);
			bool backfaceCulling = !visualMaterial.Value.RenderBackFaces;
			gao.transform.localPosition = Vector3.zero;
			MeshRenderer mr = gao.AddComponent<MeshRenderer>();
			MeshFilter mf = gao.AddComponent<MeshFilter>();
			Mesh mesh = new Mesh();
			if (Settings.s.platform == Settings.Platform._3DS) {
				if (num_vertices == 0) {
					mesh.vertices = go.verticesVisual.Value.GetVectors(go.ScaleFactor);
					mesh.normals = go.normals.Value.GetVectors(Int16.MaxValue);
				} else {
					// Use vertices located in element
					mesh.vertices = triangles.Value.verts.Select(v => v.GetVector(go.ScaleFactor)).ToArray();
					mesh.normals = triangles.Value.normals.Select(v => v.GetVector(Int16.MaxValue)).ToArray();
				}
				mesh.SetUVs(0, triangles.Value.uvs.Select(u => new Vector3(u.x, u.y, 1f)).ToList());
				mesh.triangles = triangles.Value.triangles.SelectMany(t => backfaceCulling ? new int[] { t.v2, t.v1, t.v3 } : new int[] { t.v2, t.v1, t.v3, t.v1, t.v2, t.v3 }).ToArray();
			} else if (Settings.s.platform == Settings.Platform.N64) {
				mesh = RSP.RSPParser.Parse(triangles.Value.rspCommands, vertices.Value.vertices, go, backfaceCulling);
			}
			mf.mesh = mesh;
			mr.material = visualMaterial.Value.GetMaterial();
			if (Settings.s.platform == Settings.Platform.N64) {
				// Apply vertex colors
				mr.sharedMaterial.SetVector("_Tex2Params", new Vector4(60, 0, 0, 0));
			}
			return gao;
		}
	}
}
