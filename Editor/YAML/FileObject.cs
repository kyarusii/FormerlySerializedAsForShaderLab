using System;

namespace ShaderAlmighty.YAML
{
	[Serializable]
	public  class FileObject
	{
		public long fileID { get; set; }
		public string guid { get; set; }
		public long type { get; set; }
	}
}