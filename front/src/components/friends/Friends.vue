<template>
<div v-if="!isCreatingGroup" class="groups">
    <div class="column">
        <div class="text">Find new friends</div>
            <input type="text" v-model="input" placeholder="Search username..."> 
            <div v-if="input"> 
                <div v-for="person in filteredList()" :key="person.id">
                    <p><img :src="getImageUrl(person.photo!)" class="avatar"/> @{{ person.username }}<button class="add" style="vertical-align:middle" @click.prevent="btnAddFriends(person)"></button></p>
                </div>
            <div class="item error" v-if="input&&!filteredList().length">
                <p>No results found!</p>
            </div>
        </div>
    </div>
    <ul>
        <li v-for="group in groups">
        <div class="group">
            <router-link :to="`/groups/${group.id}`" style="text-decoration:none"><div class="text name">{{group.name}}</div></router-link>
            <table>
                    <tr v-for="id in group.usersId" :key="id">
                        <span v-if="id!=loggedUser?.id && userFriends.some(x=>x.friendsId==id)">
                        <td><img :src="getImageUrl(userFriends.find(x=>x.friendsId==id)?.friendsPhoto!)" class="avatar" @click.prevent="friendsInfo(userFriends.find(x=>x.friendsId==id))"></td>
                            <td>{{people.find(x=>x.id==id)?.firstname}} {{people.find(x=>x.id==id)?.lastname}}</td>
                            <td v-if="group.statuses!=null"><span v-if="GetStatus(group, id)" class="status">{{GetStatus(group, id)}}</span></td></span>
                    </tr>
                </table>
            </div>
        </li>
        </ul>
        <div class="group">
            <div class="text">Create new group</div><br>
            <button class="create-group" @click.prevent="showCreating"></button>  
    </div>
    <div v-if="iUserInfo" class="block centered" style="height: 380px;">
        <div class="text">Friends info</div>
        <img :src="getImageUrl(selectedFriend!.friendsPhoto!)" class="avatarSmall " /> 
        <div class="info">
            @{{ selectedFriend?.friendsUsername }}<br>
            {{ selectedFriend?.friendsFirstname }} {{ selectedFriend?.friendsLastname }}
        </div>
        <br>
        <button class="edit delete" @click.prevent="showDeleteForm"></button>
        <button class="edit submit" @click.prevent="friendsInfo()"></button>
    </div>
    <div v-if="isDeleting" class="block centered">
            <div class="question">Are you sure you want to delete your friend?</div>
            <div>
                <button class="button no" @click.prevent="showDeleteForm">Cancel</button>
                <button class="button yes" @click.prevent="deleteForm(selectedFriend)">Delete</button>
            </div>
        </div>
</div>
<div v-else class="group create">
    <div class="text">Create group</div>
    <input type="text" required
                    placeholder="name"
                    name ="name"
                    v-model ="newGroup!.name"/>
                    <table class="creating">
                    <tr v-for="id in newGroup.usersId" :key="id">
                        <td class="creating"><img :src="getImageUrl(people.find(x=>x.id==id)?.photo!)" class="avatar"></td>
                            <td class="creating">{{people.find(x=>x.id==id)?.firstname}} {{people.find(x=>x.id==id)?.lastname}}</td>
                            <td class="creating"><span v-if="GetStatus(newGroup, id)" class="status">{{GetStatus(newGroup, id)}}</span>
                            <span v-else >
                                <button v-if="isAddingStatus!=id" class="add" @click.prevent="showAddingStatus(id)"><span class="tooltiptext">Add status</span></button>
                                <span v-else><input style="width:50px" type="text" v-model="statusInput" placeholder="status">
                                <img v-if="!statusInput" class="edit cancel small" @click.prevent="showAddingStatus(id)"/>
                                <span v-else style="display:inline-table; vertical-align:bottom;"><img class="edit cancel small" style="display:table-cell;" @click.prevent="showAddingStatus(id)"/>
                                    <img class="edit submit small" style="display:table-cell;" @click.prevent="addStatus(id)"/></span>
                            </span>
                            </span></td>
                    </tr>
                </table>
    <input type="text" v-model="inputInGroup" placeholder="Search username..."> 
            <div v-if="inputInGroup"> 
                <div v-for="person in filteredListForGroup()" :key="person.friendsId">
                    <p><img @click.prevent="addPerson(person.friendsId!)" :src="getImageUrl(person.friendsPhoto!)" class="avatar"/> @{{ person.friendsUsername }}</p>
                </div>
            <div class="item error" v-if="inputInGroup&&!filteredListForGroup().length">
                <p>No results found!</p>
            </div>
        </div>
        <div v-if="isError" style="color:brown"><span v-if="!newGroup.name">The group should be named!</span>
            <span v-else-if="newGroup.usersId!.length<2">The group has to have at least 2 people</span>
        <span v-else>{{showError()}}</span></div>
        <div class="box">
                <button class="edit cancel" @click.prevent="cancelCreating"></button>
                <button class="edit submit" @click.prevent="submitGroup"></button>
            </div>
</div>
</template>


<script setup lang="ts">
import { Group, User } from '@/model/user';
import { onMounted, ref } from 'vue';
import { storeToRefs } from 'pinia';
import { useUserStore } from '@/stores/usersStore';
import useGroup from '@/stores/groupStore';
import { RouterLink } from 'vue-router';
import { useUserFriendsStore } from '@/stores/userFriendsStore';
import { Friend } from '@/model/friend';

const { groups, load } = useGroup();
let isCreatingGroup = ref(false);
let isAddingStatus = ref(-1);
let newGroup = ref<Group>({name:'', usersId:[], id:groups.value.at(-1)?.id!+1});
let isError = ref(false);
let error = ref("");
let people = ref<User[]>([]);
var iUserInfo = ref(false);
let isDeleting = ref(false);
let selectedFriend = ref<Friend>();

const usersStore = useUserStore();
const { users, loggedUser } = storeToRefs(usersStore);
const userFriendsStore = useUserFriendsStore();
const { userFriends } = storeToRefs(userFriendsStore);


onMounted(async()=>{
    load();
    await usersStore.loadUsers();
    people.value = users.value!;
    await userFriendsStore.loadFriends() 
})

function showCreating() {isCreatingGroup.value = !isCreatingGroup.value;}

const friendsInfo = (friend?: Friend) => {
    iUserInfo.value = !iUserInfo.value;
    if (friend){
        selectedFriend.value = friend;
    }
    else
        selectedFriend = ref<Friend>();
    
}

const showDeleteForm = () => {
    isDeleting.value = !isDeleting.value;
}

const deleteForm = (user?: Friend) => {
    let friend = {
        friendsListId: 0,
        friendsId: user!.friendsId,
        userId: loggedUser.value?.id!,
        roles: "Other",
        friendsUsername: user!.friendsUsername,
        friendsFirstname: user!.friendsFirstname!,
        friendsLastname: user!.friendsLastname!,
        friendsPhoto: user!.friendsPhoto,
    };
    userFriendsStore.deleteFriend(friend);
    friendsInfo();
    showDeleteForm();
};

const getImageUrl = (name: number) => {
        return new URL(`/src/assets/avatars/${name!}.png`, import.meta.url).href;
    }

    const btnAddFriends = (user: User) => {
   let friend = {
        friendsListId: 0,
        friendsId: user.id!,
        userId: loggedUser.value?.id!,
        roles: "",
        friendsUsername: user.username,
        friendsFirstname: user.firstname!,
        friendsLastname: user.lastname!,
        friendsPhoto: user.photo!,
    };
    userFriendsStore.addFriend(friend);
    groups.value.find(x=>x.name=="Other friends")?.usersId?.push(user.id!);
};

function GetStatus(group: Group, userId: number) {
    var result: string = "";
    if(group.statuses)
{group.statuses!.forEach(function(value){
    if(value.userId==userId)
        result = value.status;
});}
return result;}

function addPerson(id: number) {
    newGroup.value.usersId?.push(id);
}
function showAddingStatus(id: number){ 
    if(isAddingStatus.value == id)
        isAddingStatus.value = -1;
    else
        isAddingStatus.value = id }

let statusInput = ref("");
function addStatus(id:number) {
    if(!newGroup.value.statuses)
        newGroup.value.statuses = [];
    newGroup.value.statuses?.push({userId:id,status:statusInput.value})
    statusInput.value = "";
}

function showError(errorNr?: number) {
    if(errorNr == 1){
        {isError.value = true;
        error.value = "The group should be named!";}
    }
    else if (errorNr == 2)
    {isError.value = true;
        error.value = "The group has to have at least 2 people"}
    else
        isError.value = !isError.value;
}

function everythingOff() {
    newGroup = ref<Group>({name:'', usersId:[],statuses:[], id:groups.value.at(-1)?.id!+1});
    inputInGroup.value = "";
    isCreatingGroup.value = !isCreatingGroup.value;
    isError.value = false;
    isAddingStatus.value = -1;
}

function cancelCreating() {
    everythingOff();
}
const submitGroup = () => {
    if(!newGroup.value.name || newGroup.value.usersId!.length < 2)
        isError.value = true;
    else {
        groups.value.push(newGroup.value);
        everythingOff();
    }
}

let input = ref("");
function filteredList() {
    console.log(userFriends.value);
  return users.value.filter((person) =>
    person.username.toLowerCase().includes(input.value.toLowerCase())&& !userFriends.value.some(x=>x.friendsId==person.id!) &&person.id != loggedUser.value?.id
  );
}

let inputInGroup = ref("");
function filteredListForGroup() {
  return userFriends.value.filter((person) =>
    person.friendsUsername.toLowerCase().includes(inputInGroup.value.toLowerCase())&&!newGroup.value.usersId?.includes(person.friendsId!)
  );
}

</script>


<style scoped>
.centered {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    text-align: center;
}
.block {
    width: 25%;
    box-shadow: 0 0 0 500vmax rgb(0 0 0 / 0.5);
    background-color: #dbc9b8;
    border: solid 2px #a17e61;
    height: 125px;
}
.column {
    margin-top: 20px;
    width: 200px;
    text-align: center;
    align-items: center;
    padding-left: 20px;
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
    padding-bottom: 20px;
    padding-left: 5px;
    margin: 15px;
}
.create{
    margin-left: auto;
    margin-right: auto;
    margin-top: 45px;
    width: 350px;
    align-items: center;
}
input[type=text]{
    margin-left: auto;
    margin-right: auto;
  height: 30px;
  width: 180px;
  margin-bottom: 5px;
  border: solid 1px #a17e61;
  background-color: #ede5de;
  text-align: center;
  border-radius: 8px;
  box-sizing: border-box;
  font-size: 15px;
}

label{
    font-size: 13px;
    text-transform: lowercase;
}
.create-group{
    background-image: url(https://cdn-icons-png.flaticon.com/512/5371/5371115.png);
    background-size: cover;
    border: solid 1px transparent;
    width: 100px;
    height: 100px;
    border-radius: 50%;
    box-shadow: 0 0 15px 1px rgb(169, 169, 169); 
    margin-bottom: 10px;
}
.create-group:hover{
    border: solid 1px #855832;
    cursor: pointer;
}
.sorry{
    background-image: url(https://cdn-icons-png.flaticon.com/512/3181/3181034.png);
    background-size: 100%;
    margin-right: auto;
}
.sorry:hover{
    background-color:transparent;
    border: 2px solid #74482a;
}
.sorry:active{
    filter: contrast(120%);
}
.avatar{
    margin: 0;
    border-radius: 50%;
    width: 100px;
    height: 100px;
    background-color: white;
    width: 60px;
    height: 60px;
    border-width: 1px solid #362925;
    vertical-align: middle;
    margin-bottom: 10px;
    display: inline-flex;
}
.text{
    margin: 3%;
    color: #362925;
    font-size: 30px;
}
.name:hover{
    border: #a25555;
  color: white;
}

label{
    text-align: center;
    font-size: 25px;
}
input:focus{
    text-align: center;
    outline-color: #74482a;;
}
table{
    position: relative;
    width: max-content;
    text-align: center;
}
td{
    min-width: 70px;
    font-size: 20px;
}
td.creating{
    min-width: 100px;
}
table.creating{
    padding-left: 25px;
}
.status{
    background-color:  #a25555;
    border-radius: 4px;
    padding: 2px;
    color: beige;
}
li{
    display: inline-table;
}
.add{
    border-radius: 50%;
    width:30px;
    height: 30px;
    vertical-align:text-bottom;
    background-image: url(https://cdn-icons-png.flaticon.com/512/6704/6704975.png);
    background-color:transparent;
    background-size: 80%;
    background-position: 50%;
    background-repeat: no-repeat;
    border: 0;
}
.add:hover {
    border: 1px solid #a17e61;
    background-color:  #c5b5a5;
}
.add:active{
    background-color: #a17e61;;
}
.forStatus{
    width: 20px;
}

.edit {
    background-image: url(https://cdn-icons-png.flaticon.com/512/8812/8812188.png);
    background-color: transparent;
    background-size: 80%;
    background-position: 50%;
    background-repeat: no-repeat;
    width: 60px;
    height: 60px;
    border-radius: 12px;
    border: solid 1px transparent;
    margin-left: 5%;
    margin-right: 5%;
    margin-top: 9px;
    vertical-align: middle;
}


.edit:hover {
    border: solid 2px #a17e61;
    cursor: pointer;
}
.submit {
    background-image: url(https://cdn-icons-png.flaticon.com/512/6659/6659380.png);
    background-size: 100%;
    margin-right: 2%;
}

.small{
    width: 17px;
    height: 17px;
    margin: 0;
    margin-left: 5px;
    display: inline-flex;
}
.small:hover{
    border: solid 1px #a17e61;
}

.add .tooltiptext {
  visibility: hidden;
  width: 60px;
  background-color:#bb9a7f;
  color:black;
  text-align: center;
  border-radius: 6px;
  padding: 2px 0;
  position: absolute;
  z-index: 1;
  bottom: 10px;
  
  /* Fade in tooltip - takes 1 second to go from 0% to 100% opac: */
  opacity: 0;
  transition: opacity 1s;
}

.add:hover .tooltiptext {
  visibility: visible;
  opacity: 1;
}
.avatar:hover {
    cursor: pointer;
    background-color: #fde8d6;
}
.avatarSmall {
    margin-top: 20px;
    border-radius: 50%;
    width: 100px;
    height: 100px;
    background-color: white;
    border: 3px solid var(--green-350);
}
.edit {
    background-image: url(https://cdn-icons-png.flaticon.com/512/8812/8812188.png);
    background-color: transparent;
    background-size: 80%;
    background-position: 50%;
    background-repeat: no-repeat;
    width: 60px;
    height: 60px;
    border-radius: 12px;
    border: solid 1px transparent;
    margin-left: 5%;
    margin-right: 5%;
}

.delete {
    background-image: url(https://cdn-icons-png.flaticon.com/512/8532/8532898.png);
}
.submit {
    background-image: url(https://cdn-icons-png.flaticon.com/512/6659/6659380.png);
    background-size: 100%;
    margin-right: 2%;
}
.yes {
  background-color: #a25555; 
  border: 2px solid #a25555; 
}
.yes:hover{
    color: black;
    background-color: #fae9db;  
}
.no {
  background-color:#ad7b55;
  border: 2px solid #ad7b55; 
}
.no:hover {
    color: black;
    background-color: #fae9db; 
}
.question{
  font-size: 20px;
  font-weight: bold;
  padding: 20px;
}
.cancel {
    background-image: url(https://cdn-icons-png.flaticon.com/512/391/391247.png);
}
.small{
    width: 17px;
    height: 17px;
    margin: 0;
    margin-left: 5px;
    display: inline-flex;
}
.info {
    font-size: xx-large;
    text-align: center;
    margin-right: 2%;
}
</style>