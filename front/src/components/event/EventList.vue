<template>
  <div class="">
          <table class="app-centered">
            <thead>
         <tr>
            <th>Name</th>
            <th>Description</th>
            <th>Date</th>
            <th>Start Time</th>
            <th>End Time</th>
            <th>Action</th>
        </tr>
          </thead>
        <tbody>
          <tr v-for="event in events" :key="event.id">
            <td>{{event.title}}</td>
            <td>{{event.description}}</td>
            <td>{{event.date}}</td>
            <td>{{event.startTime}}</td>
            <td>{{event.endTime}}</td>
            <td>
              <router-link :to="`${AppPaths.EDIT_EVENT}/${event.id}`"><button class = "edit"></button></router-link>
              <button class="delete" @click.prevent="getDeletingEvent(event)"></button>
            </td>
          </tr>
        </tbody>
      </table>  
        <br>
        <div> 
            <router-link to="/newevent"><button class = "add"></button></router-link>
        </div>
        <br>
        <div> 
        </div>
  </div>
  <div v-if="isDeleting" class="centered">
      <div class="question">Are you sure you want to delete "{{deletingEvent.title}}" event?</div>
      <div>
        <router-link to="/events">
          <button class ="button button3" @click.prevent="showDeleteForm()">Cancel</button>
          <button class ="button button2" @click.prevent="deleteForm()">Delete</button>
        </router-link>
    </div>
    </div>

</template>

<script setup lang="ts">
import { Event } from '@/model/event';
import { AppPaths } from '@/router';
import { storeToRefs } from 'pinia';
import { useEventStore } from '@/stores/eventsStore';
import { onMounted, ref, Ref } from 'vue';
import { RouterLink } from 'vue-router';

let isDeleting = ref(false);
let deletingEvent: Ref<Event> = ref<Event>({  id: -1, title: '', date: new Date(), startTime: new Date(), endTime: new Date(), description: '', ownerId: 0});
defineProps<{ title: string }>();

const eventsStore = useEventStore();
const { events } = storeToRefs(eventsStore);

onMounted(() => {eventsStore.loadEvents()});

const showDeleteForm= ()=> {
  isDeleting.value=!isDeleting.value;
}

const getDeletingEvent = (event: Event) => {
  showDeleteForm();
  if(event != null)
    deletingEvent.value = event;
}


const deleteForm = () => {
  eventsStore.deleteEvent(deletingEvent.value);
  showDeleteForm();
};

</script>

<style scoped>
table {
  margin-top: 3%;
  border-radius: 5px;
  border-spacing: 0;
  overflow: hidden;
  margin-left: 25%;
  width: 50%;
  text-align: left;
  position: relative;
  background-color: #f6f6f6;
  font-size: 17px;
  box-shadow: 0 0 15px 1px rgb(169, 169, 169); 
}
tbody tr:nth-child(even){
  background:#e4d6c9;
}
tbody tr:hover{
background:#c39d7f;
  color:#FFFFFF;
}
th, td {
  padding: 3px;
  text-align: left;
}
th {
  background-color: #a17e61;
  color: #362925;
}
.edit{
  background-image: url('https://upload.wikimedia.org/wikipedia/commons/thumb/6/64/Edit_icon_%28the_Noun_Project_30184%29.svg/1024px-Edit_icon_%28the_Noun_Project_30184%29.svg.png');
  background-size: cover;
  width: 20px;
  height: 20px;
  font-size: 2em;
  margin: 5px;
}
.delete{
  background-image: url(https://cdn-icons-png.flaticon.com/512/1214/1214594.png);
  background-size: cover;
  width: 20px;
  height: 20px;
}
.add{
  background-image: url('https://static.thenounproject.com/png/548269-200.png');
  background-size: cover;
  width: 50px;
  height: 50px;
  border-radius: 5px;
}
.delete:hover {
  background-color: #f34242c7;
  cursor: pointer;
}
.edit:hover {
  background-color: #3864e8c7;
  cursor: pointer;
}
.add:hover {
  background-color: #74e54b96;
  cursor: pointer;
}
.edit:active {
  background-color: #213c8fc7;
}
.add:active {
  background-color: #4e9b3296;
}
.button {
  border: none;
  color:bisque;
  padding: 10px 30px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
  font-weight: 900;
  margin: 10px 2px;
  transition-duration: 0.4s;
  cursor: pointer;
  border-radius: 12px;
}

.button:hover{
  color:black;
  background-color: bisque
}
.button1 {
  background-color: #507447;
  color: bisque;
  border: 2px solid #507447;
}

.button2 {
  background-color: #a25555; 
  color: bisque;
  border: 2px solid #a25555; 
}
.button3 {
  background-color: #a87f48;
  color: bisque;
  border: 2px solid #a87f48;
}
.question{
  font-size: 20px;
  font-weight: bold;
  padding: 20px;
  padding-bottom: -10px;
}
.centered{
  position: absolute;
    top: 45%;
    left: 50%;
    transform: translate(-50%, -50%);
    text-align: center;
    box-shadow: 0 0 0 500vmax rgb(0 0 0 / 0.5);
    background-color: #dbc9b8;
    border: solid 2px #a17e61;
    width: 400px;
    height: 170px;
    border-radius: 7px;
    text-align: center;    
}


</style>
