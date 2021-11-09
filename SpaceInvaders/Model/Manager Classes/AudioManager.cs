using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.Storage;

namespace SpaceInvaders.Model.Manager_Classes
{
    public class AudioManager
    {
        private MediaPlayer MediaPlayer { get; set; }

        private StorageFolder audioFolder { get; set; }

        private StorageFile playerShipShootingSound { get; set; }
        public StorageFile playerShipExplodingSound { get; private set; }
        public StorageFile enemyShipExplodingSound { get; private set; }

        public AudioManager()
        {
            this.MediaPlayer = new MediaPlayer();
            this.loadAudioFolder();
            this.loadSoundEffects(this.audioFolder);
            this.MediaPlayer.AutoPlay = false;
        }

        private async void loadAudioFolder()
        {
            this.audioFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
        }

        private async void loadSoundEffects(StorageFolder folder)
        {
            this.playerShipShootingSound = await folder.GetFileAsync("shoot.wav");
            this.playerShipExplodingSound = await folder.GetFileAsync("explosion.wav");

            this.enemyShipExplodingSound = await folder.GetFileAsync("invaderkilled.wav");
        }

        public void PlayPlayerShipExploding()
        {
            this.MediaPlayer.Source = MediaSource.CreateFromStorageFile(this.playerShipExplodingSound);
            this.MediaPlayer.Play();
        }

        public void PlayPlayerShipShooting()
        {
            this.MediaPlayer.Source = MediaSource.CreateFromStorageFile(this.playerShipShootingSound);
            this.MediaPlayer.Play();
        }

        public void PlayEnemyShipBeingDestroyed()
        {
            this.MediaPlayer.Source = MediaSource.CreateFromStorageFile(this.enemyShipExplodingSound);
            this.MediaPlayer.Play();
        }
    }
}
