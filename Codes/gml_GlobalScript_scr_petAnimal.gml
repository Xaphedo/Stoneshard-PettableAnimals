function scr_petAnimal(argument0) //gml_Script_scr_petAnimal
{
	with (o_stupid_beast)
	{
		if ((variable_instance_exists(id, "petting_snd_is_loaded")) && (petting_snd_is_loaded == true))
			exit
	}

	//show_message("argument0 is" + string(argument0))
	var _animal_name = scr_id_get_name(argument0)
	//show_message("_animal_name is" + string(_animal_name))
	scr_actionsLog("animalHasBeenPetted", [scr_id_get_name(o_player), _animal_name])


	with (o_player)
    {
        if (scr_instance_exists_in_list(o_b_playerHasPettedAnimalToday))
            var _dailyPettingBoost = false
        else
            var _dailyPettingBoost = true
    }

	//var _dailyPettingBoost = true
	//if (!instance_exists(o_b_playerHasPettedAnimalToday))
    //	var _dailyPettingBoost = true
    //else
    //	var _dailyPettingBoost = false

	if (_dailyPettingBoost == true)
	{
		if (!variable_global_exists("pettableAnimalsDailySanity") || !variable_global_exists("pettableAnimalsDailyMorale"))
		{
			scr_psychic_change("Sanity", 3)
			scr_psychic_change("Morale", 5)
			scr_actionsLog("animalHasBeenPettedDaily", [scr_id_get_name(o_player), scr_id_get_name(o_player)])
		}
		else if ((global.pettableAnimalsDailySanity > 0) && (global.pettableAnimalsDailyMorale > 0))
		{
			scr_psychic_change("Sanity", global.pettableAnimalsDailySanity)
			scr_psychic_change("Morale", global.pettableAnimalsDailyMorale)
			scr_actionsLog("animalHasBeenPettedDaily", [scr_id_get_name(o_player), scr_id_get_name(o_player)])
		}
		else if ((global.pettableAnimalsDailySanity = 0) || (global.pettableAnimalsDailyMorale = 0))
		{
			scr_psychic_change("Sanity", global.pettableAnimalsDailySanity)
			scr_psychic_change("Morale", global.pettableAnimalsDailyMorale)
		}
		with(o_player)
		{
			scr_effect_create(o_b_playerHasPettedAnimalToday, 2400) //this lasts for 20 hours, 2880 would be 24 hours
		}
	}

	
	var _snd_petting_path = "modsounds/snd_genericpet_short.ogg"
	
	var _animal_object_name = object_get_name(argument0)
	switch _animal_object_name
	{
		case "o_cat_sleep": 
			_snd_petting_path = choose("modsounds/snd_catpurr_short_1.ogg", "modsounds/snd_catpurr_short_2.ogg", "modsounds/snd_catmeow_short_1.ogg", "modsounds/snd_catmeow_short_2.ogg")
			break
		case "o_villagechicken": 
		case "o_villagechicken2": 
			_snd_petting_path = choose("modsounds/snd_chickencluck_short_1.ogg", "modsounds/snd_chickencluck_short_2.ogg", "modsounds/snd_chickencluck_short_3.ogg")
			break
		case "o_sheep_idle01": 
		case "o_sheep_idle02": 
			_snd_petting_path = choose("modsounds/snd_sheepbleat_short_1.ogg", "modsounds/snd_sheepbleat_short_2.ogg", "modsounds/snd_sheepbleat_short_3.ogg")
			break
		case "o_villagepig01": 
		case "o_villagepig02": 
		case "o_villagepig03": 
			_snd_petting_path = choose("modsounds/snd_piggrunt_short_1.ogg", "modsounds/snd_piggrunt_short_2.ogg", "modsounds/snd_piggrunt_short_3.ogg")
			break
		case "o_horse_random_side": 
		case "o_horse_buckskin_idle_back": 
		case "o_horse_palomino_idle_back": 
		case "o_horse_brown_idle_back": 
		case "o_horse_brown_caravan": 
		case "o_horse_buckskin_caravan": 
			_snd_petting_path = choose("modsounds/snd_horsesnort_short_1.ogg", "modsounds/snd_horsesnort_short_2.ogg", "modsounds/snd_horsesnort_short_3.ogg")
			break
	}

	with (o_stupid_beast)
	{
		//show_message("audio stream is being created")

		snd_petting = audio_create_stream(_snd_petting_path)
		snd_petting_length = min(audio_sound_length(snd_petting), 4)

		//audio_play_sound(snd_petting, 10, false)
		audio_play_sound_at(snd_petting, x, y, 0, 0, 120, 1, 0, 10)

		alarm[8] = room_speed * snd_petting_length

		attempts_at_ending_snd_petting = 0
		petting_snd_is_loaded = true
	}
}