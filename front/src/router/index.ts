import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import EventsVue from '@/views/Events.vue';
import AddEventVue from '@/views/AddEvent.vue';
import EditEventVue from '@/views/EditEvent.vue';
import CalendarVue from '@/views/Calendar.vue';
import CalendarDayVue from '@/views/CalendarDay.vue';
import ProfileVue from '@/views/Profile.vue';
import ProfileEditVue from '@/views/ProfileEdit.vue';
import FriendsVue from '@/views/Friends.vue';
import PrivacyVue from '@/views/Privacy.vue';
import SettingsVue from '@/views/Settings.vue';
import { useAuthStore } from '@/stores/authStore';
import LoginVue from '@/views/Login.vue';
import RegisterVue from '@/views/Register.vue';
import GroupEventsVue from '@/views/GroupEvents.vue';

export enum AppPaths {
  EDIT_EVENT = "/editevent",
  calendarday = '/calendarday',
  EVENTS = "/events",
  CALENDAR = '/',
  PROFILE ='/profile',
  ProfileEdit = '/profile/edit',
  FRIENDS = '/friends',
  PRIVACY = '/privacy',
  SETTINGS = '/settings',
  LOGIN = '/login',
  REGISTER = '/register',
  GROUPS = '/groups'
}

const routes: Array<RouteRecordRaw> = [
  {
    path: AppPaths.calendarday + "/:date",
    component: CalendarDayVue,
  },
  {
    path: `${AppPaths.EVENTS}`,
    name: 'Event list',
    component: EventsVue,
    props: { title: 'Events' },
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/newevent',
    name: 'Add events',
    component: AddEventVue,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: `${AppPaths.EDIT_EVENT}/:id`,
    name: 'Edit events',
    component: EditEventVue,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: `${AppPaths.CALENDAR}`,
    name: 'Calendar',
    component: CalendarVue
  },
  {
    path: `${AppPaths.PROFILE}`,
    name: "Profile",
    component: ProfileVue,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: `${AppPaths.ProfileEdit}`,
    name: "ProfileEdit",
    component: ProfileEditVue,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: `${AppPaths.FRIENDS}`,
    name: "Friends",
    component: FriendsVue,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: `${AppPaths.GROUPS}/:id`,
    name: "Group events",
    component: GroupEventsVue,
    meta: {
      requiredAuth: true
    }
  },
  {
    path: `${AppPaths.PRIVACY}`,
    name: "Privacy",
    component: PrivacyVue
  },
  {
    path: `${AppPaths.SETTINGS}`,
    name: "Settings",
    component: SettingsVue,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: `${AppPaths.LOGIN}`,
    name: "Login",
    component: LoginVue
  },
  {
    path: `${AppPaths.REGISTER}`,
    name: "Register",
    component: RegisterVue
  },
  {
    path: "/:pathMatch(.*)*",
    name: "not-found",
    component: () => import("@/views/PageNotFound.vue"),
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const useAuth = useAuthStore();
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (!useAuth.isAuthenticated && !useAuth.token) {
      next({
          path: `${AppPaths.LOGIN}`
        })
    }else {
      next()
    }
  } else {
    next()
  }
})

export default router;
