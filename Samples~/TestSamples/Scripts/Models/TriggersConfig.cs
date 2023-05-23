using System;
using System.Collections.Generic;
using Better.Attributes.Runtime.Gizmo;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

namespace Samples.Models
{
    [Serializable]
    public abstract class TriggersConfig
    {
        [SerializeField] private List<Trigger> abilities;
        [SerializeField] private Trigger cameraBoss = new Trigger();
        [SerializeField] private Trigger finish = new Trigger();
        private LocationInfo _currentLocation;
 
        private bool _initialized;
 
        [JsonIgnore] public List<Trigger> Abilities => abilities;
        [JsonIgnore] public Trigger CameraBoss => cameraBoss;
        [JsonIgnore] public Trigger Finish => finish;
 
        public void AddAbility(Trigger variant)
        {
            abilities ??= new List<Trigger>();
            abilities.Add(variant);
        }
 
        public void BindCameraBoss(Trigger variant) => cameraBoss = variant;
        public void BindFinish(Trigger variant) => finish = variant;
 
        public void BindLocation(LocationInfo location) => _currentLocation = location;
 
        public void Update(Vector3 position)
        {
            foreach (var ability in abilities)
            {
                ability.Update(position);
                cameraBoss.Update(position);
                finish.Update(position);
            }
        }
 
        public void Draw()
        {
            cameraBoss ??= new Trigger();
            cameraBoss.BindColor(Color.magenta);
            cameraBoss.Draw();
 
            finish ??= new Trigger();
            finish.BindColor(Color.green);
            finish.Draw();
 
            if (abilities.Count == 0) return;
            foreach (var ability in abilities)
            {
                ability.BindColor(Color.cyan);
                ability.Draw();
            }
        }
 
        public void Initialize()
        {
            if (_initialized) return;
            _initialized = true;
            cameraBoss.Initialize(OnBoss, OnExit);
            finish.Initialize(OnFinish);
            if (abilities.Count > 0)
            {
                foreach (var ability in abilities)
                {
                    ability.Initialize(OnAbilities, OnExit);
                }
            }
 
            Print("Initialize completed");
        }
 
        protected abstract void OnAbilities();
 
        protected abstract void OnBoss();
 
        protected abstract void OnFinish();
 
        protected abstract void OnExit();
 
        protected virtual void Print(string text) => Debug.Log($"TriggerConfig: {text}");
    }

    [Serializable]
    public class BaseConfig : TriggersConfig
    {
        protected override void OnAbilities()
        {
            
        }

        protected override void OnBoss()
        {
        }

        protected override void OnFinish()
        {
        }

        protected override void OnExit()
        {
        }
    }

    [Serializable]
    public class Trigger
    {
        [Gizmo][SerializeField] private Vector3 vector3;
        public void Initialize(Action onAbilities = null, Action onExit = null)
        {
        }

        public void BindColor(Color cyan)
        {
            
        }

        public void Draw()
        {
            
        }

        public void Update(Vector3 position)
        {
            
        }
    }
}