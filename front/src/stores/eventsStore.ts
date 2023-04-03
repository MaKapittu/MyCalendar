import useApi, { useApiRawRequest } from '@/model/api';
import { Event } from '@/model/event';
import { defineStore } from 'pinia';
import { ref } from 'vue';
import { useAuthStore } from '@/stores/authStore';

export const useEventStore = defineStore('eventsStore', () => {
  const authStore = useAuthStore();
  let loadedEvents: Event[] = [];
  let events = ref<Event[]>([]);
  let allEvents = ref<Event[]>([]);
  let allLoadedEvents: Event[] = [];

  const apiGetEvents = async () => {
    const getEvents = useApi<Event[]>('events', {
      method: 'GET',
      headers:
      { Authorization: 'Bearer ' + authStore.token,
        Accept: 'application/json',
        'Content-Type': 'application/json'}});

        await getEvents.request();

        if (getEvents.response.value) {
          return getEvents.response.value;
        }
    return [];
  };

  const loadEvents = async () => {
    loadedEvents = await apiGetEvents();
    events.value = loadedEvents;
  };

  const apiGetAllEvents = async () => {
    const getAllEvents = useApi<Event[]>('Events/allEvents', {
      method: 'GET',
      headers:
      { Authorization: 'Bearer ' + authStore.token,
        Accept: 'application/json',
        'Content-Type': 'application/json'}});

        await getAllEvents.request();

        if (getAllEvents.response.value) {
          return getAllEvents.response.value;
        }
    return [];
  };

  const loadAllEvents = async () => {
    allLoadedEvents = await apiGetAllEvents();
    allEvents.value = allLoadedEvents;
    return allEvents.value;
  };


  const getEventById = (id: number) => {
    return loadedEvents.find((event) => event.id === id);
  };

  const addEvent = async (event: Event) => {
    const apiAddEvent = useApi<Event>('events', {
      method: 'POST',
      headers: {
        Authorization: 'Bearer ' + authStore.token,
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({...event, date: event.date.toString(), startTime: event.startTime.toString(), endTime: event.endTime.toString() }),
    });

    await apiAddEvent.request();
    if (apiAddEvent.response.value) {
      loadedEvents.push(apiAddEvent.response.value!);
      events.value = loadedEvents;
    }
  };
  
  const editEvent = async (event: Event) => {
    const apiAddEvent = useApi<Event>('Events/' + event.id, {
      method: 'PUT',
      headers: {
        Authorization: 'Bearer ' + authStore.token,
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(event),
    });

    await apiAddEvent.request();
    if (apiAddEvent.response.value) {
      loadEvents();
    }
    loadEvents();
  };

  const deleteEvent = async (event: Event) => {
    const apiDeleteEventRequest = useApiRawRequest(`Events/${event.id}`, {
      method: 'DELETE',
      headers: { Authorization: 'Bearer ' + authStore.token },
    });

    const res = await apiDeleteEventRequest();

    if (res.status === 204) {
      let id = loadedEvents.indexOf(event);

      if (id !== -1) {
        loadedEvents.splice(id, 1);
      }

      id = loadedEvents.indexOf(event);

      if (id !== -1) {
        loadedEvents.splice(id, 1);
      }
      loadEvents();
    }
  };

return { events, allEvents, loadAllEvents,loadEvents, addEvent, getEventById, editEvent, deleteEvent };
}); 