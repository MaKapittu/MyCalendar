 import useApi, { useApiRawRequest } from '@/model/api';
 import { defineStore } from 'pinia';
 import { ref } from 'vue';
 import { useAuthStore } from '@/stores/authStore';
 import { Friend } from '@/model/friend';
import { useUserStore } from './usersStore';

export const useUserFriendsStore = defineStore('userFriendsStore', () => {
  const authStore = useAuthStore();
  const userStore = useUserStore();

  let allFriends: Friend[] = [];
  let userFriends = ref<Friend[]>([]);
  const userPromise = userStore.getLoggedUser();

  const apiGetFriends = async (): Promise<Friend[]> => {
    return userPromise.then(async loggedUser => {
      if (loggedUser) {
        const id = loggedUser.id;
        const getFriends = useApi<Friend[]>('UserFriends/'+ id, {
          method: 'GET',
          headers:
          { Authorization: 'Bearer ' + authStore.token,
            Accept: 'application/json',
            'Content-Type': 'application/json'}
        });
        await getFriends.request();
        if (getFriends.response.value) {
          return getFriends.response.value;
        }
        return [];
      } else {
        console.log("User is empty");
        return [];
      }
    });
  };
   const loadFriends = async () => {
     allFriends = await apiGetFriends();
     userFriends.value = allFriends;
   };

   const getFriendsById = (id: number) => {
     return allFriends.find((friends) => friends.friendsId === id);
   };

   const addFriend = async (friend: Friend) => {
     const apiAddFriend = useApi<Friend>(`UserFriends/${friend.userId}?friendsId=${friend.friendsId}`, {
       method: 'PUT',
       headers: {
         Authorization: 'Bearer ' + authStore.token,
         Accept: 'application/json',
         'Content-Type': 'application/json',
       },
       body: JSON.stringify(friend),
     });

     await apiAddFriend.request();
     if (apiAddFriend.response.value) {
        loadFriends();
     }
     loadFriends();
   };
   
   const updateFriendsRole = async (friend: Friend) => {
    const apiUpdateFriendsRole = useApi<Friend>(`UserFriends/${friend.userId}/friends/${friend.friendsId}/roles`, {
      method: 'PUT',
      headers: {
        Authorization: 'Bearer ' + authStore.token,
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(friend),
    });
  
    await apiUpdateFriendsRole.request();
    if (apiUpdateFriendsRole.response.value) {
      loadFriends();
    }
    loadFriends();
  };

   const deleteFriend = async (friend: Friend) => {
     const apiDeleteEventRequest = useApiRawRequest(`UserFriends/${friend.userId}?friendId=${friend.friendsId}`, {
       method: 'DELETE',
       headers: { Authorization: 'Bearer ' + authStore.token },
     });

     const res = await apiDeleteEventRequest();
     if (res.status === 204) {
       let id = allFriends.indexOf(friend);

       if (id !== -1) {
        allFriends.splice(id, 1);
       }

       id = allFriends.indexOf(friend);

       if (id !== -1) {
        allFriends.splice(id, 1);
       }
       loadFriends();
     }
   };

 return { userFriends, loadFriends, getFriendsById, addFriend, deleteFriend, updateFriendsRole };
 }); 