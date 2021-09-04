using System.IO;
using System.Linq;
using System.Text;
using ShaderAlmighty.YAML;
using UnityEditor;
using UnityEngine;
using YamlDotNet.Serialization;

namespace ShaderAlmighty
{
	public static class MenuItems
	{
		private const string MENU = "Shader/";
		[MenuItem(MENU + "Test1")]
		private static void TestFunction1()
		{
			Object obj = Selection.activeObject;
			if (obj is Material material)
			{
				// string[] properties = material.GetTexturePropertyNames();
				// foreach (string property in properties)
				// {
				//     // Debug.Log(property);
				// }
				//
				// Shader shader = material.shader;
				// int count = shader.GetPropertyCount();
				//
				// for (int i = 0; i < count; i++)
				// {
				//     ShaderPropertyType type = shader.GetPropertyType(i);
				//     string[] attributes = shader.GetPropertyAttributes(i);
				//
				//     foreach (string attribute in attributes)
				//     {
				//         Debug.Log($"{i} : {attribute}");
				//     }
				// }
                
				// var path = AssetDatabase.GetAssetPath(material);

			} 
            
			var path = "Assets/Content/C_Materials/Green.mat";
			var serializer = new DeserializerBuilder()
				.IgnoreUnmatchedProperties()
				.Build();

			var sb = new StringBuilder();
			var lines = File.ReadAllLines(path);
			for (int i = 0; i < lines.Length; i++)
			{
				if (i < 3) continue;
				if (lines[i].Contains("---")) break;

				sb.AppendLine(lines[i]);
			}
            
			var deserialized = serializer.Deserialize<MaterialRoot>(sb.ToString());
            
			foreach (var key in deserialized.Material.m_SavedProperties.m_TexEnvs)
			{
				Debug.Log(key.Keys.FirstOrDefault());
			}
		}
	}
}