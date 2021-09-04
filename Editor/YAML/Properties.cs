using System;
using System.Collections.Generic;

namespace ShaderAlmighty.YAML
{
	[Serializable]
	internal class Properties
	{
		public int serializedVersion;
		public Dictionary<string, TexturePropertyObject>[] m_TexEnvs { get; set; }
		public Dictionary<string, int>[] m_Ints { get; set; }
		public Dictionary<string, double>[] m_Floats { get; set; }
		public Dictionary<string, ColorObject>[] m_Colors { get; set; }
	}
}