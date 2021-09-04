using System.Collections.Generic;
using System.IO;
using System.Text;
using ShaderAlmighty.YAML;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TextCore.LowLevel;
using YamlDotNet.Serialization;

namespace ShaderAlmighty
{
	internal static class ShaderFileBackend
	{
		internal static bool GetHiddenFloat(this MaterialObject material, string propertyName, out float floatValue)
		{
			var floats = material.m_SavedProperties.m_Floats;
			
			foreach (Dictionary<string,double> dictionary in floats)
			{
				foreach (KeyValuePair<string,double> pair in dictionary)
				{
					if (pair.Key == propertyName)
					{
						floatValue = (float)pair.Value;
						return true;
					}
				}
			}

			floatValue = 0f;
			return false;
		}
		
		internal static bool GetHiddenInt(this MaterialObject material, string propertyName, out int intValue)
		{
			var ints = material.m_SavedProperties.m_Ints;
			
			foreach (var dictionary in ints)
			{
				foreach (var pair in dictionary)
				{
					if (pair.Key == propertyName)
					{
						intValue = (int)pair.Value;
						return true;
					}
				}
			}

			intValue = 0;
			return false;
		}

		internal static bool GetHiddneColor(this MaterialObject material, string propertyName, out Color color)
		{
			var colors = material.m_SavedProperties.m_Colors;

			foreach (var dictionary in colors)
			{
				foreach (var pair in dictionary)
				{
					if (pair.Key == propertyName)
					{
						ColorObject v = pair.Value;
						color = new Color((float)v.r, (float)v.g, (float)v.b, (float)v.a);
						return true;
					}
				}

			}

			color = Color.black;
			return false;
		}

		internal static bool GetHiddenVector(this MaterialObject material, string propertyName, out Vector4 vector)
		{
			var vectors = material.m_SavedProperties.m_Colors;
			
			foreach (var dictionary in vectors)
			{
				foreach (var pair in dictionary)
				{
					if (pair.Key == propertyName)
					{
						ColorObject v = pair.Value;
						vector = new Vector4((float)v.r, (float)v.g, (float)v.b, (float)v.a);
						return true;
					}
				}

			}

			vector = Color.black;
			return false;
		}

		internal static bool GetHiddenTextureAsset(this MaterialObject material, string propertyName, out TexturePropertyObject texObject)
		{
			var textureFiles = material.m_SavedProperties.m_TexEnvs;
			
			foreach (var dictionary in textureFiles)
			{
				foreach (var pair in dictionary)
				{
					if (pair.Key == propertyName)
					{
						texObject = pair.Value;
						return true;
					}
				}

			}

			texObject = null;
			return false;
		}

		internal static MaterialObject BuildObject(this Material material)
		{
			Assert.IsNotNull(material);
			
			string path = AssetDatabase.GetAssetPath(material);
			IDeserializer serializer = new DeserializerBuilder()
				.IgnoreUnmatchedProperties()
				.Build();

			bool begin = false;
			
			StringBuilder sb = new StringBuilder();
			
			string[] lines = File.ReadAllLines(path);
			foreach (string t in lines)
			{
				if (t.Contains("Material:"))
				{
					begin = true;
				}

				if (t.Contains("---"))
				{
					begin = false;
				}

				if (!begin) continue;
				sb.AppendLine(t);
			}

			MaterialRoot deserialized = serializer.Deserialize<MaterialRoot>(sb.ToString());

			return deserialized.Material;
		}
	}
}