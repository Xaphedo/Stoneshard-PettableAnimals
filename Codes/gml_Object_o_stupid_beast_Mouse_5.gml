if (is_visible() && visible)
{
    if instance_exists(o_player)
    {
        if (!global.skill_activate)
        {
            if (!(scr_isInFog(grid_x, grid_y)))
            {
                if (!scr_is_cutscene())
                {
                    if is_allow_actions()
                    {
                        if can_press_gui()
                        {
                            if (!instance_exists(o_side_inventory))
                            {
                                if (state == "KO")
                                    scr_create_context_menu("Explore")
                                else if (state != "attack" || state != "alarm" || state != "flee")
                                    scr_create_context_menu("Pet_Animal", "Surrender", "Dialog", "Trade", "Swap", "Explore", "Atack")
                                else
                                    scr_create_context_menu("Surrender", "Dialog", "Trade", "Swap", "Explore", "Atack")
                            }
                        }
                    }
                }
            }
        }
    }
}