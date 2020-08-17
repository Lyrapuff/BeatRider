using System.Collections;
using System.Collections.Generic;
using General.AudioTracks;
using General.Behaviours;
using General.Storage;
using UI.AudioTracks.Searching;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AudioTracks
{
    public class TrackHistory : ExtendedBehaviour
    {
        [SerializeField] private GameObject _trackPrefab;

        private void Start()
        {
            DeleteTracks();

            GameData gameData = GameDataStorage.Instance.GetData();
            
            List<AudioTrack> tracks = gameData.TrackHistory;
            tracks.Reverse();
            
            foreach (AudioTrack audioTrack in tracks)
            {
                GameObject instance = Instantiate(_trackPrefab, transform);
                instance.GetComponentInChildren<Text>().text = audioTrack.Title;
                instance.GetComponent<SearchTrackTest>().Track = audioTrack;

                string path = Application.persistentDataPath + "/trackcache/" + audioTrack.Id;
                
                StartCoroutine(SetImage(instance.transform.GetChild(1).GetComponent<Image>(), path + "/thumbnail.jpeg"));
            }
        }
        
        private void DeleteTracks()
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }

        
        private IEnumerator SetImage(Image image, string url)
        {
            WWW www = new WWW(url);
            
            yield return www;

            byte[] bytes = www.bytes;
            Texture2D texture = new Texture2D(1,1,TextureFormat.ARGB32,true);
            ImageConversion.LoadImage(texture, bytes, false);
            
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
        }
    }
}