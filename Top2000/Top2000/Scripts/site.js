$(document).ready(function () {

    // Get the selected year from the url path
    const selectedYear = window.location.pathname.split('/')[1];

    // Select the current year when the page loaded
    $('.list-head > select').val(selectedYear);

    // Update the year when the select input changed
    $('.list-head > select').on('change', (element) => window.location.href = element.target.value);


    // Audio

    const setSwitchState = ($mediaPlayer, flip) => {
        const
            pause = 'M11,10 L18,13.74 18,22.28 11,26 M18,13.74 L26,18 26,18 18,22.28',
            play = 'M11,10 L17,10 17,26 11,26 M20,10 L26,10 26,26 20,26';

        $animation = $mediaPlayer.find('animate');

        $animation.attr({
            'from': flip ? pause : play,
            'to': flip ? play : pause
        }).get(0).beginElement();
    };


    let audioPlaying = null;
    const loadedAudio = [];

    $('.tbl-media-player').on('click', (element) => {
        // Get the media player element, kinda hacky but it will work for now
        const $mediaPlayer = $(element.target).closest('.tbl-media-player');
        const audioSource = $mediaPlayer.attr('audio-src');

        if (audioSource == 'NULL' || audioSource == null) {
            // No valid audio source
            return;
        }

        // Check if the audio is already loaded
        let audio = loadedAudio.find(x => x.key === audioSource);

        if (!audio) {
            // If the audio is not loaded yet, create the audio element

            // Create the audio element, we do this with lazy loading so it won't load all audio from the page at once
            const audioElement = document.createElement('audio');
            audioElement.setAttribute('src', audioSource);
            audioElement.volume = 0.4;

            const audioKeyValuePair = {
                key: audioSource,
                value: audioElement,
                mediaPlayer: $mediaPlayer
            }

            // Add the audio to the loaded audio so it can be used again later
            loadedAudio.push(audioKeyValuePair);
            audio = audioKeyValuePair;
        }

        if (audioPlaying) {
            // If there is already audio playing stop 
            audioPlaying.value.pause();
            audioPlaying.value.currentTime = 0;

            if ((audioPlaying && audioPlaying.key == audio.key)) {
                setSwitchState($mediaPlayer, false);
                audioPlaying = null;
                return;
            }
        }

        if ((audioPlaying && audioPlaying.key != audio.key) || audioPlaying == null) {
            // Set the pause/play switch button state
            setSwitchState($mediaPlayer, true);

            if (audioPlaying) {
                setSwitchState(audioPlaying.mediaPlayer, false);
            }

            // Play the audio
            audio.value.play();
            audioPlaying = audio;

        }
    });

});