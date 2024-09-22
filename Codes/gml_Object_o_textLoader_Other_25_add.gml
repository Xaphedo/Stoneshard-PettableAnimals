    //show_message("local language is " + string(global.language))
    switch global.language
    {
        case 1: //"Русский"
            ds_list_add(global.context_menu,"Погладить")
            break
        case 2: //"English"
            ds_list_add(global.context_menu,"Pet")
            break
        case 3: //"中文"
            ds_list_add(global.context_menu,"抚摸")
            break
        case 4: //"Deutsch"
            ds_list_add(global.context_menu,"Streichle")
            break
        case 5: //"Español (LATAM)"
            ds_list_add(global.context_menu,"Acaricia")
            break
        case 6: //"Français"
            ds_list_add(global.context_menu,"Caresse")
            break
        case 7: //"Italiano"
            ds_list_add(global.context_menu,"Accarezza")
            break
        case 8: //"Português"
            ds_list_add(global.context_menu,"Acaricia")
            break
        case 9: //"Polski"
            ds_list_add(global.context_menu,"Pogłaszcz")
            break
        case 10: //"Türkçe"
            ds_list_add(global.context_menu,"Okşa")
            break
        case 11: //"日本語"
            ds_list_add(global.context_menu,"撫でる")
            break
        case 12: //"한국어"
            ds_list_add(global.context_menu,"쓰다듬어")
            break
    
        default:
            ds_list_add(global.context_menu,"Pet")
            break
    }
    global.context_menu_petAnimalID = (ds_list_size(global.context_menu) - 1)