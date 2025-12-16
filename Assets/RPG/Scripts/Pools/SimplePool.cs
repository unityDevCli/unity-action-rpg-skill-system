using System.Collections.Generic;
using UnityEngine;

namespace RPG.Scripts.Pools
{
    public class SimplePool<T> where T : Component, IPoolable
    {
        private readonly Queue<T> pool = new();
        private readonly T prefab;

        public SimplePool(T prefab, int preload)
        {
            this.prefab = prefab;
            for (var i = 0; i < preload; i++)
            {
                Create();
            }
        }

        private T Create()
        {
            var obj = GameObject.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
            return obj;
        }

        public T Get()
        {
            if (pool.Count <= 0) Create();
            var obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            obj.OnSpawn();
            return obj;
        }

        public void Release(T obj)
        {
            obj.OnDespawn();
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}