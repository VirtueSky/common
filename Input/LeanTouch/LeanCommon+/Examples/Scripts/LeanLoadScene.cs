using UnityEngine;
using UnityEngine.SceneManagement;
using Virtuesky.CW.Common;

namespace Virtuesky.Lean.Common
{
	/// <summary>This component allows you to load the specified scene when you manually call the <b>Load</b> method.</summary>
	[HelpURL(LeanCommon.PlusHelpUrlPrefix + "LeanLoadScene")]
	[AddComponentMenu(LeanCommon.ComponentPathPrefix + "Load Scene")]
	public class LeanLoadScene : MonoBehaviour
	{
		/// <summary>The name of the scene you want to load.</summary>
		public string SceneName { set { sceneName = value; } get { return sceneName; } } [SerializeField] private string sceneName;

		/// <summary>Load the scene asynchronously?</summary>
		public bool ASync { set { aSync = value; } get { return aSync; } } [SerializeField] private bool aSync;

		/// <summary>Keep the existing scene(s) loaded?</summary>
		public bool Additive { set { additive = value; } get { return additive; } } [SerializeField] private bool additive;
		
		[ContextMenu("Load")]
		public void Load()
		{
			Load(sceneName);
		}

		public void Load(string sceneName)
		{
			if (aSync == true)
			{
				SceneManager.LoadSceneAsync(sceneName, additive == true ? LoadSceneMode.Additive : LoadSceneMode.Single);
			}
			else
			{
				SceneManager.LoadScene(sceneName, additive == true ? LoadSceneMode.Additive : LoadSceneMode.Single);
			}
		}
	}
}

#if UNITY_EDITOR
namespace Virtuesky.Lean.Common.Editor
{
	using UnityEditor;
	using TARGET = LeanLoadScene;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(TARGET))]
	public class LeanLoadScene_Editor : CwEditor
	{
		[System.NonSerialized] TARGET tgt; [System.NonSerialized] TARGET[] tgts;

		protected override void OnInspector()
		{
			GetTargets(out tgt, out tgts);

			DrawSceneName();
			Draw("aSync", "Load the scene asynchronously?");
			Draw("additive", "Keep the existing scene(s) loaded?");
		}

		private void DrawSceneName()
		{
			EditorGUILayout.BeginHorizontal();
				Draw("sceneName", "The name of the scene you want to load.");
				if (GUILayout.Button("List", GUILayout.Width(40)) == true)
				{
					var menu = new GenericMenu();

					foreach (var scene in EditorBuildSettings.scenes)
					{
						var sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);

						menu.AddItem(new GUIContent(sceneName), tgt.SceneName == sceneName, () => { serializedObject.FindProperty("sceneName").stringValue = sceneName; serializedObject.ApplyModifiedProperties(); });
					}

					menu.ShowAsContext();
				}
			EditorGUILayout.EndHorizontal();
		}
	}
}
#endif