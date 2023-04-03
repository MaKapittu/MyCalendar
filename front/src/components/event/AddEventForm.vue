<template>
  <form>
   <div class="table">
      <label for="title">Title</label><br>
          <input type="text"
              id="title"
              name="title"
              v-model="event.title"
              placeholder="Enter event title"
              required
            /><br>
      <label for="description">Description</label><br>
          <input type="text"
              id="description"
              name="description"
              v-model="event.description"
              placeholder="Enter description"
           /><br>
      <label for="date">Date</label><br>
          <input type="date" 
              id="date"
              name="date"
              v-model="event.date"
              placeholder="Choose date"
              required
            /><br>
      <label for="startTime">Start time</label><br>
          <input type="time"
              id="startTime"
              name="startTime"
              v-model="event.startTime"
              placeholder="Choose start time"
              required
            /><br>
      <label for="endTime">End time</label><br>
          <input type="time"
              id="endTime"
              name="endTime"
              v-model="event.endTime"
              placeholder="Choose end time"
              required
            /> 
    </div>
    <div>
        <button type ="submit" class ="button button1">
            <span v-if="editId" @click.prevent="submitForm">Save</span>
            <span v-else @click.prevent="submitForm">Add event</span>
        </button>

            <span v-if="editId">
              <button class ="button button2" @click.prevent="showDeleteForm">Delete</button>
            </span>
            <router-link to="/events"><button class ="button button3"> Back</button>
        </router-link>
    </div>
  </form>
  <div v-if="isDeleting" class="centered block">
      <div class="question">Are you sure you want to delete "{{event.title}}" event?</div>
      <div>
        <router-link to="/events">
          <button class ="button button3" @click.prevent="showDeleteForm">Cancel</button>
          <button class ="button button2" @click.prevent="deleteForm">Delete</button>
        </router-link>
    </div>
    </div>

</template>

<script lang="ts">
import { Event } from '@/model/event';
import { useEventStore } from '@/stores/eventsStore';
import { defineComponent, ref, Ref } from 'vue';
import { useRouter } from 'vue-router';

export default defineComponent({
  data: () => ({
  isDeleting: false
  }),
  methods: {
    showDeleteForm() {
  this.isDeleting=!this.isDeleting;
  } },
props: {
  editId: {
    type: Number,
    required: false,
  }
},
  setup(props) {
    const event: Ref<Event> = ref({ 
      title: '',
      id: 0,
      description: '',
      date: new Date(),
      startTime: new Date(),
      endTime:new Date(),
    ownerId: 0});

      const { addEvent, getEventById, editEvent, deleteEvent } = useEventStore();

      if(props.editId){
        const eventDb = getEventById(props.editId);
        if(eventDb) {
          event.value = eventDb;
        }}
    const router = useRouter();
        
    const submitForm = () => {
      if(props.editId) {
        editEvent({...event.value});
      } else {
        if(event.value.startTime >= event.value.endTime)
        {
          alert("Start time should be before end time!")
        }
        addEvent({ ...event.value });
      }
      event.value.title = '';
      event.value.description = '';
      event.value.date = new Date();
      event.value.startTime = new Date();
      event.value.endTime = new Date()

      router.push({name:"Event list"});
    };
    const deleteForm = () => {
    deleteEvent(event.value);
    router.push({name:"Event list"});
};

    return {event, router, submitForm, editId: props.editId, deleteForm};
  }
});
</script>

<style scoped>
input[type=text],input[type=date], input[type=time] {
  width: 70%;
  height: 35px;  
  padding: 4px;
  margin: 6px;
  display: inline-block;
  border: solid 1px #362925;
  border-radius: 12px;
  box-sizing: border-box;
}
label{
font-size: large;
}
.table {
  border-radius: 12px;
  border-width: 100px;
  padding: 22px;
  background-color: #dbc9b8;
  color: #362925;
  width: 50%;
  margin-top: 2%;
  margin-left: auto;
  margin-right: auto;
  border: solid 2px #a17e61;
  box-shadow: 0 0 15px 1px rgb(169, 169, 169); 
}
.button {
  border: none;
  color:#ffe1c9;
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
  background-color: #ffe1c9;
}
.button1 {
  background-color: #507447;
  color: #ffe1c9;
  border: 2px solid #507447;
}

.button2 {
  background-color: #a25555; 
  color: #ffe1c9;
  border: 2px solid #a25555; 
}
.button3 {
  background-color: #a17e61;
  color: #ffe1c9;;
  border: 2px solid #a17e61;
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
