using System;

namespace ShaderAlmighty.YAML
{
	[Serializable]
	internal class TexturePropertyObject
	{
		public FileObject m_Texture { get; set; }

		public Vector2Object m_Scale { get; set; }
		public Vector2Object m_Offset { get; set; }

	}
}