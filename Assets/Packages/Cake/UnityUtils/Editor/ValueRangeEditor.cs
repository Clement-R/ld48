using UnityEngine;

using UnityEditor;

namespace Cake.Data
{
    [CustomPropertyDrawer(typeof(ValueRange))]
    public class ValueRangeDrawer : PropertyDrawer
    {
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            var labelRect = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 1;

            // Properties
            var labelSize = 40f;
            var minLabelRect = new Rect(position.x + 150, position.y, labelSize, position.height);
            var minRect = new Rect(minLabelRect.x + labelSize + 5f, position.y, 75f, position.height);
            var maxLabelRect = new Rect(minRect.x + 80f, position.y, labelSize, position.height);
            var maxRect = new Rect(maxLabelRect.x + labelSize + 5f, position.y, 75f, position.height);

            var propMin = property.FindPropertyRelative("Min");
            var propMax = property.FindPropertyRelative("Max");
            EditorGUI.LabelField(minLabelRect, "Min");
            propMin.floatValue = EditorGUI.FloatField(minRect, propMin.floatValue);
            EditorGUI.LabelField(maxLabelRect, "Max");
            propMax.floatValue = EditorGUI.FloatField(maxRect, propMax.floatValue);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}