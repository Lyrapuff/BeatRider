using System.Linq;
using Game.ObjectManagement.Spawning.Scriptable.Objects;
using UnityEditor;
using UnityEngine;

namespace ObjectManagement.Spawning.Scriptable
{
    [CustomEditor(typeof(RandomSpawnModule))]
    public class RandomSpawnModuleEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            RandomSpawnModule module = (RandomSpawnModule) target;

            if (module == null)
            {
                return;
            }

            float totalFill = module.Options.Sum(x => x.Fill);

            if (!Mathf.Approximately(totalFill, 100f))
            {
                EditorGUILayout.HelpBox("Fills sum must be equal to 100, not " + totalFill, MessageType.Warning);

                if (GUILayout.Button("Arrange automatically"))
                {
                    if (totalFill > 0f)
                    {
                        for (int i = 0; i < module.Options.Length; i++)
                        {
                            module.Options[i].Fill = module.Options[i].Fill / totalFill * 100f;
                        }

                        module.Options = module.Options.OrderBy(x => x.Fill).ToArray();
                    }
                }
            }
        }
    }
}