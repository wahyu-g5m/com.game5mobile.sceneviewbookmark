using UnityEditor;
using UnityEngine;

namespace Toolbox.SceneViewBookmark
{
    [System.Serializable]
    public struct SceneViewBookmark
    {
        public const string Icon = "Packages/com.game5mobile.sceneviewbookmark/Editor Default Resources/Bookmark@2x.png";

        public string Label;
        public bool IsOrthographic;
        public float Size;
        public Vector3 Pivot;
        public Quaternion Rotation;

        public static SceneViewBookmark CreateFromSceneView(SceneView sceneView)
        {
            SceneViewBookmark bookmark = new SceneViewBookmark()
            {
                Pivot = sceneView.pivot,
                Rotation = sceneView.rotation,
                Size = sceneView.size,
                IsOrthographic = sceneView.orthographic
            };
            return bookmark;
        }

        public void SetSceneViewOrientation(SceneView sceneView)
        {
            sceneView.pivot = Pivot;
            if (!SceneView.lastActiveSceneView.in2DMode)
                sceneView.rotation = Rotation;
            sceneView.size = Size;
            sceneView.orthographic = IsOrthographic;
        }
    }
}
