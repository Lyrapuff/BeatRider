using System.Threading.Tasks;
using General.AudioTracks;
using General.AudioTracks.Processing;
using General.Behaviours;
using General.Storage;
using General.UI.Popups;
using General.UI.Windows;
using SmallTail.Localization;
using UnityEngine;

namespace Menu.PCUI.AudioTracks.Processing
{
    [RequireComponent(typeof(TrackCarousel))]
    public class CarouselTrackProcessor : ExtendedBehaviour
    {
        [SerializeField] private Window _configurationWindow;
        [SerializeField] private ProcessingPipeline _pipeline;

        private IWindowFactory _windowFactory;
        private IPopupFactory _popupFactory;
        private IStorage _storage;
        private IPlaylist _playlist;
        private TrackCarousel _trackCarousel;
        
        private ProcessingContext _context;
        private IAudioTrack _track;
        private IPopup _loadingPopup;
        
        private void Awake()
        {
            _windowFactory = FindComponentOfInterface<IWindowFactory>();
            _popupFactory = FindComponentOfInterface<IPopupFactory>();
            _storage = FindComponentOfInterface<IStorage>();
            _playlist = FindComponentOfInterface<IPlaylist>();
            
            _trackCarousel = GetComponent<TrackCarousel>();
        }

        private void OnEnable()
        {
            _trackCarousel.OnSelected += HandleSelected;
            _pipeline.OnProcessed += HandleProcessed;
        }

        private void OnDisable()
        {
            _trackCarousel.OnSelected -= HandleSelected;
            _pipeline.OnProcessed -= HandleProcessed;
        }

        private void HandleSelected(TrackElement trackElement)
        {
            string path = Application.persistentDataPath + "/playlist/";
            Task.Run(() => _pipeline.Process(trackElement.Track, path));
            
            _track = trackElement.Track;

            _loadingPopup = _popupFactory.CreatePopup("WaitPopup", LocalizationService.GetValue("popup_loading"), new[]
            {
                "Wait"
            }, null);
        }

        private void HandleProcessed(ProcessingContext context)
        {
            _context = context;
        }

        private void Update()
        {
            if (_context != null)
            {
                _loadingPopup.Close("Wait");
                
                AudioClip clip = AudioUtil.AssembleClip(_context);

                AudioTrack track = new AudioTrack
                {
                    Id = _track.Id,
                    Title = _track.Title,
                    Thumbnail = _track.Thumbnail,
                    AudioClip = clip,
                    AnalyzedAudio = _context.AnalyzedAudio
                };

                _playlist.Add(track);
                
                _storage.Store("Game/Track", track);
                
                _storage.Store("Game/Seed", Random.Range(-100000f, 100000f));
                
                _windowFactory.CloseAll();
                _windowFactory.Open(_configurationWindow);

                _context = null;
            }
        }
    }
}