using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using MatchaIsSpent.Characters.AbilitySystem;
using MatchaIsSpent.WorldGeneration;
using MatchaIsSpent.Characters.FootstepsSystem;

namespace MatchaIsSpent.CustomTools
{

    /// <summary>
    /// This class is used to create a custom editor window for MatchaIsSpent.
    /// </summary>
    public class MatchaIsSpentEditor : EditorWindow
    {
        /// <summary>
        /// The buttons to create new Ability assets.
        /// </summary>
        private Button createAbility;
        /// <summary>
        /// The buttons to create new Decorator assets.
        /// </summary>
        private Button createDecorator;
        /// <summary>
        /// The buttons to create new Composite assets.
        /// </summary>
        private Button createComposite;
        /// <summary>
        /// The buttons to create new TileData assets.
        /// </summary>
        private Button createTileData;
        /// <summary>
        /// The buttons to create new RandomWalkData settings.
        /// </summary>
        private Button createRandomWalkData;
        /// <summary>
        /// The buttons to create new Prop assets.
        /// </summary>
        private Button createProp;
        /// <summary>
        /// The buttons to initialize the scene.
        /// </summary>
        private Button initializeScene;
        /// <summary>
        /// The buttons to regenerate the world.
        /// </summary>
        private Button regenerateWorld;
        /// <summary>
        /// The buttons to create new FootstepTileData assets.
        /// </summary>
        private Button createFootstepTileData;
        /// <summary>
        /// The buttons to show the credits.
        /// </summary>
        private Button credits;

        [MenuItem("MatchaIsSpentAssingment/MatchaIsSpentEditor")]
        private static void ShowWindow()
        {
            var window = GetWindow<MatchaIsSpentEditor>();
            window.titleContent = new GUIContent("MatchaIsSpent");
            window.Show();
        }

        private void CreateGUI()
        {
            var root = rootVisualElement;

            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/_MatchaIsSpent/Editor/MatchaIsSpentEditor.uxml");
            visualTree.CloneTree(root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/_MatchaIsSpent/Editor/MatchaIsSpentEditor.uss");
            root.styleSheets.Add(styleSheet);

            createAbility = root.Q<Button>("CreateAbility");
            createAbility.clicked += CreateAbility;
            createDecorator = root.Q<Button>("CreateDecorator");
            createDecorator.clicked += CreateDecorator;
            createComposite = root.Q<Button>("CreateComposite");
            createComposite.clicked += CreateComposite;
            createTileData = root.Q<Button>("CreateTileData");
            createTileData.clicked += CreateTileData;
            createRandomWalkData = root.Q<Button>("CreateRandomWalkData");
            createRandomWalkData.clicked += CreateRandomWalkData;
            createProp = root.Q<Button>("CreateProp");
            createProp.clicked += CreateProp;
            initializeScene = root.Q<Button>("InitializeScene");
            initializeScene.clicked += InitializeScene;
            regenerateWorld = root.Q<Button>("RegenerateWorld");
            regenerateWorld.clicked += RegenerateWorld;
            createFootstepTileData = root.Q<Button>("CreateFootstepTileData");
            createFootstepTileData.clicked += CreateFootstepTileData;
            credits = root.Q<Button>("Credits");
            credits.clicked += Credits;
        }

        /// <summary>
        /// Create a new ability.
        /// </summary>
        private void CreateAbility()
        {
            var path = EditorUtility.SaveFilePanelInProject("Create Ability", "New Ability", "asset", "Create a new ability");
            if (path == "")
                return;

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BaseAbilitySO>(), path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Create a new decorator.
        /// </summary>
        private void CreateDecorator()
        {
            var path = EditorUtility.SaveFilePanelInProject("Create Decorator", "New Decorator", "asset", "Create a new decorator");
            if (path == "")
                return;

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DelayDecorator>(), path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Create a new composite.
        /// </summary>
        private void CreateComposite()
        {
            var path = EditorUtility.SaveFilePanelInProject("Create Composite", "New Composite", "asset", "Create a new composite");
            if (path == "")
                return;

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<SequenceComposite>(), path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Create a new tile data.
        /// </summary>
        private void CreateTileData()
        {
            var path = EditorUtility.SaveFilePanelInProject("Create TileData", "New TileData", "asset", "Create a new tile data");
            if (path == "")
                return;

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<TileData>(), path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Create a new random walk data settings.
        /// </summary>
        private void CreateRandomWalkData()
        {
            var path = EditorUtility.SaveFilePanelInProject("Create RandomWalkDataSettings", "New RandomWalkData", "asset", "Create a new random walk data");
            if (path == "")
                return;

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<SimpleRandomWalkSO>(), path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Create a new prop.
        /// </summary>
        private void CreateProp()
        {
            var path = EditorUtility.SaveFilePanelInProject("Create Prop", "New Prop", "asset", "Create a new prop");
            if (path == "")
                return;

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<Prop>(), path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Initialize the scene.
        /// Create the systems and the world.
        /// Destroy all the objects in the scene.
        /// </summary>
        private void InitializeScene()
        {
            var objects = FindObjectsOfType<GameObject>();
            foreach (var obj in objects)
            {
                DestroyImmediate(obj);
            }
            var systems = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_MatchaIsSpent/Prefabs/Fundamentals/---Systems---.prefab");
            var systemsInstance = PrefabUtility.InstantiatePrefab(systems) as GameObject;
            var world = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_MatchaIsSpent/Prefabs/Fundamentals/---World---.prefab");
            var worldInstance = PrefabUtility.InstantiatePrefab(world) as GameObject;

            Undo.RegisterCreatedObjectUndo(systemsInstance, "Create Systems");
            Undo.RegisterCreatedObjectUndo(worldInstance, "Create World");
        }

        /// <summary>
        /// Regenerate the world.
        /// </summary>
        private void RegenerateWorld()
        {
            var world = FindObjectOfType<MapGenerator>();
            if (world == null)
            {
                EditorUtility.DisplayDialog("No world found", "World Generator Tools not found", "OK");

                Debug.LogError("No world found");
                return;
            }
            world.Generate();
        }

        /// <summary>
        /// Create a new footstep tile data.
        /// </summary>
        private void CreateFootstepTileData()
        {
            var path = EditorUtility.SaveFilePanelInProject("Create FootstepTileData", "New FootstepTileData", "asset", "Create a new footstep tile data");
            if (path == "")
                return;

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<SoundTileData>(), path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Show the credits.
        /// </summary>
        private void Credits()
        {
            EditorUtility.DisplayDialog("Credits", "Programmer Test for Cute Newt:\n\n" +
                "By German Dartsch", "OK");
        }

        private void OnDestroy()
        {
            createAbility.clicked -= CreateAbility;
            createDecorator.clicked -= CreateDecorator;
            createComposite.clicked -= CreateComposite;
            createTileData.clicked -= CreateTileData;
            createRandomWalkData.clicked -= CreateRandomWalkData;
            createProp.clicked -= CreateProp;
            initializeScene.clicked -= InitializeScene;
            regenerateWorld.clicked -= RegenerateWorld;
            createFootstepTileData.clicked -= CreateFootstepTileData;
            credits.clicked -= Credits;
        }
    }
}