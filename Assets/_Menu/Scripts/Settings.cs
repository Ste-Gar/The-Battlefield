using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

namespace Utilities.MainMenu
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] TMP_Dropdown resolutionDropdown;
        Resolution[] resolutions;

        [SerializeField] AudioMixer audioMixer;

        private void Start()
        {
            SetupResolution();
        }

        private void SetupResolution()
        {
            resolutions = Screen.resolutions;

            resolutionDropdown.ClearOptions();

            int currentResolutionIndex = 0;
            List<string> options = new List<string>();

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);
                if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        public void SetResolution(int resolutionIndex)
        {
            Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
        }

        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("MasterVolume", volume);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }
    }
}