using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystem.GameCore
{
    public class ComponentContainer
    {
        private GameObject owner;
        private List<Component> components;

        public ComponentContainer(GameObject owner)
        {
            this.owner = owner;
        }

        public T AddComponent<T>(GameSourceManager gsMgr) where T : Component, new()
        {
            lock (components)
            {
                // create component and initialize with gsMgr and SID
                T component = gsMgr.Create<T>();
                component.gameObject = owner;
                component.transform = owner.transform;
                components.Add(component);
                return component;
            }
        }

        public void RemoveComponent(Component component)
        {
            lock (components)
            {
                components.Remove(component);
            }
        }

        public IEnumerable<Component> GetAllComponents()
        {
            return new List<Component>(components);
        }

        public T GetComponent<T>() where T : Component
        {
            Component c = components.Find(component => component.GetType() == typeof(T));
            if (c != null)
                return (T)c;
            return null;
        }

        public void CopyTo(ref ComponentContainer container)
        {
            Component component;
            Type type;
            // clone every component
            for (int i = 0; i < components.Count; i++)
            {
                component = components[i];
                type = component.GetType();
                object obj = Activator.CreateInstance(type);
                // clone fields
                var fields = type.GetFields();
                for (int f = 0; f < fields.Length; f++)
                {
                    fields[f].SetValue(obj, fields[f].GetValue(component));
                }
                // clone properties
                var properties = type.GetProperties();
                for (int p = 0; p < properties.Length; p++)
                {
                    properties[p].SetValue(obj, properties[p].GetValue(component));
                }
                // add component into clone
                if (component != null)
                {
                    container.components.Add(component);
                }
            }
        }

        public void Clear()
        {
            components.Clear();
        }
    }
}
