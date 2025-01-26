using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace GGJ2025.UIComponents
{
    public abstract class UISelector<T> : MonoBehaviour where T : Object
    {
        public bool SelectRandomOnStart;
        public T SelectedObject;
        public TMP_Dropdown Dropdown;
        public virtual string Path { get; }

        private T[] GatherObjects(string Path)
        {
            var objects = Resources.LoadAll<T>(Path);
            return objects;
        }
        
        public void Start()
        {
            var objects = GatherObjects(Path);
            Dropdown.ClearOptions();
            var options = new List<TMP_Dropdown.OptionData>();
            foreach (var obj in objects)
            {
                options.Add(new TMP_Dropdown.OptionData(obj.name));
            }
            Dropdown.AddOptions(options);
            Dropdown.onValueChanged.AddListener(OnValueChanged);
            
            if (SelectRandomOnStart)
            {
                Dropdown.value = UnityEngine.Random.Range(0, objects.Length);
            }
            else OnValueChanged(0);
        }

        private void OnValueChanged(int index)
        {
            var objects = GatherObjects(Path);
            SelectedObject = objects[index];
        }
        
        public T GetSelectedObject()
        {
            if (SelectedObject == null)
            {
                var objects = GatherObjects(Path);
                SelectedObject = objects[0];
            }
            return SelectedObject;
        }
    }
}