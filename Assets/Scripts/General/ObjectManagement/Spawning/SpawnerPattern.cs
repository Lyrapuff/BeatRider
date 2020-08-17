using System;
using System.Collections.Generic;
using UnityEngine;

namespace General.ObjectManagement.Spawning
{
    [Serializable]
    public struct SpawnerPattern
    {
        [SerializeField] private Texture2D _pattern;

        public Dictionary<int, ObjectType> GetObjects(int line)
        {
            Dictionary<int, ObjectType> objects = new Dictionary<int, ObjectType>();

            for (int x = 0; x < _pattern.width; x++)
            {
                Color pixel = _pattern.GetPixel(x, line);

                if (pixel.r > 0)
                {
                    objects.Add(x, ObjectType.Enemy);
                }
                else if (pixel.g > 0)
                {
                    objects.Add(x, ObjectType.Point);
                }
            }

            return objects;
        }

        public int GetDuration()
        {
            return _pattern.height;
        }
    }
}