using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace ShaderAlmighty
{
	using YAML;

	public class MaterialPostprocessor : AssetPostprocessor
	{
		private const string ATTRIBUTE = "FormerlySerializedAs(";

		private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
			string[] movedAssets,
			string[] movedFromAssetPaths)
		{
			foreach (string assetPath in importedAssets)
			{
				if (assetPath.Contains(".mat"))
				{
					Material material = AssetDatabase.LoadAssetAtPath<Material>(assetPath);
					Shader shader = material.shader;

					if (shader == null)
					{
						return;
					}

					int count = shader.GetPropertyCount();

					for (int i = 0; i < count; i++)
					{
						string[] attributes = shader.GetPropertyAttributes(i);
						ShaderPropertyType propertyType = shader.GetPropertyType(i);
						string propertyname = shader.GetPropertyName(i);

						foreach (string attribute in attributes)
						{
							if (attribute.Contains(ATTRIBUTE))
							{
								string formerPropertyName = attribute.Replace(ATTRIBUTE, "")
									.Replace(")", "");

								Reserialize(formerPropertyName, propertyname, propertyType);
							}
						}
					}

					void Reserialize(string formerPropertyName, string propertyname, ShaderPropertyType propertyType)
					{
						MaterialObject mObj = material.BuildObject();

						SerializedObject serializedObject = new(material);
						SerializedProperty property = null;

						bool hasProperty = false;
						bool processed = false;

						switch (propertyType)
						{
							case ShaderPropertyType.Color:
								hasProperty = mObj.GetHiddneColor(formerPropertyName, out Color hiddneColorValue);

								if (hasProperty)
								{
									RemoveOutdatedProperty("m_SavedProperties.m_Colors");

									material.SetColor(propertyname, hiddneColorValue);
									processed = true;
								}

								break;
							case ShaderPropertyType.Vector:
								hasProperty = mObj.GetHiddenVector(formerPropertyName, out Vector4 hiddenVectorValue);

								if (hasProperty)
								{
									RemoveOutdatedProperty("m_SavedProperties.m_Colors");

									material.SetVector(propertyname, hiddenVectorValue);
									processed = true;
								}

								break;
							case ShaderPropertyType.Float:
							{
								hasProperty = mObj.GetHiddenFloat(formerPropertyName, out float hiddenFloatValue);

								if (hasProperty)
								{
									RemoveOutdatedProperty("m_SavedProperties.m_Floats");

									material.SetFloat(propertyname, hiddenFloatValue);
									processed = true;
								}

								break;
							}
							case ShaderPropertyType.Texture:
								hasProperty = mObj.GetHiddenTextureAsset(formerPropertyName,
									out TexturePropertyObject hiddenTexObjValue);
								if (hasProperty)
								{
									RemoveOutdatedProperty("m_SavedProperties.m_TexEnvs");

									string path = AssetDatabase.GUIDToAssetPath(hiddenTexObjValue.m_Texture.guid);

									material.SetTexture(propertyname, AssetDatabase.LoadAssetAtPath<Texture>(path));
									material.SetTextureOffset(propertyname, hiddenTexObjValue.m_Offset.ToVector2());
									material.SetTextureScale(propertyname, hiddenTexObjValue.m_Scale.ToVector2());

									processed = true;
								}

								break;
#if UNITY_2021_1_OR_NEWER
							case ShaderPropertyType.Int:
								hasProperty = mObj.GetHiddenInt(formerPropertyName, out int hiddenIntValue);

								if (hasProperty)
								{
									RemoveOutdatedProperty("m_SavedProperties.m_Ints");

									material.SetInt(propertyname, hiddenIntValue);
									processed = true;
								}

								break;
#endif
							default:
								throw new ArgumentOutOfRangeException(nameof(propertyType), propertyType, null);
						}

						if (processed)
						{
							Debug.Log($"Reserialize material properties... [{formerPropertyName}]->[{propertyname}]",
								material);

							EditorUtility.SetDirty(material);
							AssetDatabase.SaveAssets();
						}

						void RemoveOutdatedProperty(string propertyPath)
						{
							property = serializedObject.FindProperty(propertyPath);

							int arraySize = property.arraySize;
							for (int i = 0; i < arraySize; i++)
							{
								if (property.GetArrayElementAtIndex(i).displayName == formerPropertyName)
								{
									property.DeleteArrayElementAtIndex(i);
									serializedObject.ApplyModifiedProperties();
									break;
								}
							}
						}
					}
				}
			}
		}
	}
}