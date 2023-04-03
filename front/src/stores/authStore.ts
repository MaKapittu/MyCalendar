import useApi from '@/model/api';
import { AuthResponse, ILoginRequest, IRegisterRequest, User } from '@/model/user';
import { defineStore } from 'pinia';
import { computed, ref } from 'vue'; 
import { useRouter } from 'vue-router';

export const useAuthStore = defineStore('userStore', () => {
  let token = ref<string | undefined | null>(localStorage.getItem('token'));
  const isAuthenticated = computed(() => Boolean(token.value));
  const router = useRouter();

  const login = async (loginUser: ILoginRequest): Promise<boolean> => {
    const apiLogin = useApi<AuthResponse>('AuthManagement/login', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(loginUser),
    });

    await apiLogin.request();

    if (apiLogin.response.value && apiLogin.response.value.token) {
      token.value = apiLogin.response.value.token;
      localStorage.setItem('token', token.value)
      return true;
    }

    return false;
  };
  const register = async (newUser: IRegisterRequest): Promise<boolean> => {
    const apiRegister = useApi<IRegisterRequest>('AuthManagement/registration', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({...newUser, birthday: newUser.birthday.toString()}),
    });

    await apiRegister.request();

    if (apiRegister.response.value) {
      return true;
    }

    return false;
  };

  const logout = () => {
    token.value = undefined;
    localStorage.removeItem('token');
    router.push("/login");
  };

  return {
    isAuthenticated,
    token,
    login,
    logout,
    register
  };
});
