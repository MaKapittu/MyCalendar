<template>
    <div class="timeBox">
        <span v-if="isNotDate!" class="text" style="color:brown">Choose time and date!</span>
    <span v-else class="text" for="chosenTime">Choose time and date!</span>
    <br><input type="time" id="chosenTime" name="chosenTime" v-model="chosenTime" placeholder={{chosenTime}} required />
    <input style="width:110px" type="date" id="chosenDate" name="chosenDate" v-model="chosenDate" placeholder={{chosenDate}} required />
    <br><img class="look" @click.prevent="showEvents">
    <div class="text" style="font-size:50px">{{ group.name }}</div>
</div>
<div class="groups">
    <ul>
        <li v-for="user in groupUsers">
            <div class="group">
            <img :src.prevent="getImageUrl(user.photo!)" class="avatar"/>
            <div class="textBoxes">{{ user.firstname }} {{ user.lastname }}</div>
            <div v-if="isChanged">
            <div v-if="GetEvent(user.id!)">
                <span class="status" style="background-color:brown">Busy</span>
                <div>
                    {{ event?.startTime }} - {{ event?.endTime }}<br>
                    {{ event?.title }}
                </div>
            </div>
            <div v-else><span class="status">Free</span></div>
            <br>
        </div>
    </div>
        </li>
    </ul>
</div>
</template>

<script setup lang="ts">
import { Group, User } from '@/model/user';
import { Event } from '@/model/event';
import { useEventStore } from '@/stores/eventsStore';
import useGroup from '@/stores/groupStore';
import { useUserStore } from '@/stores/usersStore';
import { storeToRefs } from 'pinia';
import { onMounted, ref } from 'vue';
const props = defineProps<{ groupId: String }>();
let groupEvents = ref<Event[]>([]);
let groupUsers = ref<User[]>([]);
let isNotDate=ref(false);
let event = ref<Event>();
let isChanged=ref(false);

let chosenTime = ref<Date>();
let chosenDate = ref<Date>();

const { groups, load } = useGroup();
const usersStore = useUserStore();
const { users } = storeToRefs(usersStore);
const eventsStore = useEventStore();
const { allEvents } = storeToRefs(eventsStore);

let group = ref<Group>({ id: 0 });

onMounted(async () => {
    load();
    group.value = groups.value.find(x => x.id == Number(props.groupId))!;
    await eventsStore.loadAllEvents();
    await usersStore.loadUsers();
    getUsers();
});

const getImageUrl = (name: number) => {
        return new URL(`/src/assets/avatars/${name!}.png`, import.meta.url).href;
    }

const getUsers = () => {
    group.value.usersId!.forEach(id => {
        let user = users.value.find(x => x.id == id);
        groupUsers.value.push(user!);
    });
}

function GetEvent(id: number) {
    if(groupEvents.value.find(x=>x.ownerId==id))
    {   event.value = groupEvents.value.find(x=>x.ownerId==id)
        return true;}
    else
        event = ref<Event>();
    return false;
}

function showEvents(): void {
    isChanged.value = false;
    groupEvents = ref<Event[]>([]);
    if(chosenTime.value||chosenDate.value)
    {
        isNotDate.value = false;
        group.value.usersId!.forEach(id => {
        let evv = allEvents.value.find((event) => event.ownerId == id && event.startTime < chosenTime.value! && event.endTime > chosenTime.value! && event.date == chosenDate.value!);
        if (evv)
            groupEvents.value.push(evv);
    })
    isChanged.value = true;}
    else
        isNotDate.value = true;
    console.log(groupEvents.value);
}

</script>

<style scoped>
.text{
    color: #362925;
    font-size: 30px;
}
input{
    margin-top: 5px;
    margin-left: auto;
    margin-right: auto;
  height: 30px;
  width: 80px;
  margin-bottom: 5px;
  border: solid 1px #a17e61;
  background-color: #ede5de;
  text-align: center;
  border-radius: 8px;
  box-sizing: border-box;
  font-size: 17px;
}
.timeBox{
    padding: 30px;
}
.look {
    background-image: url(https://cdn-icons-png.flaticon.com/512/4695/4695312.png);
    background-color: transparent;
    background-size: 80%;
    background-position: 50%;
    background-repeat: no-repeat;
    width: 100px;
    height: 100px;
    border-radius: 50%;
    margin-left: 5%;
    margin-right: 5%;
    margin-top: 9px;
    margin-bottom: 55px;
    vertical-align: middle;
}


.look:hover {
    background-color: bisque;
    width: 96px;
    height: 96px;
    border: solid 2px #a17e61;
    cursor: pointer;
}

.groups{
    display: flex;
    margin-top: 30px;
}
.group{
    position: relative;
    background-color: #dbc9b8;
    border: solid 2px #a17e61;
    box-shadow: 0 0 15px 1px rgb(169, 169, 169); 
    border-radius: 12px;
    height:fit-content;
    width:fit-content;
    min-width: 200px;
    padding-left: 5px;
    margin: 15px;
    font-size: 20px;
}

li{
    display: inline-table;
}
ul{
    margin-left: auto;
    margin-right: auto;
}

.avatar{
    border-radius: 50%;
    width: 100px;
    height: 100px;
    background-color: white;
    width: 60px;
    height: 60px;
    border-width: 1px solid #362925;
    vertical-align: middle;
    margin: 10px;
    display: inline-flex;
}
.textBoxes{
    color: #362925;
    font-size: 30px;
}
.status{
    color: #f7f3f0;
    background-color: seagreen;
    padding: 3px;
    padding-top: 2px;
    border-radius: 5px;
}

</style>