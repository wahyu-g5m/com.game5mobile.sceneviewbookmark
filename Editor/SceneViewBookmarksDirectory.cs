using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace Toolbox.SceneViewBookmark
{
    [Icon(SceneViewBookmark.Icon)]
    public class SceneViewBookmarksDirectory : ScriptableObject
    {
        [SerializeField] private string _sceneGuid;
        [SerializeField] private SceneViewBookmark[] _bookmarks;

        public int Count => _bookmarks == null ? 0 : _bookmarks.Length;

        public static SceneViewBookmarksDirectory Create(Scene scene)
        {
            SceneViewBookmarksDirectory directory =
                ScriptableObject.CreateInstance<SceneViewBookmarksDirectory>();

            string sceneGuid = AssetDatabase.AssetPathToGUID(scene.path);
            directory._sceneGuid = sceneGuid;

            string sceneName = Path.GetFileNameWithoutExtension(scene.path);
            string path = scene.path.Substring(
                0,
                scene.path.Length - Path.GetFileName(scene.path).Length);
            path = Path.Combine(path, "svBookmark_" + sceneName + ".asset");

            AssetDatabase.CreateAsset(directory, path);

            return directory;
        }

        public static SceneViewBookmarksDirectory Find(Scene scene)
        {
            string sceneGuid = AssetDatabase.AssetPathToGUID(scene.path);

            var directories = AssetDatabase.FindAssets("t:SceneViewBookmarksDirectory");
            foreach (string directoryGuid in directories)
            {
                string path = AssetDatabase.GUIDToAssetPath(directoryGuid);
                var dir = AssetDatabase.LoadAssetAtPath<SceneViewBookmarksDirectory>(path);
                if (dir._sceneGuid.Equals(sceneGuid))
                    return dir;
            }
            return null;
        }

        public SceneViewBookmark? GetBookmark(int index)
        {
            if (_bookmarks == null || _bookmarks.Length <= index)
                return null;

            return _bookmarks[index];
        }

        public void AddBookmark(SceneViewBookmark bookmark)
        {
            if (_bookmarks == null)
            {
                _bookmarks = new SceneViewBookmark[1];
                _bookmarks[0] = bookmark;
            }
            else
            {
                ArrayUtility.Add<SceneViewBookmark>(ref _bookmarks, bookmark);
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }
    }
}
