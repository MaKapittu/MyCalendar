<script>
import dayjs from "dayjs";
import { useEventStore } from '@/stores/eventsStore';
import { RouterLink } from 'vue-router';
import { storeToRefs } from 'pinia'


export default {
  name: "CalendarDay",
  data() {
    return {
      selectedDay: dayjs()
    };
  },
  setup() {
    const eventStore = useEventStore();
    const {events} = storeToRefs(eventStore);
    const {eventsByDay} = storeToRefs(eventStore);

    return {loadEvents: eventStore.loadEvents,loadEventsByDate: eventStore.loadEventsByDate, events, eventsByDay};
  },
  async mounted() {
    const selectedDate = this.$route.params["date"];

    if(selectedDate && typeof selectedDate == "string" && dayjs(selectedDate, 'YYYY-MM-DD').isValid()) {
      this.selectedDay = dayjs(selectedDate);
      this.loadEventsByDate(selectedDate); 
    }

    await this.loadEventsByDate(selectedDate);
  },
};
</script>

<template>
  <div>
      <router-link to="/"><button type="button" class="button">Back to the default view</button></router-link>
  </div>
  <h1>{{selectedDay.date()}}. {{selectedDay.format("MMMM YYYY")}}</h1>


  <div class="calendar-day">
          <table class="app-centered">
            <thead>
         <tr>
            <th>Name</th>
            <th>Description</th>
            <th>Start Time</th>
            <th>End Time</th>
        </tr>
          </thead>
        <tbody>
            <tr v-for="event in this.eventsByDay" :key="event.date">
            <td>{{event.title}}</td>
            <td>{{event.description}}</td>
            <td>{{event.startTime}}</td>
            <td>{{event.endTime}}</td>
          </tr>
        </tbody>
      </table>  
        <br>
        <br>
        <div> 
        </div>
  </div>
</template>


<style scoped>
h1{
  margin: 0 auto;
  text-align: center;
  font-size: 30px;
  color: #855832;
  text-transform: uppercase;
  font-weight: 300;
  text-align: center;
  margin-top: 10px;
}

.calendar-day {
  position: relative;
  background-color: var(--grey-200);
  border: solid 1px var(--grey-300);
}

table {
  border-collapse: collapse;
  width: 80%;
  text-transform: uppercase;
  text-align: left;
  position: relative;
  border: 3px solid; 
  border-color: #855832;
  background-color: #f6f6f6;
  font-weight: 300;
  font-size: 0.9em;
  font-family: sans-serif;
}
tbody tr:nth-child(even){
  background:#d6bd9c3a;
}
tbody tr:hover{
background:#c5ac97;
  color:#FFFFFF;
}

th, td {
  padding: 15px;
  text-align: left;
}

th {
  background-color: #a17e61;
  color: #f8f2ec;
}

.button {
  cursor: pointer;
  padding-bottom: 5px;
  padding-top: 10px;
  left: 50%;
  font-size: 16px;
  background-color:#855832;
  color: #fff;
  border:none;
  }
</style>