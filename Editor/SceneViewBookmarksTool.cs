using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace Toolbox.SceneViewBookmark
{
    [Icon(SceneViewBookmark.Icon)]
    public class SceneViewBookmarksTool
    {
        private const string MENU_PATH = "Tools/View-Bookmarks/";

        private static int _bookmarkIndex;

        [InitializeOnLoadMethod]
        static void Initialize()
        {
            EditorSceneManager.sceneOpened += OnSceneOpened;
        }

        private static void OnSceneOpened(Scene scene, OpenSceneMode mode)
        {
            _bookmarkIndex = 0;
        }

        [Shortcut(MENU_PATH + "Add", KeyCode.B, ShortcutModifiers.Shift)]
        public static void Add()
        {
            Scene scene = EditorSceneManager.GetActiveScene();
            SceneViewBookmarksDirectory dir = SceneViewBookmarksDirectory.Find(scene);

            if (dir == null)
                dir = SceneViewBookmarksDirectory.Create(scene);

            dir.AddBookmark(SceneViewBookmark.CreateFromSceneView(SceneView.lastActiveSceneView));
        }

        [Shortcut(MENU_PATH + "Switch", KeyCode.B, ShortcutModifiers.Alt)]
        public static void Switch()
        {
            Scene scene = EditorSceneManager.GetActiveScene();
            SceneViewBookmarksDirectory dir = SceneViewBookmarksDirectory.Find(scene);

            if (dir == null)
                return;

            if (dir.Count <= 1)
                _bookmarkIndex = 0;

            SceneViewBookmark? bookmark = dir.GetBookmark(_bookmarkIndex);

            if (!bookmark.HasValue)
                return;

            bookmark.Value.SetSceneViewOrientation(SceneView.lastActiveSceneView);

            _bookmarkIndex++;
            if (_bookmarkIndex >= dir.Count)
                _bookmarkIndex = 0;
        }
    }
}
