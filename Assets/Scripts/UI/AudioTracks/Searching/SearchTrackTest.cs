using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using General.Audio;
using General.AudioTracks;
using General.Behaviours;
using General.Storage;
using General.UI.CanvasManagement;
using Game.Services;
using Game.Services.Implementations;
using General.AudioTracks.Searching;
using SmallTail.Localization;
using UI.Popups;
using UnityEngine;
using YouTubeSearch;

namespace UI.AudioTracks.Searching
{
    public class SearchTrackTest : ExtendedBehaviour
    {
        public YoutubeSearchResult searchResult;

        private GameSettingsService _gameSettings;
        private TrackProcessorService _trackProcessor;
        private ICanvasSwitcher _canvasSwitcher;
        private IPopupFactory _popupFactory;

        private IPopup _processingPopup;
        private AnalyzedAudio _analyzedAudio;
        private AudioClip _clip;
        
        private void Awake()
        {
            _trackProcessor = Toolbox.Instance.GetService<TrackProcessorService>();
            _gameSettings = Toolbox.Instance.GetService<GameSettingsService>();
            
            _canvasSwitcher = FindComponentOfInterface<ICanvasSwitcher, NullCanvasSwitcher>();
            _popupFactory = FindComponentOfInterface<IPopupFactory, NullPopupFactory>();
        }

        public void Click()
        {
            _popupFactory.CreatePopup("Popup", LocalizationService.GetValue("ui_popup_track_text"), new[]
            {
                LocalizationService.GetValue("ui_popup_no"),
                LocalizationService.GetValue("ui_popup_yes")
            },  optionName =>
            {
                if (optionName == LocalizationService.GetValue("ui_popup_yes"))
                {
                    IEnumerable<VideoInfo> infos = DownloadUrlResolver.GetDownloadUrls(searchResult.VideoURL);

                    VideoInfo info = infos
                        .Where(i => i.VideoType != VideoType.Mp4)
                        .OrderByDescending(i => i.AudioBitrate)
                        .FirstOrDefault();

                    if (info == null)
                    {
                        return;
                    }

                    if (info.RequiresDecryption)
                    {
                        DownloadUrlResolver.DecryptDownloadUrl(info);
                    }

                    string path = Application.persistentDataPath + "/trackcache/" + searchResult.Id + "/";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    if (!File.Exists(path + "audio.m4a"))
                    {
                        _processingPopup = _popupFactory.CreatePopup("WaitPopup",
                            LocalizationService.GetValue("ui_tracklist_downloading"),
                            null,
                            null);
                        
                        using (WebClient client = new WebClient()) 
                        {
                            client.DownloadFile(new Uri(searchResult.Thumbnail), path + "thumbnail.jpeg");
                        }
                        
                        VideoDownloader downloader = new VideoDownloader();
                        downloader.DownloadFile(info.DownloadUrl, "audio", false, path, info.VideoExtension);
                       
                        downloader.OnDownloaded = () =>
                        {
                            _processingPopup.CloseSilent();
                            Process(path);
                        };
                        
                        return;
                    }

                    Process(path);
                }
            });
        }

        private async void Process(string path)
        {
            _processingPopup = _popupFactory.CreatePopup("WaitPopup",
                LocalizationService.GetValue("ui_tracklist_processing"),
                null,
                null);

            await Task.Delay(100);
            
            _trackProcessor.Process(path, OnProcessed);
        }
        
        private void OnProcessed(AudioClip clip, AnalyzedAudio analyzedAudio)
        {
            _clip = clip;
            _analyzedAudio = analyzedAudio;
        }

        private void Update()
        {
            if (_analyzedAudio != null)
            {
                _processingPopup.CloseSilent();

                _gameSettings.Clip = _clip;
                _gameSettings.AnalyzedAudio = _analyzedAudio;
                
                _canvasSwitcher.Open("GameConfiguration");

                _analyzedAudio = null;
            }
        }
    }
}