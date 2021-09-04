using System;
using UnityEngine;

namespace ShaderAlmighty.YAML
{
	[Serializable]
	public  class Vector2Object
	{
		public long x { get; set; }
		public long y { get; set; }

		public Vector2 ToVector2()
		{
			return new Vector2(x, y);
		}
	}
}