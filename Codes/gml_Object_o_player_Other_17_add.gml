if (is_numeric(interract_event) && !is_array(interract_event))
  event_user(interract_event)
else
{
  script_execute_ext(interract_event[0], interract_event, 1)
}