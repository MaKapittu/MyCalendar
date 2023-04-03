import { State } from '@/model/user';
import { reactive, toRefs } from 'vue';

const state = reactive<State>({
  groups: [],
});

let initialized = false;

export default function useGroup() {
  const load = () => {
    if (!initialized) {
      state.groups = [
        {name: "Family", id: 0, usersId: [1,2,3,4], statuses:[{userId: 1, status: "mom"}, {userId: 2,status:"dad"}, {userId: 3,status:"child"},{userId: 4,status:"child"}]},
        {name:"Work", id:1, usersId:[1,2,3,5,6,7,8], statuses:[{userId:1, status:"boss"}, {userId:2, status:"boss"}]},
        {name: "Other friends", id: 2, usersId: [9,10,11,12,13]}
    ];

      initialized = true;
    }
  };

  return { ...toRefs(state), load };
}
