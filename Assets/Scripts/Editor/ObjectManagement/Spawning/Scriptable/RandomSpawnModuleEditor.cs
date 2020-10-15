using System.Linq;
using Game.ObjectManagement.Spawning.Scriptable.Modules;
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

            float totalFill = module.Objects?.Sum(x => x.Fill) ?? 0;

            if (!Mathf.Approximately(totalFill, 100f))
            {
                EditorGUILayout.HelpBox("Fills sum must be equal to 100, not " + totalFill, MessageType.Warning);

                if (GUILayout.Button("Arrange automatically"))
                {
                    if (totalFill > 0f)
                    {
                        for (int i = 0; i < module.Objects.Length; i++)
                        {
                            module.Objects[i].Fill = module.Objects[i].Fill / totalFill * 100f;
                        }

                        module.Objects = module.Objects.OrderBy(x => x.Fill).ToArray();
                    }
                }
            }
        }
    }
}