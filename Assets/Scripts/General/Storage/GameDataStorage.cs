using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using General.Behaviours;
using UnityEngine;

namespace General.Storage
{
    public class GameDataStorage : SingletonBehaviour<GameDataStorage>
    {
        public Action<GameData> OnChanged { get; set; }

        private GameData _gameData;
        private BinaryFormatter _binaryFormatter;

        private void Awake()
        {
            _binaryFormatter = new BinaryFormatter();
            Load();
        }

        public void SetData(GameData gameData)
        {
            _gameData = gameData;
            Commit();
        }

        public GameData GetData()
        {
            return _gameData;
        }

        private void Load()
        {
            if (!PlayerPrefs.HasKey("gamedata"))
            {
                _gameData = new GameData();
                return;
            }

            string data = PlayerPrefs.GetString("gamedata");

            byte[] bytes = Convert.FromBase64String(data);

            using (MemoryStream memory = new MemoryStream())
            {
                memory.Write(bytes, 0, bytes.Length);
                memory.Position = 0;

                _gameData = _binaryFormatter.Deserialize(memory) as GameData;
            }
        }

        private void Commit()
        {
            OnChanged?.Invoke(_gameData);

            string data;

            using (MemoryStream memory = new MemoryStream())
            {
                _binaryFormatter.Serialize(memory, _gameData);
                memory.Position = 0;

                data = Convert.ToBase64String(memory.ToArray());
            }

            PlayerPrefs.SetString("gamedata", data);
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.Save();
        }
    }
}