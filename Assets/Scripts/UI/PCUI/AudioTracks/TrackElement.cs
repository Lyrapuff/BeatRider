using System.Collections;
using General.AudioTracks.Searching;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PCUI.AudioTracks
{
    public class TrackElement : MonoBehaviour
    {
        public ISearchResult SearchResult => _searchResult;
        
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _image;
        
        private ISearchResult _searchResult;

        public void SetResult(ISearchResult searchResult)
        {
            _searchResult = searchResult;

            _title.text = searchResult.Title;

            StartCoroutine(SetImage(_image, searchResult.Thumbnail));
        }
        
        private IEnumerator SetImage(Image image, string url)
        {
            WWW www = new WWW(url);
            
            yield return www;
            
            image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        }
    }
}