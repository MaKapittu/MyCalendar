<template>
    <GroupEventsVue :group-id="groupId"/>
</template>

<script setup lang="ts">
import GroupEventsVue from '@/components/friends/GroupEvents.vue';
import { useRoute } from 'vue-router';

let groupId = String(useRoute().params.id);
</script>
