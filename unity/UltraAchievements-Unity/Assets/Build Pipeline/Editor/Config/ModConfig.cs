using System;
using UnityEngine;

namespace BuildPipeline.Editor.Config
{
	[Serializable]
	public class ModConfig
	{
		public string Name;
		public string AssetPathLocation;
		public string MonoscriptBundleNaming;
		public string CatalogPostfix;
		public string BuildPath;
		public string[] GroupNames;
		public bool DoCopy;
		public string CopyPath;

		public ModConfig()
		{
			Name = "New Mod";
			AssetPathLocation = "";
			MonoscriptBundleNaming = "";
			CatalogPostfix = "";
			BuildPath = "Built Bundles/ModName";
			GroupNames = Array.Empty<string>();
			DoCopy = false;
			CopyPath = "";
		}
	}
}
