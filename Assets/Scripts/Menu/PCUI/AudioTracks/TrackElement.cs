using System.Collections;
using General.AudioTracks;
using General.AudioTracks.Searching;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.PCUI.AudioTracks
{
    public class TrackElement : MonoBehaviour
    {
        public IAudioTrack Track => _track;
        
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _image;
        
        private IAudioTrack _track;

        public void SetResult(IAudioTrack track)
        {
            _track = track;

            _title.text = track.Title;

            StartCoroutine(SetImage(_image, track.Thumbnail));
        }
        
        private IEnumerator SetImage(Image image, string url)
        {
            WWW www = new WWW(url);
            
            yield return www;
            
            image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        }
    }
}