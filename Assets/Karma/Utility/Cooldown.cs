using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Karma
{
    [System.Serializable]
    public class Cooldown
    {
        public float duration;
        private float m_CooldownCompleteTime;
        public bool IsReady => Time.time >= m_CooldownCompleteTime || m_CooldownCompleteTime < 0;
        public float RemainingTime => m_CooldownCompleteTime - Time.time;
        public float PercentComplete => 1 - (RemainingTime / duration);
        
        public Cooldown(float cooldown)
        {
            duration = cooldown;
            m_CooldownCompleteTime = -1f;
        }
        public void Disable() => m_CooldownCompleteTime = -1f;

        public void StartCooldown()
        {
            m_CooldownCompleteTime = Time.time + duration;
        }
        public void Reset() => StartCooldown();
        public void StartCooldown(float duration)
        {
            this.duration = duration;
            StartCooldown();
        }
    }

#if UNITY_EDITOR
    // PropertyDrawer for Cooldown
    [CustomPropertyDrawer(typeof(Cooldown))]
    public class CooldownPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var duration = property.FindPropertyRelative("duration");

            float labelWidth = EditorGUIUtility.labelWidth;

            Rect labelRect = new Rect(position.x, position.y, labelWidth, position.height);
            Rect fieldRect = new Rect(position.x + labelWidth, position.y, position.width - labelWidth, position.height);

            // Add field that can be changed via mouse drag
            EditorGUI.LabelField(labelRect, label);
            float newDuration = EditorGUI.FloatField(fieldRect, duration.floatValue);

            // Clamp the value to be positive
            newDuration = Mathf.Max(0, newDuration);

            duration.floatValue = newDuration;

            EditorGUI.EndProperty();
        }
    }
#endif
}
