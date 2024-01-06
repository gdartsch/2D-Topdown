using UnityEditor;
using UnityEngine;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// Custom editor for the MapGenerator class.
    /// </summary>
    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            MapGenerator mapGenerator = (MapGenerator)target;

            GUILayout.Space(10);

            if (GUILayout.Button("Generate"))
            {
                mapGenerator.Generate();
            }
        }
    }
}
