using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace MatrixHierarchies
{
    class SceneObject
    {
        protected SceneObject parent = null; 
        protected List<SceneObject> children = new List<SceneObject>();

        protected Matrix3 localTransform = new Matrix3(); 
        protected Matrix3 globalTransform = new Matrix3();

        public SceneObject Parent
        {
            get => parent;
        }
        public Matrix3 LocalTransform
        {
            get => localTransform;
        }
        public Matrix3 GlobalTransform
        {
            get => globalTransform;
        }

        public SceneObject() 
        { 

        }

        public void Update(float deltaTime)
        {
            OnUpdate(deltaTime);

            for (int x = 0; x < children.Count; x++)
            {
                children[x].Update(deltaTime);
            }
        }

        public void Draw()
        {
            OnDraw();

            for (int x = 0; x < children.Count; x++)
            {
                children[x].Draw();
            }
        }

        public virtual void OnUpdate(float deltaTime)
        {

        }

        public virtual void OnDraw()
        {

        }

        public void UpdateTransform()
        {
            if (parent != null)
                globalTransform = parent.globalTransform * localTransform;
            else
                globalTransform = localTransform;

            for (int x = 0; x < children.Count; x++) 
            {
                children[x].UpdateTransform();
            }
        }
        public void SetPosition(float x, float y) 
        { 
            localTransform.SetTranslation(x, y); 
            UpdateTransform(); 
        }
        public void SetRotate(float radians) 
        { 
            localTransform.SetRotateZ(radians); 
            UpdateTransform(); 
        }
        public void SetScale(float width, float height) 
        { 
            localTransform.SetScale(width, height, 1); 
            UpdateTransform(); 
        }
        public void Translate(float x, float y) 
        { 
            localTransform.Translate(x, y); 
            UpdateTransform(); 
        }
        public void Rotate(float radians) 
        { 
            localTransform.RotateZ(radians); 
            UpdateTransform(); 
        }
        public void Scale(float width, float height) 
        { 
            localTransform.Scale(width, height, 1); 
            UpdateTransform(); 
        }

        public int GetChildCount()
        {
            return children.Count;
        }

        public SceneObject GetChild(int index)
        {
            return children[index];
        }

        public void AddChild(SceneObject child)
        {
            Debug.Assert(child.parent == null, "child already has parent");

            child.parent = this;
            children.Add(child);
        }

        public void RemoveChild(SceneObject child)
        {
            if (children.Remove(child))
            {
                child.parent = null;
            }
        }

        ~SceneObject()
        {
            if(parent != null)
            {
                parent.RemoveChild(this);
            }

            foreach(SceneObject so in children)
            {
                so.parent = null;
            }
        }
    }
}
