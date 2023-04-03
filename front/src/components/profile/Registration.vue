<template>
    <div class="login-page">
        <div class="form" @submit.prevent="submit">
            <form class="register-form">
              <input type="text" required
                    placeholder="username"
                    name ="username"
                    v-model ="newUser.username"/>
              <input type="text" required
                    placeholder="firstname"
                    name ="firtsname"
                    v-model ="newUser.firstname"/> 
              <input type="text" required
                    placeholder="lastname"
                    name ="lastname"
                    v-model ="newUser.lastname"/> 
              <input type="email" required
                    placeholder="email"
                    name ="email"
                    v-model ="newUser.email"/>      
              <input type="password" required
                 placeholder="password"
                 name ="password"
                 v-model ="newUser.password"/>
              <input type="date" required
                 placeholder="birthday"
                 name ="birthday"
                 v-model ="newUser.birthday"
                />  

              <button type="submit" >Create</button>
              <p class="message">Already registered? <router-link :to="`${AppPaths.LOGIN}`"> Sign In </router-link></p>
            </form> 
            <p v-if="showError">The email or username already exist</p>
        </div>
    </div>
</template>
  
<script setup lang="ts">
import { IRegisterRequest } from '@/model/user';
import router from '@/router';
import { useAuthStore } from '@/stores/authStore';
import { Ref, ref } from 'vue';
import { AppPaths } from '@/router';

const auth = useAuthStore();

 const newUser: IRegisterRequest =
 {username: '', password: '', firstname: '', lastname: '', birthday: new Date(), email: ''};

let showError = ref(false);

const submit = async () => {
  if(await auth.register(newUser) == false)
  {
    alert('Username or email is already teken')
  }
    newUser.username = '';
    newUser.email = '';
    newUser.password = '';
    newUser.firstname = '';
    newUser.lastname = '';
    newUser.birthday = new Date();
    
    router.push('/login');
};
</script> 

<style scoped>
.form {
  position: relative;
  background-color: #dbc9b8;
  margin-top: 1%;
  max-width: 300%;
  padding: 45px;
  border: solid 2px #a17e61;
  border-radius: 12px;
  box-shadow: 0 0 15px 1px rgb(169, 169, 169); 
}
.form input {
  background: #f2f2f2;
  width: 100%;
  border: solid 1px #74482a;
  border-radius: 12px;
  margin: 0 0 15px;
  padding: 15px;
  box-sizing: border-box;
  font-size: 15px;
}
.form button {
  text-transform: uppercase;
  background: #74482a;
  width: 100%;
  border-radius: 12px;
  border: transparent;
  padding: 15px;
  color: #FFFFFF;
  font-size: 16px;
  -webkit-transition: all 0.3 ease;
  transition: all 0.3 ease;
  cursor: pointer;
}
.form button:hover,.form button:active,.form button:focus {
  background: #855832;
}
.form .message {
  margin: 15px 0 0;
  color: #362925;
  font-size: 14px;
}
.form .message a {
  color: #74482a;
  text-decoration: none;
}
.login-page {
  width: 360px;
  padding: 2% 0 0;
  margin: auto;
}
</style>