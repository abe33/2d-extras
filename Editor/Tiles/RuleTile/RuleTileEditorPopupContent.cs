using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace UnityEditor.Tilemaps
{

    public class RuleTileEditorPopupContent : PopupWindowContent
    {
        int[] _values;
        string[] _labels;
        RuleTile.TilingRule _tilingRule;
        Dictionary<Vector3Int, int> _neighbors;
        Vector3Int _position;

        public RuleTileEditorPopupContent(
            RuleTile.TilingRule tilingRule,
            Dictionary<Vector3Int, int> neighbors,
            Vector3Int position,
            int[] values,
            string[] labels)
        {
            _tilingRule = tilingRule;
            _neighbors = neighbors;
            _position = position;
            _values = values;
            _labels = labels;

        }
        public override Vector2 GetWindowSize()
        {
            return new Vector2(200, (_values.Length + 1) * 24f);
        }

        public override void OnGUI(Rect rect)
        {
            if (Event.current.type == EventType.MouseMove)
                Event.current.Use();

            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape)
            {
                editorWindow.Close();
                GUIUtility.ExitGUI();
            }

            if (GUILayout.Button("None"))
            {
                ChangeNeighbor(0);
                editorWindow.Close();
                GUIUtility.ExitGUI();
            }

            for (int i = 0; i < _values.Length; i++)
            {
                if (GUILayout.Button(_labels[i]))
                {
                    ChangeNeighbor(_values[i]);
                    editorWindow.Close();
                    GUIUtility.ExitGUI();
                }
            }
        }

        void ChangeNeighbor(int i)
        {
            if (_neighbors.ContainsKey(_position))
            {
                if (i > 0)
                {
                    _neighbors[_position] = i;
                }
                else
                {
                    _neighbors.Remove(_position);
                }
            }
            else
            {
                _neighbors.Add(_position, i);
            }
            _tilingRule.ApplyNeighbors(_neighbors);
        }
    }
}