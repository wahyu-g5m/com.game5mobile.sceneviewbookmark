using UnityEngine;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEditor.Overlays;

namespace Toolbox.SceneViewBookmark
{
    [Overlay(typeof(SceneView), "View Bookmark")]
    [Icon(SceneViewBookmark.Icon)]
    public class SceneViewBookmarkOverlay : ToolbarOverlay
    {
        SceneViewBookmarkOverlay() : base(BookmarkDropdownToggle.Id) {}

        [EditorToolbarElement(Id, typeof(SceneView))]
        class BookmarkDropdownToggle : EditorToolbarDropdown, IAccessContainerWindow
        {
            public const string Id = "SceneViewBookmarkOverlay/BookmarkDropdownToggle";

            public EditorWindow containerWindow { get; set; }

            BookmarkDropdownToggle()
            {
                text = "View Bookmarks";
                tooltip = "Select a view bookmark";
                icon = AssetDatabase.LoadAssetAtPath<Texture2D>(SceneViewBookmark.Icon);

                clicked += ShowBookmark;
            }

            private void ShowBookmark()
            {
                GenericMenu menu = new();

                menu.AddItem(new GUIContent("Add _#b"), false, () => SceneViewBookmarksTool.Add());
                menu.AddItem(new GUIContent("Switch _&b"), false, () => SceneViewBookmarksTool.Switch());

                menu.ShowAsContext();
            }
        }
    }
}
