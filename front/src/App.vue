<template>
    <div id="app">
      <div class ="topnav">
        <span v-if="!isShowSidebar">
          <button class ="hideBtn" @click="hideSidebar"></button>
        </span>
        <span v-else>
          <button class ="showBtn" @click="hideSidebar"></button>
        </span>
        <router-link to="/">
          <button class="btn">
            <div class = "name">
              <img src="https://img.icons8.com/doodle/48/null/capybara.png" class="capy"/>
              MyCalendar
            </div>
          </button>
      </router-link>
      </div>
      <div :class="'app-container ' + (isShowSidebar ? '' : 'app-container-show')">
        <div id="sidebar" class='sidebar'>
          <SidebarMenu/>
        </div>
        <div class="app-body">
          <router-view />
        </div>
      </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import SidebarMenu from './components/sidebar/SidebarMenu.vue';

export default defineComponent({
  data: () => ({
  isShowSidebar: false
  }),
  methods: {
    hideSidebar() {
      this.isShowSidebar = !this.isShowSidebar;
    }
  },
  components: {SidebarMenu}
});
</script>

<style> 
.topnav {
  border-bottom: 2px solid #83746b; 
  background-color: #bda28c;  
  /* position: fixed;   */
  left: 0;  
  right: 0;  
  top: 0;  
  height: 50px;  
  display: flex;  
  align-items: center;  
}
.app-body{
  min-height: 100vh;
}
.app-container {
  display: flex;
  justify-content: space-between;
}
.app-container .app-body {
  transition: all 0.5s;
  width: 100%;
}
.app-container-show .app-body {
  width: 88%;
  transition: all 0.5s;
}
.app-container .sidebar {
  opacity: 0;
  width: 0%;
    transition: all 0.5s;
}
.app-container-show .sidebar{
width: 12%;
opacity: 1;
transition: all 0.5s;
}


.hideBtn{
  cursor: pointer;
  background-image: url('https://img.icons8.com/material/48/000000/circled-chevron-left--v1.png');
  background-size: cover;
        width: 30px;
        height: 30px;
        margin: 5px;
        border: 0;
  background-color: transparent; 
}
.showBtn{
  cursor: pointer;
  background-image: url('https://img.icons8.com/material/48/000000/circled-chevron-right--v1.png');
  background-size: cover;
        width: 30px;
        height: 30px;
        margin: 5px;
        border: 0;
        background-color: transparent; 

}
.name{
  position: relative;
  font-size: 30px;
  left: 1%;
}
.capy{
  float: left;
  margin: -6px 10px;
}
.btn{
  background-color: transparent;
  border: transparent;
  cursor: pointer;
}
/* .btn:hover{
  background-color: #efd7b6;
  cursor: pointer;
} */

</style>