<template>
    <div class="login-page">
        <div class="form" @submit.prevent="submit">
            <form class="login-form">
                <input type="text" required
                    placeholder="username"
                    name ="username"
                    v-model ="user.username"
                />
                <input type="password" required
                 placeholder="password"
                 name ="password"
                 v-model ="user.password"
                 />
                <button type="submit">login</button>
                <p class="message">Not registered? <router-link :to="`${AppPaths.REGISTER}`"> Create an account </router-link></p>
            </form>
            <p v-if="showError"> Wrong username or password </p>
        </div>
    </div>
</template>
  
<script setup lang="ts">
import { ILoginRequest} from '@/model/user';
import router from '@/router';
import { useAuthStore } from '@/stores/authStore';
import { ref } from 'vue';
import { AppPaths } from '@/router';

const auth = useAuthStore();

const user: ILoginRequest =
 { username: '', password: '' };

let showError = ref(false);
  
const submit = async () => {
  if(!(await auth.login(user)))
  {
    showError.value = !(await auth.login(user));
  }
  else
    router.push("/");
};

</script> 

<style scoped>
.login-page {
  width: 360px;
  padding: 8% 0 0;
  margin: auto;
}
.form {
  position: relative;
  background-color: #dbc9b8;
  max-width: 360px;
  margin: 0 auto 100px;
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
</style>