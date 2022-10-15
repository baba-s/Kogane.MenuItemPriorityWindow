using System.Text;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    internal sealed class MenuItemPriorityWindow : EditorWindow
    {
        private string m_name = string.Empty;
        private bool   m_isCopied;
        private double m_copiedTime;

        [MenuItem( "Window/Kogane/Menu Item Priority", false, 1424636249 )]
        private static void Open()
        {
            var window = GetWindow<MenuItemPriorityWindow>( "Menu Item Priority" );
            window.maxSize = new( window.maxSize.x, 96 );
        }

        private void Update()
        {
            if ( !m_isCopied ) return;
            if ( EditorApplication.timeSinceStartup - m_copiedTime < 2 ) return;
            m_isCopied = false;
            Repaint();
        }

        private void OnGUI()
        {
            m_name = EditorGUILayout.TextField( "Item Name", m_name );
            var priority = GetMenuItemPriority( m_name );
            EditorGUILayout.LabelField( "Priority", priority );

            if ( GUILayout.Button( "Copy" ) )
            {
                EditorGUIUtility.systemCopyBuffer = priority;
                Debug.Log( $"Copied! `{priority}`" );
                m_isCopied   = true;
                m_copiedTime = EditorApplication.timeSinceStartup;
            }

            if ( m_isCopied )
            {
                EditorGUILayout.LabelField( "Copied!" );
            }
        }

        private static string GetMenuItemPriority( string name )
        {
            const int maxCount = 10;

            var count         = Mathf.Min( maxCount - 1, name.Length );
            var stringBuilder = new StringBuilder();

            stringBuilder.Append( "1" );

            for ( var i = 0; i < count; i++ )
            {
                var ch = name[ i ];
                if ( !char.IsLetterOrDigit( ch ) ) continue;
                stringBuilder.Append( GetNumber( ch ).ToString() );
            }

            while ( stringBuilder.Length < maxCount )
            {
                stringBuilder.Append( "9" );
            }

            return stringBuilder.ToString();

            static int GetNumber( char ch )
            {
                return ch switch
                {
                    >= '0' and <= '9' => 0,
                    >= 'A' and <= 'D' => 1,
                    >= 'a' and <= 'd' => 1,
                    >= 'E' and <= 'G' => 2,
                    >= 'e' and <= 'g' => 2,
                    >= 'H' and <= 'K' => 3,
                    >= 'h' and <= 'k' => 3,
                    >= 'L' and <= 'N' => 4,
                    >= 'l' and <= 'n' => 4,
                    >= 'O' and <= 'R' => 5,
                    >= 'o' and <= 'r' => 5,
                    >= 'S' and <= 'U' => 6,
                    >= 's' and <= 'u' => 6,
                    >= 'V' and <= 'W' => 7,
                    >= 'v' and <= 'w' => 7,
                    >= 'X' and <= 'Z' => 8,
                    >= 'x' and <= 'z' => 8,
                    _                 => 9
                };
            }
        }
    }
}