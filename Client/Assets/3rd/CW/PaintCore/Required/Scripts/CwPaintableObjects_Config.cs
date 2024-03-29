﻿#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using CW.Common;

namespace PaintCore
{
	public partial class CwPaintableObjects
	{
		public enum ExportTextureFormat
		{
			PNG,
			TGA
		}

		[System.Serializable]
		public class SettingsData
		{
			public int UndoRedoStates = 10;

			public ExportTextureFormat DefaultTextureFormat;
		}

		public static SettingsData Settings = new SettingsData();

		private Vector2 configScrollPosition;

		private static void ClearSettings()
		{
			if (EditorPrefs.HasKey("PaintIn3D.Settings") == true)
			{
				EditorPrefs.DeleteKey("PaintIn3D.Settings");

				Settings = new SettingsData();
			}
		}

		private static void SaveSettings()
		{
			EditorPrefs.SetString("PaintIn3D.Settings", EditorJsonUtility.ToJson(Settings));
		}

		private static void LoadSettings()
		{
			if (EditorPrefs.HasKey("PaintIn3D.Settings") == true)
			{
				var json = EditorPrefs.GetString("PaintIn3D.Settings");

				if (string.IsNullOrEmpty(json) == false)
				{
					EditorJsonUtility.FromJsonOverwrite(json, Settings);
				}
			}
		}

		private void DrawConfigTab()
		{
			configScrollPosition = GUILayout.BeginScrollView(configScrollPosition, GUILayout.ExpandHeight(true));
				CwEditor.BeginLabelWidth(110);
					EditorGUILayout.BeginHorizontal();
						Settings.UndoRedoStates = EditorGUILayout.IntField("Undo/Redo States", Settings.UndoRedoStates);
						if (GUILayout.Button(new GUIContent("Apply", "Apply this undo/redo state limit to all CwPaintableTexture components in the scene?"), EditorStyles.miniButton, GUILayout.ExpandWidth(false)) == true)
						{
							if (EditorUtility.DisplayDialog("Are you sure?", "This will apply this StateLimit to all CwPaintableTexture components in the scene.", "OK") == true)
							{
								ApplyUndoRedoStates();
							}
						}
					EditorGUILayout.EndHorizontal();
					Settings.DefaultTextureFormat = (ExportTextureFormat)EditorGUILayout.EnumPopup("Default Format", Settings.DefaultTextureFormat);

					GUILayout.FlexibleSpace();

					if (GUILayout.Button("Clear Settings") == true)
					{
						if (EditorUtility.DisplayDialog("Are you sure?", "This will reset all editor painting settings to default.", "OK") == true)
						{
							ClearSettings();
						}
					}
				CwEditor.EndLabelWidth();
			GUILayout.EndScrollView();
		}

		private void ApplyUndoRedoStates()
		{
			var paintableTextures = CwHelper.FindObjectsByType<CwPaintableTexture>();

			Undo.RecordObjects(paintableTextures, "Apply Undo/Redo States");

			foreach (var paintableTexture in paintableTextures)
			{
				if (paintableTexture.UndoRedo != CwPaintableTexture.UndoRedoType.LocalCommandCopy)
				{
					paintableTexture.UndoRedo   = CwPaintableTexture.UndoRedoType.FullTextureCopy;
					paintableTexture.StateLimit = Settings.UndoRedoStates;

					EditorUtility.SetDirty(paintableTexture);
				}
			}
		}
	}
}
#endif