using StansAssets.Foundation.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.Foundation.Samples
{
    class AudioCenterSample : MonoBehaviour
    {
        [SerializeField]
        AudioClip m_TestClip;

        [SerializeField]
        AudioListener m_AudioListener;

        [SerializeField]
        Button m_PlayButton;

        AudioCenter m_AudioCenter;

        void Start()
        {
            m_AudioCenter = new AudioCenter("Sample Audio Center", m_AudioListener);
            m_PlayButton.onClick.AddListener(() =>
            {
                m_AudioCenter.PlayOneShot(m_TestClip);
            });
        }
    }
}
