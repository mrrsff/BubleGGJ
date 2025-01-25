using System.Collections.Generic;
using UnityEngine;

namespace Karma.Utility
{
    public enum LayoutType
    {
        XAxis,
        YAxis,
        ZAxis,
    }
    public enum Alignment
    {
        Left,
        Center,
        Right
    }
    [ExecuteInEditMode]
    public class ThreeDLayoutGroup : MonoBehaviour
    {
        // 3D layout group that arranges children in a 3D grid or horizontal/vertical line
        public LayoutType layoutType;
        public Alignment alignment;
        public float spacing = 1f;
        public int CellCount => transform.childCount;
        private static readonly Dictionary<LayoutType, Vector3> layoutToVector = new()
        {
            {LayoutType.XAxis, Vector3.right},
            {LayoutType.YAxis, Vector3.up},
            {LayoutType.ZAxis, Vector3.forward},
        };

        private void OnValidate()
        {
            ArrangeChildren();
        }

        private void OnTransformChildrenChanged()
        {
            ArrangeChildren();
        }

        void ArrangeChildren()
        {
            if (CellCount == 0) return;
                
            ArrangeInLine(layoutToVector[layoutType]);
        }
        void ArrangeInLine(Vector3 direction)
        {
            float currentPosition = 0;
            switch (alignment)
            {
                case Alignment.Left:
                    currentPosition = 0;
                    break;
                case Alignment.Center:
                    currentPosition = -spacing * (CellCount - 1) / 2;
                    break;
                case Alignment.Right:
                    currentPosition = -spacing * (CellCount - 1);
                    break;
            }
            for (int i = 0; i < CellCount; i++)
            {
                Transform child = transform.GetChild(i);

                child.localPosition = currentPosition * direction;
                currentPosition += spacing;
            }
        }
    }
}
