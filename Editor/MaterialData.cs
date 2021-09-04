using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace ShaderAlmighty
{
    public class MaterialData
    {
        
    }

    public static class MenuItems
    {
        private const string MENU = "Shader/";
        [MenuItem(MENU + "Test1")]
        private static void TestFunction1()
        {
            Object obj = Selection.activeObject;
            if (obj is Material material)
            {
                string[] properties = material.GetTexturePropertyNames();
                foreach (string property in properties)
                {
                    // Debug.Log(property);
                }

                Shader shader = material.shader;
                int count = shader.GetPropertyCount();

                for (int i = 0; i < count; i++)
                {
                    ShaderPropertyType type = shader.GetPropertyType(i);
                    string[] attributes = shader.GetPropertyAttributes(i);

                    foreach (string attribute in attributes)
                    {
                        Debug.Log($"{i} : {attribute}");
                    }
                }
            } 
        }
    }
}