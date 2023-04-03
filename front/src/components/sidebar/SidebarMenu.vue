<template>
  <div class="sidebar-content">
   <div class="sidebar-wrap">
    <router-link :to="`${AppPaths.CALENDAR}`"><button class = "home"> Home </button></router-link>
    <router-link :to="`${AppPaths.EVENTS}`"><button class = "events">Events</button></router-link>
    <router-link :to="`${AppPaths.PROFILE}`"><button class = "profile">Profile</button></router-link>
    <router-link :to="`${AppPaths.FRIENDS}`"><button class = "friends">Friends</button></router-link>
    <router-link :to="`${AppPaths.SETTINGS}`"><button class = "settings">Settings</button></router-link>
    <router-link :to="`${AppPaths.PRIVACY}`"><button class = "privacy">Privacy</button></router-link>
    <br>
    <button class = "LogOut" v-if=status @click="signOut">Log out </button>
    <router-link :to="`${AppPaths.LOGIN}`" v-if=!status><button class = "Login">Log into</button></router-link>
  </div>
  </div>
</template>
  
  
<script setup lang="ts">
  import router, { AppPaths } from '@/router';  
  import { useAuthStore } from '@/stores/authStore';
  import { storeToRefs } from 'pinia';

  const authStore = useAuthStore();
  const { logout } = authStore;

  const status = storeToRefs(authStore).isAuthenticated && storeToRefs(authStore).token;

  const signOut = () => {
    if(confirm('Do you really want to log out?'))
    {
      logout();
      router.push("/login");
    }
    false;
  };
</script>
  
  <style scoped>
  .sidebar-wrap {
    padding: 40px 0;

  }
  .sidebar-content {
  height: 100%;
  background-color: #bda28c; 
  border: none;
  }
  .sidebar a {
  padding: 5px 0;
  display: flex;
  text-decoration: none;
  }
 
button{
  display: block;
  border: none;
  position: relative;  
  padding-left : 30%;
  padding-top : 2px;
  font-size: 25px;
  width: 100%;
  text-align: left;
  margin-left: 5% ;
}
button:hover{
  cursor: pointer;
  background-color:#bcafa3;
}
button:active{
  color :#5583b8
}
.Login
{
  width: 95%;
  padding-top : 1%;
  background: url("https://img.icons8.com/external-outline-black-m-oki-orlando/64/null/external-login-information-technology-outline-black-m-oki-orlando.png") no-repeat;
  background-size: 35px;
}
.LogOut
{
  width: 95%;
  padding-top : 1%;
  background: url("https://img.icons8.com/fluency-systems-filled/96/null/exit.png") no-repeat;
  background-size: 30px;
}
.home{
    background: url('https://img.icons8.com/material-outlined/24/null/home-page.png') no-repeat ;
    background-size: 30px;
  }
.events{
    background: url('https://img.icons8.com/windows/32/null/event-accepted-tentatively.png') no-repeat ;
    background-size: 30px;
  }
.profile{
    background: url('https://img.icons8.com/material-outlined/24/null/user-group-man-man.png') no-repeat ;
    background-size: 30px;
  }
.settings{
    background: url('https://img.icons8.com/material-outlined/24/null/settings--v1.png') no-repeat ;
    background-size: 30px;
  }
.privacy{
    background: url('https://img.icons8.com/material-outlined/24/null/privacy-policy.png') no-repeat ;
    background-size: 30px;
  }
.friends{
  background: url('https://img.icons8.com/external-anggara-basic-outline-anggara-putra/24/null/external-friends-social-media-interface-anggara-basic-outline-anggara-putra.png') no-repeat ;
  background-size: 30px;
}
  </style>
