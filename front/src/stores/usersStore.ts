import useApi, { useApiRawRequest } from '@/model/api';
import { User } from '@/model/user';
import { defineStore } from 'pinia';
import { ref } from 'vue';
import { useAuthStore } from '@/stores/authStore';

export const useUserStore = defineStore('usersStore', () => {
    const authStore = useAuthStore();
    const {logout} = authStore;
    let allUsers: User[] = [];
    let loggedUser = ref<User>();
    let users = ref<User[]>([]);

    const apiGetUsers = async () => {
        const getUsers = useApi<User[]>('user', {
          method: 'GET',
          headers:
          { Authorization: 'Bearer ' + authStore.token,
            Accept: 'application/json',
            'Content-Type': 'application/json'}});
    
            await getUsers.request();
    
            if (getUsers.response.value) {
              return getUsers.response.value;
            }
        return [];
      };
      
    const loadUsers = async () => {
        allUsers = await apiGetUsers();
        users.value = allUsers;
      };

    const getUserById = (id: number) => {
        return allUsers.find((user) => user.id === id);
      };

      const getLoggedUser = async () => {
        const apiGetLoggedUser = useApi<User>('user/loggedUser', {
          method: 'GET',
          headers:
          { Authorization: 'Bearer ' + authStore.token,
          Accept: 'application/json',
          'Content-Type': 'application/json'}});
    
            await apiGetLoggedUser.request();

            if (apiGetLoggedUser.response.value) {  
              loggedUser.value = apiGetLoggedUser.response.value; 
              return apiGetLoggedUser.response.value;
            }
      };
    
    const addUser = async (user: User) => {
    const apiAddUser = useApi<User>('user', {
      method: 'POST',
      headers: {
        Authorization: 'Bearer ' + authStore.token,
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({...user, date: user.birthday?.toString()}),
    });

    await apiAddUser.request();
    if (apiAddUser.response.value) {
      allUsers.push(apiAddUser.response.value!);
      users.value = allUsers;
    }
  };

    const editUser = async (user: User) => {
      const apiAddUser = useApi<User>('user/' + user.id, {
        method: 'PUT',
        headers: {
          Authorization: 'Bearer ' + authStore.token,
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(user),
      });
  
      await apiAddUser.request();
      if (apiAddUser.response.value) {
        loadUsers();
      }
      loadUsers();
    };

    const deleteUser = async (user: User) => {
      const apiDeleteUserRequest = useApiRawRequest('user/' + user.username, {
        method: 'DELETE',
      });

      const res = await apiDeleteUserRequest();

      if (res.status === 204) {
        let id = allUsers.indexOf(user);
  
        if (id !== -1) {
          allUsers.splice(id, 1);
        }
  
        id = allUsers.indexOf(user);
  
        if (id !== -1) {
          allUsers.splice(id, 1);
        }
        loadUsers();
      }
    };
  
  return { users, loggedUser, getLoggedUser, loadUsers, addUser, getUserById, editUser, deleteUser };
  });