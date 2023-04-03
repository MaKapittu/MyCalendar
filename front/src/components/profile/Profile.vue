<template>
    <div class="title">My profile</div>
    <div class="profile-block">
        <div class="column">
            <div class="box">
                <div class="image-box">
                    <img class="avatar" :src="getImageUrl()" />
                </div>
                <div class="avatar-info">
                    <div class="text name">{{ user.firstname }} {{ user.lastname }}</div>
                    <div class="text">@{{ user.username }}</div>
                    <div class="text">{{ user.email }}</div>
                    <div class="text">{{ user.birthday }}</div>
                    <button class="edit delete" @click.prevent="showDeleteForm"></button>
                    <router-link to="/profile/edit"><button class="edit"></button></router-link>
                </div>
            </div>
        </div>
        <div v-if="isDeleting" class="centered">
            <div class="question">Are you sure you want to delete your account?</div>
            <div>
                <button class="button no" @click.prevent="showDeleteForm">Cancel</button>
                <button class="button yes" @click.prevent="deleteForm">Delete</button>
            </div>
        </div>
    </div>  
</template>

<script setup lang="ts">
import { User } from '@/model/user';
import { useAuthStore } from '@/stores/authStore';
import { useUserStore } from '@/stores/usersStore';
import { storeToRefs } from 'pinia';
import { onMounted, Ref, ref } from 'vue';

let isDeleting = ref(false);
const usersStore = useUserStore();
const { loggedUser } = storeToRefs(usersStore);
let user: Ref<User> = ref<User>({username:'',password:''})

onMounted(async() => {
    await usersStore.getLoggedUser();
    user.value = loggedUser.value!;
});

const getImageUrl = () => {
        return new URL(`/src/assets/avatars/${user.value.photo!}.png`, import.meta.url).href;
    }

const authStore = useAuthStore();
const { logout } = authStore;

const showDeleteForm = () => {
    isDeleting.value = !isDeleting.value;
}

const { deleteUser } = useUserStore();


const deleteForm = () => {
    deleteUser({ ...user.value });
    logout();
}
</script>

<style scoped>
.title {
    margin-right: auto;
    margin-top: 3%;
    font-size: 45px;
    color: #362925;
    text-align: center;
}

.profile-block {
    position: relative;
}

.column {
    margin-top: 3%;
    margin-right: auto;
    margin-left: auto;
    background-color: #dbc9b8;
    border: solid 2px #a17e61;
    width: 500px;
    text-align: center;
    box-shadow: 0 0 15px 1px rgb(169, 169, 169);
    border-radius: 12px;
}

.box {
    margin-bottom: 5%;
    margin-right: 5%;
    position: relative;
    padding: 5px;
}

.image-box {
    align-items: center;
    text-align: center;
    margin-left: 3%;
    margin-right: auto;
}

.avatar {
    margin-top: 20px;
    border-radius: 50%;
    width: 130px;
    height: 130px;
    background-color: white;
    border: 3px solid var(--green-350);
}

.avatar-info {
    margin-left: 3%;
}

.text {
    margin: 25px;
    color: var(--green-850);
    font-size: 32px;
}

.name {
    font-size: 40px;
}

.inputName {
    display: inline;
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

.centered {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    text-align: center;
    box-shadow: 0 0 0 500vmax rgb(0 0 0 / 0.5);
    background-color: #dbc9b8;
    border: solid 2px #a17e61;
    width: 400px;
    height: 150px;
    border-radius: 7px;
    text-align: center;    
}

.edit:hover {
    border: solid 2px #a17e61;
    cursor: pointer;
}

label {
    text-align: center;
    font-size: 25px;
}

input:focus {
    text-align: center;
    outline-color: var(--green-350);
}

.submit {
    background-image: url(https://cdn-icons-png.flaticon.com/512/6659/6659380.png);
    background-size: 100%;
    margin-top: 9px;
    margin-right: 2%;
}

.cancel {
    background-image: url(https://cdn-icons-png.flaticon.com/512/391/391247.png);
}

table {
    position: relative;
    width: max-content;
    text-align: center;
}

td {
    margin: 50px;
    font-size: 18px;
    min-width: 70px;
    max-width: 200px;
    font-size: 20px;
}

input {
    height: 30px;
    width: 180px;
    margin-bottom: 5px;
    background-color: #f5ede8;
    text-align: center;
    border-radius: 8px;
    font-size: 20px;
}
.question{
  font-size: 20px;
  font-weight: bold;
  padding: 20px;
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
.yes {
  background-color: #a25555; 
}
.no {
  background-color:#ad7b55
}


</style>
