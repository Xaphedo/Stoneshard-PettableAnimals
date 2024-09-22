if (audio_exists(snd_petting))
{
    if (!audio_is_playing(snd_petting) || attempts_at_ending_snd_petting > 10)
    {
        audio_destroy_stream(snd_petting)
        petting_snd_is_loaded = false
        //show_message("audio stream was destroyed")
    }
    else
    {
        attempts_at_ending_snd_petting = attempts_at_ending_snd_petting + 1
        alarm[8] = room_speed * 1
        //show_message("audio stream was not destroyed")
    }
}