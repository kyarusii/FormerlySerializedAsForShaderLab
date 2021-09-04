using System;

namespace ShaderAlmighty.YAML
{
	[Serializable]
	internal class MaterialObject
	{
		public long serializedVersion;
		public long m_ObjectHideFlags;
		public FileObject m_CorrespondingSourceObject { get; set; }
		public FileObject m_PrefabInstance { get; set; }
		public FileObject m_PrefabAsset { get; set; }
		public string m_Name { get; set; }
		public ShaderObject m_Shader { get; set; }
		public object m_ShaderKeywords { get; set; }
		public long m_LightmapFlags { get; set; }
		public long m_EnableInstancingVariants { get; set; }
		public long m_DoubleSidedGI { get; set; }
		public long m_CustomRenderQueue { get; set; }
		public StringTagMap stringTagMap { get; set; }
		public object[] disabledShaderPasses { get; set; }
		public object[] m_BuildTextureStacks { get; set; }

		public Properties m_SavedProperties;
	}
}