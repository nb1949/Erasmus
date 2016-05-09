using System;

namespace AssemblyCSharp
{
	public static class Utils
	{

		// Remap float from range1 to range 2, used: 2.Remap(1, 3, 0, 10)
		// From http://forum.unity3d.com/threads/re-map-a-number-from-one-range-to-another.119437/
			public static float Remap (this float value, float from1, float to1, float from2, float to2) {
				return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
			}
	}
}

