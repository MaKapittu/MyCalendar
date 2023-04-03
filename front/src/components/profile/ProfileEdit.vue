<template>
        <div class="title">Edit profile</div>
    <div class="profile-block">
        <div v-if="!isChoosingPhoto" class="column">
            <div class="box" style="height:530px">
                <div class="image-box">
                    <img class="avatar" :src="getImageUrl(edittedUser.photo!)" @click.prevent="showPhotos"/>
                </div>
                    <div class="edit-info">
                    <label for="firstname">Name</label><br>
                    <input class="inputName" type="text" id="firstname" name="firstname" v-model="edittedUser.firstname"
                        placeholder="firstname" />
                    <input class="inputName" type="text" id="lastname" name="lastname" v-model="edittedUser.lastname"
                        placeholder="lastname" /><br>
                    <label for="username">Username</label><br>
                    <input class="username" type="text" id="username" name="username" v-model="edittedUser.username"
                        placeholder="username" /><br>
                    <label for="email">Email</label><br>
                    <input type="text" id="email" name="email" v-model="edittedUser.email" placeholder="email" /><br>
                    <label for="email">Birthday</label><br>
                    <input type="date" id="birthday" name="birthday" v-model="edittedUser.birthday"
                        placeholder="birthday" /><br>
                    <router-link to="/profile">
                        <button class="edit cancel"></button></router-link>
                        <button class="edit submit" @click.prevent="submitForm"></button>
                </div>
                </div>
        </div>
        <div v-else class="column" style="width:700px">
            <ul>
            <li v-for="photo in photos">
                <img v-if="photo==chosenPhoto" class = "avatar chosen" :src="getImageUrl(photo)">
                <img v-else class = "avatar" :src="getImageUrl(photo)" @click.prevent="choosePhoto(photo)">
            </li>
    </ul>
            <div class="box">
                <button class="edit cancel" @click.prevent="showPhotos"></button>
                <button class="edit submit" @click.prevent="submitPhoto"></button>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { User } from '@/model/user';
import { useUserStore } from '@/stores/usersStore';
import { storeToRefs } from 'pinia';
import { onMounted, Ref, ref } from 'vue';
import { useRouter } from 'vue-router';

const usersStore = useUserStore();
const { loggedUser } = storeToRefs(usersStore);
const {editUser} = usersStore;
let isChoosingPhoto = ref(false);
let chosenPhoto = ref(1);
let edittedUser: Ref<User> = ref<User>({username:'',password:''})
let photos = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16];

onMounted(async () => {
    await usersStore.getLoggedUser();
    edittedUser.value = loggedUser.value!;
    chosenPhoto.value = edittedUser.value.photo!;
});

const getImageUrl = (name: number) => {
        return new URL(`/src/assets/avatars/${name!}.png`, import.meta.url).href;
    }

const router = useRouter();

const showPhotos = () => {
    isChoosingPhoto.value = !isChoosingPhoto.value;
}

const choosePhoto = (photo: number) => {
    chosenPhoto.value = photo;
}

const submitPhoto = () => {
    edittedUser.value.photo = chosenPhoto.value;
    showPhotos();
}

const submitForm = () => {
    router.push("/profile");
    console.log(loggedUser.value);
    editUser({...edittedUser.value});
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
    margin-bottom: 10px;
    margin-right: 5%;
    position: relative;
    padding: 5px;
}

.image-box {
    align-items: center;
    text-align: center;
    margin-left: 3%;
    margin-right: auto;
    height: 165px;
    padding-top: 10px;
}

.avatar {
    border-radius: 50%;
    width: 134px;
    height: 134px;
    border: solid 2px #dbc9b8;
    background-color: white;
}

.avatar:hover {
    width: 131px;
    height: 131px;
    border: solid 3px #c29774;
    cursor: pointer;
}

.avatar:active {
    background-color: bisque;
}

.chosen {
    width: 134px;
    height: 134px;
    border: solid 2px #795d46;
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
    margin-top: 9px;
}


.edit:hover {
    border: solid 2px #a17e61;
    cursor: pointer;
}

.edit-info {
    margin-left: 5%;
    text-align: center;
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
    margin-right: 2%;
}

.cancel {
    background-image: url(https://cdn-icons-png.flaticon.com/512/391/391247.png);
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

ul{
    padding-left: 0;
    padding-top: 15px;
}
li{
    display: inline;
    margin: 10px;
}
</style>

