// Copyright (C)
// See LICENSE file for extended copyright information.
// This file is part of the repository from .

using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib;
using UndertaleModLib.Models;
using System.Text.RegularExpressions;

namespace PettableAnimals
{

    public class ScriptSet
    {
        public string Name;
        public string File;
        public EventType Type;
        public uint Subtype;

        public ScriptSet(string myName, string myFile, EventType myType = EventType.Create, uint mySubtype = 0)
        {
            Name = myName;
            File = myFile;
            Type = myType;
            Subtype = mySubtype;
        }
    }
    public class PettableAnimals : Mod
    {
        public override string Author => "Xaphedo";
        public override string Name => "Pettable Animals";
        public override string Description => "Right click on a harmless animal to pet it and increase your sanity and morale once a day.";
        public override string Version => "1.0.0.0";
        public override string TargetVersion => "0.8.2.10";



    public static IEnumerable<string> CreateContextMenuAssemblyIterator(IEnumerable<string> input)
    {
        bool fill_found = false;
        bool fill_case_found = false;
        bool jmptbl_injected = false;
        string jmp_fill = "";
        bool only_once = false;

        foreach(string item in input)
        {
            yield return item;

            if (!fill_found && item.Contains("Eat"))
            {
                fill_found = true;
            }
            else if (fill_found && !jmptbl_injected && item.Contains("bt"))
            {
                jmptbl_injected = true;
                jmp_fill = new Regex(@"\[\d+\]").Match(item).Value;
            
                yield return @"
dup.v 0
push.s ""Pet_Animal""
cmp.s.v EQ
bt [3000]
";
            }
            else if (jmp_fill != "" && item.Contains(jmp_fill))
            {
                fill_case_found = true;
            }
            else if (!only_once && fill_case_found && item.Contains("b ["))
            {
                only_once = true;
                string jmp_end = new Regex(@"\[\d+\]").Match(item).Value;
                yield return @$"
:[3000]
pushglb.v global.context_menu_petAnimalID
pushglb.v global.context_menu
call.i ds_list_find_value(argc=2)
push.s ""Pet_Animal""
conv.s.v
push.v self.context_name
call.i ds_list_add(argc=3)
popz.v
pushi.e 0
conv.i.v
pushi.e 1
conv.i.v
push.v self.context_desc
call.i ds_list_add(argc=3)
popz.v
b {jmp_end}
";
            }
        }
    }

    public static IEnumerable<string> ContextMenuMouse4AssemblyIterator(IEnumerable<string> input)
    {
        bool fill_found = false;
        bool fill_case_found = false;
        bool jmptbl_injected = false;
        string jmp_fill = "";
        bool only_once = false;

        foreach(string item in input)
        {
            yield return item;

            if (!fill_found && item.Contains("Eat"))
            {
                fill_found = true;
            }
            else if (fill_found && !jmptbl_injected && item.Contains("bt"))
            {
                jmptbl_injected = true;
                jmp_fill = new Regex(@"\[\d+\]").Match(item).Value;
            
                yield return @"
dup.v 0
push.s ""Pet_Animal""
cmp.s.v EQ
bt [3000]
";
            }
            else if (jmp_fill != "" && item.Contains(jmp_fill))
            {
                fill_case_found = true;
            }
            else if (!only_once && fill_case_found && item.Contains("b ["))
            {
                only_once = true;
                string jmp_end = new Regex(@"\[\d+\]").Match(item).Value;
                yield return @$":[3000]
push.v self.interact_id
pushi.e -9
push.v [stacktop]self.object_index
pop.v.v local._pettedAnimalID
push.v self.interact_id
pushi.e -9
pushenv [3006]

:[3001]
push.v self.in_grid
push.v self.id
call.i gml_Script_scr_can_interract_posgrid(argc=2)
conv.v.b
bf [3003]

:[3002]
pushloc.v local._pettedAnimalID
call.i gml_Script_scr_petAnimal(argc=1)
popz.v
b [3006]

:[3003]
pushloc.v local._pettedAnimalID
push.i gml_Script_scr_petAnimal
conv.i.v
call.i @@NewGMLArray@@(argc=2)
pop.v.v self.interract_event
push.v self.in_grid
conv.v.b
not.b
bf [3005]

:[3004]
call.i gml_Script_scr_delay_move(argc=0)
popz.v
b [3006]

:[3005]
call.i gml_Script_scr_delay_move_grid(argc=0)
popz.v

:[3006]
popenv [3001]
b {jmp_end}
";
            }
        }
        
    }


    private static IEnumerable<string> LogTextIterator(IEnumerable<string> input)
    {
        string findtext = " \"repAllTownsUp;"; //the string that the iterator is looking for
        
        string inserttext = "\"animalHasBeenPetted;~w~$~/~ гладит животное (~w~$~/~);~w~$~/~ pets ~w~$~/~;~w~$~/~抚摸了一只动物~w~$~/~;~w~$~/~ streichelt ~w~$~/~;~w~$~/~ acaricia a ~w~$~/~;~w~$~/~ caresse ~w~$~/~;~w~$~/~ accarezza ~w~$~/~;~w~$~/~ acaricia ~w~$~/~;~w~$~/~ głaszcze zwierzę (~w~$~/~);~w~$~/~ bir hayvanı okşuyor (~w~$~/~);~w~$~/~は動物を撫でる(~w~$~/~);~w~$~/~가 동물을 쓰다듬는다 (~w~$~/~)\", \"animalHasBeenPettedDaily;Это первый раз, когда ~w~$~/~ гладит животное сегодня. ~lg~Мораль~/~ и ~lg~Психика~/~ улучшаются!;This is the first time ~w~$~/~ pets an animal today. ~w~$~/~'s ~lg~Morale~/~ and ~lg~Sanity~/~ improve!;这是~w~$~/~今天第一次抚摸动物。~w~$~/~的~lg~士气~/~和~lg~理智~/~提升了！;Das ist das erste Mal, dass ~w~$~/~ heute ein Tier streichelt. ~w~$~/~'s ~lg~Moral~/~ und ~lg~Geisteszustand~/~ verbessern sich!;Esta es la primera vez que ~w~$~/~ acaricia un animal hoy. El ~lg~ánimo~/~ y la ~lg~cordura~/~ de ~w~$~/~ mejoran!;C'est la première fois que ~w~$~/~ caresse un animal aujourd'hui. ~w~$~/~ voit sa ~lg~morale~/~ et sa ~lg~santé mentale~/~ s'améliorer!;Questa è la prima volta che ~w~$~/~ accarezza un animale oggi. Il ~lg~morale~/~ e la ~lg~sanità mentale~/~ di ~w~$~/~ migliorano!;Esta é a primeira vez que ~w~$~/~ acaricia um animal hoje. O ~lg~ânimo~/~ e a ~lg~sanidade~/~ de ~w~$~/~ melhoram!;To jest pierwszy raz, gdy ~w~$~/~ głaszcze dziś zwierzę. ~w~$~/~'s ~lg~morale~/~ i ~lg~stan umysłowy~/~ poprawiają się!;Bu, ~w~$~/~'ın bugün bir hayvanı ilk kez okşaması. ~w~$~/~'nın ~lg~Morali~/~ ve ~lg~Akıl Sağlığı~/~ düzeliyor!;今日は~w~$~/~が初めて動物を撫でました。~w~$~/~の~lg~士気~/~と~lg~正気度~/~が向上しました！;오늘 ~w~$~/~이 처음으로 동물을 쓰다듬습니다. ~w~$~/~의 ~lg~사기~/~와 ~lg~정신력~/~이 향상됩니다!\","; //the string that the iterator will insert
        
        foreach(string item in input)
        {
            if (item.Contains(findtext))
            {
                string newItem = item.Insert(item.IndexOf(findtext), inserttext); //this adds the new string just before the found string. To add it after, simply add ' + findtext.Length' right after 'item.Insert(item.IndexOf(findtext)'
                yield return newItem;
            }
            else
            {
                yield return item;
            }
        }
    }

        public override void PatchMod()
        {

            UndertaleGameObject buffplayerHasPettedAnimalToday = Msl.AddObject(
                name:"o_b_playerHasPettedAnimalToday",
                parentName:"o_invisible_buff",
                isVisible:true,
                isAwake:true
            );
            

            //listing all the scripts to create.
            ScriptSet[] scriptsToAdd = new ScriptSet[]
            {
                new ScriptSet("o_stupid_beast", "gml_Object_o_stupid_beast_Mouse_5.gml", EventType.Mouse, 5),
                new ScriptSet("o_stupid_beast", "gml_Object_o_stupid_beast_Alarm_8.gml", EventType.Alarm, 8),

                new ScriptSet("o_b_playerHasPettedAnimalToday", "gml_Object_o_b_playerHasPettedAnimalToday_Create_0.gml", EventType.Create, 0)
            };

            //creating all the listed scripts.
            foreach (var newScript in scriptsToAdd)
            {
                Msl.AddNewEvent(
                    newScript.Name,
                    ModFiles.GetCode(newScript.File),
                    newScript.Type,
                    newScript.Subtype
                );
            }

            Msl.AddMenu("Pettable Animals",
                new UIComponent(name:"Daily sanity increase", associatedGlobal:"pettableAnimalsDailySanity", UIComponentType.Slider, (0, 30), 3, false),
                new UIComponent(name:"Daily morale increase", associatedGlobal:"pettableAnimalsDailyMorale", UIComponentType.Slider, (0, 30), 5, false)
            ); 

            Msl.AddFunction(ModFiles.GetCode("gml_GlobalScript_scr_petAnimal.gml"), "scr_petAnimal");

            Msl.LoadGML("gml_Object_o_textLoader_Other_25")
                .MatchFrom("scr_tableWriteList(global.context_menu, _array, \"context_menu\")") // Finding the line
                .InsertBelow(ModFiles, "gml_Object_o_textLoader_Other_25_add.gml") // Inserting the snippet above
                .Save();

            Msl.LoadGML("gml_Object_o_player_Other_17")
                .MatchFrom("event_user(interract_event)") // Finding the line
                .ReplaceBy(ModFiles, "gml_Object_o_player_Other_17_add.gml") // Inserting the snippet above
                .Save();          


            Msl.LoadGML("gml_Object_o_player_Other_17")
                .MatchFrom("script_execute(interract_event[0], interract_event[1], interract_event[2], interract_event[3])") // this is specifically to resolve compatibility with the craftable arrows mod
                .ReplaceBy("script_execute_ext(interract_event[0], interract_event, 1)") // replacing it with a more reliable way of executing an external script
                .Save();       

            Msl.LoadGML("gml_GlobalScript_table_log_text")
                .Apply(LogTextIterator)
                //.Peek()
                .Save();

            Msl.LoadAssemblyAsString("gml_Object_o_context_button_Mouse_4")
                .Apply(ContextMenuMouse4AssemblyIterator)
                //.Peek()
                .Save();

            Msl.LoadAssemblyAsString("gml_GlobalScript_scr_create_context_menu")
                .Apply(CreateContextMenuAssemblyIterator)
                //.Peek()
                .Save();
            
        }
    }
}
