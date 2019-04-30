using System;
using System.Collections.Generic;
using System.Reflection;
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
            components = new List<Component>();
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

        public T AddComponent<T>(T component) where T : Component
        {
            lock (components)
            {
                // create component and initialize with gsMgr and SID
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
                Component clone = (Component)Activator.CreateInstance(type);
                clone.SID = container.owner.Manager.NewSID();
                clone.Manager = container.owner.Manager;
                container.owner.Manager.AddGameSource(clone);
                container.AddComponent(clone);

                // clone fields
                FieldInfo[] fInfos = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                for (int f = 0; f < fInfos.Length; f++)
                {
                    fInfos[f].SetValue(clone, fInfos[f].GetValue(component));
                }
                clone.transform = clone.gameObject.transform;

                // clone properties
                //PropertyInfo[] pInfos = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                //for (int p = 0; p < pInfos.Length; p++)
                //{
                //    if(pInfos[i].CanWrite)
                //        pInfos[p].SetValue(clone, pInfos[p].GetValue(component));
                //}
                // add component into clone

            }
        }

        public void Clear()
        {
            components.Clear();
        }
    }
}
