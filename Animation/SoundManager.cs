using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Guus_Reise.Animation
{
    class SoundManager
    {
        private static SoundEffect[] _sounds;
        private static bool _isPlaying = false;
        private static int _playtimes;
        private static double _timer;
        private static double _currentEndTime;
        private static int repeater = -1;

        public SoundManager()
        {
            _sounds = new SoundEffect[10];
            _playtimes = 1;
        }

        public void Load(ContentManager content)
        {
            _sounds[0] = content.Load<SoundEffect>("Sounds\\guu_wave");
            _sounds[1] = content.Load<SoundEffect>("Sounds\\paul_wave");
            _sounds[2] = content.Load<SoundEffect>("Sounds\\timmae_wave");
            _sounds[3] = content.Load<SoundEffect>("Sounds\\guu_move");



        }

        public void Update(GameTime gt)
        {
            if (_isPlaying && _playtimes > 0)
            {
                _timer += gt.ElapsedGameTime.TotalMilliseconds;
                if (_timer > _currentEndTime)
                {
                    _timer = 0;
                    
                    if(repeater > 0)
                    {
                        _sounds[repeater].Play();
                        _currentEndTime = _sounds[repeater].Duration.TotalMilliseconds;
                    }
                    _playtimes = _playtimes -1;
                    if (_playtimes <= 0)
                    {
                        repeater = -1;
                        _isPlaying = false;
                    }

                }
            }
        }


        public void PlaySound(int index)
        {
            if (!_isPlaying && index >= 0 && index < _sounds.Length && _playtimes>0)
            {
                _isPlaying = true;
                _sounds[index].Play();
                _timer = 0;
                _currentEndTime = _sounds[index].Duration.TotalMilliseconds;
                
            }

        }

        public void RestPlayTimes()
        {

            if (_playtimes < 1)
            {
                _playtimes = 1;
            }
       
        }


        public void PlaySoundXTimes(int index, int x)
        {
            if (!_isPlaying && index >= 0 && index < _sounds.Length)
            {
                _isPlaying = true;
                _sounds[index].Play();
                _timer = 0;
                _currentEndTime = _sounds[index].Duration.TotalMilliseconds;
                _playtimes = x;
                repeater = index;
            }

        }

    }
}
